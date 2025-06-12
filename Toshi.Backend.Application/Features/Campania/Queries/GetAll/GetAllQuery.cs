using Toshi.Backend.Domain.DTO.Campania;

namespace Toshi.Backend.Application.Features.Campania.Queries.GetAll
{
    public class GetAllQuery : MediatR.IRequest<List<CampaniaItemDTO>>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public int? num_anio { get; set; }
        public string? cod_proceso { get; set; }
    }
}
