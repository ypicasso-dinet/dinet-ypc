using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using Toshi.Backend.Application.Features.Auth.Command.Retrieve;
using Toshi.Backend.Application.Features.Auth.Command.Signin;
using Toshi.Backend.Domain.DTO.Auth;

namespace Toshi.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        //private readonly TokenBlacklistService blackListService;

        public AuthController(IMediator mediator/*, TokenBlacklistService tokenBlacklistService*/)
        {
            this.mediator = mediator;
            //this.blackListService = tokenBlacklistService;
        }

        [HttpGet("ping")]
        public async Task<ActionResult> Ping()
        {
            await Task.Run(() =>
            {
                // fake content
            });

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<AuthDTO> Signin([FromBody] SigninCommand request) => await mediator.Send(request.SetIndicator(GetMobileIndicator()));

        [HttpPost("signout")]
        public async Task<string> Signout()
        {
            await Task.Run(() =>
            {
                var token = GetToken();

                if (!string.IsNullOrEmpty(token))
                {
                    //if (!blackListService.IsTokenBlacklisted(token))
                    //{
                    //    var handler = new JwtSecurityTokenHandler();
                    //    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                    //    if (jwtToken != null)
                    //    {
                    //        var expiry = jwtToken.ValidTo;

                    //        blackListService.InvalidateToken(token, expiry);
                    //    }
                    //}
                }
            });

            return "Sesíón cerrada correctamente";
        }

        [HttpPost("retrieve")]
        public async Task<string> Retrieve([FromBody] RetrieveCommand request) => await mediator.Send(request);


        private string GetToken()
        {
            string strToken = string.Empty;

            try
            {
                StringValues value = new StringValues();

                if (HttpContext.Request.Headers.TryGetValue("Authorization", out value))
                {
                    strToken = value[0]!.Split(" ")[1];

                }
            }
            catch (Exception ex)
            {
            }

            return strToken;
        }

        private bool GetMobileIndicator()
        {
            bool indicator = false;

            try
            {
                StringValues value = new StringValues();

                if (HttpContext.Request.Headers.TryGetValue("X-TOSHI-MOBILE", out value))
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