using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Usuario.Commands.Create;
using Toshi.Backend.Application.Features.Usuario.Commands.Delete;
using Toshi.Backend.Application.Features.Usuario.Commands.DeleteLicencia;
using Toshi.Backend.Application.Features.Usuario.Commands.Update;
using Toshi.Backend.Application.Features.Usuario.Commands.UpsertLicencia;
using Toshi.Backend.Application.Features.Usuario.Querys.GetAll;
using Toshi.Backend.Application.Features.Usuario.Querys.GetById;
using Toshi.Backend.Application.Features.Usuario.Querys.ScreenParams;
using Toshi.Backend.Application.Features.Usuario.Querys.ScreenParamsLic;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.API.Controllers
{
    public class UsuarioController : AppBaseController
    {
        public UsuarioController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ScreenParams")]
        public async Task<UsuarioScreenParamsDTO> ScreenParams([FromQuery] ScreenParamsQuery command) => await _mediator.Send(command);

        [HttpGet("GetAll")]
        public async Task<List<UsuarioItemDTO>> GetAll([FromQuery] GetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<UsuarioDTO?> GetById([FromQuery] GetByIdQuery command) => await _mediator.Send(command);

        [HttpPost]
        public async Task<UsuarioCreateResponseDTO> Create([FromBody] UsuarioCreateCommand command) => await _mediator.Send(command);

        [HttpPut]
        public async Task<string> Update([FromBody] UsuarioUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete]
        public async Task<string> Delete([FromQuery] UsuarioDeleteCommand command) => await _mediator.Send(command);

        [HttpGet("ScreenParamsLic")]
        public async Task<List<CodeTextDTO>> ScreenParamsLic([FromQuery] ScreenParamsLicQuery command) => await _mediator.Send(command);

        [HttpPost("UpsertLicencia")]
        public async Task<UsuarioLicenciaResponseDTO> UpsertLicencia([FromBody] UpsertLicenciaCommand command) => await _mediator.Send(command);

        [HttpPost("DeleteLicencia")]
        public async Task<string> DeleteLicencia([FromBody] UsuarioDeleteLicenciaCommand command) => await _mediator.Send(command);
    }
}
