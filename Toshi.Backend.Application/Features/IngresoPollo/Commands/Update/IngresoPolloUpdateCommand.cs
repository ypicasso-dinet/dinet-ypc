using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Common;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.Update
{
    public class IngresoPolloUpdateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_ingreso_pollo { get; set; }
        public string? id_plantel { get; set; }
        public string? id_campania { get; set; }
        public string? fec_registro { get; set; }
        public int? num_galpon { get; set; }
        public string? cod_genero { get; set; }
        public int? can_ingreso { get; set; }
        public string? lot_procedencia { get; set; }
        public string? nom_procedencia { get; set; }
        public string? num_guia { get; set; }
        public string? cod_edad { get; set; }
        public string? cod_lote { get; set; }
        public string? cod_linea { get; set; }
        public decimal? val_peso { get; set; }
        public int? can_muertos { get; set; }
        //public int? can_real { get; set; }
        public string? num_vehiculo { get; set; }
        public decimal? temp_cabina { get; set; }
        public decimal? hum_cabina { get; set; }

        public List<IngresoPolloImagen>? imagenes { get; set; }
    }
}
