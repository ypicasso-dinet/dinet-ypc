using System.Transactions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Create;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Delete;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Update;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetAll;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetById;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetListParams;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.IngresoProducto;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;
using Toshi.Backend.Utilities;
using System.Globalization;
using Toshi.Backend.Domain.DTO.SalidaProducto;
using System.Threading.Tasks;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class IngresoProductoRepository : RepositoryBase<IngresoProductoEntity>, IIngresoProductoRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;
        private readonly EncryptionService _encryptor;

        const string MENU_CODE = "FEAT-2100";

        public IngresoProductoRepository(
            ToshiDBContext context,
            SessionStorage sessionStorage,
            IWebHostEnvironment env,
            IHttpContextAccessor http,
            EncryptionService encryptor) : base(context, sessionStorage)
        {
            _env = env;
            _http = http;
            _encryptor = encryptor;
        }

        public async Task<IngresoProductoListResponseDTO> GetAll(IngresoProductoGetAllQuery request)
        {
            var todos = await ValidateAction(MENU_CODE, ActionRol.Read);

            var response = new IngresoProductoListResponseDTO();

            var codigos = await CodigosPlantas.ObtenerPlanteles(_context, ID_USUARIO, request.id_plantel);

                var cod_producto = string.IsNullOrEmpty(request.id_producto) ? default(string?) : request.id_producto.ToString();

                var idProducto = await _context.Producto.Where(x => x.gid_producto == cod_producto && x.cod_estado == true).Select(x => x.id_producto).FirstOrDefaultAsync();

            var cod_estado_campania = string.IsNullOrEmpty(request.cod_estado_campania) ? default(int?) : request.cod_estado_campania.ToInt();
            var cod_estado = string.IsNullOrEmpty(request.cod_estado) ? default(bool?) : request.cod_estado.ToBool();
            var fec_desde = request.fec_desde.ToDate();
            var fec_hasta = request.fec_hasta.ToDate();

            var allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);
            var allowDelete = await IsValidAction(MENU_CODE, ActionRol.Delete);

            #region TAB 1...
            var records = await (
                from s in _context.IngresoProducto
                join c in _context.Campania on s.id_campania equals c.id_campania
                join p in _context.UsuarioPlantel on c.id_plantel equals p.id_plantel
                join producto in _context.Producto on s.id_producto equals producto.id_producto
                join envio in _context.EnvioDiario on new { campania = s.id_campania, fecha = s.fec_registro.Date } equals new { campania = envio.id_campania, fecha = envio.fec_envio } into df
                from dfi in df.DefaultIfEmpty()

                where (todos || s.id_usuario == ID_USUARIO)
                    && p.id_usuario == ID_USUARIO
                    && p.cod_estado == true
                    && codigos.Contains(s.campania.id_plantel) &&
                    (string.IsNullOrEmpty(request.id_campania) || s.campania.gid_campania == request.id_campania) &&
                    (cod_producto == null || s.id_producto == idProducto) &&
                    (cod_estado == null || s.cod_estado == cod_estado) &&
                    (fec_desde == null || s.fec_registro.Date >= fec_desde) &&
                    (fec_hasta == null || s.fec_registro.Date <= fec_hasta)

                orderby s.fec_registro descending

                select new IngresoProductoTab1DTO()
                {
                    id_ingreso_producto = s.gid_ingreso_producto,
                    fec_registro = s.fec_registro.ToDateTime(),
                    nom_plantel = s.campania.plantel.nom_plantel,
                    nom_campania = s.campania.cod_campania,
                    can_ingreso = s.can_ingreso,
                    cod_producto = producto.cod_producto,
                    desc_producto = producto.nom_producto,
                    unidad_medida = s.uni_producto,
                    tipo_producto = s.tip_producto,
                    guia_recepcion = s.guia_recepcion,
                    guia_proveedor = s.guia_proveedor,
                    id_producto = producto.gid_producto,

                    cod_estado = s.cod_estado,
                    nom_estado = s.cod_estado.ToStatus(),
                    cod_plantel = s.campania.plantel.gid_plantel,
                    observacion = s.observacion ?? "",

                    allow_update = s.cod_estado == false ? false : dfi == null ? allowUpdate : !dfi.ind_enviado,
                    allow_delete = s.cod_estado == false ? false : dfi == null ? allowDelete : !dfi.ind_enviado,

                    ind_enviado = dfi == null ? false : dfi.ind_enviado,
                    nom_enviado = s.cod_estado != true ? "---" : (dfi == null ? false : dfi.ind_enviado) ? "Enviado" : "Por Enviar",
                    color = s.cod_estado != true ? "#585858" : (dfi == null ? false : dfi.ind_enviado) ? "#70A83B" : "#ED9F38",

                    imagenes = _context.IngresoProductoImagen
                        .Where(i => i.id_ingreso_producto == s.id_ingreso_producto)
                        .Select(i => i.url_imagen)
                    .ToList(),
                }
            ).ToListAsync();

            response.info_tab1 = records;
            #endregion

            #region TAB 2...
            var queryIngresos = _context.IngresoProducto
                .Where(x => x.cod_estado == true &&
                    codigos.Contains(x.campania.id_plantel) &&
                    (string.IsNullOrEmpty(request.id_campania) || x.campania.gid_campania == request.id_campania) &&
                    (cod_producto == null || x.id_producto == idProducto) &&
                    (fec_desde == null || x.fec_registro.Date >= fec_desde) &&
                    (fec_hasta == null || x.fec_registro.Date <= fec_hasta))
                .Select(i => new
                {
                    i.id_producto,
                    i.producto.cod_producto,
                    i.producto.nom_producto,
                    i.producto.uni_medida,
                    ingreso = i.can_ingreso,
                    salida = 0m
                });

            var querySalidas = _context.SalidaProducto
                .Where(x => x.cod_estado == true &&
                    codigos.Contains(x.campania.id_plantel) &&
                    (string.IsNullOrEmpty(request.id_campania) || x.campania.gid_campania == request.id_campania) &&
                    (cod_producto == null || x.id_producto == idProducto) &&
                    (fec_desde == null || x.fec_registro.Date >= fec_desde) &&
                    (fec_hasta == null || x.fec_registro.Date <= fec_hasta))
                .Select(i => new
                {
                    i.id_producto,
                    i.producto.cod_producto,
                    i.producto.nom_producto,
                    i.producto.uni_medida,
                    ingreso = 0m,
                    salida = i.can_salida
                });

            var combinado = queryIngresos.Concat(querySalidas);

            response.info_tab2 = await combinado
                .GroupBy(x => new { x.id_producto, x.nom_producto, x.cod_producto, x.uni_medida })
                .Select(g => new IngresoProductoTab2DTO
                {
                    cod_producto = g.Key.cod_producto,
                    nom_producto = g.Key.nom_producto,
                    uni_medida = g.Key.uni_medida,
                    can_ingreso = g.Sum(x => x.ingreso),
                    can_salida = g.Sum(x => x.salida)
                })
                .OrderBy(r => r.nom_producto)
                .ToListAsync();
            #endregion

            return response;
        }

        public async Task<IngresoProductoDTO?> GetById(IngresoProductoGetByIdQuery request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Create(IngresoProductoCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            ProductoEntity? producto = null;
            string nombreProducto = "";

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var campania = await _context.Campania.Where(w => w.gid_campania == request.id_campania && w.cod_estado == true).FirstOrDefaultAsync();
                producto = await _context.Producto.Where(w => w.gid_producto == request.id_producto && w.cod_estado == true).FirstOrDefaultAsync();

                ThrowTrue(campania, "No se encuentra campaña disponible para el plantel");
                ThrowTrue(producto, "No se encuentra producto disponible");

                nombreProducto = producto.nom_producto;

                var gid = Guid.NewGuid().ToString();
                var fec_registro = DateTime.ParseExact(
                    request.fec_registro,
                    "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture
                );

                var tipoProducto = await _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "PROD_TIPO" && w.cod_detalle == producto!.cod_tipo && w.cod_estado == true).FirstOrDefaultAsync();
                var uniMedida = await _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "UNIDADES" && w.cod_detalle == producto!.uni_medida && w.cod_estado == true).FirstOrDefaultAsync();

                var record = new IngresoProductoEntity
                {
                    // Specifying field's
                    gid_ingreso_producto = gid,
                    id_usuario = ID_USUARIO!.Value,
                    id_campania = campania!.id_campania,
                    fec_registro = fec_registro,
                    id_producto = producto!.id_producto,
                    can_ingreso = request.can_ingreso!.Value,
                    tip_producto = tipoProducto!.nom_detalle,
                    uni_producto = uniMedida!.nom_detalle,
                    guia_proveedor = request.guia_proveedor ?? "",
                    guia_recepcion = request.guia_recepcion ?? "",
                    observacion = request.observacion ?? ""
                };

                ValidarFechaRegistro(request.fec_registro, campania!);
                await ValidarRangoIngresos(request.can_ingreso, producto.id_producto, producto.nom_producto);

                await ValidateStatus(record);
                await AddAsync(record);

                if (ES_MOBILE && request!.imagenes != null)
                {
                    var separator = Path.PathSeparator;

                    foreach (var item in request!.imagenes!)
                    {
                        var url_imagen = ProcesarImagen(item.url_imagen!);

                        var imagen = new IngresoProductoImagenEntity
                        {
                            id_ingreso_producto = record.id_ingreso_producto,
                            gid_ingreso_producto_imagen = Guid.NewGuid().ToString(),
                            nom_imagen = item.nom_imagen!,
                            url_imagen = url_imagen
                        };

                        await _context.IngresoProductoImagen.AddAsync(imagen);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();

            }
            return $"El ingreso del producto '{nombreProducto}' se registró satisfactoriamente.";
        }

        public async Task<string> Update(IngresoProductoUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.IngresoProducto.Where(w => w.gid_ingreso_producto == request.id_ingreso_producto).FirstOrDefaultAsync();
                var producto = await _context.Producto.Where(w => w.gid_producto == request.id_producto && w.cod_estado == true).FirstOrDefaultAsync();

                ThrowTrue(record == null, "No se encuentra registrado el ingreso de producto");

                await ValidateStatus(record!);

                var codProducto = await _context.Producto.Where(w => w.gid_producto == request.id_producto).Select(a => a.id_producto).FirstOrDefaultAsync();
                var tipoProducto = await _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "PROD_TIPO" && w.cod_detalle == request!.tipo_producto && w.cod_estado == true).FirstOrDefaultAsync();
                var uniMedida = await _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "UNIDADES" && w.cod_detalle == request!.unidad_medida && w.cod_estado == true).FirstOrDefaultAsync();

                var campania = await _context.Campania.FirstOrDefaultAsync(w => w.gid_campania == request.id_campania);
                ThrowTrue(campania, "La campaña no se encuentra disponible");

                ValidarFechaRegistro(request.fec_registro, campania!);
                await ValidarRangoIngresos(request.can_ingreso, producto.id_producto, producto.nom_producto);

                record!.id_campania = campania!.id_campania;
                record!.fec_registro = request!.fec_registro.ToDate()!.Value;
                record!.id_producto = codProducto;
                record!.guia_proveedor = request!.guia_proveedor ?? "";
                record!.guia_recepcion = request!.guia_recepcion ?? "";
                record!.can_ingreso = request!.can_ingreso ?? 0;
                record!.uni_producto = uniMedida.nom_detalle ?? "";
                record!.tip_producto = tipoProducto.nom_detalle ?? "";
                record!.observacion = request!.observacion ?? "";

                await UpdateAsync(record!);

                if (ES_MOBILE && request!.imagenes != null)
                {
                    var codigos = request!.imagenes!.Select(s => s.gid_imagen).ToList();

                    var disabledItems = await _context.IngresoProductoImagen
                        .Where(w => w.id_ingreso_producto == record!.id_ingreso_producto && !codigos.Contains(w.gid_ingreso_producto_imagen) && w.cod_estado == true)
                        .ToListAsync();

                    if (disabledItems.Any())
                    {
                        foreach (var item in disabledItems)
                        {
                            _context.IngresoProductoImagen.Entry(item).State = EntityState.Deleted;
                        }

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in request!.imagenes!)
                    {
                        if (!string.IsNullOrEmpty(item.gid_imagen))
                            continue;

                        var imagen = new IngresoProductoImagenEntity
                        {
                            id_ingreso_producto = record.id_ingreso_producto,
                            gid_ingreso_producto_imagen = Guid.NewGuid().ToString(),
                            nom_imagen = item.nom_imagen!,
                            url_imagen = item.url_imagen!
                        };

                        await _context.IngresoProductoImagen.AddAsync(imagen);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();
            }

            return "El ingreso del producto se actualizó satisfactoriamente.";
        }

        public async Task<string> Delete(IngresoProductoDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.IngresoProducto
                        .Where(w => w.gid_ingreso_producto == request.id)
                        .FirstOrDefaultAsync();

                ThrowTrue(record == null, "No se encuentra registrado el ingreso del producto");

                await ValidateStatus(record!);
                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Ingreso de producto eliminado satisfactoriamente";
        }

        public async Task<IngresoProductoListParamsDTO> GetListParams(IngresoProductoGetListParamsQuery request)
        {
            var response = new IngresoProductoListParamsDTO();
            var codigos = await CodigosPlantas.ObtenerPlanteles(_context, ID_USUARIO);

            response.planteles = await (
                from p in _context.Plantel.Where(w => w.cod_estado == true)
                join c in _context.Campania.Where(w => w.cod_estado == true) on p.id_plantel equals c.id_plantel
                where codigos.Contains(p.id_plantel)
                group new { p, campania = c } by new { p.gid_plantel, p.nom_plantel } into g
                select new CodeTextDTO
                {
                    code = g.Key.gid_plantel,
                    text = g.Key.nom_plantel,
                    childs = g
                        .Select(s => new CodeTextDTO
                        {
                            code = s.campania.gid_campania,
                            text = s.campania.cod_campania
                        }).ToList()
                }
            ).ToListAsync();

            response.estados_campanias = await _context.Configuracion
                .Where(w => w.cod_config == "INDICADOR_CAMPANIA")
                .SelectMany(s => s.configuracion_detalles!)
                .Where(w => w.cod_estado == true)
                .OrderBy(o => o.ord_config_det)
                .Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle))
                .ToListAsync();

            response.productos = await (
                from p in _context.Producto
                where p.cod_estado == true
                orderby p.nom_producto
                select new CodeTextDTO
                {
                    code = p.gid_producto,
                    text = p.cod_producto + " - " + p.nom_producto
                }
            ).ToListAsync();


            response.estados = await _context.Configuracion
                .Where(w => w.cod_config == "ESTADOS")
                .SelectMany(s => s.configuracion_detalles!)
                .Where(w => w.cod_estado == true)
                .OrderBy(o => o.nom_detalle)
                .Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle))
                .ToListAsync();

            return response;
        }

        public async Task<IngresoProductoScreenParamsDTO> GetScreenParams(IngresoProductoGetScreenParamsQuery request)
        {
            var response = new IngresoProductoScreenParamsDTO();
            var codigos = await CodigosPlantas.ObtenerPlanteles(_context, ID_USUARIO);

            response.planteles = await (
                    from p in _context.Plantel.Where(w => w.cod_estado == true)
                    join c in _context.Campania.Where(w => w.cod_estado == true) on p.id_plantel equals c.id_plantel
                    where codigos.Contains(p.id_plantel) && c.ind_proceso != 3
                    group new { p, campania = c } by new { p.gid_plantel, p.nom_plantel } into g
                    select new CodeTextDTO
                    {
                        code = g.Key.gid_plantel,
                        text = g.Key.nom_plantel,
                        childs = g.Select(s => new CodeTextDTO
                        {
                            code = s.campania.gid_campania,
                            text = s.campania.cod_campania
                        }).ToList()
                    }
                ).ToListAsync();

            response.productos = await (
                from p in _context.Producto
                where p.cod_estado == true
                orderby p.nom_producto
                select new CodeTextDTO
                {
                    code = p.gid_producto,
                    text = p.cod_producto + " - " + p.nom_producto
                }
            ).ToListAsync();

            return response;
        }

        public async Task<IngresoProductoListCampaniaPorEstadoDTO> GetCampaniasByEstadoAsync(IngresoProductoGetCampaniasPorEstadoQuery request)
        {
            var response = new IngresoProductoListCampaniaPorEstadoDTO();

            var idPlantel = await _context.Plantel
                .Where(w => w.gid_plantel == request.Id_plantel)
                .Select(s => s.id_plantel)
                .FirstOrDefaultAsync();

            var query = _context.Campania.AsQueryable();

            query = query.Where(w => w.id_plantel == idPlantel);

            if (request.CodEstadoCampania == "true")
            {
                query = query.Where(w => w.ind_proceso != 3);
            }
            else if (request.CodEstadoCampania != "Todos" && request.CodEstadoCampania != "null")
            {
                query = query.Where(w => w.ind_proceso == 3);
            }

            response.indicador_campanias = await query
                .Select(s => new CodeTextDTO(s.gid_campania, s.cod_campania))
                .ToListAsync();

            return response;
        }

        public async Task<IngresoProductoGetTipoProductoPorProductoDTO> GetTipoProductoPorProducto(IngresoProductoGetTipoProductoPorProductoQuery request)
        {
            var response = new IngresoProductoGetTipoProductoPorProductoDTO();

            response.tipo_producto = await (
                from cd in _context.ConfiguracionDetalle
                join c in _context.Configuracion on cd.id_config equals c.id_config
                join p in _context.Producto on cd.cod_detalle equals p.cod_tipo
                where p.gid_producto == request.CodProducto
                select new CodeTextDTO
                {
                    code = cd.cod_detalle,
                    text = cd.nom_detalle
                }
            ).ToListAsync();

            return response;
        }

        public async Task<IngresoProductoGetUnidadMedidaPorProductoDTO> GetUnidadMedidaPorProducto(IngresoProductoGetUnidadMedidaPorProductoQuery request)
        {
            var response = new IngresoProductoGetUnidadMedidaPorProductoDTO();

            response.unidad_medida = await (
                from cd in _context.ConfiguracionDetalle
                join c in _context.Configuracion on cd.id_config equals c.id_config
                join p in _context.Producto on cd.cod_detalle equals p.uni_medida
                where p.gid_producto == request.CodProducto
                select new CodeTextDTO
                {
                    code = cd.cod_detalle,
                    text = cd.nom_detalle
                }
            ).ToListAsync();

            return response;
        }

        private void ValidarFechaRegistro(string? fec_registro, CampaniaEntity campania)
        {
            var fecRegistro = fec_registro.ToDate()!.Value.Date;

            ThrowTrue(fecRegistro > DateTime.Now.Date, "La fecha de ingreso del producto no puede ser mayor al día de hoy");
            ThrowTrue(fecRegistro < campania.fec_limpieza.Date, $"La fecha de registro no puede ser menor a la fecha de inicio de campaña. " +
                $"Fecha inicio de campaña {campania.cod_campania}: {campania.fec_limpieza.ToString("dd/MM/yyyy")}");
        }

        private async Task ValidarRangoIngresos(decimal? canIngreso, int? idProducto, string nomProducto)
        {
            var producto = await _context.Producto
                .Where(x => x.id_producto == idProducto)
                .Select(x => new { x.min_ingreso, x.max_ingreso })
                .FirstOrDefaultAsync();

            ThrowTrue(producto == null, "Producto no encontrado.");

            ThrowTrue(
                canIngreso.HasValue &&
                (canIngreso < producto.min_ingreso || canIngreso > producto.max_ingreso),
                    $"Los rangos permitidos para el producto '{nomProducto}' son:" +
                    $"\nMÍNIMO: {producto.min_ingreso?.ToString("0.00")}, MÁXIMO: {producto.max_ingreso?.ToString("0.00")}"


            );
        }





        private async Task ValidateStatus(IngresoProductoEntity record)
        {
            var campania = await _context.Campania.Where(w => w.id_campania == record.id_campania).Include(i => i.plantel).FirstOrDefaultAsync();

            ThrowTrue(campania, "No se encuentra una campaña activa para el plantel indicado");
            ThrowTrue(campania!.ind_proceso == 3, "No se encuentra una campaña activa para el plantel indicado");


            var enviado = await EnvioDiarioService.Enviado(_context, ID_USUARIO, campania!.plantel!.gid_plantel, record.fec_registro.Date);

            ThrowTrue(enviado == true, $"No se puede registrar información para la fecha {record.fec_registro.ToString("dd/MM/yyyy")}");
        }

        private string ProcesarImagen(string url_imagen)
        {
            var urlImagen = _encryptor.Decrypt(url_imagen!);
            var sourcePath = Path.Combine(_env.ContentRootPath, urlImagen);
            var sourceInfo = new FileInfo(sourcePath);
            var destinyPath = Path.Combine(_env.ContentRootPath, "wwwroot", "ingreso_producto", "images", sourceInfo.Name);

            sourceInfo.CopyTo(destinyPath);

            var path1 = Path.Combine(_env.ContentRootPath, "wwwroot", "ingreso_producto");
            var path2 = Path.Combine(_env.ContentRootPath, "wwwroot", "ingreso_producto", "images");

            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);

            if (!Directory.Exists(path2))
                Directory.CreateDirectory(path2);

            return urlImagen!.Replace("wwwroot/temp/images", "ingreso_producto/images").Replace("\\", "/");
        }

        public async Task<byte[]> GenerateExcelAsync(Dictionary<string, object> payload, string webRootPath)
        {
            throw new NotImplementedException();
        }        
    }
}
