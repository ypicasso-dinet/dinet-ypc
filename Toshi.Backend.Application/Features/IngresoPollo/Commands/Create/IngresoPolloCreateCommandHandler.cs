using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.Create
{
    public class IngresoPolloCreateCommandHandler : MediatR.IRequestHandler<IngresoPolloCreateCommand, string>
    {
        protected readonly IIngresoPolloRepository repository;

        public IngresoPolloCreateCommandHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(IngresoPolloCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
