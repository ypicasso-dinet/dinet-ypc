using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Auth.Command.Retrieve;
using Toshi.Backend.Application.Features.Auth.Command.Signin;
using Toshi.Backend.Application.Features.Auth.Command.Signout;
using Toshi.Backend.Application.Models.Identity;
using Toshi.Backend.Domain.DTO.Auth;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class AuthRepository : RepositoryBase<UsuarioEntity>, IAuthRepository
    {
        private readonly JwtSettings _jwtSettings;
        private readonly EmailSettings _emailSettings;
        private readonly EncryptionService _encryption;

        public AuthRepository(
            ToshiDBContext context,
            IServiceScopeFactory factory,
            IOptions<JwtSettings> jwtSettings,
            IOptions<EmailSettings> emailSettings,
            EncryptionService encryption) : base(context, null)
        {
            _jwtSettings = jwtSettings.Value;
            _emailSettings = emailSettings.Value;
            _encryption = encryption;
        }

        public async Task<string> Retrieve(RetrieveCommand request)
        {
            var usuario = await _context.Usuario.Where(w => w.cod_usuario == request.cod_usuario).FirstOrDefaultAsync();

            ThrowTrue(usuario, "El usuario indicado no se encuentra registrado");

            var isEmail = false;
            var messageEmail = "";
            var messageOthers = "";

            try
            {
                if (usuario!.usu_email.IsValidEmail())
                {
                    var ocultos = await CorreosOcultos();

                    var password = _encryption.Decrypt(usuario!.pwd_usuario);
                    var values = usuario!.usu_email!.Split('@');
                    var parte1 = values[0].Substring(0, 3) + "".PadRight(values[0].Length - 3, '*');
                    var parte2 = values[1];
                    var emailBody = $"""
                    Hola {usuario.nom_usuario} {usuario.ape_paterno} {usuario.ape_materno},
                    <br />
                    Te enviamos la contraseña de tu Cuenta Toshi <b>{password}</b>.
                    <br /><br />
                    Saludos cordiales.
                    """;

                    var emailData = new EmailData()
                    {
                        title = "Solicitud de contraseña",
                        body = emailBody,
                        destinies = [usuario!.usu_email!],
                        hiddens = ocultos.Where(w => w.correo.IsValidEmail()).Select(s => s.correo).ToList(),
                        settings = _emailSettings
                    };

                    await EnviarCorreo(emailData);

                    messageEmail = $"Se ha enviado la información requerida al correo {parte1}@{parte2}";
                    isEmail = true;
                }
                else
                {
                    var solicitud = await _context.SolContrasenia.Where(w => w.id_usuario == usuario!.id_usuario).OrderByDescending(o => o.fec_insert).FirstOrDefaultAsync();

                    if (solicitud != null)
                    {
                        ThrowTrue(solicitud.ind_proceso == 0, "Usted tiene solicitud pendiente por revisión");
                    }

                    solicitud = new SolContraseniaEntity
                    {
                        id_usuario = usuario!.id_usuario,
                        ind_proceso = 0,
                        cod_estado = true,
                        fec_insert = NOW,
                        usu_insert = COD_USUARIO
                    };

                    await _context.SolContrasenia.AddAsync(solicitud);
                    await _context.SaveChangesAsync();

                    messageOthers = $"Se ha generado la solicitud de recuperación de contraseña N° {solicitud.id_sol_contrasenia}";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido procesar su solicitud. Intentelo en unos minutos.");
            }

            return isEmail ? messageEmail : messageOthers;
        }

        public async Task<AuthDTO> Signin(SigninCommand request)
        {
            var response = new AuthDTO();
            var usuario = await _context.Usuario.Where(w => w.cod_usuario == request.username /* && w.pwd_usuario == request.password */).FirstOrDefaultAsync();

            ThrowTrue(usuario, "Las credenciales son incorrectas");

            var password = _encryption.Decrypt(usuario!.pwd_usuario);

            ThrowTrue(password != request.password, "Las credenciales son incorrectas");


            var rol = await _context.Rol.Where(w => w.id_rol == usuario!.id_rol).FirstOrDefaultAsync();

            var token = GenerateToken(usuario!);

            response.id_usuario = usuario!.gid_usuario;
            response.cod_usuario = usuario!.cod_usuario;
            response.nom_usuario = usuario!.nom_usuario;
            response.ape_paterno = usuario!.ape_paterno;
            response.ape_materno = usuario!.ape_materno ?? "";
            response.email = usuario!.usu_email;
            response.cod_rol = rol?.cod_rol;
            response.nom_rol = rol?.nom_rol;

            response.tip_documento = usuario.tip_documento;
            response.num_documento = usuario.num_documento;
            response.num_telefono = usuario.num_telefono;
            response.fec_nacimiento = usuario.fec_nacimiento.ToSpanish();
            response.tip_usuario = usuario.tip_usuario;

            response.ind_repassword = usuario.ind_repassword;
            response.ind_turno = usuario.ind_turno;
            response.nom_turno = usuario.ind_turno.ToTurno();

            response.planteles = await _context.UsuarioPlantel
                .Where(w => w.id_usuario == usuario!.id_usuario)
                .Select(s => new AuthPlantelDTO
                {
                    id_plantel = s.plantel!.gid_plantel,
                    nom_plantel = s.plantel!.nom_plantel,
                    //ind_turno = s.ind_turno
                })
                .ToListAsync();

            response.nom_proveedor = (await _context.Proveedor.Where(w => w.id_proveedor == usuario.id_proveedor).FirstOrDefaultAsync())?.nom_proveedor;

            response.token = new JwtSecurityTokenHandler().WriteToken(token);

            var opciones = await (
                    from m in _context.Menu//.Where(w => w.cod_estado == true)
                    join rm in _context.RolMenu on m.id_menu equals rm.id_menu
                    where rm.id_rol == usuario!.id_rol
                    && m.id_padre != null
                    && rm.ind_read == true
                    && m.cod_estado == true
                    && rm.cod_estado == true
                    && (!request.GetIndicator() || m.ind_entorno == 2)
                    orderby m.ord_menu
                    select new
                    {
                        m.cod_menu,
                        m.tit_menu,
                        m.ico_menu,
                        m.url_menu,
                        m.id_padre,
                        m.ord_menu,
                        rm.ind_create,
                        rm.ind_read,
                        rm.ind_update,
                        rm.ind_delete,
                        rm.ind_all
                    }
                ).ToListAsync();

            var padres = opciones.Select(s => s.id_padre).ToList();

            var menus = await _context.Menu.Where(w => w.id_padre == null && padres.Contains(w.id_menu)).ToListAsync();

            response.menus = (
                from m in menus
                select new MenuPadreDTO
                {
                    tit_menu = m.tit_menu,
                    url_menu = m.url_menu,
                    ico_menu = m.ico_menu,
                    items = opciones
                        .Where(w => w.id_padre == m.id_menu)
                        .Select(s => new MenuHijoDTO
                        {
                            cod_menu = s.cod_menu,
                            tit_menu = s.tit_menu,
                            url_menu = s.url_menu,
                            ico_menu = s.ico_menu,
                            ind_create = s.ind_create,
                            ind_read = s.ind_read,
                            ind_update = s.ind_update,
                            ind_delete = s.ind_delete,
                            ind_all = s.ind_all,
                            ord_menu = s.ord_menu
                        })
                        .OrderBy(o => o.ord_menu)
                        .ToList()
                }
            ).ToList();

            return response;
        }

        public Task<bool> Signout(SignoutCommand request)
        {
            throw new NotImplementedException();
        }

        private JwtSecurityToken GenerateToken(UsuarioEntity usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.gid_usuario!),
                new Claim(JwtRegisteredClaimNames.GivenName, $"{usuario.nom_usuario} {usuario.ape_paterno ?? ""} {usuario.ape_materno ?? ""}".Trim()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.usu_email ?? ""),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.cod_usuario ?? ""),
                //------------------------------------------------------------------------------------------------------------------------------------
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: BaseDomainModel.GetNow().AddMinutes(_jwtSettings.MinDuration),
                signingCredentials: signinCredentials
            );

            return jwtSecurityToken;
        }

        public async Task<bool> IsActiveUser(string? id)
        {
            var usuario = await _context.Usuario.Where(w => w.gid_usuario == id).FirstOrDefaultAsync();

            return usuario?.cod_estado ?? false;
        }
    }
}
