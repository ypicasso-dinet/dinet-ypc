using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.Update
{
    public class IngresoPolloUpdateCommandHandler : MediatR.IRequestHandler<IngresoPolloUpdateCommand, string>
    {
        protected readonly IIngresoPolloRepository repository;

        public IngresoPolloUpdateCommandHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(IngresoPolloUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
