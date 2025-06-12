using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Menu.Commands.Create
{
    public class MenuCreateCommandHandler : MediatR.IRequestHandler<MenuCreateCommand, string>
    {
        protected readonly IMenuRepository repository;

        public MenuCreateCommandHandler(IMenuRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
