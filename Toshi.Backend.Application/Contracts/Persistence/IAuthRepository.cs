using Toshi.Backend.Application.Features.Auth.Command.Retrieve;
using Toshi.Backend.Application.Features.Auth.Command.Signin;
using Toshi.Backend.Application.Features.Auth.Command.Signout;
using Toshi.Backend.Domain.DTO.Auth;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IAuthRepository : IAsyncRepository<UsuarioEntity>
    {
        Task<AuthDTO> Signin(SigninCommand request);
        Task<bool> Signout(SignoutCommand request);
        Task<string> Retrieve(RetrieveCommand request);
        Task<bool> IsActiveUser(string? id);
    }
}
