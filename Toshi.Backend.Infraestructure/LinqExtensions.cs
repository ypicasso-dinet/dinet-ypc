using Microsoft.EntityFrameworkCore;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure
{
    public static class LinqExtensions
    {
        public static IQueryable<T> GetActives<T>(this IQueryable<T> query) where T : BaseDomainModel
        {
            return query.Where(w => w.cod_estado == true);
        }

        public static IEnumerable<T> GetActivesEnumerable<T>(this IQueryable<T> query) where T : BaseDomainModel
        {
            return query.Where(w => w.cod_estado == true);
        }

        public static IQueryable<T> GetConfigActives<T>(this IQueryable<T> query, string cod_config) where T : ConfiguracionDetalleEntity
        {
            return query.Where(w => w.cod_estado == true && w.configuracion!.cod_estado == true && w.configuracion!.cod_config == cod_config);
        }

        public static IQueryable<CodeTextDTO> GetConfigList<T>(this IQueryable<T> query, string cod_config) where T : ConfiguracionDetalleEntity
        {
            return query.GetConfigActives(cod_config)
                .OrderBy(o => o.ord_config_det)
                .ThenBy(t => t.nom_detalle)
                .Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle));
        }

        public static IQueryable<T> GetConfigRanges<T>(this IQueryable<T> query, string cod_config) where T : ConfiguracionDetalleEntity
        {
            return query.Where(w => w.cod_estado == true && w.configuracion!.cod_estado == true && w.configuracion!.cod_config == cod_config)
                .OrderBy(o => o.ord_config_det)
                .ThenBy(t => t.min_value);
        }

        public static IQueryable<CodeTextDTO> GetConfigEmails<T>(this IQueryable<T> query, string cod_config) where T : ConfiguracionDetalleEntity
        {
            return query.GetConfigActives(cod_config)
                .OrderBy(o => o.ord_config_det)
                .ThenBy(t => t.nom_detalle)
                .Select(s => new CodeTextDTO(s.des_email, s.val_email));
        }
    }
}
