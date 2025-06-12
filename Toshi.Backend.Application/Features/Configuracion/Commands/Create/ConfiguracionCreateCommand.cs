using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Create
{
    public class ConfiguracionCreateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_config { get; set; }
        public string? tip_config { get; set; }
        public int? ord_detalle { get; set; }
        public string? cod_detalle { get; set; }
        public string? nom_detalle { get; set; }
        public decimal? min_value { get; set; }
        public decimal? max_value { get; set; }
        public string? des_email { get; set; }
        public string? val_email { get; set; }
    }
}
