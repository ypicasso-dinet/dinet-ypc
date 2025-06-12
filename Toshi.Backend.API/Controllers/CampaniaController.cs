using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Campania.Commands.CerrarCampania;
using Toshi.Backend.Application.Features.Campania.Commands.CreateCampania;
using Toshi.Backend.Application.Features.Campania.Queries.GetAll;
using Toshi.Backend.Application.Features.Campania.Queries.GetById;
using Toshi.Backend.Application.Features.Campania.Queries.ScreenParams;
using Toshi.Backend.Domain.DTO.Campania;

namespace Toshi.Backend.API.Controllers
{
    public class CampaniaController : AppBaseController
    {
        public CampaniaController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ScreenParams")]
        public async Task<CampaniaScreenParamsDTO?> ScreenParams() => await _mediator.Send(new ScreenParamsQuery());

        [HttpGet("GetAll")]
        public async Task<List<CampaniaItemDTO>> GetAll([FromQuery] GetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<CampaniaDTO?> GetById([FromQuery] GetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] CreateCampaniaCommand command) => await _mediator.Send(command);

        [HttpPut("Cerrar")]
        public async Task<string> Cerrar([FromBody] CerrarCampaniaCommand command) => await _mediator.Send(command);
    }
}
