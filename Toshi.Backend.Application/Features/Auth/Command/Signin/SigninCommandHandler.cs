using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Auth;
using MediatR;

namespace Toshi.Backend.Application.Features.Auth.Command.Signin
{
    public class SigninCommandHandler : IRequestHandler<SigninCommand, AuthDTO>
    {
        private readonly IAuthRepository repository;

        public SigninCommandHandler(IAuthRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AuthDTO> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            return await repository.Signin(request);
        }
    }
}
