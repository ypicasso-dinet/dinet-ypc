using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Campania.Commands.CerrarCampania;
using Toshi.Backend.Application.Features.Campania.Commands.CreateCampania;
using Toshi.Backend.Application.Features.Campania.Queries.GetAll;
using Toshi.Backend.Application.Features.Campania.Queries.GetById;
using Toshi.Backend.Application.Features.Campania.Queries.ScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Campania;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class CampaniaRepository : RepositoryBase<CampaniaEntity>, ICampaniaRepository
    {
        const string MENU_CODE = "FEAT-1800";

        const int PROC_LIMPIEZA = 0;    // => "Limpieza y Desinfección",
        const int PROC_INGRESO = 1;     // => "Ingreso de Pollo Bebe",
        const int PROC_VENTA = 2;       // => "Venta de Pollo Bebe",
        const int PROC_FINALIZADO = 3;  // => "Finalizada",

        public CampaniaRepository(ToshiDBContext context, SessionStorage? sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<CampaniaScreenParamsDTO> ScreenParams(ScreenParamsQuery request)
        {
            var screenParams = new CampaniaScreenParamsDTO();

            screenParams.anios = await _context.Campania.OrderBy(o => o.num_anio).GroupBy(g => g.num_anio).Select(s => new CodeTextDTO(s.Key)).ToListAsync();

            screenParams.planteles = await _context.Plantel.Where(w => w.cod_estado == true).OrderBy(o => o.nom_plantel).Select(s => new CodeTextDTO(s.gid_plantel, s.nom_plantel)).ToListAsync();

            screenParams.estados = new List<CodeTextDTO>
            {
                new CodeTextDTO(PROC_LIMPIEZA, NomProceso(PROC_LIMPIEZA)),
                new CodeTextDTO(PROC_INGRESO, NomProceso(PROC_INGRESO)),
                new CodeTextDTO(PROC_VENTA, NomProceso(PROC_VENTA)),
                new CodeTextDTO(PROC_FINALIZADO, NomProceso(PROC_FINALIZADO)),
            };

            return screenParams;
        }

        public async Task<List<CampaniaItemDTO>> GetAll(GetAllQuery request)
        {
            bool allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);

            var items = await (
                from p in _context.Plantel
                join c in _context.Campania on p.id_plantel equals c.id_plantel
                orderby c.num_anio descending, c.num_campania descending
                where (string.IsNullOrEmpty(request.id_plantel) || c.plantel!.gid_plantel == request.id_plantel)
                && (request.num_anio == null || c.num_anio == request.num_anio!.Value)
                && (string.IsNullOrEmpty(request.cod_proceso) || c.ind_proceso == Convert.ToInt32(request.cod_proceso))
                select new CampaniaItemDTO
                {
                    id_campania = c.gid_campania,
                    nom_plantel = c.plantel!.nom_plantel,
                    num_anio = c.num_anio,
                    cod_campania = c.cod_campania,
                    fec_limpieza = c.fec_limpieza.ToSpanish(),
                    fec_ingreso = c.fec_ingreso.ToSpanish() ?? "--/--/----",
                    fec_venta = c.fec_venta.ToSpanish() ?? "--/--/----",
                    fec_cierre = c.fec_cierre.ToSpanish() ?? "--/--/----",
                    can_ingreso = c.can_ingreso ?? 0,
                    can_mortalidad = c.can_mortalidad ?? 0,
                    can_venta = c.can_venta ?? 0,
                    ind_proceso = c.ind_proceso,
                    nom_proceso = NomProceso(c.ind_proceso),
                    allow_update = c.ind_proceso == PROC_FINALIZADO ? false : allowUpdate,
                }
            ).ToListAsync();

            return items;
        }

        public async Task<CampaniaDTO?> GetById(GetByIdQuery request)
        {
            bool allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);

            var source = await (
                from p in _context.Plantel
                join c in _context.Campania on p.id_plantel equals c.id_plantel
                where c.gid_campania == request.id
                select new CampaniaDTO
                {
                    id_campania = c.gid_campania,
                    nom_plantel = c.plantel!.nom_plantel,
                    num_anio = c.num_anio,
                    cod_campania = c.cod_campania,
                    fec_limpieza = c.fec_limpieza.ToSpanish(),
                    fec_ingreso = c.fec_ingreso.ToSpanish() ?? "--/--/----",
                    fec_venta = c.fec_venta.ToSpanish() ?? "--/--/----",
                    fec_cierre = c.fec_cierre.ToSpanish() ?? "--/--/----",
                    can_ingreso = c.can_ingreso ?? 0,
                    can_mortalidad = c.can_mortalidad ?? 0,
                    can_venta = c.can_venta ?? 0,
                    ind_proceso = c.ind_proceso,
                    nom_proceso = NomProceso(c.ind_proceso),
                    //---------------------------------------------------------------------
                    allow_update = c.ind_proceso == PROC_FINALIZADO ? false : allowUpdate,
                }
            ).FirstOrDefaultAsync();

            return source;
        }

        private static string NomProceso(int proceso)
        {
            return proceso switch
            {
                PROC_LIMPIEZA => "Limpieza y Desinfección",
                PROC_INGRESO => "Ingreso de Pollo Bebe",
                PROC_VENTA => "Venta de Pollo Bebe",
                PROC_FINALIZADO => "Finalizada",
                _ => "---"
            };
        }

        public async Task<string> CreateCampania(CreateCampaniaCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            string cod_campania = string.Empty;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var plantel = await _context.Plantel.Where(w => w.gid_plantel == request.id_plantel).FirstOrDefaultAsync();

                ThrowTrue(plantel, "El plantel no se encuentra disponible");

                var fechaLimpieza = request.fec_limpieza.ToDate();
                var anioCampania = DateTime.Now.Year + (request.ind_actual == true ? 0 : 1);
                var maxFechaCierre = await _context.Campania.Where(w => w.id_plantel == plantel!.id_plantel && w.num_anio == anioCampania && w.ind_proceso == PROC_FINALIZADO && w.fec_cierre != null).Select(s => s.fec_cierre).MaxAsync();

                ThrowTrue(fechaLimpieza <= maxFechaCierre, "La fecha de limpieza es anterior a la última fecha de cierre");


                var campaniaActiva = await _context.Campania.Where(w => w.cod_estado == true && w.id_plantel == plantel!.id_plantel && w.num_anio == anioCampania && w.ind_proceso != PROC_FINALIZADO).FirstOrDefaultAsync();

                ThrowTrue(campaniaActiva != null, "No se puede registrar una nueva campaña mientras haya una campaña no finalizada");


                var cantidadCampanias = await _context.Campania.Where(w => w.id_plantel == plantel!.id_plantel && w.num_anio == anioCampania).CountAsync() + 1;

                cod_campania = anioCampania.ToString().Substring(2) + cantidadCampanias.ToString().PadLeft(2, '0');

                var entidad = new CampaniaEntity
                {
                    gid_campania = Guid.NewGuid().ToString(),
                    id_plantel = plantel!.id_plantel,
                    num_anio = anioCampania,
                    num_campania = cantidadCampanias,
                    cod_campania = cod_campania,
                    fec_limpieza = fechaLimpieza!.Value,
                    ind_proceso = PROC_LIMPIEZA,
                    can_ingreso = 0,
                    can_mortalidad = 0,
                    can_venta = 0,
                };

                await AddAsync(entidad);

                scope.Complete();
            }

            return $"Campaña {cod_campania} creada satisfactoriamente";
        }

        public async Task<string> CerrarCampania(CerrarCampaniaCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            string cod_campania = string.Empty;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var campania = await _context.Campania.Where(w => w.gid_campania == request.id_campania).FirstOrDefaultAsync();

                ThrowTrue(campania, "La campaña no se encuentra disponible");
                ThrowTrue(campania!.ind_proceso == PROC_FINALIZADO, "La campaña ya se encuentra finalizada");

                var plantel = await _context.Plantel.Where(w => w.id_plantel == campania!.id_plantel).FirstOrDefaultAsync();

                ThrowTrue(plantel, "El plantel no se encuentra disponible");

                var fechaCierre = request.fec_cierre.ToDate();

                campania.ind_proceso = PROC_FINALIZADO;
                campania.fec_cierre = fechaCierre;
                cod_campania = campania!.cod_campania;

                await UpdateEntityAsync(campania!);

                scope.Complete();
            }

            return $"Campaña {cod_campania} cerrada satisfactoriamente";
        }
    }
}
