using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Data;
using System.Transactions;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Usuario.Commands.Create;
using Toshi.Backend.Application.Features.Usuario.Commands.Delete;
using Toshi.Backend.Application.Features.Usuario.Commands.DeleteLicencia;
using Toshi.Backend.Application.Features.Usuario.Commands.Update;
using Toshi.Backend.Application.Features.Usuario.Commands.UpsertLicencia;
using Toshi.Backend.Application.Features.Usuario.Querys.GetAll;
using Toshi.Backend.Application.Features.Usuario.Querys.GetById;
using Toshi.Backend.Application.Features.Usuario.Querys.ScreenParams;
using Toshi.Backend.Application.Features.Usuario.Querys.ScreenParamsLic;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Rol;
using Toshi.Backend.Domain.DTO.Usuario;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class UsuarioRepository : RepositoryBase<UsuarioEntity>, IUsuarioRepository
    {
        const string MENU_CODE = "FEAT-1300";
        const string EMAIL_REGEX = "^[a-zA-Z0-9._%+-]+@gtoshi\\.com$\r\n";

        private readonly EmailSettings emailSettings;
        private readonly EncryptionService _encryption;

        public UsuarioRepository(
            ToshiDBContext context,
            SessionStorage sessionStorage,
            IOptions<EmailSettings> emailSettings,
            EncryptionService encryption) : base(context, sessionStorage)
        {
            this.emailSettings = emailSettings.Value;
            this._encryption = encryption;
        }

        public async Task<UsuarioScreenParamsDTO> ScreenParams(ScreenParamsQuery request)
        {
            var source = new UsuarioScreenParamsDTO();

            if (request.upsert == true)
            {
                source.tipos_documento = await GetConfigs("TIPDOC");
                source.tipos_usuario = await GetConfigs("TIPUSU");

                source.roles = await _context.Rol
                    .Where(w => w.cod_estado == true)
                    .OrderBy(o => o.nom_rol)
                    .Select(s => new RolItemDTO
                    {
                        id = s.gid_rol,
                        cod_rol = s.cod_rol,
                        nom_rol = s.nom_rol
                    }).ToListAsync();

                source.planteles = await _context.Plantel.Where(w => w.cod_estado == true).OrderBy(o => o.nom_plantel).Select(s => new UsuarioPlantelDTO { id_plantel = s.gid_plantel, nom_plantel = s.nom_plantel }).ToListAsync();
                source.proveedores = await _context.Proveedor.Where(w => w.cod_estado == true).OrderBy(o => o.nom_proveedor).Select(s => new CodeTextDTO(s.gid_proveedor, s.nom_proveedor)).ToListAsync();
            }
            else
            {
                source.estados = await GetConfigs("ESTADOS");

                source.planteles = await _context.Plantel
                    .Where(w => w.cod_estado == true)
                    .OrderBy(o => o.nom_plantel)
                    .Select(s => new UsuarioPlantelDTO
                    {
                        id_plantel = s.gid_plantel,
                        nom_plantel = s.nom_plantel
                    })
                    .ToListAsync();
            }

            return source;
        }

        public async Task<List<UsuarioItemDTO>> GetAll(GetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var source = await (
                from u in _context.Usuario
                join t in _context.ConfiguracionDetalle.Where(w => w.configuracion!.cod_config == "TIPDOC" && w.cod_estado == true) on u.tip_documento equals t.cod_detalle into it
                from dt in it.DefaultIfEmpty()
                where
                    (string.IsNullOrEmpty(request.cod_usuario) || u.cod_usuario.Contains(request.cod_usuario)) &&
                    (string.IsNullOrEmpty(request.nom_usuario) || u.nom_usuario.Contains(request.nom_usuario)) &&
                    (string.IsNullOrEmpty(request.num_documento) || u.num_documento.Contains(request.num_documento)) &&
                    (string.IsNullOrEmpty(request.cod_estado) || u.cod_estado == request.cod_estado!.ToBool()) &&
                    (string.IsNullOrEmpty(request.id_plantel) || u.usuario_planteles.Any(a => a.cod_estado == true && a.plantel.gid_plantel == request.id_plantel)) &&
                    u.tip_usuario != "PROV"
                select new UsuarioItemDTO
                {
                    id_usuario = u.gid_usuario,
                    //-----------------------------------
                    nom_rol = u.rol!.nom_rol,
                    cod_usuario = u.cod_usuario,
                    nom_usuario = u.nom_usuario,
                    ape_paterno = u.ape_paterno,
                    ape_materno = u.ape_materno,
                    tip_documento = dt == null ? null : dt.nom_detalle,
                    num_documento = u.num_documento,
                    usu_email = u.usu_email,
                    num_telefono = u.num_telefono,
                    nom_estado = u.cod_estado.ToStatus(),
                    nom_plantel = string.Join(", ", u.usuario_planteles.Where(w => w.plantel.cod_estado == true).OrderBy(s => s.plantel.nom_plantel).Select(s => s.plantel.nom_plantel))
                })
                .ToListAsync();

            return source;
        }

        public async Task<UsuarioDTO?> GetById(GetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var source = await (
                from u in _context.Usuario
                join t in _context.ConfiguracionDetalle.Where(w => w.configuracion!.cod_config == "TIPDOC" && w.cod_estado == true) on u.tip_documento equals t.cod_detalle into it
                from dt in it.DefaultIfEmpty()

                where u.gid_usuario == request.id
                select new UsuarioDTO
                {
                    id_usuario = u.gid_usuario,
                    cod_usuario = u.cod_usuario,
                    nom_usuario = u.nom_usuario,
                    ape_paterno = u.ape_paterno,
                    ape_materno = u.ape_materno,
                    fec_nacimiento = u.fec_nacimiento.ToSpanish(),
                    date_nacimiento = u.fec_nacimiento.ToDate(),
                    tip_documento = u.tip_documento,
                    num_documento = u.num_documento,
                    usu_email = u.usu_email,
                    num_telefono = u.num_telefono,
                    fec_cese = u.fec_cese.ToSpanish(),
                    obs_cese = u.obs_cese,
                    id_rol = u.rol!.gid_rol,
                    tip_usuario = u.tip_usuario,
                    id_proveedor = u.proveedor == null ? null : u.proveedor!.gid_proveedor,
                    //nom_proveedor = u.proveedor == null ? null : u.proveedor!.nom_proveedor,
                    //---------------------------------------------------------------------------
                    //nom_rol = u.rol!.nom_rol,
                    nom_estado = u.cod_estado.ToStatus(),
                    ind_turno = u.ind_turno,
                    licencias = u.licencias.Where(w => w.cod_estado == true).Select(l => new UsuarioLicenciaDTO
                    {
                        id_licencia = l.gid_licencia,
                        tip_licencia = l.tip_licencia,
                        fec_desde = l.fec_desde.ToSpanish(),
                        fec_hasta = l.fec_hasta.ToSpanish(),
                        obs_licencia = l.obs_licencia,
                        //num_dias = (l.fec_hasta - l.fec_desde).Days,
                        num_dias = (int)l.fec_hasta.Date.Subtract(l.fec_desde.Date).Days + 1,
                        usu_insert = l.usu_insert,
                        fec_insert = l.fec_insert.ToSpanish()
                    }).ToList(),
                    planteles = (
                        from xp in _context.Plantel.Where(w => w.cod_estado == true)
                        join xup in u.usuario_planteles.Where(w => w.cod_estado == true) on xp.id_plantel equals xup.id_plantel into yup
                        from tup in yup.DefaultIfEmpty()
                        orderby xp.nom_plantel
                        select new UsuarioPlantelDTO
                        {
                            id_plantel = xp.gid_plantel,
                            nom_plantel = xp.nom_plantel,
                            //ind_turno = tup == null ? false : tup.ind_turno,
                            selected = tup != null
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return source;
        }

        public async Task<UsuarioCreateResponseDTO> Create(UsuarioCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            var new_id_usuario = string.Empty;
            var new_password = string.Empty;
            var usuario = new UsuarioDTO();
            var ocultos = new List<Destinatario>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Usuario.Where(w => w.cod_usuario == request.cod_usuario).FirstOrDefaultAsync();

                ThrowTrue(record != null && record.cod_estado == true, "El código de usuario ya se encuentra registrado");


                var rol = await _context.Rol.Where(w => w.gid_rol == request.id_rol).FirstOrDefaultAsync();

                ThrowTrue(rol, "El rol indicado no se encuentra registrado");

                var proveedor = await _context.Proveedor.Where(w => w.gid_proveedor == request.id_proveedor).FirstOrDefaultAsync();


                var tipdoc = await _context.ConfiguracionDetalle.Where(w => w.configuracion!.cod_config == "TIPDOC" && w.configuracion.cod_estado == true && w.cod_detalle == request.tip_documento && w.cod_estado == true).FirstOrDefaultAsync();
                var tipusu = await _context.ConfiguracionDetalle.Where(w => w.configuracion!.cod_config == "TIPUSU" && w.configuracion.cod_estado == true && w.cod_detalle == request.tip_usuario && w.cod_estado == true).FirstOrDefaultAsync();

                ThrowTrue(tipdoc, "El tipo de documento no se encuentra registrado");
                ThrowTrue(tipusu, "El tipo de usuario no se encuentra registrado");

                var append = record == null;

                if (append)
                {
                    record = new UsuarioEntity() { gid_usuario = Guid.NewGuid().ToString() };
                }

                record!.cod_usuario = request!.cod_usuario!;
                record!.nom_usuario = request!.nom_usuario!;
                record!.ape_paterno = request!.ape_paterno!;
                record!.ape_materno = request!.ape_materno;
                record!.fec_nacimiento = request!.fec_nacimiento.ToDate();
                record!.tip_documento = request!.tip_documento!;
                record!.num_documento = request!.num_documento!;
                record!.usu_email = request!.usu_email;
                record!.num_telefono = request!.num_telefono;
                record!.ind_turno = request.ind_turno!.Value;
                record!.ind_repassword = true;

                record!.fec_cese = null;
                record!.obs_cese = null;
                record!.id_rol = rol!.id_rol;
                record!.tip_usuario = request!.tip_usuario!;
                record!.id_proveedor = !string.Equals("PROV", request.tip_usuario, StringComparison.OrdinalIgnoreCase) ? null : proveedor!.id_proveedor;

                var password = GenerarPassword(6);

                record!.pwd_usuario = _encryption.Encrypt(password);

                if (append)
                    await AddAsync(record!);
                else
                    await UpdateAsync(record!);

                await ProcessPlanteles(record!, request.planteles!);


                var contrasenia = new ContraseniaEntity { id_usuario = record!.id_usuario, pwd_usuario = record!.pwd_usuario };

                await _context.Contrasenia.AddAsync(contrasenia);


                ocultos = await CorreosOcultos();

                scope.Complete();

                new_id_usuario = record!.gid_usuario;
                new_password = password;// record!.pwd_usuario;

                usuario.nom_usuario = $"{record.nom_usuario} {record.ape_paterno} {record.ape_materno}".Trim();
                usuario.cod_usuario = record!.cod_usuario;
                usuario.usu_email = record!.usu_email;
                usuario.pwd_usuario = new_password;
            }

            await EnviarCorreoNuevoUsuario(usuario);

            return new UsuarioCreateResponseDTO { message = "Usuario registrado satisfactoriamente", id = new_id_usuario };
        }

        public async Task<string> Update(UsuarioUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            UsuarioDTO? usuario = null;
            string? password = null;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Usuario.Where(w => w.gid_usuario == request.id_usuario).FirstOrDefaultAsync();

                ThrowTrue(record == null, "El usuario no se encuentra registrado");


                var existente = await _context.Usuario.Where(w => w.cod_usuario == request.cod_usuario && w.id_usuario != record!.id_usuario).FirstOrDefaultAsync();

                ThrowTrue(existente != null && existente.cod_estado == true, "El código de usuario ya se encuentra registrado");


                var rol = await _context.Rol.Where(w => w.gid_rol == request.id_rol).FirstOrDefaultAsync();

                ThrowTrue(rol, "El rol indicado no se encuentra registrado");

                var proveedor = await _context.Proveedor.Where(w => w.gid_proveedor == request.id_proveedor).FirstOrDefaultAsync();


                var tipdoc = await _context.ConfiguracionDetalle.Where(w => w.configuracion!.cod_config == "TIPDOC" && w.configuracion.cod_estado == true && w.cod_detalle == request.tip_documento && w.cod_estado == true).FirstOrDefaultAsync();
                var tipusu = await _context.ConfiguracionDetalle.Where(w => w.configuracion!.cod_config == "TIPUSU" && w.configuracion.cod_estado == true && w.cod_detalle == request.tip_usuario && w.cod_estado == true).FirstOrDefaultAsync();

                ThrowTrue(tipdoc, "El tipo de documento no se encuentra registrado");
                ThrowTrue(tipusu, "El tipo de usuario no se encuentra registrado");

                var enviarCorreo = record.cod_estado == false;

                //record!.cod_usuario = request!.cod_usuario!;
                record!.nom_usuario = request!.nom_usuario!;
                record!.ape_paterno = request!.ape_paterno!;
                record!.ape_materno = request!.ape_materno;
                record!.fec_nacimiento = request!.fec_nacimiento.ToDate();
                record!.tip_documento = request!.tip_documento!;
                record!.num_documento = request!.num_documento!;
                record!.usu_email = request!.usu_email;
                record!.num_telefono = request!.num_telefono;
                record!.ind_turno = request.ind_turno!.Value;

                record!.fec_cese = null;
                record!.obs_cese = null;
                record!.id_rol = rol!.id_rol;
                record!.tip_usuario = request!.tip_usuario!;
                record!.id_proveedor = !string.Equals("PROV", request.tip_usuario, StringComparison.OrdinalIgnoreCase) ? null : proveedor!.id_proveedor;

                if (enviarCorreo)
                {
                    password = GenerarPassword(6);

                    record!.pwd_usuario = _encryption.Encrypt(password);

                    var contrasenia = new ContraseniaEntity { id_usuario = record!.id_usuario, pwd_usuario = record!.pwd_usuario };

                    await _context.Contrasenia.AddAsync(contrasenia);
                }

                await UpdateAsync(record!);
                await ProcessPlanteles(record!, request.planteles!);

                scope.Complete();

                if (enviarCorreo)
                {
                    usuario = new UsuarioDTO
                    {
                        nom_usuario = $"{record.nom_usuario} {record.ape_paterno} {record.ape_materno}".Trim(),
                        cod_usuario = record!.cod_usuario,
                        usu_email = record!.usu_email,
                        pwd_usuario = password,// record.pwd_usuario,
                    };
                }
            }

            if (usuario != null)
            {
                await EnviarCorreoNuevoUsuario(usuario);
            }

            return "Usuario actualizado satisfactoriamente";
        }

        private async Task EnviarCorreoNuevoUsuario(UsuarioDTO usuario)
        {
            var ocultos = new List<Destinatario>();

            try
            {
                ocultos = await CorreosOcultos();
            }
            catch (Exception ex)
            {
            }

            // Envío de correo
            try
            {
                var emailData = new EmailData { settings = emailSettings };

                emailData.SetEmails(ocultos);

                emailData.title = $"SPC - Granja Toshi - Contraseña de Nuevo Usuario - {usuario.nom_usuario}";

                if (string.IsNullOrEmpty(usuario.usu_email))
                {
                    // Envío de correo al generador del usuario
                    emailData.body = $"""
                        <div style="{BOX_STYLE}">
                            Hola {NOM_USUARIO},
                            <br />
                            <br />
                            Te envíamos la contraseña del nuevo usuario que has creado para el uso del sistema.
                            <br />
                            Las credenciales son las siguientes:
                            <br />
                            <br />
                            <b>Usuario: </b>{usuario.nom_usuario}<br />
                            <b>Código: </b>{usuario.cod_usuario}<br />
                            <b>Contraseña: </b>{usuario.pwd_usuario}<br />
                            <br />
                            Saludos cordiales.
                        </div>
                        """;

                    if (USU_EMAIL.IsValidEmail()) emailData.destinies.Add(USU_EMAIL!);
                }
                else
                {
                    // Envío de correo al mismo usuario
                    emailData.body = $"""
                        <div style="{BOX_STYLE}">
                            Hola {usuario.nom_usuario},
                            <br />
                            <br />
                            Se te ha generado un usuario para el uso del sistema SPC de Granja Toshi.
                            <br />
                            Te brindamos las credenciales para el acceso al sistema:
                            <br />
                            <br />
                            <b>Código: </b>{usuario.cod_usuario}<br />
                            <b>Contraseña: </b>{usuario.pwd_usuario}<br />
                            <br />
                            Saludos cordiales.
                        </div>
                        """;

                    if (usuario.usu_email.IsValidEmail()) emailData.destinies.Add(usuario.usu_email!);
                }

                //
                await EnviarCorreo(emailData);
            }
            catch (Exception ex)
            {
                //
                Log.Error(ex.Message);

                Exception? inner = ex.InnerException;

                while (inner != null)
                {
                    Log.Error(inner.Message);

                    inner = inner.InnerException;
                }
            }
        }

        private async Task ProcessPlanteles(UsuarioEntity usuario, List<UsuarioPlantelDTO> planteles)
        {
            var userPlanteles = await (
                from p in _context.Plantel
                join up in _context.UsuarioPlantel.Where(w => w.id_usuario == usuario.id_usuario) on p.id_plantel equals up.id_plantel into iup
                from dup in iup.DefaultIfEmpty()
                where p.cod_estado == true
                select new
                {
                    p.id_plantel,
                    p.gid_plantel,
                    usuario_plantel = dup
                }).ToListAsync();

            var es_galponero = usuario.rol!.cod_rol!.Equals("GALP");

            foreach (var item in userPlanteles)
            {
                var rp = planteles.FirstOrDefault(w => w.id_plantel == item.gid_plantel);

                if (rp == null)
                {
                    if (item.usuario_plantel != null && item.usuario_plantel.cod_estado == true)
                    {
                        _context.Entry(item.usuario_plantel).State = EntityState.Deleted;

                        await _context.SaveChangesAsync();
                    }

                    continue;
                }

                if (item.usuario_plantel == null)
                {
                    // nuevo registro
                    var usuario_plantel = new UsuarioPlantelEntity
                    {
                        id_usuario = usuario.id_usuario,
                        id_plantel = item.id_plantel,
                        //ind_turno = es_galponero ? rp.ind_turno : null
                    };

                    await _context.UsuarioPlantel.AddAsync(usuario_plantel);
                }
                else // if (item.usuario_plantel!.cod_estado == false || item.usuario_plantel!.ind_turno != rp.ind_turno)
                {
                    // item.usuario_plantel!.ind_turno = es_galponero ? rp.ind_turno : null;

                    _context.UsuarioPlantel.Update(item.usuario_plantel!);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> Delete(UsuarioDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var entidad = await _context.Usuario.Where(w => w.gid_usuario == request.id).FirstOrDefaultAsync();

                ThrowTrue(entidad, "El usuario no se encuentra registrado");

                entidad!.fec_cese = NOW;

                await DeleteAsync(entidad!);

                scope.Complete();
            }

            return "Usuario eliminado satisfactoriamente";
        }

        public async Task<List<CodeTextDTO>> ScreenParamsLic(ScreenParamsLicQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            return await GetConfigs("TIPLIC");
        }

        public async Task<UsuarioLicenciaResponseDTO> UpsertLicencia(UpsertLicenciaCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            var licencia = (UsuarioLicenciaDTO?)null;
            var extra = "registrada";

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                LicenciaEntity? record = null;
                bool append = false;

                var usuario = await _context.Usuario.Where(w => w.gid_usuario == request.id_usuario).FirstOrDefaultAsync();

                ThrowTrue(usuario == null, "El usuario indicado no se encuentra registrado");

                if (!string.IsNullOrEmpty(request.id_licencia))
                {
                    record = await _context.Licencia.Where(w => w.gid_licencia == request.id_licencia).FirstOrDefaultAsync();

                    ThrowTrue(record, "La licencia indicada no se encuentra registrada");
                }
                else
                {
                    append = true;
                    record = new LicenciaEntity()
                    {
                        gid_licencia = Guid.NewGuid().ToString(),
                        id_usuario = usuario!.id_usuario
                    };
                }

                record!.tip_licencia = request!.tip_licencia!;
                record!.fec_desde = request!.fec_desde!.ToDate()!.Value;
                record!.fec_hasta = request!.fec_hasta!.ToDate()!.Value;
                record!.obs_licencia = request!.obs_licencia;

                if (append)
                {
                    await _context.Licencia.AddAsync(record!);
                }
                else
                {
                    _context.Licencia.Update(record!);
                }

                await _context.SaveChangesAsync();

                scope.Complete();

                licencia = new UsuarioLicenciaDTO
                {
                    id_licencia = record!.gid_licencia,
                    tip_licencia = record!.tip_licencia,
                    fec_desde = record!.fec_desde.ToSpanish(),
                    fec_hasta = record!.fec_hasta.ToSpanish(),
                    obs_licencia = record!.obs_licencia,
                    usu_insert = record!.usu_insert,
                    fec_insert = record!.fec_insert.ToSpanish(),
                    num_dias = (int)record!.fec_hasta.Date.Subtract(record!.fec_desde.Date).Days + 1,
                };

                if (!append) extra = "actualizada";
            }

            return new UsuarioLicenciaResponseDTO { licencia = licencia, message = $"La licencia ha sido {extra} satisfactoriamente" };
        }

        public async Task<string> DeleteLicencia(UsuarioDeleteLicenciaCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var usuario = await _context.Usuario.Where(w => w.gid_usuario == request.id_usuario).FirstOrDefaultAsync();

                ThrowTrue(usuario, "El usuario no se encuentra registrado");

                var entidad = await _context.Licencia.Where(w => w.id_usuario == usuario!.id_usuario && w.gid_licencia == request.id_licencia).FirstOrDefaultAsync();

                ThrowTrue(entidad, "La licencia no se encuentra registrada");

                _context.Entry(entidad!).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                scope.Complete();
            }

            return "La licencia ha sido eliminada satisfactoriamente";
        }
    }
}
