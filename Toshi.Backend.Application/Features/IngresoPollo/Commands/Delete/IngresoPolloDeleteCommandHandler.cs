using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.Delete
{
    public class IngresoPolloDeleteCommandHandler : MediatR.IRequestHandler<IngresoPolloDeleteCommand, string>
    {
        protected readonly IIngresoPolloRepository repository;

        public IngresoPolloDeleteCommandHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(IngresoPolloDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
