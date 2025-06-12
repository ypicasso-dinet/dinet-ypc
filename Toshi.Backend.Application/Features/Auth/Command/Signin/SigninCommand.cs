using Toshi.Backend.Domain.DTO.Auth;

namespace Toshi.Backend.Application.Features.Auth.Command.Signin
{
    public class SigninCommand : MediatR.IRequest<AuthDTO>
    {
        public string? username { get; set; }
        public string? password { get; set; }

        private bool? mobileIndicator { get; set; }

        public SigninCommand SetIndicator(bool? mobileIndicator)
        {
            this.mobileIndicator = mobileIndicator;

            return this;
        }

        public bool GetIndicator() => this.mobileIndicator ?? false;
    }
}
