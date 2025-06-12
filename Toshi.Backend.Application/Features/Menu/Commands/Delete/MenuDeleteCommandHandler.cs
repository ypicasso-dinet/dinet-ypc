using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Menu.Commands.Delete
{
    public class MenuDeleteCommandHandler : MediatR.IRequestHandler<MenuDeleteCommand, string>
    {
        protected readonly IMenuRepository repository;

        public MenuDeleteCommandHandler(IMenuRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(MenuDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
