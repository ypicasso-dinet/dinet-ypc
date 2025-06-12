using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Update
{
    public class EstandarUpdateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_estandar { get; set; }
        public string? cod_lote { get; set; }
        public int? val_edad { get; set; }
        public string? cod_sexo { get; set; }
        public int? val_estandar { get; set; }
        public int? val_peso { get; set; }
        public int? val_consumo { get; set; }
        public decimal? val_mortalidad { get; set; }
        public decimal? val_conversion { get; set; }
        public decimal? val_eficiencia { get; set; }
    }
}
