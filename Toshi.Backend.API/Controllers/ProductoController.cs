using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Producto.Commands.Create;
using Toshi.Backend.Application.Features.Producto.Commands.Delete;
using Toshi.Backend.Application.Features.Producto.Commands.Update;
using Toshi.Backend.Application.Features.Producto.Querys.GetAll;
using Toshi.Backend.Application.Features.Producto.Querys.GetById;
using Toshi.Backend.Application.Features.Producto.Querys.ScreenParams;
using Toshi.Backend.Domain.DTO.Producto;

namespace Toshi.Backend.API.Controllers
{
    public class ProductoController : AppBaseController
    {
        public ProductoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ScreenParams")]
        public async Task<ProductoScreenParamsDTO> ScreenParams() => await _mediator.Send(new ScreenParamsQuery());

        [HttpGet("GetAll")]
        public async Task<ICollection<ProductoItemDTO>> GetAll([FromQuery] ProductoGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<ProductoDTO?> GetById([FromQuery] ProductoGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] ProductoCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] ProductoUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] ProductoDeleteCommand command) => await _mediator.Send(command);
    }
}
