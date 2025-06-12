using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Menu;

namespace Toshi.Backend.Application.Features.Menu.Querys.GetAll
{
    public class MenuGetAllQuery : AppBaseCommand, IRequest<List<MenuItemDTO>>
    {
        public MenuGetAllQuery(string? userCode)
        {
            this.UserCode = userCode;
        }
    }
}
