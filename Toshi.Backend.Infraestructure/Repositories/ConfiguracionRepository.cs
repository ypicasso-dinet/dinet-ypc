using Microsoft.EntityFrameworkCore;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Configuracion.Commands.Create;
using Toshi.Backend.Application.Features.Configuracion.Commands.Delete;
using Toshi.Backend.Application.Features.Configuracion.Commands.Update;
using Toshi.Backend.Application.Features.Configuracion.Querys.GetAll;
using Toshi.Backend.Application.Features.Configuracion.Querys.GetById;
using Toshi.Backend.Application.Features.Configuracion.Querys.ScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Configuracion;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class ConfiguracionRepository : RepositoryBase<ConfiguracionEntity>, IConfiguracionRepository
    {
        const string MENU_CODE = "FEAT-1500";

        public ConfiguracionRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<List<ConfiguracionItemDTO>> GetAll(ConfiguracionGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var records = await _context.Configuracion
                .OrderBy(o => o.nom_config)
                .Select(s => new ConfiguracionItemDTO()
                {
                    // Specifying field's
                    id_config = s.gid_config,
                    nom_config = s.nom_config,
                    tip_config = s.tip_config,

                }).ToListAsync();

            return records;
        }

        public async Task<ConfiguracionDTO?> GetById(ConfiguracionGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.Configuracion
                .Where(w => w.gid_config == request.id)
                .Select(s => new ConfiguracionDTO()
                {
                    // Specifying field's    
                    id_config = s.gid_config,
                    nom_config = s.nom_config,
                    tip_config = s.tip_config,
                    detalle = _context.ConfiguracionDetalle
                        .Where(w => w.id_config == s.id_config)
                        .OrderBy(o => o.ord_config_det)
                        .ThenBy(o => o.cod_detalle)
                        .ThenBy(o => o.min_value)
                        .Select(d => new ConfiguracionDetalleDTO
                        {
                            id_detalle = d.gid_config_det,
                            ord_detalle = d.ord_config_det,
                            cod_detalle = d.cod_detalle,
                            nom_detalle = d.nom_detalle,
                            min_value = d.min_value,
                            max_value = d.max_value,
                            des_email = d.des_email,
                            val_email = d.val_email,
                            cod_estado = d.cod_estado,
                            nom_estado = d.cod_estado.ToStatus(),
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> Create(ConfiguracionCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var config = await _context.Configuracion.Where(w => w.gid_config == request.id_config).FirstOrDefaultAsync();

                ThrowTrue(config, "La configuración indicada no se encuentra disponible");

                var gid = Guid.NewGuid().ToString();

                var record = new ConfiguracionDetalleEntity
                {
                    id_config = config!.id_config,
                    gid_config_det = Guid.NewGuid().ToString(),
                    cod_detalle = request.cod_detalle,
                    nom_detalle = request.nom_detalle,
                    ord_config_det = request.ord_detalle ?? 0,
                    min_value = request.min_value,
                    max_value = request.max_value,
                    des_email = request.des_email,
                    val_email = request.val_email,
                };

                await _context.ConfiguracionDetalle.AddAsync(record);

                if (!string.Equals(config!.tip_config, request.tip_config))
                {
                    config!.tip_config = request.tip_config!;

                    _context.Configuracion.Update(config!);
                }

                await _context.SaveChangesAsync();

                scope.Complete();
            }

            return $"Configuracion creada satisfactoriamente.";
        }

        public async Task<string> Update(ConfiguracionUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var config = await _context.Configuracion.Where(w => w.gid_config == request.id_config).FirstOrDefaultAsync();

                ThrowTrue(config, "La configuración indicada no se encuentra disponible");

                var record = await _context.ConfiguracionDetalle.Where(w => w.gid_config_det == request.id_detalle).FirstOrDefaultAsync();

                ThrowTrue(record == null, "La configuración indicada no se encuentra disponible");

                record!.id_config = config!.id_config;
                record!.gid_config_det = Guid.NewGuid().ToString();
                record!.cod_detalle = request.cod_detalle;
                record!.nom_detalle = request.nom_detalle;
                record!.ord_config_det = request.ord_detalle ?? 0;
                record!.min_value = request.min_value;
                record!.max_value = request.max_value;
                record!.des_email = request.des_email;
                record!.val_email = request.val_email;

                _context.ConfiguracionDetalle.Update(record!);

                await _context.SaveChangesAsync();

                scope.Complete();
            }

            return "Configuracion actualizada satisfactoriamente";
        }

        public async Task<string> Delete(ConfiguracionDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.ConfiguracionDetalle.Where(w => w.gid_config_det == request.id).FirstOrDefaultAsync();

                ThrowTrue(record == null, "La configuración indicada no se encuentra disponible");

                _context.ConfiguracionDetalle.Entry(record!).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                scope.Complete();
            }

            return "Configuracion eliminada satisfactoriamente";
        }

        public async Task<ConfiguracionScreenParamsDTO> ScreenParams(ScreenParamsQuery request)
        {
            var source = new ConfiguracionScreenParamsDTO();

            source.destinatarios = await GetConfigs("DESTINATARIOS");

            return source;
        }
    }
}
