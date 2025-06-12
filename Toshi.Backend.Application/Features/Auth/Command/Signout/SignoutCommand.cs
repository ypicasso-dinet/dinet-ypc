namespace Toshi.Backend.Application.Features.Auth.Command.Signout
{
    public class SignoutCommand : MediatR.IRequest<string>
    {
        public string? token { get; set; }

        public SignoutCommand(string? token)
        {
            this.token = token;
        }
    }
}
