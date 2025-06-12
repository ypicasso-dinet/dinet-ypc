using Toshi.Backend.Domain.DTO.Campania;

namespace Toshi.Backend.Application.Features.Campania.Queries.GetById
{
    public class GetByIdQuery : MediatR.IRequest<CampaniaDTO?>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
