using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Cuenta.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly ICuentaRepository _repository;

        public ChangePasswordCommandHandler(ICuentaRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _repository.ChangePassword(request);
        }
    }
}
