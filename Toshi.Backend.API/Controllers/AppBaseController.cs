using Toshi.Backend.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Toshi.Backend.API.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AppBaseController : ControllerBase
    {
        public string COD_USUARIO => HttpContext.Items[Constants.ID_USUARIO]?.ToString() ?? string.Empty;
        public string NOM_USUARIO => HttpContext.Items[Constants.NOM_USUARIO]?.ToString() ?? string.Empty;

        public string TOKEN
        {
            get
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
        }

        protected readonly IMediator _mediator;

        public AppBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
