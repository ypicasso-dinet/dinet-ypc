using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Configuracion.Commands.Create;
using Toshi.Backend.Application.Features.Configuracion.Commands.Delete;
using Toshi.Backend.Application.Features.Configuracion.Commands.Update;
using Toshi.Backend.Application.Features.Configuracion.Querys.GetAll;
using Toshi.Backend.Application.Features.Configuracion.Querys.GetById;
using Toshi.Backend.Application.Features.Configuracion.Querys.ScreenParams;
using Toshi.Backend.Domain.DTO.Configuracion;

namespace Toshi.Backend.API.Controllers
{
    public class ConfiguracionController : AppBaseController
    {
        public ConfiguracionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ScreenParams")]
        public async Task<ConfiguracionScreenParamsDTO> ScreenParams() => await _mediator.Send(new ScreenParamsQuery());

        [HttpGet("GetAll")]
        public async Task<ICollection<ConfiguracionItemDTO>> GetAll([FromQuery] ConfiguracionGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<ConfiguracionDTO?> GetById([FromQuery] ConfiguracionGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] ConfiguracionCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] ConfiguracionUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] ConfiguracionDeleteCommand command) => await _mediator.Send(command);
    }
}
