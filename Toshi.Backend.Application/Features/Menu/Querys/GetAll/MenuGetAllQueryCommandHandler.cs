using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Menu;

namespace Toshi.Backend.Application.Features.Menu.Querys.GetAll
{
    public class MenuGetAllQueryHandler : IRequestHandler<MenuGetAllQuery, List<MenuItemDTO>>
    {
        protected readonly IMenuRepository repository;

        public MenuGetAllQueryHandler(IMenuRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<MenuItemDTO>> Handle(MenuGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
