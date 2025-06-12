using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Delete
{
    public class EstandarDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
