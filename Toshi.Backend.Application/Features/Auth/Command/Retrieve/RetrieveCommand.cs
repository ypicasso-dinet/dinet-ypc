namespace Toshi.Backend.Application.Features.Auth.Command.Retrieve
{
    public class RetrieveCommand : MediatR.IRequest<string>
    {
        public string? cod_usuario { get; set; }

        public RetrieveCommand(string? cod_usuario)
        {
            this.cod_usuario = cod_usuario;
        }
    }
}
