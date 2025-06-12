using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Menu.Commands.Delete
{
    public class MenuDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id;

        public MenuDeleteCommand(string? id, string? userCode)
        {
            this.id = id;
            this.UserCode = userCode;
        }
    }
}
