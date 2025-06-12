using Microsoft.EntityFrameworkCore;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Producto.Commands.Create;
using Toshi.Backend.Application.Features.Producto.Commands.Delete;
using Toshi.Backend.Application.Features.Producto.Commands.Update;
using Toshi.Backend.Application.Features.Producto.Querys.GetAll;
using Toshi.Backend.Application.Features.Producto.Querys.GetById;
using Toshi.Backend.Application.Features.Producto.Querys.ScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Producto;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class ProductoRepository : RepositoryBase<ProductoEntity>, IProductoRepository
    {
        const string MENU_CODE = "FEAT-1600";

        public ProductoRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }
        public async Task<ProductoScreenParamsDTO> ScreenParams(ScreenParamsQuery request)
        {
            var source = new ProductoScreenParamsDTO();

            source.unidades = await GetConfigs("UNIDADES");
            source.tipos = await GetConfigs("PROD_TIPO");
            source.segmentos = await GetConfigs("PROD_SEGMENTO");
            source.estados = await GetConfigs("ESTADOS");

            return source;
        }

        public async Task<List<ProductoItemDTO>> GetAll(ProductoGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var records = await _context.Producto
                .Where(w =>
                    (string.IsNullOrEmpty(request.cod_producto) || w.cod_producto.Contains(request.cod_producto))
                    && (string.IsNullOrEmpty(request.nom_producto) || w.nom_producto.Contains(request.nom_producto))
                    && (string.IsNullOrEmpty(request.tip_producto) || w.cod_tipo.Equals(request.tip_producto))
                    && (string.IsNullOrEmpty(request.cod_estado) || w.cod_estado.Equals(request.cod_estado.ToBool()))
                )
                .Select(s => new ProductoItemDTO()
                {
                    // Specifying field's
                    id_producto = s.gid_producto,
                    cod_producto = s.cod_producto,
                    nom_producto = s.nom_producto,
                    uni_medida = s.uni_medida,
                    nom_tipo = s.cod_tipo,
                    nom_segmento = s.cod_segmento,
                    min_ingreso = s.min_ingreso,
                    max_ingreso = s.max_ingreso,
                    min_salida = s.min_salida,
                    max_salida = s.max_salida,
                    min_transfer = s.min_transfer,
                    max_transfer = s.max_transfer,

                    nom_estado = s.cod_estado.ToStatus()
                })
                .ToListAsync();

            return records;
        }

        public async Task<ProductoDTO?> GetById(ProductoGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.Producto
                .Where(w => w.gid_producto == request.id)
                .Select(s => new ProductoDTO()
                {
                    // Specifying field's    
                    id_producto = s.gid_producto,
                    cod_producto = s.cod_producto,
                    nom_producto = s.nom_producto,
                    uni_medida = s.uni_medida,
                    nom_tipo = s.cod_tipo,
                    nom_segmento = s.cod_segmento,
                    min_ingreso = s.min_ingreso,
                    max_ingreso = s.max_ingreso,
                    min_salida = s.min_salida,
                    max_salida = s.max_salida,
                    min_transfer = s.min_transfer,
                    max_transfer = s.max_transfer,

                    nom_estado = s.cod_estado.ToStatus()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> Create(ProductoCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Producto.Where(w => w.cod_producto == request.cod_producto).FirstOrDefaultAsync();

                ThrowTrue(record != null && record.cod_estado == true, "El código producto ya se encuentra registrado");

                var append = record == null;

                if (append)
                {
                    record = new ProductoEntity { gid_producto = Guid.NewGuid().ToString() };
                }

                //------------------------------------------
                record!.cod_producto = request.cod_producto!;
                record!.nom_producto = request.nom_producto!;
                record!.uni_medida = request.uni_medida!;
                record!.cod_tipo = request.cod_tipo!;
                record!.cod_segmento = request.cod_segmento!;
                //------------------------------------------
                record!.min_ingreso = request.min_ingreso;
                record!.max_ingreso = request.max_ingreso;
                record!.min_salida = request.min_salida;
                record!.max_salida = request.max_salida;
                record!.min_transfer = request.min_transfer;
                record!.max_transfer = request.max_transfer;

                if (append)
                    await AddAsync(record);
                else
                    await UpdateAsync(record!);

                scope.Complete();
            }

            return $"Producto creado satisfactoriamente.";
        }

        public async Task<string> Update(ProductoUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Producto.Where(w => w.gid_producto == request.id_producto).FirstOrDefaultAsync();

                ThrowTrue(record == null, "El producto no se encuentra registrado");

                var existing = await _context.Producto.Where(w => w.cod_producto == request.cod_producto && w.id_producto != record!.id_producto).FirstOrDefaultAsync();

                ThrowTrue(existing != null && existing.cod_estado == true, "El código de producto ya se encuentra registrado para otro producto");

                //----------------------------------------------
                record!.cod_producto = request.cod_producto!;
                record!.nom_producto = request.nom_producto!;
                record!.uni_medida = request.uni_medida!;
                record!.cod_tipo = request.cod_tipo!;
                record!.cod_segmento = request.cod_segmento!;
                //----------------------------------------------
                record!.min_ingreso = request.min_ingreso;
                record!.max_ingreso = request.max_ingreso;
                record!.min_salida = request.min_salida;
                record!.max_salida = request.max_salida;
                record!.min_transfer = request.min_transfer;
                record!.max_transfer = request.max_transfer;

                await UpdateAsync(record!);

                scope.Complete();
            }

            return "Producto actualizado satisfactoriamente";
        }

        public async Task<string> Delete(ProductoDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Producto.Where(w => w.gid_producto == request.id).FirstOrDefaultAsync();

                ThrowTrue(record, "El producto no se encuentra en los registros");

                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Producto eliminado satisfactoriamente";
        }

    }
}
