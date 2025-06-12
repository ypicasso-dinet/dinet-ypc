using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Menu;

namespace Toshi.Backend.Application.Features.Menu.Querys.GetById
{
    public class MenuGetByIdQuery : AppBaseCommand, IRequest<MenuDTO?>
    {
        public string? id;

        public MenuGetByIdQuery(string? id, string? userCode)
        {
            this.id = id;
            this.UserCode = userCode;
        }
    }
}
