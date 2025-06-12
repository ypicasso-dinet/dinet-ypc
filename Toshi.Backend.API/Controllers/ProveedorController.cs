using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Proveedor.Commands.Create;
using Toshi.Backend.Application.Features.Proveedor.Commands.Delete;
using Toshi.Backend.Application.Features.Proveedor.Commands.PersonalCreate;
using Toshi.Backend.Application.Features.Proveedor.Commands.PersonalDelete;
using Toshi.Backend.Application.Features.Proveedor.Commands.PersonalUpdate;
using Toshi.Backend.Application.Features.Proveedor.Commands.Update;
using Toshi.Backend.Application.Features.Proveedor.Querys.BuscarPersona;
using Toshi.Backend.Application.Features.Proveedor.Querys.GetAll;
using Toshi.Backend.Application.Features.Proveedor.Querys.GetById;
using Toshi.Backend.Application.Features.Proveedor.Querys.UpsertParams;
using Toshi.Backend.Application.Features.Proveedor.Querys.UsersByRol;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.API.Controllers
{
    public class ProveedorController : AppBaseController
    {
        public ProveedorController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetAll")]
        public async Task<ICollection<ProveedorItemDTO>> GetAll([FromQuery] ProveedorGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<ProveedorDTO?> GetById([FromQuery] ProveedorGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<ProveedorCreateResponseDTO> Create([FromBody] ProveedorCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] ProveedorUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] ProveedorDeleteCommand command) => await _mediator.Send(command);

        [HttpGet("UpsertParams")]
        public async Task<ProveedorUpsertParamsDTO> UpsertParams() => await _mediator.Send(new UpsertParamsQuery());

        [HttpGet("PersonalByRol")]
        public async Task<List<ProveedorRolPersonalDTO>> PersonalByRol([FromQuery] PersonalByRolQuery command) => await _mediator.Send(command);

        [HttpPost("PersonalCreate")]
        public async Task<ProveedorPersonalResponseDTO> PersonalCreate([FromBody] PersonalCreateCommand command) => await _mediator.Send(command);

        [HttpPut("PersonalUpdate")]
        public async Task<ProveedorPersonalResponseDTO> PersonalUpdate([FromBody] PersonalUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete("PersonalDelete")]
        public async Task<ProveedorPersonalResponseDTO> PersonalDelete([FromQuery] PersonalDeleteCommand command) => await _mediator.Send(command);

        [HttpGet("BuscarPersona")]
        public async Task<ProveedorPersonaDTO?> BuscarPersona([FromQuery] BuscarPersonaCommand command) => await _mediator.Send(command);
    }
}
