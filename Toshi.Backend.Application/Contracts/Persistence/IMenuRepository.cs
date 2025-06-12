using Toshi.Backend.Application.Features.Menu.Commands.Create;
using Toshi.Backend.Application.Features.Menu.Commands.Delete;
using Toshi.Backend.Application.Features.Menu.Commands.Update;
using Toshi.Backend.Application.Features.Menu.Querys.GetAll;
using Toshi.Backend.Application.Features.Menu.Querys.GetById;
using Toshi.Backend.Domain.DTO.Menu;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IMenuRepository : IAsyncRepository<MenuEntity>
    {
        Task<string> Create(MenuCreateCommand request);
        Task<string> Update(MenuUpdateCommand request);
        Task<string> Delete(MenuDeleteCommand request);
        Task<List<MenuItemDTO>> GetAll(MenuGetAllQuery request);
        Task<MenuDTO?> GetById(MenuGetByIdQuery request);
    }
}
