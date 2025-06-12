using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Estandar.Commands.Create;
using Toshi.Backend.Application.Features.Estandar.Commands.Delete;
using Toshi.Backend.Application.Features.Estandar.Commands.Update;
using Toshi.Backend.Application.Features.Estandar.Querys.GetAll;
using Toshi.Backend.Application.Features.Estandar.Querys.GetById;
using Toshi.Backend.Application.Features.Estandar.Querys.ScreenParams;
using Toshi.Backend.Domain.DTO.Estandar;

namespace Toshi.Backend.API.Controllers
{
    public class EstandarController : AppBaseController
    {
        public EstandarController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ScreenParams")]
        public async Task<EstandarScreenParamsDTO> ScreenParams() => await _mediator.Send(new ScreenParamsQuery());

        [HttpGet("GetAll")]
        public async Task<ICollection<EstandarItemDTO>> GetAll([FromQuery] EstandarGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<EstandarDTO?> GetById([FromQuery] EstandarGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] EstandarCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] EstandarUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] EstandarDeleteCommand command) => await _mediator.Send(command);
    }
}
