using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Toshi.Backend.API.Errors;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Exceptions;
using Toshi.Backend.Application.Models.Identity;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Auth;
using Toshi.Backend.Infraestructure.Repositories;

namespace Toshi.Backend.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly string _key;

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly SessionStorage _sessionStorage;

        //private readonly TokenBlacklistService tokenBlacklistService;

        private const string SESSION_MESSAGE = "Su sesión ha expirado, por favor vuelva a iniciar sesión";

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment,
            IOptions<JwtSettings> settings,

            IServiceScopeFactory scopeFactory,
            SessionStorage sessionStorage
        //,TokenBlacklistService tokenBlacklistService
        )
        {
            //
            _key = settings.Value.Key;

            _next = next;
            _logger = logger;
            _environment = environment;

            _scopeFactory = scopeFactory;
            _sessionStorage = sessionStorage;

            //this.tokenBlacklistService = tokenBlacklistService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    //if (tokenBlacklistService.IsTokenBlacklisted(token))
                    //{
                    //    context.Response.ContentType = "application/json";
                    //    context.Response.StatusCode = 401;

                    //    var resultado = JsonConvert.SerializeObject(new CodeErrorException(401, SESSION_MESSAGE, null));

                    //    await context.Response.WriteAsync(resultado);

                    //    return;
                    //}

                    await attachUserToContext(context, token!);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                var innerEx = ex.InnerException;
                var errorMessage = ex.Message;
                var codigo = (int)HttpStatusCode.InternalServerError;
                //var resultado = string.Empty;
                var errores = new List<string>();

                switch (ex)
                {
                    case NotFoundException nfe:
                        codigo = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException vex:
                        codigo = (int)HttpStatusCode.BadRequest;
                        //var validationJson = JsonConvert.SerializeObject(vex.Errors);
                        //resultado = JsonConvert.SerializeObject(new CodeErrorException(codigo, ex.Message, validationJson));
                        errores = vex.Errors.Select(s => s.Key ?? "").ToList();
                        break;

                    case BadRequestException brx:
                        codigo = (int)HttpStatusCode.BadRequest;
                        break;

                    case SecurityTokenSignatureKeyNotFoundException:
                    case SecurityTokenExpiredException:
                        codigo = (int)HttpStatusCode.Unauthorized;
                        errorMessage = SESSION_MESSAGE;
                        break;
                    case FluentValidation.ValidationException fve:
                        codigo = (int)HttpStatusCode.BadRequest;
                        errorMessage = string.Join(Environment.NewLine, fve.Errors.Select(s => $"-{s.ErrorMessage}.").ToList());
                        errores = fve.Errors.Select(s => s.ErrorMessage).ToList();
                        break;
                    default:
                        break;
                }

                while (innerEx != null)
                {
                    _logger.LogError(innerEx, innerEx.Message);

                    errorMessage += " " + innerEx.Message;
                    innerEx = innerEx.InnerException;
                }

                //if (string.IsNullOrEmpty(resultado))
                var resultado = JsonConvert.SerializeObject(new CodeErrorException(codigo, errorMessage, ex.StackTrace) { errors = errores });

                context.Response.StatusCode = codigo;

                await context.Response.WriteAsync(resultado);
            }
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.FromDays(1)

            }, out SecurityToken validatedToken);

            var jwtTokenClaims = ((JwtSecurityToken)validatedToken).Claims;

            var userID = jwtTokenClaims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            var userCode = jwtTokenClaims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
            var userName = jwtTokenClaims.First(x => x.Type == JwtRegisteredClaimNames.GivenName).Value;
            var userEmail = jwtTokenClaims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;

            using (var scope = _scopeFactory.CreateScope())
            {
                var authService = scope.ServiceProvider.GetService<IAuthRepository>();
                var userSession = await authService!.IsActiveUser(userID);

                if (!userSession)
                {
                    throw new SecurityTokenExpiredException();
                }

                var sessionData = new UserContainerDTO()
                {
                    IdUsuario = userID,
                    CodUsuario = userCode,
                    NomUsuario = userName,
                    EmaUsuario = userEmail,
                    EsMobile = GetMobileIndicator(context)
                };

                _sessionStorage.SetUser(sessionData);
            }

            context.Items[Constants.ID_USUARIO] = userID;
            context.Items[Constants.COD_USUARIO] = userCode;
            context.Items[Constants.NOM_USUARIO] = userName;
            context.Items[Constants.EMA_USUARIO] = userEmail;
        }

        private bool GetMobileIndicator(HttpContext context)
        {
            bool indicator = false;

            try
            {
                StringValues value = new StringValues();

                if (context.Request.Headers.TryGetValue("X-TOSHI-MOBILE", out value))
                {
                    indicator = true;
                }
            }
            catch (Exception ex)
            {
            }

            return indicator;
        }
    }
}
