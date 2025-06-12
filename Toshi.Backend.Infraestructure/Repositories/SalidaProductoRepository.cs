using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Create;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Delete;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Update;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetAll;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetById;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetListParams;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.IngresoProducto;
using Toshi.Backend.Domain.DTO.SalidaProducto;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class SalidaProductoRepository : RepositoryBase<SalidaProductoEntity>, ISalidaProductoRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;
        private readonly EncryptionService _encryptor;

        const string MENU_CODE = "FEAT-2300";

        public SalidaProductoRepository(
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

        public async Task<SalidaProductoListParamsDTO> GetListParams(GetSalidaListParamsQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var response = new SalidaProductoListParamsDTO();
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

            response.estados = await _context.ConfiguracionDetalle.GetActives()
                .Where(w => w.configuracion.cod_config == Constants.CONFIG_ESTADOS)
                .Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle))
                .ToListAsync();

            return response;
        }

        public async Task<SalidaProductoGetAllResponseDTO> GetAll(SalidaProductoGetAllQuery request)
        {
            var todos = await ValidateAction(MENU_CODE, ActionRol.Read);

            var response = new SalidaProductoGetAllResponseDTO();
            var desde = request.fec_desde.ToDate();
            var hasta = request.fec_hasta.ToDate();
            var codigos = await CodigosPlantas.ObtenerPlanteles(_context, ID_USUARIO, request.id_plantel);

            var cod_producto = string.IsNullOrEmpty(request.id_producto) ? default(string?) : request.id_producto.ToString();

            var idProducto = await _context.Producto.Where(x => x.gid_producto == cod_producto && x.cod_estado == true).Select(x => x.id_producto).FirstOrDefaultAsync();
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        

            response.salidas = await (
                from s in _context.SalidaProducto
                join c in _context.Campania on s.id_campania equals c.id_campania
                join p in _context.UsuarioPlantel on c.id_plantel equals p.id_plantel
                join producto in _context.Producto on s.id_producto equals producto.id_producto
                join f in _context.EnvioDiario.Where(w => w.cod_estado == true) on new { s.id_campania, fecha = s.fec_registro.Date } equals new { f.id_campania, fecha = f.fec_envio.Date } into jf
                from df in jf.DefaultIfEmpty()

                where (todos || s.id_usuario == ID_USUARIO)
                && p.id_usuario == ID_USUARIO
                && p.cod_estado == true
                //------------------------------------------------------------------------------------------------------------
                && (string.IsNullOrEmpty(request.id_plantel) || s.campania.plantel.gid_plantel == request.id_plantel)
                && (string.IsNullOrEmpty(request.id_campania) || s.campania.gid_campania == request.id_campania)
                && (cod_producto == null || s.id_producto == idProducto)
                //&& (string.IsNullOrEmpty(request.nom_producto) || (s.producto.cod_producto.Contains(request.nom_producto) || s.producto.nom_producto.Contains(request.nom_producto)))
                && (string.IsNullOrEmpty(request.cod_estado) || s.cod_estado == request.cod_estado.ToBool())
                && (desde == null || s.fec_registro.Date >= desde)
                && (hasta == null || s.fec_registro.Date <= hasta)
                //------------------------------------------------------------

                orderby s.fec_registro descending

                select new SalidaProductoItemDTO()
                {
                    // Specifying field's
                    id_salida_producto = s.gid_salida_producto,
                    nom_plantel = s.campania.plantel.nom_plantel,
                    cod_plantel = s.campania.plantel.gid_plantel,
                    fec_registro = s.fec_registro.ToDate(),
                    nom_producto = s.producto.nom_producto,
                    can_salida = s.can_salida,
                    tip_producto = s.tip_producto,
                    uni_producto = s.uni_producto,
                    cod_campania = c.cod_campania,
                    cod_producto = producto.cod_producto,
                    id_producto = producto.gid_producto,
                    //observacion = s.observacion,
                    //------------------------------------
                    cod_estado = s.cod_estado,
                    nom_estado = s.cod_estado.ToStatus(),
                    //------------------------------------
                    allow_update = df == null ? true : !df.ind_enviado,
                    allow_delete = df == null ? true : !df.ind_enviado,

                    ind_enviado = df == null ? false : df.ind_enviado,
                    nom_enviado = s.cod_estado != true ? "---" : (df == null ? false : df.ind_enviado) ? "Enviado" : "Por Enviar",
                    color = s.cod_estado != true ? "#585858" : (df == null ? false : df.ind_enviado) ? "#70A83B" : "#ED9F38",

                    imagenes = _context.SalidaProductoImagen
                        .Where(i => i.id_salida_producto == s.id_salida_producto)
                        .Select(i => i.url_imagen)
                    .ToList(),
                }
            ).ToListAsync();

            var queryIngresos = _context.IngresoProducto
                 .Where(x => x.cod_estado == true &&
                    codigos.Contains(x.campania.id_plantel) &&
                    (string.IsNullOrEmpty(request.id_campania) || x.campania.gid_campania == request.id_campania) &&
                    (cod_producto == null || x.id_producto == idProducto) &&
                    (desde == null || x.fec_registro.Date >= desde) &&
                    (hasta == null || x.fec_registro.Date <= hasta))
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
                    (desde == null || x.fec_registro.Date >= desde) &&
                    (hasta == null || x.fec_registro.Date <= hasta))
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

            response.totales = await combinado
                .GroupBy(x => new { x.id_producto, x.nom_producto, x.cod_producto, x.uni_medida })
                .Select(g => new SalidaProductoTotalesDTO
                {
                    cod_producto = g.Key.cod_producto,
                    nom_producto = g.Key.nom_producto,
                    uni_medida = g.Key.uni_medida,
                    can_ingreso = g.Sum(x => x.ingreso),
                    can_salida = g.Sum(x => x.salida)
                })
                .OrderBy(r => r.nom_producto)
                //.ThenBy(r => r.fecha)
                .ToListAsync();

            return response;
        }

        public async Task<SalidaProductoDTO?> GetById(SalidaProductoGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.SalidaProducto
                .Where(w => w.gid_salida_producto == request.id)
                .Select(s => new SalidaProductoDTO()
                {
                    // Specifying field's    
                    id_salida_producto = s.gid_salida_producto,
                    nom_plantel = s.campania.plantel.nom_plantel,
                    cod_campania = s.campania.cod_campania,
                    fec_registro = s.fec_registro.ToString(),
                    nom_producto = s.producto.nom_producto,
                    tip_producto = s.tip_producto,
                    uni_producto = s.uni_producto,
                    can_salida = s.can_salida,
                    cod_estado = s.cod_estado,
                    nom_estado = s.cod_estado.ToStatus(),
                    observacion = s.observacion ?? "",
                    imagenes = s.salida_producto_imagenes.Select(i => new SalidaProductoImagenDTO
                    {
                        gid_imagen = i.gid_salida_producto_imagen,
                        nom_imagen = i.nom_imagen,
                        url_imagen = i.url_imagen
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<SalidaProductoScreenParamsDTO> GetScreenParams(GetSalidaScreenParamsQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            try
            {
                await ValidateAction(MENU_CODE, ActionRol.Create);
            }
            catch (Exception ex)
            {
                await ValidateAction(MENU_CODE, ActionRol.Update);
            }


            var response = new SalidaProductoScreenParamsDTO();
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

            response.estados = await _context.ConfiguracionDetalle.GetActives()
                .Where(w => w.configuracion.cod_config == Constants.CONFIG_ESTADOS)
                .Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle))
                .ToListAsync();

            response.productos = await (
                from p in _context.Producto.GetActives()
                join tp in _context.ConfiguracionDetalle.GetActives().Where(W => W.configuracion.cod_config == Constants.CONFIG_PROD_TIPO) on p.cod_tipo equals tp.cod_detalle
                join um in _context.ConfiguracionDetalle.GetActives().Where(W => W.configuracion.cod_config == Constants.CONFIG_UNIDADES) on p.uni_medida equals um.cod_detalle
                select new SalidaProductoProductoDTO
                {
                    id_producto = p.gid_producto,
                    nom_producto = p.nom_producto,
                    tip_producto = tp.nom_detalle,
                    uni_producto = um.nom_detalle,
                    text = $"{p.cod_producto} - {p.nom_producto}"
                })
                .ToListAsync();

            return response;
        }

        public async Task<string> Create(SalidaProductoCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var campania = await _context.Campania.GetActives().Where(w => w.gid_campania == request.id_campania).FirstOrDefaultAsync();
                var producto = await _context.Producto.GetActives().Where(w => w.gid_producto == request.id_producto).FirstOrDefaultAsync();

                ThrowTrue(campania, "No se encuentra campaña disponible para el plantel");
                ThrowTrue(producto, "No se encuentra producto disponible");

                var gid = Guid.NewGuid().ToString();
                var fec_registro = request.fec_registro.ToDate()!.Value.Date;
                var tipoProducto = await _context.ConfiguracionDetalle.GetActives().Where(w => w.configuracion.cod_config == Constants.CONFIG_PROD_TIPO && w.cod_detalle == producto!.cod_tipo).FirstOrDefaultAsync();
                var uniMedida = await _context.ConfiguracionDetalle.GetActives().Where(w => w.configuracion.cod_config == Constants.CONFIG_UNIDADES && w.cod_detalle == producto!.uni_medida).FirstOrDefaultAsync();

                var record = new SalidaProductoEntity
                {
                    // Specifying field's
                    gid_salida_producto = gid,
                    id_usuario = ID_USUARIO!.Value,
                    id_campania = campania!.id_campania,
                    fec_registro = fec_registro,
                    id_producto = producto!.id_producto,
                    can_salida = request.can_salida!.Value,
                    tip_producto = tipoProducto!.nom_detalle,
                    uni_producto = uniMedida!.nom_detalle,
                    observacion = request.observacion ?? ""
                };

                ValidarFechaRegistro(request.fec_registro, campania!);

                await ValidarRangoSalidas(request.can_salida, producto.id_producto, producto.nom_producto);
                await ValidarSaldo(campania!.id_campania, producto!.id_producto, record.can_salida, 0);
                await ValidateStatus(record);
                await AddAsync(record);

                if (ES_MOBILE && request!.imagenes != null)
                {
                    var separator = Path.PathSeparator;

                    foreach (var item in request!.imagenes!)
                    {
                        var url_imagen = ProcesarImagen(item.url_imagen!);

                        var imagen = new SalidaProductoImagenEntity
                        {
                            id_salida_producto = record.id_salida_producto,
                            gid_salida_producto_imagen = Guid.NewGuid().ToString(),
                            nom_imagen = item.nom_imagen!,
                            url_imagen = url_imagen
                        };

                        await _context.SalidaProductoImagen.AddAsync(imagen);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();
            }

            return $"Salida de producto registrada satisfactoriamente.";
        }

        public async Task<string> Update(SalidaProductoUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.SalidaProducto.Where(w => w.gid_salida_producto == request.id_salida_producto).FirstOrDefaultAsync();
                var campania = await _context.Campania.FirstOrDefaultAsync(w => w.gid_campania == request.id_campania);
                var producto = await _context.Producto.GetActives().Where(w => w.gid_producto == request.id_producto).FirstOrDefaultAsync();

                ThrowTrue(record == null, "No se encuentra registrado la salida de producto");
                ThrowTrue(campania, "La campaña no se encuentra disponible");
                ThrowTrue(producto, "No se encuentra producto disponible");


                var tipoProducto = await _context.ConfiguracionDetalle.GetActives().Where(w => w.configuracion.cod_config == Constants.CONFIG_PROD_TIPO && w.cod_detalle == producto!.cod_tipo).FirstOrDefaultAsync();
                var uniMedida = await _context.ConfiguracionDetalle.GetActives().Where(w => w.configuracion.cod_config == Constants.CONFIG_UNIDADES && w.cod_detalle == producto!.uni_medida).FirstOrDefaultAsync();

                ValidarFechaRegistro(request.fec_registro, campania!);

                var fec_registro = request.fec_registro.ToDate()!.Value.Date;

                record!.id_campania = campania!.id_campania;
                record!.fec_registro = fec_registro;
                record!.id_producto = producto!.id_producto;
                record!.can_salida = request.can_salida!.Value;
                record!.tip_producto = tipoProducto!.nom_detalle;
                record!.uni_producto = uniMedida!.nom_detalle;
                record!.observacion = request!.observacion ?? "";

                await ValidarSaldo(campania!.id_campania, producto!.id_producto, record.can_salida, 0);
                await ValidarRangoSalidas(request.can_salida, producto.id_producto, producto.nom_producto);
                await ValidateStatus(record!);
                await UpdateAsync(record!);

                if (ES_MOBILE && request!.imagenes != null)
                {
                    var codigos = request!.imagenes!.Select(s => s.gid_imagen).ToList();

                    var disabledItems = await _context.SalidaProductoImagen
                        .Where(w => w.id_salida_producto == record!.id_salida_producto && !codigos.Contains(w.gid_salida_producto_imagen) && w.cod_estado == true)
                        .ToListAsync();

                    if (disabledItems.Any())
                    {
                        foreach (var item in disabledItems)
                        {
                            _context.SalidaProductoImagen.Entry(item).State = EntityState.Deleted;
                        }

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in request!.imagenes!)
                    {
                        if (!string.IsNullOrEmpty(item.gid_imagen))
                            continue;

                        var imagen = new SalidaProductoImagenEntity
                        {
                            id_salida_producto = record.id_salida_producto,
                            gid_salida_producto_imagen = Guid.NewGuid().ToString(),
                            nom_imagen = item.nom_imagen!,
                            url_imagen = item.url_imagen!
                        };

                        await _context.SalidaProductoImagen.AddAsync(imagen);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();
            }

            return "Salida de producto actualizada satisfactoriamente";
        }

        public async Task<string> Delete(SalidaProductoDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.SalidaProducto
                        .Where(w => w.gid_salida_producto == request.id)
                        .FirstOrDefaultAsync();

                ThrowTrue(record == null, "No se encuentra registrada la salida de producto");

                await ValidateStatus(record!);
                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Salida de producto eliminada satisfactoriamente";
        }

        private async Task ValidarSaldo(int id_campania, int id_producto, decimal can_salida, int id_salida)
        {
            var listIngresos = await _context.IngresoProducto
                .GetActives()
                .Where(w => w.id_campania == id_campania && w.id_producto == id_producto)
                .Select(s => s.can_ingreso)
                .ToListAsync();

            var listSalidas = await _context.SalidaProducto
                .GetActives()
                .Where(w => w.id_campania == id_campania && w.id_producto == id_producto && w.id_salida_producto != id_salida)
                .Select(s => s.can_salida)
                .ToListAsync();

            var canIngresos = listIngresos.Sum(s => s);
            var canSalidas = listSalidas.Sum(s => s);
            var saldo = Convert.ToInt32(canIngresos) - Convert.ToInt32(canSalidas);

            ThrowTrue((saldo - can_salida) < 0, $"La cantidad de salida excede el saldo disponible de {saldo}");
        }

        private string ProcesarImagen(string url_imagen)
        {
            var urlImagen = _encryptor.Decrypt(url_imagen!);
            var sourcePath = Path.Combine(_env.ContentRootPath, urlImagen);
            var sourceInfo = new FileInfo(sourcePath);
            var destinyPath = Path.Combine(_env.ContentRootPath, "wwwroot", "salida_producto", "images", sourceInfo.Name);

            sourceInfo.CopyTo(destinyPath);

            var path1 = Path.Combine(_env.ContentRootPath, "wwwroot", "salida_producto");
            var path2 = Path.Combine(_env.ContentRootPath, "wwwroot", "salida_producto", "images");

            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);

            if (!Directory.Exists(path2))
                Directory.CreateDirectory(path2);

            return urlImagen!.Replace("wwwroot/temp/images", "salida_producto/images").Replace("\\", "/");
        }

        private void ValidarFechaRegistro(string? fec_registro, CampaniaEntity campania)
        {
            var fecRegistro = fec_registro.ToDate()!.Value.Date;

            ThrowTrue(fecRegistro > DateTime.Now.Date, "La fecha de registro no puede ser mayor al día de hoy");
            ThrowTrue(fecRegistro < campania.fec_limpieza.Date, $"La fecha de registro no puede ser menor a la fecha de inicio de campaña. " +
                $"Fecha inicio de campaña {campania.cod_campania}: {campania.fec_limpieza.ToString("dd/MM/yyyy")}");
        }

        private async Task ValidarRangoSalidas(decimal? canSalida, int? idProducto, string nomProducto)
        {
            var producto = await _context.Producto
                .Where(x => x.id_producto == idProducto)
                .Select(x => new { x.min_salida, x.max_salida })
                .FirstOrDefaultAsync();

            ThrowTrue(producto == null, "Producto no encontrado.");

            ThrowTrue(
                canSalida.HasValue &&
                (canSalida < producto.min_salida || canSalida > producto.max_salida),
                    $"Los rangos permitidos para el producto '{nomProducto}' son:" +
                    $"\nMÍNIMO: {producto.min_salida?.ToString("0.00")}, MÁXIMO: {producto.max_salida?.ToString("0.00")}"


            );
        }

        private async Task ValidateStatus(SalidaProductoEntity record)
        {
            var campania = await _context.Campania.Where(w => w.id_campania == record.id_campania).Include(i => i.plantel).FirstOrDefaultAsync();

            ThrowTrue(campania, "No se encuentra una campaña activa para el plantel indicado");
            ThrowTrue(campania!.ind_proceso == 3, "No se encuentra una campaña activa para el plantel indicado");


            var enviado = await EnvioDiarioService.Enviado(_context, ID_USUARIO, campania!.plantel!.gid_plantel, record.fec_registro.Date);

            ThrowTrue(enviado == true, $"No se puede registrar información para la fecha {record.fec_registro.ToString("dd/MM/yyyy")}");
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
    }
}
