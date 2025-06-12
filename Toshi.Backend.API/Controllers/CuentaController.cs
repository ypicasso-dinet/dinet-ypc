using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Cuenta.Commands.ChangePassword;

namespace Toshi.Backend.API.Controllers
{
    public class CuentaController : AppBaseController
    {
        public CuentaController(IMediator mediator) : base(mediator)
        {
            //
        }

        [HttpPost("ChangePassword")]
        public async Task<string> ChangePassword([FromBody] ChangePasswordCommand command) => await _mediator.Send(command);
        
        //[HttpGet("OfflineInfo")]
    }
}
