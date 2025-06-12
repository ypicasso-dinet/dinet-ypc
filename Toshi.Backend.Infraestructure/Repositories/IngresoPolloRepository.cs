using Azure.Core;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text.Json;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Create;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Delete;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.DownloadExcel;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Update;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetAll;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetById;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetListParams;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetScreenParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.IngresoPollo;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class IngresoPolloRepository : RepositoryBase<IngresoPolloEntity>, IIngresoPolloRepository
    {
        const string MENU_CODE = "FEAT-2000";

        public IngresoPolloRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        private decimal Porcentaje(int? input1, int? input2)
        {
            int value1 = input1 ?? 0;
            int value2 = input2 ?? 0;

            return (value2 == 0) ? 0 : Math.Round((value1 * 100.0m) / value2, 2);
        }

        public async Task<IngresoPolloListResponseDTO> GetAll(IngresoPolloGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var response = new IngresoPolloListResponseDTO();

            var codigos = await CodigosPlantas.ObtenerPlanteles(_context, ID_USUARIO, request.id_plantel);

            var num_galpon = string.IsNullOrEmpty(request.num_galpon) ? default(int?) : request.num_galpon.ToInt();
            var cod_estado = string.IsNullOrEmpty(request.cod_estado) ? default(bool?) : request.cod_estado.ToBool();
            var fec_desde = request.fec_desde.ToDate();
            var fec_hasta = request.fec_hasta.ToDate();

            var allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);
            var allowDelete = await IsValidAction(MENU_CODE, ActionRol.Delete);

            #region Tab 1...

            var records = await (
                from s in _context.IngresoPollo
                join lote in _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "LOTE") on s.cod_lote equals lote.cod_detalle
                join sexo in _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "SEXO") on s.cod_genero equals sexo.cod_detalle
                    join envio in _context.EnvioDiario on new { campania = s.id_campania, fecha = s.fec_registro.Date } equals new { campania = envio.id_campania, fecha = envio.fec_envio } into df
                    from dfi in df.DefaultIfEmpty()

                where codigos.Contains(s.campania.id_plantel) &&
                    (string.IsNullOrEmpty(request.id_campania) || s.campania.gid_campania == request.id_campania) &&
                    (num_galpon == null || s.num_galpon == num_galpon!) &&
                    (cod_estado == null || s.cod_estado == cod_estado) &&
                    (fec_desde == null || s.fec_registro.Date >= fec_desde) &&
                    (fec_hasta == null || s.fec_registro.Date <= fec_hasta)

                orderby s.fec_registro descending

                select new IngresoPolloTab1DTO()
                {
                    // Specifying field's
                    id_ingreso_pollo = s.gid_ingreso_pollo,
                    id_campania = s.campania.gid_campania,
                    fec_registro = s.fec_registro.ToDateTime(),
                    num_galpon = s.num_galpon,
                    cod_genero = s.cod_genero,
                    can_ingreso = s.can_ingreso,
                    lot_procedencia = s.lot_procedencia,
                    nom_procedencia = s.nom_procedencia,
                    num_guia = s.num_guia,
                    cod_edad = s.cod_edad,
                    cod_lote = s.cod_lote,
                    cod_linea = s.cod_linea,
                    val_peso = s.val_peso,
                    can_muertos = s.can_muertos,
                    can_real = s.can_real,
                    num_vehiculo = s.num_vehiculo,
                    temp_cabina = s.temp_cabina,
                    hum_cabina = s.hum_cabina,

                    nom_plantel = s.campania.plantel.nom_plantel,
                    nom_campania = s.campania.cod_campania,
                    nom_lote = lote.nom_detalle,// s.cod_lote,
                    nom_genero = sexo.nom_detalle,// s.cod_genero,
                    nom_linea = s.cod_linea,
                    dia_mes = s.fec_registro.ToString("dd-MMM"),
                    hora_registro = s.fec_registro.ToString("HH:mm"),

                    cod_estado = s.cod_estado,
                    nom_estado = s.cod_estado.ToStatus(),

                    cod_plantel = s.campania.plantel.gid_plantel,

                    allow_update = s.cod_estado == false ? false : dfi == null ? allowUpdate : !dfi.ind_enviado,
                    allow_delete = s.cod_estado == false ? false : dfi == null ? allowDelete : !dfi.ind_enviado,

                    ind_enviado = dfi == null ? false : dfi.ind_enviado,
                    nom_enviado = s.cod_estado != true ? "---" : (dfi == null ? false : dfi.ind_enviado) ? "Enviado" : "Por Enviar",
                    color = s.cod_estado != true ? "#585858" : (dfi == null ? false : dfi.ind_enviado) ? "#70A83B" : "#ED9F38",

                    imagenes = _context.IngresoPolloImagen
                        .Where(i => i.id_ingreso_pollo == s.id_ingreso_pollo)
                        .Select(i => i.url_imagen)  
                    .ToList(),
                })
                .ToListAsync();

            response.info_tab1 = records;
            response.lineas = records.OrderBy(o => o.cod_linea).GroupBy(g => g.cod_linea).Select(s => s.Key).ToList() ?? [];
            response.lotes = records.OrderBy(o => o.nom_lote).GroupBy(g => g.nom_lote).Select(s => s.Key).ToList() ?? [];

            #endregion

            #region Tab 2.1...

            var sexos = records.OrderBy(o => o.nom_genero).GroupBy(g => g.nom_genero).Select(s => new { sexo = s.Key, total = s.Sum(x => x.can_real) }).ToList();
            var lotes = records.OrderBy(o => o.nom_lote).GroupBy(g => g.nom_lote).Select(s => new { lote = s.Key, total = s.Sum(x => x.can_real) }).ToList();

            response.info_tab2_1 = new List<IngresoPolloTab2_1DTO>();

            foreach (var item in sexos)
            {
                var info = new IngresoPolloTab2_1DTO
                {
                    sexo = item.sexo,
                    total = item.total,
                    lotes = records
                        .Where(w => w.nom_genero == item.sexo)
                        .OrderBy(g => g.nom_lote)
                        .GroupBy(g => g.nom_lote)
                        .Select(s => new IngresoPolloLoteDTO
                        {
                            lote = s.Key,
                            cantidad = s.Sum(x => x.can_real),
                            porcentaje = Porcentaje(s.Sum(x => x.can_real), item.total)
                        }).ToList()
                };

                response.info_tab2_1.Add(info);
            }

            var granTotal = lotes.Sum(s => s.total);

            var granItem = new IngresoPolloTab2_1DTO
            {
                sexo = "Total",
                lotes = lotes.Select(s => new IngresoPolloLoteDTO
                {
                    lote = s.lote,
                    cantidad = s.total,
                    porcentaje = Porcentaje(s.total, granTotal),
                }).ToList(),
                total = granTotal
            };

            var percentItem = new IngresoPolloTab2_1DTO
            {
                sexo = "%",
                lotes = lotes.Select(s => new IngresoPolloLoteDTO
                {
                    lote = s.lote,
                    cantidad = Convert.ToInt32(Porcentaje(s.total, granTotal))
                }).ToList()
            };

            response.info_tab2_1.Add(granItem);
            response.info_tab2_1.Add(percentItem);

            #endregion

            #region Tab 2.2...

            var info_tab2_2 = records
                .OrderBy(o => o.fec_registro)
                .ThenBy(t => t.num_galpon)
                .ThenBy(t => t.nom_genero)
                .GroupBy(g => new { fecha = g.fec_registro.Substring(0, 10), galpon = g.num_galpon, sexo = g.nom_genero })
                .Select(s => new IngresoPolloTab2_2DTO
                {
                    fecha = s.Key.fecha.ToDate()!.Value.ToString("dd-MMM"),
                    galpon = s.Key.galpon,
                    sexo = s.Key.sexo,
                    lotes = new List<int>(),// s.OrderBy(o => o.nom_lote).Select(z => z.can_real ?? 0).ToList(),
                    total = s.Sum(z => z.can_real ?? 0)
                })
                .ToList();

            foreach (var info in info_tab2_2)
            {
                foreach (var lote in response.lotes)
                {
                    var total = records.Where(w =>
                        w.dia_mes == info.fecha &&
                        w.num_galpon == info.galpon &&
                        w.nom_genero == info.sexo &&
                        w.nom_lote == lote).Sum(s => s.can_real);

                    info!.lotes!.Add(total ?? 0);
                }
            }

            response.info_tab2_2 = info_tab2_2;

            #endregion

            #region Tab 3...

            response.info_tab3 = records
                .OrderBy(o => o.num_galpon)
                .ThenBy(o => o.nom_genero)
                .ThenBy(o => o.nom_lote)
                .ThenBy(o => o.cod_linea)
                .GroupBy(g => new { galpon = g.num_galpon, sexo = g.nom_genero })
                .Select(s => new IngresoPolloTab3DTO
                {
                    galpon = s.Key.galpon,
                    sexo = s.Key.sexo,
                    //lineas = s.OrderBy(o => o.cod_linea).GroupBy(g => new { linea = g.cod_linea }).Select(z => z.Sum(m => m.can_real ?? 0)).ToList(),
                    total = s.Sum(z => z.can_real ?? 0)
                })
                .ToList();

            foreach (var item in response.info_tab3)
            {
                item.lineas = new List<int>();

                foreach (var linea in response.lineas)
                {
                    var can_real = records.Where(w => w.num_galpon == item.galpon
                        && w.nom_genero == item.sexo && w.cod_linea == linea)
                    .Select(s => s.can_real ?? 0).Sum();

                    item.lineas.Add(can_real);

                }
            }


            #endregion

            return response;
        }

        public async Task<IngresoPolloDTO?> GetById(IngresoPolloGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);

            var record = await (
                from s in _context.IngresoPollo
                join sexo in _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "SEXO") on s.cod_genero equals sexo.cod_detalle
                join lote in _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "LOTE") on s.cod_lote equals lote.cod_detalle
                join linea in _context.ConfiguracionDetalle.Where(w => w.configuracion.cod_config == "LINEA_POLLO") on s.cod_linea equals linea.cod_detalle
                join e in _context.EnvioDiario on new { c = s.id_campania, f = s.fec_registro.Date } equals new { c = e.id_campania, f = e.fec_envio.Date } into j
                from dj in j.DefaultIfEmpty()

                where s.gid_ingreso_pollo == request.id

                select new IngresoPolloDTO()
                {
                    // Specifying field's    
                    id_ingreso_pollo = s.gid_ingreso_pollo,
                    id_plantel = s.campania.plantel.gid_plantel,
                    id_campania = s.campania.gid_campania,
                    fec_registro = s.fec_registro.ToDateTime(),
                    num_galpon = s.num_galpon,
                    cod_genero = s.cod_genero,
                    can_ingreso = s.can_ingreso,
                    lot_procedencia = s.lot_procedencia,
                    nom_procedencia = s.nom_procedencia,
                    num_guia = s.num_guia,
                    cod_edad = s.cod_edad,
                    cod_lote = s.cod_lote,
                    cod_linea = s.cod_linea,
                    val_peso = s.val_peso,
                    can_muertos = s.can_muertos,
                    can_real = s.can_real,
                    num_vehiculo = s.num_vehiculo,
                    temp_cabina = s.temp_cabina,
                    hum_cabina = s.hum_cabina,

                    fec_upsert = (s.fec_update ?? s.fec_insert)!.Value.ToString("dd/MM/yyyy HH:mm"),
                    hor_registro = s.fec_registro.ToTime(),

                    nom_plantel = s.campania.plantel.nom_plantel,
                    nom_campania = s.campania.cod_campania,
                    nom_sexo = sexo.nom_detalle,
                    nom_lote = lote.nom_detalle,
                    nom_linea = linea.nom_detalle,

                    allow_update = dj == null ? allowUpdate : !dj.ind_enviado,

                    imagenes = s.ingreso_pollo_imagenes.Where(w => w.cod_estado == true).Select(i => new IngresoPolloImagenDTO
                    {
                        gid_imagen = i.gid_ingreso_pollo_imagen,
                        nom_imagen = i.nom_imagen,
                        url_imagen = i.url_imagen
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> Create(IngresoPolloCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var campania = await _context.Campania.FirstOrDefaultAsync(w => w.gid_campania == request.id_campania);
                ThrowTrue(campania, "La campaña no se encuentra disponible");

                ValidarFechaRegistro(request.fec_registro, campania!);

                var record = new IngresoPolloEntity
                {
                    // Specifying field's
                    gid_ingreso_pollo = Guid.NewGuid().ToString(),
                    id_campania = campania!.id_campania,
                    fec_registro = request!.fec_registro.ToDate()!.Value,
                    num_galpon = request!.num_galpon!.Value,
                    cod_genero = request!.cod_genero ?? "",
                    can_ingreso = request!.can_ingreso!.Value,
                    lot_procedencia = request!.lot_procedencia ?? "",
                    nom_procedencia = request!.nom_procedencia ?? "",
                    num_guia = request!.num_guia ?? "",
                    cod_edad = request!.cod_edad ?? "",
                    cod_lote = request!.cod_lote ?? "",
                    cod_linea = request!.cod_linea ?? "",
                    val_peso = request!.val_peso!.Value,
                    can_muertos = request!.can_muertos ?? 0,
                    can_real = (request.can_ingreso ?? 0) - (request.can_muertos ?? 0),
                    num_vehiculo = request!.num_vehiculo ?? "",
                    temp_cabina = request!.temp_cabina ?? 0,
                    hum_cabina = request!.hum_cabina ?? 0,
                };

                await ValidateStatus(record);
                await AddAsync(record);

                if (ES_MOBILE && request!.imagenes != null)
                {
                    foreach (var item in request!.imagenes!)
                    {
                        var imagen = new IngresoPolloImagenEntity
                        {
                            id_ingreso_pollo = record.id_ingreso_pollo,
                            gid_ingreso_pollo_imagen = Guid.NewGuid().ToString(),
                            nom_imagen = item.nom_imagen!,
                            url_imagen = item.url_imagen!
                        };

                        await _context.IngresoPolloImagen.AddAsync(imagen);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();
            }

            return $"El ingreso de pollo BB se registró satisfactoriamente.";
        }

        public async Task<string> Update(IngresoPolloUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.IngresoPollo.Where(w => w.gid_ingreso_pollo == request.id_ingreso_pollo).FirstOrDefaultAsync();

                ThrowTrue(record == null, "No se encuentra registrado el ingreso de pollo bebe");

                await ValidateStatus(record!);

                var campania = await _context.Campania.FirstOrDefaultAsync(w => w.gid_campania == request.id_campania);
                ThrowTrue(campania, "La campaña no se encuentra disponible");

                ValidarFechaRegistro(request.fec_registro, campania!);

                record!.id_campania = campania!.id_campania;
                record!.fec_registro = request!.fec_registro.ToDate()!.Value;
                record!.num_galpon = request!.num_galpon!.Value;
                record!.cod_genero = request!.cod_genero!;
                record!.can_ingreso = request!.can_ingreso!.Value;
                record!.lot_procedencia = request!.lot_procedencia ?? "";
                record!.nom_procedencia = request!.nom_procedencia ?? "";
                record!.num_guia = request!.num_guia ?? "";
                record!.cod_edad = request!.cod_edad ?? "";
                record!.cod_lote = request!.cod_lote ?? "";
                record!.cod_linea = request!.cod_linea ?? "";
                record!.val_peso = request!.val_peso ?? 0;
                record!.can_muertos = request!.can_muertos ?? 0;
                record!.can_real = request!.can_ingreso!.Value - (request!.can_muertos ?? 0);
                record!.num_vehiculo = request!.num_vehiculo ?? "";
                record!.temp_cabina = request!.temp_cabina ?? 0;
                record!.hum_cabina = request!.hum_cabina ?? 0;

                await UpdateAsync(record!);

                if (ES_MOBILE && request!.imagenes != null)
                {
                    var codigos = request!.imagenes!.Select(s => s.gid_imagen).ToList();

                    var disabledItems = await _context.IngresoPolloImagen
                        .Where(w => w.id_ingreso_pollo == record!.id_ingreso_pollo && !codigos.Contains(w.gid_ingreso_pollo_imagen) && w.cod_estado == true)
                        .ToListAsync();

                    if (disabledItems.Any())
                    {
                        foreach (var item in disabledItems)
                        {
                            _context.IngresoPolloImagen.Entry(item).State = EntityState.Deleted;
                        }

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in request!.imagenes!)
                    {
                        if (!string.IsNullOrEmpty(item.gid_imagen))
                            continue;

                        var imagen = new IngresoPolloImagenEntity
                        {
                            id_ingreso_pollo = record.id_ingreso_pollo,
                            gid_ingreso_pollo_imagen = Guid.NewGuid().ToString(),
                            nom_imagen = item.nom_imagen!,
                            url_imagen = item.url_imagen!
                        };

                        await _context.IngresoPolloImagen.AddAsync(imagen);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();
            }

            return "El ingreso de pollo BB se actualizó satisfactoriamente.";
        }

        private void ValidarFechaRegistro(string? fec_registro, CampaniaEntity campania)
        {
            var fecRegistro = fec_registro.ToDate()!.Value.Date;

            ThrowTrue(fecRegistro > DateTime.Now.Date, "La fecha de registro no puede ser mayor al día de hoy");
            ThrowTrue(fecRegistro < campania.fec_limpieza.Date, "La fecha de registro no puede ser menor a la fecha de inicio de campaña");
        }

        public async Task<string> Delete(IngresoPolloDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.IngresoPollo
                        .Where(w => w.gid_ingreso_pollo == request.id)
                        .FirstOrDefaultAsync();

                ThrowTrue(record == null, "No se encuentra registrado el ingreso de pollo bebe");

                await ValidateStatus(record!);
                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Ingreso de Pollo eliminado satisfactoriamente";
        }

        public async Task<IngresoPolloListParamsDTO> GetListParams(GetListParamsQuery request)
        {
            var response = new IngresoPolloListParamsDTO();
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
                        childs = g.Select(s => new CodeTextDTO
                        {
                            code = s.campania.gid_campania,
                            text = s.campania.cod_campania
                        }).ToList()
                    }
                ).ToListAsync();

            var galpones = await _context.ConfiguracionDetalle
               .OrderBy(o => o.ord_config_det)
               .Where(w => w.configuracion!.cod_config == "GALPONES" && w.configuracion!.cod_estado == true && w.cod_estado == true)
               .FirstOrDefaultAsync();

            if (galpones != null)
            {
                var min = Convert.ToInt32(galpones.min_value);
                var max = Convert.ToInt32(galpones.max_value);

                response.galpones = Enumerable.Range(min, max).Select(s => new CodeTextDTO(s)).ToList();
            }

            response.estados = await _context.Configuracion
                .Where(w => w.cod_config == "ESTADOS")
                .SelectMany(s => s.configuracion_detalles!)
                .Where(w => w.cod_estado == true)
                .OrderBy(o => o.nom_detalle)
                .Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle))
                .ToListAsync();

            return response;
        }

        public async Task<IngresoPolloScreenParamsDTO> GetScreenParams(GetScreenParamsQuery request)
        {
            var response = new IngresoPolloScreenParamsDTO();
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

            var galpones = await _context.ConfiguracionDetalle
               .OrderBy(o => o.ord_config_det)
               .Where(w => w.configuracion!.cod_config == "GALPONES" && w.configuracion!.cod_estado == true && w.cod_estado == true)
               .FirstOrDefaultAsync();

            if (galpones != null)
            {
                var min = Convert.ToInt32(galpones.min_value);
                var max = Convert.ToInt32(galpones.max_value);

                response.galpones = Enumerable.Range(min, max).Select(s => new CodeTextDTO(s)).ToList();
            }

            response.sexos = await GetConfigs("SEXO");

            //response.edades = await GetConfigs("EDADES");
            var edades = await _context.ConfiguracionDetalle
               .OrderBy(o => o.ord_config_det)
               .Where(w => w.configuracion!.cod_config == "EDADES" && w.configuracion!.cod_estado == true && w.cod_estado == true)
               .FirstOrDefaultAsync();

            if (edades != null)
            {
                var min = Convert.ToInt32(edades.min_value);
                var max = Convert.ToInt32(edades.max_value);

                response.edades = Enumerable.Range(min, max).Select(s => new CodeTextDTO(s)).ToList();
            }

            response.lotes = await GetConfigs("LOTE");
            response.lineas = await GetConfigs("LINEA_POLLO");

            return response;
        }

        private async Task ValidateStatus(IngresoPolloEntity record)
        {
            var campania = await _context.Campania.Where(w => w.id_campania == record.id_campania).Include(i => i.plantel).FirstOrDefaultAsync();
            var enviado = await EnvioDiarioService.Enviado(_context, ID_USUARIO, campania!.plantel!.gid_plantel, record.fec_registro.Date);

            ThrowTrue(enviado == true, $"No se puede registrar información para la fecha {record.fec_registro.ToString("dd/MM/yyyy")}");
        }

        public async Task<byte[]> GenerateExcelAsync(Dictionary<string, object> payload, string webRootPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var source1 = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(payload["source1"]?.ToString() ?? "[]");
            var source2_1 = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(payload["source2_1"]?.ToString() ?? "[]");
            var source2_2 = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(payload["source2_2"]?.ToString() ?? "[]");
            var source3 = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(payload["source3"]?.ToString() ?? "[]");
            var loteNames = JsonSerializer.Deserialize<List<string>>(payload["lotes"]?.ToString() ?? "[]");
            var lineaNames = JsonSerializer.Deserialize<List<string>>(payload["lineas"]?.ToString() ?? "[]");

            var logoPath = Path.Combine(webRootPath, "images", "logo-black.png");

            using var package = new ExcelPackage();

            var headers = new Dictionary<string, string>
            {
                { "CAMPAÑA", "nom_campania" }, { "PLANTEL", "nom_plantel" }, { "FECHA", "fec_registro" },
                { "GALPON", "num_galpon" }, { "SEXO", "cod_genero" }, { "CANTIDAD", "can_ingreso" },
                { "LOTE DE PROCEDENCIA", "lot_procedencia" }, { "PROCEDENCIA", "nom_procedencia" },
                { "GUIA", "num_guia" }, { "EDAD", "cod_edad" }, { "LOTE", "nom_lote" }, { "LINEA", "cod_linea" },
                { "PESO", "val_peso" }, { "MUERTOS", "can_muertos" }, { "CANT. REAL", "can_real" },
                { "HORA", "hora_registro" }, { "VEHICULO", "num_vehiculo" }, { "T° CABINA", "temp_cabina" },
                { "HUMEDAD", "hum_cabina" }
            };

            #region TAB1
            var sheet1 = package.Workbook.Worksheets.Add("Listado IPBB");
            InsertLogoToshi(sheet1, logoPath);
            FillSheetWithData(sheet1, source1, headers);
            #endregion

            #region TAB2-1
            var sheet2 = package.Workbook.Worksheets.Add("Reporte 2");
            InsertLogoToshi(sheet2, logoPath);
            FillSheetWithDataForSexAndLotes(sheet2, source2_1);
            #endregion

            #region TAB2-2
            int lastRow = sheet2.Dimension.End.Row + 5;
            FillSheetWithDataForDateAndLotes(sheet2, source2_2, loteNames, startRow: lastRow);
            #endregion

            #region TAB3
            var sheet3 = package.Workbook.Worksheets.Add("Reporte 3");
            InsertLogoToshi(sheet3, logoPath);
            FillSheetWithDataForGalponAndSexo(sheet3, source3, lineaNames);
            #endregion

            return package.GetAsByteArray();
        }

        #region Insert Logo
        public static void InsertLogoToshi(ExcelWorksheet sheet, string logoPath)
        {
            if (File.Exists(logoPath))
            {
                using var image = Image.FromFile(logoPath);
                using var stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                var picture = sheet.Drawings.AddPicture("Logo", stream);
                picture.SetPosition(0, 0, 0, 0);
                picture.SetSize(20);
            }

            sheet.Row(1).Height = 50;
        }
        #endregion

        #region TAB1
        public static void FillSheetWithData(ExcelWorksheet sheet, List<Dictionary<string, object>> data, Dictionary<string, string> headerMappings)
        {
            if (data == null || data.Count == 0)
                return;

            int headerRow = 5;
            int dataStartRow = headerRow + 1;

            var headers = headerMappings.Keys.ToList();
            var keys = headerMappings.Values.ToList();

            Color zebraColor = ColorTranslator.FromHtml("#F2F2F2");

            for (int i = 0; i < headers.Count; i++)
            {
                var cell = sheet.Cells[headerRow, i + 1];
                cell.Value = headers[i];

                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(Color.White);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#004C97"));

                ExcelHelper.SetWhiteBorders(cell, Color.White);

                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            var camposACentrar = new List<string> {
                "nom_campania",
                "nom_plantel",
                "fec_registro",
                "num_galpon",
                "cod_genero",
                "can_ingreso",
                "cod_edad",
                "val_peso",
                "can_muertos",
                "can_real",
                "hora_registro",
                "temp_cabina",
                "hum_cabina" };

            for (int row = 0; row < data.Count; row++)
            {
                var rowData = data[row];
                for (int col = 0; col < keys.Count; col++)
                {
                    bool isEvenRow = (row % 2 == 0);
                    Color bgColor = isEvenRow ? Color.White : zebraColor;

                    var key = keys[col];
                    var cell = sheet.Cells[dataStartRow + row, col + 1];

                    var camposNumericos = new List<string> {
                        "can_ingreso", "cod_edad", "val_peso",
                        "can_muertos", "can_real", "temp_cabina", "hum_cabina"
                    };


                    if (rowData.ContainsKey(key) && rowData[key] != null)
                    {
                        var value = rowData[key].ToString();

                        if (key == "fec_registro" && DateTime.TryParse(value, out DateTime fecha))
                        {
                            cell.Value = fecha;
                            cell.Style.Numberformat.Format = "dd/MM/yyyy";
                        }
                        else if (camposNumericos.Contains(key))
                        {
                            if (int.TryParse(value, out int entero))
                            {
                                cell.Value = entero;
                                cell.Style.Numberformat.Format = "#,##0"; // Formato para enteros
                            }
                            else if (double.TryParse(value, out double decimalNum))
                            {
                                cell.Value = decimalNum;
                                cell.Style.Numberformat.Format = "#,##0.00"; // Formato para decimales
                            }
                            else
                            {
                                cell.Value = value;
                            }
                        }
                        else
                        {
                            cell.Value = value;
                        }
                    }
                    else
                    {
                        cell.Value = "";
                    }

                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bgColor);

                    if (camposACentrar.Contains(key))
                    {
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }
            }

            int columnaCantidadReal = headers.FindIndex(h => h == "CANT. REAL") + 1;
            int filaTotal = 6 + data.Count;

            var cellTotal = sheet.Cells[filaTotal, columnaCantidadReal];
            cellTotal.Formula = $"SUM({sheet.Cells[6, columnaCantidadReal].Address}:{sheet.Cells[filaTotal - 1, columnaCantidadReal].Address})";
            cellTotal.Style.Font.Bold = true;
            cellTotal.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellTotal.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cellTotal.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
            cellTotal.Style.Numberformat.Format = "#,##0";


            sheet.Cells.AutoFitColumns();
        }
        #endregion

        #region TAB2-1
        public static void FillSheetWithDataForSexAndLotes(ExcelWorksheet sheet, List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
                return;

            int headerRow = 5;
            int dataStartRow = headerRow + 1;

            // Paso 1: Obtener nombres de lotes únicos en orden de aparición
            var loteNames = new List<string>();

            Color zebraColor = ColorTranslator.FromHtml("#F2F2F2");

            foreach (var row in data)
            {
                if (row.ContainsKey("lotes"))
                {
                    var lotesElement = (JsonElement)row["lotes"];
                    var lotes = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(lotesElement.GetRawText());

                    foreach (var lote in lotes)
                    {
                        var nombre = lote["lote"].ToString();
                        if (!loteNames.Contains(nombre))
                        {
                            loteNames.Add(nombre);
                        }
                    }
                }
            }

            // Paso 2: Construir cabeceras
            var headers = new List<string> { "Sexo" };
            foreach (var lote in loteNames)
            {
                headers.Add(lote);
                headers.Add("%");
            }
            headers.Add("Total");

            // Paso 3: Insertar cabeceras en Excel
            for (int i = 0; i < headers.Count; i++)
            {
                var cell = sheet.Cells[headerRow, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#004C97"));
                cell.Style.Font.Color.SetColor(Color.White);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ExcelHelper.SetWhiteBorders(cell, Color.White);
            }

            // Paso 4: Llenar los datos
            int rowIndex = dataStartRow;

            foreach (var rowData in data)
            {
                var sexo = rowData["sexo"].ToString();
                sheet.Cells[rowIndex, 1].Value = sexo;

                var lotesElement = (JsonElement)rowData["lotes"];
                var lotes = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(lotesElement.GetRawText());

                var loteDict = lotes.ToDictionary(l => l["lote"].ToString(), l => l);

                int colIndex = 2;

                foreach (var loteName in loteNames)
                {
                    if (loteDict.TryGetValue(loteName, out var lote))
                    {
                        var cantidadElement = (JsonElement)lote["cantidad"];
                        double cantidad = cantidadElement.ValueKind == JsonValueKind.Number
                            ? cantidadElement.GetDouble()
                            : double.Parse(cantidadElement.GetRawText());

                        double? porcentaje = null;
                        if (lote.ContainsKey("porcentaje") && lote["porcentaje"] is JsonElement porcentajeElement && porcentajeElement.ValueKind == JsonValueKind.Number)
                        {
                            porcentaje = porcentajeElement.GetDouble() / 100.0;
                        }

                        if (sexo == "%")
                        {
                            sheet.Cells[rowIndex, colIndex].Value = cantidad;
                            sheet.Cells[rowIndex, colIndex].Style.Numberformat.Format = "0\\%";
                        }
                        else
                        {
                            sheet.Cells[rowIndex, colIndex].Value = cantidad;
                            sheet.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,##0";
                        }

                        if (porcentaje.HasValue)
                        {
                            sheet.Cells[rowIndex, colIndex + 1].Value = porcentaje.Value * 100;
                            sheet.Cells[rowIndex, colIndex + 1].Style.Numberformat.Format = "0.00";
                        }
                        else
                        {
                            sheet.Cells[rowIndex, colIndex + 1].Value = null;
                        }
                    }

                    colIndex += 2; // Aumentar el índice para la siguiente pareja de cantidad/porcentaje
                }

                if (rowData.TryGetValue("total", out var total) && total != null)
                {
                    var totalElement = (JsonElement)total;

                    double totalValue = totalElement.ValueKind == JsonValueKind.Number
                        ? totalElement.GetDouble()
                        : double.Parse(totalElement.GetRawText());

                    sheet.Cells[rowIndex, headers.Count].Value = totalValue;
                    sheet.Cells[rowIndex, headers.Count].Style.Numberformat.Format = "#,##0";
                }

                rowIndex++;
            }

            int dataEndRow = rowIndex - 1;
            int totalCols = headers.Count;

            for (int r = dataStartRow; r <= dataEndRow; r++)
            {
                for (int c = 1; c <= totalCols; c++)
                {
                    var cell = sheet.Cells[r, c];
                    bool isEvenRow = (r - dataStartRow) % 2 == 0;
                    Color bgColor = isEvenRow ? Color.White : zebraColor;

                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bgColor);
                }
            }
            double columnWidth = 15;

            for (int col = 1; col <= headers.Count; col++)
            {
                sheet.Column(col).Width = columnWidth;
            }
            sheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        #endregion

        #region TAB2-2
        public static void FillSheetWithDataForDateAndLotes(ExcelWorksheet sheet, List<Dictionary<string, object>> data, List<string> loteNames, int startRow)
        {
            if (data == null || data.Count == 0 || loteNames == null || loteNames.Count == 0)
                return;

            int headerRow = startRow;
            int dataStartRow = headerRow + 1;

            var headersT = new List<string> { "Fecha", "Galpón", "Sexo" };
            headersT.AddRange(loteNames);
            headersT.Add("Total");
            Color headerBgColor = ColorTranslator.FromHtml("#004C97");
            Color headerFontColor = Color.White;
            Color headerBorderColor = Color.White;
            Color zebraColor = ColorTranslator.FromHtml("#F2F2F2");

            for (int i = 0; i < headersT.Count; i++)
            {
                var cell = sheet.Cells[headerRow, i + 1];
                cell.Value = headersT[i];

                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(headerFontColor);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(headerBgColor);

                // Bordes blancos
                ExcelHelper.SetWhiteBorders(cell, headerBorderColor);

                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // ?? Escribir datos fila por fila con acebrado
            int rowIndex = dataStartRow;
            foreach (var rowData in data)
            {
                bool isEvenRow = (rowIndex - dataStartRow) % 2 == 0;
                Color bgColor = isEvenRow ? Color.White : zebraColor;

                // Fecha
                var cellFecha = sheet.Cells[rowIndex, 1];
                cellFecha.Value = rowData["fecha"]?.ToString();
                cellFecha.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cellFecha.Style.Fill.BackgroundColor.SetColor(bgColor);

                // Galpón
                var cellGalpon = sheet.Cells[rowIndex, 2];
                cellGalpon.Value = rowData["galpon"]?.ToString();
                cellGalpon.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cellGalpon.Style.Fill.BackgroundColor.SetColor(bgColor);

                // Sexo
                var cellSexo = sheet.Cells[rowIndex, 3];
                cellSexo.Value = rowData["sexo"]?.ToString();
                cellSexo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cellSexo.Style.Fill.BackgroundColor.SetColor(bgColor);

                // Lotes
                var lotesElement = (JsonElement)rowData["lotes"];
                var lotes = JsonSerializer.Deserialize<List<double>>(lotesElement.GetRawText());

                int colIndex = 4;
                for (int i = 0; i < loteNames.Count; i++)
                {
                    double cantidad = i < lotes.Count ? lotes[i] : 0;
                    var cell = sheet.Cells[rowIndex, colIndex];
                    cell.Value = cantidad;
                    cell.Style.Numberformat.Format = "#,##0";
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bgColor);
                    colIndex++;
                }

                // Total
                if (rowData.TryGetValue("total", out var total) && total is JsonElement totalElement)
                {
                    double totalValue = totalElement.ValueKind == JsonValueKind.Number
                        ? totalElement.GetDouble()
                        : double.TryParse(totalElement.GetRawText(), out double parsed) ? parsed : 0;

                    var cell = sheet.Cells[rowIndex, colIndex];
                    cell.Value = totalValue;
                    cell.Style.Numberformat.Format = "#,##0";
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bgColor);
                }

                rowIndex++;
            }

            double columnWidth = 15;

            for (int col = 1; col <= headersT.Count; col++)
            {
                sheet.Column(col).Width = columnWidth;
            }
        }
        #endregion

        #region TAB3
        public static void FillSheetWithDataForGalponAndSexo(ExcelWorksheet sheet, List<Dictionary<string, object>> data, List<string> lineaNames)
        {
            if (data == null || data.Count == 0 || lineaNames == null || lineaNames.Count == 0)
                return;

            int headerRow = 5;
            int dataStartRow = headerRow + 1;

            // Armar cabeceras
            var headers = new List<string> { "Galpón", "Sexo" };
            headers.AddRange(lineaNames);
            headers.Add("Total");

            //Estilos
            Color headerBgColor = ColorTranslator.FromHtml("#004C97");
            Color zebraColor = ColorTranslator.FromHtml("#F2F2F2");
            Color whiteBorder = Color.White;
            Color fontColor = Color.White;

            //Escribir cabecera
            for (int col = 0; col < headers.Count; col++)
            {
                var cell = sheet.Cells[headerRow, col + 1];
                cell.Value = headers[col];
                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(fontColor);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(headerBgColor);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Bordes blancos
                ExcelHelper.SetWhiteBorders(cell, whiteBorder);
            }

            //Escribir datos
            int rowIndex = dataStartRow;
            foreach (var item in data)
            {
                bool isEvenRow = (rowIndex - dataStartRow) % 2 == 0;
                Color bgColor = isEvenRow ? Color.White : zebraColor;

                // Galpón
                var cellGalpon = sheet.Cells[rowIndex, 1];
                cellGalpon.Value = item["galpon"];
                cellGalpon.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cellGalpon.Style.Fill.BackgroundColor.SetColor(bgColor);

                // Sexo
                var cellSexo = sheet.Cells[rowIndex, 2];
                cellSexo.Value = item["sexo"]?.ToString();
                cellSexo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cellSexo.Style.Fill.BackgroundColor.SetColor(bgColor);

                // Lineas (COBB, ROSS, etc.)
                var lineasElement = (JsonElement)item["lineas"];
                var lineas = JsonSerializer.Deserialize<List<double>>(lineasElement.GetRawText());

                int colIndex = 3;
                for (int i = 0; i < lineaNames.Count; i++)
                {
                    double valor = i < lineas.Count ? lineas[i] : 0;
                    var cell = sheet.Cells[rowIndex, colIndex];
                    cell.Value = valor;
                    cell.Style.Numberformat.Format = "#,##0";
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bgColor);
                    colIndex++;
                }

                // Total
                if (item.TryGetValue("total", out var totalVal) && totalVal is JsonElement totalElement)
                {
                    double total = totalElement.ValueKind == JsonValueKind.Number
                        ? totalElement.GetDouble()
                        : double.TryParse(totalElement.GetRawText(), out double parsed) ? parsed : 0;

                    var cell = sheet.Cells[rowIndex, colIndex];
                    cell.Value = total;
                    cell.Style.Numberformat.Format = "#,##0";
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bgColor);
                }

                rowIndex++;
            }

            double columnWidth = 20;

            for (int col = 1; col <= headers.Count; col++)
            {
                sheet.Column(col).Width = columnWidth;
            }
            sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        #endregion
    }
}
