using Microsoft.EntityFrameworkCore;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Estandar.Commands.Create;
using Toshi.Backend.Application.Features.Estandar.Commands.Delete;
using Toshi.Backend.Application.Features.Estandar.Commands.Update;
using Toshi.Backend.Application.Features.Estandar.Querys.GetAll;
using Toshi.Backend.Application.Features.Estandar.Querys.GetById;
using Toshi.Backend.Application.Features.Estandar.Querys.ScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Estandar;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class EstandarRepository : RepositoryBase<EstandarEntity>, IEstandarRepository
    {
        const string MENU_CODE = "FEAT-1400";

        public EstandarRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<EstandarScreenParamsDTO> ScreenParams(ScreenParamsQuery request)
        {
            var source = new EstandarScreenParamsDTO();

            source.lotes = await GetConfigs("LOTE");
            source.sexos = await GetConfigs("SEXO");
            source.edades = [];

            var edades = await _context.ConfiguracionDetalle
                .OrderBy(o => o.ord_config_det)
                .Where(w => w.configuracion!.cod_config == "EDADES" && w.configuracion!.cod_estado == true && w.cod_estado == true)
                .FirstOrDefaultAsync();

            if (edades != null)
            {
                var min = Convert.ToInt32(edades.min_value);
                var max = Convert.ToInt32(edades.max_value);

                source.edades = Enumerable.Range(min, max).Select(s => new Domain.DTO.Common.CodeTextDTO(s)).ToList();
            }

            return source;
        }

        public async Task<List<EstandarItemDTO>> GetAll(EstandarGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var records = await _context.Estandar
                .Where(w =>
                    (string.IsNullOrEmpty(request.cod_lote) || w.cod_lote == request.cod_lote) &&
                    (string.IsNullOrEmpty(request.val_edad) || w.val_edad == Convert.ToInt32(request.val_edad)) &&
                    (string.IsNullOrEmpty(request.cod_sexo) || w.cod_sexo == request.cod_sexo)
                )
                .OrderBy(o => o.cod_lote)
                .ThenBy(o => o.val_edad)
                .ThenBy(o => o.cod_sexo)
                .Select(s => new EstandarItemDTO()
                {
                    // Specifying field's
                    id_estandar = s.gid_estandar,
                    cod_carde = s.cod_carde,
                    cod_lote = s.cod_lote,
                    val_edad = s.val_edad,
                    cod_sexo = s.cod_sexo,
                    val_estandar = s.val_estandar,
                    val_peso = s.val_peso,
                    val_consumo = s.val_consumo,
                    val_mortalidad = s.val_mortalidad,
                    val_conversion = s.val_conversion,
                    val_eficiencia = s.val_eficiencia,
                    nom_estado = s.cod_estado.ToStatus()
                }).ToListAsync();

            return records;
        }

        public async Task<EstandarDTO?> GetById(EstandarGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.Estandar
                .Where(w => w.gid_estandar == request.id)
                .Select(s => new EstandarDTO()
                {
                    // Specifying field's    
                    id_estandar = s.gid_estandar,
                    cod_carde = s.cod_carde,
                    cod_lote = s.cod_lote,
                    val_edad = s.val_edad,
                    cod_sexo = s.cod_sexo,
                    val_estandar = s.val_estandar,
                    val_peso = s.val_peso,
                    val_consumo = s.val_consumo,
                    val_mortalidad = s.val_mortalidad,
                    val_conversion = s.val_conversion,
                    val_eficiencia = s.val_eficiencia,
                    nom_estado = s.cod_estado.ToStatus()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> Create(EstandarCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Estandar.Where(w =>
                       w.cod_lote == request.cod_lote
                    && w.val_edad == request.val_edad
                    && w.cod_sexo == request.cod_sexo).FirstOrDefaultAsync();

                ThrowTrue(record != null && record!.cod_estado == true, "El Dato Estándar ya se encuentra registrado");


                var append = record == null;

                if (append)
                    record = new EstandarEntity() { gid_estandar = Guid.NewGuid().ToString() };

                record!.cod_lote = request!.cod_lote!;
                record!.val_edad = request!.val_edad!.Value;
                record!.cod_sexo = request!.cod_sexo!;
                record!.val_estandar = request!.val_estandar!.Value;
                record!.val_peso = request!.val_peso!.Value;
                record!.val_consumo = request!.val_consumo!.Value;
                record!.val_mortalidad = request!.val_mortalidad!.Value;
                record!.val_conversion = request!.val_conversion!.Value;
                record!.val_eficiencia = request!.val_eficiencia!.Value;

                record!.cod_carde = $"{request.cod_lote}{request.val_edad}{request.cod_sexo}".ToUpper();

                if (append)
                    await AddAsync(record!);
                else
                    await UpdateAsync(record!);

                scope.Complete();
            }

            return $"Dato Estándar creado satisfactoriamente.";
        }

        public async Task<string> Update(EstandarUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Estandar.Where(w => w.gid_estandar == request.id_estandar).FirstOrDefaultAsync();

                ThrowTrue(record == null, "El Dato Estándar no se encuentra disponible");


                var existente = await _context.Estandar.Where(w =>
                       w.cod_lote == request.cod_lote
                    && w.val_edad == request.val_edad
                    && w.cod_sexo == request.cod_sexo
                    && w.id_estandar != record!.id_estandar).FirstOrDefaultAsync();

                ThrowTrue(existente != null, "El Dato Estándar ya se encuentra registrado");


                record!.cod_lote = request!.cod_lote!;
                record!.val_edad = request!.val_edad!.Value;
                record!.cod_sexo = request!.cod_sexo!;
                record!.val_estandar = request!.val_estandar!.Value;
                record!.val_peso = request!.val_peso!.Value;
                record!.val_consumo = request!.val_consumo!.Value;
                record!.val_mortalidad = request!.val_mortalidad!.Value;
                record!.val_conversion = request!.val_conversion!.Value;
                record!.val_eficiencia = request!.val_eficiencia!.Value;

                record!.cod_carde = $"{request.cod_lote}{request.val_edad}{request.cod_sexo}".ToUpper();

                await UpdateAsync(record!);

                scope.Complete();
            }

            return "Dato Estándar actualizado satisfactoriamente";
        }

        public async Task<string> Delete(EstandarDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Estandar.Where(w => w.gid_estandar == request.id).FirstOrDefaultAsync();

                ThrowTrue(record == null, "El Dato Estándar no se encuentra disponible");

                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Dato Estándar eliminado satisfactoriamente";
        }
    }
}
