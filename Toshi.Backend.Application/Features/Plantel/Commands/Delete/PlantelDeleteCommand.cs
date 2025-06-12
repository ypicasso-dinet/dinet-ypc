namespace Toshi.Backend.Application.Features.Plantel.Commands.Delete
{
    public class PlantelDeleteCommand : MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
