using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Rol.Commands.Create;
using Toshi.Backend.Application.Features.Rol.Commands.Delete;
using Toshi.Backend.Application.Features.Rol.Commands.SaveAcciones;
using Toshi.Backend.Application.Features.Rol.Commands.Update;
using Toshi.Backend.Application.Features.Rol.Querys.GetAll;
using Toshi.Backend.Application.Features.Rol.Querys.GetById;
using Toshi.Backend.Application.Features.Rol.Querys.Opciones;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.API.Controllers
{
    public class RolController : AppBaseController
    {
        public RolController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetAll")]
        public async Task<ICollection<RolItemDTO>> GetAll([FromQuery] GetAllQuery query) => await _mediator.Send(query);

        [HttpGet("GetById")]
        public async Task<RolDTO> GetById([FromQuery] string? id) => await _mediator.Send(new GetByIdQuery(id));

        [HttpPost()]
        public async Task<string> Create([FromBody] RolCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] RolUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] string? id) => await _mediator.Send(new RolDeleteCommand(id));

        [HttpGet("Opciones")]
        public async Task<List<RolMenuDTO>> Opciones([FromQuery] string? id) => await _mediator.Send(new OpcionesQuery(id));

        [HttpPost("Opciones")]
        public async Task<string> SaveOpciones([FromBody] RolSaveAccionesCommand command) => await _mediator.Send(command);
    }
}
