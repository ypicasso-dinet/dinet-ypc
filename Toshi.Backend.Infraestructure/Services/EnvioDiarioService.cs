using Microsoft.EntityFrameworkCore;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Services
{
    public sealed class EnvioDiarioService
    {
        public static async Task<bool> Enviado(ToshiDBContext context, int? id_usuario, string? gid_plantel, DateTime fecha)
        {
            var codigos = await CodigosPlantas.ObtenerPlanteles(context, id_usuario, gid_plantel);
            var item = await context.EnvioDiario.Where(w => codigos.Contains(w.campania!.id_plantel) && w.fec_envio.Date == fecha.Date).FirstOrDefaultAsync();

            return item?.ind_enviado ?? false;
        }
    }
}
