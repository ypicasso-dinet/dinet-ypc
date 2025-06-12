using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Commands.CreateCampania
{
    public class CreateCampaniaCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public bool? ind_actual { get; set; }
        public string? fec_limpieza { get; set; }
    }
}
