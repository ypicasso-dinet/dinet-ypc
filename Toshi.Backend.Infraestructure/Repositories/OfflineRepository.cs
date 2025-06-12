using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Offline.Queries.GetInitialInfo;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Offline;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class OfflineRepository : RepositoryBase<PlantelEntity>, IOfflineRepository
    {
        public OfflineRepository(ToshiDBContext context, SessionStorage? sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<OfflineResponseDTO> GetInitialInfo(GetInitialInfoQuery request)
        {
            var response = new OfflineResponseDTO();

            var configEdad = await _context.ConfiguracionDetalle.GetConfigRanges(Constants.CONFIG_EDADES).FirstOrDefaultAsync();
            var edades = new List<CodeTextDTO>();

            if (configEdad != null)
            {
                var min = Convert.ToInt32(configEdad!.min_value);
                var max = Convert.ToInt32(configEdad!.max_value);

                edades = Enumerable.Range(min, max).Select(s => new CodeTextDTO(s)).ToList();
            }

            response.edades = edades;
            response.estados_ipbb = await _context.ConfiguracionDetalle.GetConfigList(Constants.CONFIG_ESTADOS).ToListAsync();
            response.lineas = await _context.ConfiguracionDetalle.GetConfigList(Constants.CONFIG_LINEA_POLLO).ToListAsync();
            response.lotes = await _context.ConfiguracionDetalle.GetConfigList(Constants.CONFIG_LOTE).ToListAsync();
            response.sexos = await _context.ConfiguracionDetalle.GetConfigList(Constants.CONFIG_SEXO).ToListAsync();

            response.productos = await (
                from p in _context.Producto.GetActives()
                join tpd in _context.ConfiguracionDetalle.GetActives() on p.cod_tipo equals tpd.cod_detalle
                join tpc in _context.Configuracion.GetActives() on tpd.id_config equals tpc.id_config
                join umd in _context.ConfiguracionDetalle.GetActives() on p.uni_medida equals umd.cod_detalle
                join umc in _context.Configuracion.GetActives() on umd.id_config equals umc.id_config
                where tpc.cod_config == Constants.CONFIG_PROD_TIPO
                && umc.cod_config == Constants.CONFIG_UNIDADES
                select new OfflineProductoDTO
                {
                    id_producto = p.gid_producto,
                    nom_producto = p.nom_producto,
                    tip_producto = tpd.nom_detalle,
                    uni_producto = umd.nom_detalle
                }).ToListAsync();

            var rango = await _context.ConfiguracionDetalle.GetConfigRanges(Constants.CONFIG_GALPONES).FirstOrDefaultAsync();
            var galpones = new List<CodeTextDTO>();

            if (rango != null)
                galpones = Enumerable.Range(Convert.ToInt32(rango.min_value!.Value), Convert.ToInt32(rango.max_value!.Value)).Select(s => new CodeTextDTO(s)).ToList();

            //TODO: futura carga de galpones por rol de usuario
            //
            //--------------------------------------------------

            response.planteles = await _context.Plantel.GetActives().Select(s => new OfflinePlantelDTO
            {
                id_plantel = s.gid_plantel,
                nom_plantel = s.nom_plantel,
                galpones = galpones,
                campanias = s.campanias.Where(w => w.cod_estado == true).Select(c => new OfflinePlantelCampaniaDTO
                {
                    id_campania = c.gid_campania,
                    cod_campania = c.cod_campania,
                    nom_campania = c.cod_campania,
                    ind_activa = c.ind_proceso < 3
                }).ToList()
            }).ToListAsync();


            return response;
        }
    }
}
