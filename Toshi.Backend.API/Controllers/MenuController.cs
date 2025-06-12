using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Menu.Commands.Create;
using Toshi.Backend.Application.Features.Menu.Commands.Delete;
using Toshi.Backend.Application.Features.Menu.Commands.Update;
using Toshi.Backend.Application.Features.Menu.Querys.GetAll;
using Toshi.Backend.Application.Features.Menu.Querys.GetById;
using Toshi.Backend.Domain.DTO.Menu;

namespace Toshi.Backend.API.Controllers
{
    public class MenuController : AppBaseController
    {
        public MenuController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetAll")]
        public async Task<ICollection<MenuItemDTO>> GetAll() => await _mediator.Send(new MenuGetAllQuery(COD_USUARIO));

        [HttpGet("GetById")]
        public async Task<MenuDTO?> GetById([FromQuery] string? id) => await _mediator.Send(new MenuGetByIdQuery(id, COD_USUARIO));

        [HttpPost()]
        public async Task<string> Create([FromBody] MenuCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] MenuUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] string? id) => await _mediator.Send(new MenuDeleteCommand(id, COD_USUARIO));
    }
}
