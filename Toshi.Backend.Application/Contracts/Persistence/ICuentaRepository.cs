using Toshi.Backend.Application.Features.Cuenta.Commands.ChangePassword;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface ICuentaRepository : IAsyncRepository<UsuarioEntity>
    {
        Task<string> ChangePassword(ChangePasswordCommand request);
    }
}
