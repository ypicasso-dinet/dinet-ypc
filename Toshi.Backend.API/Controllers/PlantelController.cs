using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Plantel.Commands.Create;
using Toshi.Backend.Application.Features.Plantel.Commands.Delete;
using Toshi.Backend.Application.Features.Plantel.Commands.Update;
using Toshi.Backend.Application.Features.Plantel.Commands.UserAppend;
using Toshi.Backend.Application.Features.Plantel.Commands.UserDelete;
using Toshi.Backend.Application.Features.Plantel.Querys.GetAll;
using Toshi.Backend.Application.Features.Plantel.Querys.GetById;
using Toshi.Backend.Application.Features.Plantel.Querys.ListParams;
using Toshi.Backend.Application.Features.Plantel.Querys.UpsertParams;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.API.Controllers
{
    public class PlantelController : AppBaseController
    {
        public PlantelController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ListParams")]
        public async Task<PlantelListParamsDTO> ListParams([FromQuery] ListParamsQuery command) => await _mediator.Send(command);

        [HttpGet("UpsertParams")]
        public async Task<PlantelUpsertParamsDTO> UpsertParams([FromQuery] UpsertParamsQuery command) => await _mediator.Send(command);

        [HttpGet("GetAll")]
        public async Task<ICollection<PlantelItemDTO>> GetAll([FromQuery] PlantelGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<PlantelDTO?> GetById([FromQuery] PlantelGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<StatusResponse> Create([FromBody] PlantelCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] PlantelUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] PlantelDeleteCommand command) => await _mediator.Send(command);

        [HttpPost("UserAppend")]
        public async Task<PlantelUserResponseDTO> UserAppend([FromBody] UserAppendCommand command) => await _mediator.Send(command);

        [HttpDelete("UserDelete")]
        public async Task<PlantelUserResponseDTO> UserDelete([FromBody] UserDeleteCommand command) => await _mediator.Send(command);
    }
}
