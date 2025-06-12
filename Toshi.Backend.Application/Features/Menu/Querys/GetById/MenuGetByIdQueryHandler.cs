using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Menu;

namespace Toshi.Backend.Application.Features.Menu.Querys.GetById
{
    public class MenuGetByIdQueryHandler : IRequestHandler<MenuGetByIdQuery, MenuDTO?>
    {
        protected readonly IMenuRepository repository;

        public MenuGetByIdQueryHandler(IMenuRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MenuDTO?> Handle(MenuGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
