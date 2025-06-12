using MediatR;

namespace Toshi.Backend.Application.Features.Cuenta.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<string>
    {
        public string? old_password { get; set; }
        public string? new_password { get; set; }
        public string? con_password { get; set; }
    }
}
