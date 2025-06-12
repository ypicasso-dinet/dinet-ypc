using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Menu.Commands.Update
{
    public class MenuUpdateCommandHandler : MediatR.IRequestHandler<MenuUpdateCommand, string>
    {
        protected readonly IMenuRepository repository;

        public MenuUpdateCommandHandler(IMenuRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(MenuUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
