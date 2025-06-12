using Microsoft.EntityFrameworkCore;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Menu.Commands.Create;
using Toshi.Backend.Application.Features.Menu.Commands.Delete;
using Toshi.Backend.Application.Features.Menu.Commands.Update;
using Toshi.Backend.Application.Features.Menu.Querys.GetAll;
using Toshi.Backend.Application.Features.Menu.Querys.GetById;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Menu;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class MenuRepository : RepositoryBase<MenuEntity>, IMenuRepository
    {
        const string MENU_CODE = "MENU-CODE";

        public MenuRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<List<MenuItemDTO>> GetAll(MenuGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var records = await _context.Menu.Select(s => new MenuItemDTO()
            {
                // Specifying field's
            }).ToListAsync();

            return records;
        }

        public async Task<MenuDTO?> GetById(MenuGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.Menu
                //.Where(w => w.gid_Menu == request.id)
                .Select(s => new MenuDTO()
                {
                    // Specifying field's    
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> Create(MenuCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var gid = Guid.NewGuid().ToString();

                var record = new MenuEntity
                {
                    // Specifying field's
                };

                await AddAsync(record);

                scope.Complete();
            }

            return $"Menu creado satisfactoriamente.";
        }

        public async Task<string> Update(MenuUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            return "Menu actualizado satisfactoriamente";
        }

        public async Task<string> Delete(MenuDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            return "Menu eliminado satisfactoriamente";
        }
    }
}
