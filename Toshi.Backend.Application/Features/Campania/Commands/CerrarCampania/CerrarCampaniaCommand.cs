using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Commands.CerrarCampania
{
    public class CerrarCampaniaCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_campania { get; set; }
        public string? fec_cierre { get; set; }
    }
}
