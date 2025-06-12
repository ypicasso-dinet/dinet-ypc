using Microsoft.EntityFrameworkCore;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Services
{
    public sealed class CodigosPlantas
    {
        public static async Task<List<int>> ObtenerPlanteles(ToshiDBContext context, int? id_usuario, string? id_plantel = null)
        {
            var planteles = await context.UsuarioPlantel
                .Where(w => w.id_usuario == id_usuario 
                    && w.plantel.cod_estado == true 
                    && w.cod_estado == true 
                    && (string.IsNullOrEmpty(id_plantel) || w.plantel.gid_plantel == id_plantel)
                )
                .Select(s => s.id_plantel)
                .ToListAsync();

            return planteles;
        }
    }
}
