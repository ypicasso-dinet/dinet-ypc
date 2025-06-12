using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Auth.Command.Retrieve
{
    public class RetrieveCommandHandler : MediatR.IRequestHandler<RetrieveCommand, string>
    {
        private readonly IAuthRepository repository;

        public RetrieveCommandHandler(IAuthRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(RetrieveCommand request, CancellationToken cancellationToken)
        {
            return await repository.Retrieve(request);
        }
    }
}
