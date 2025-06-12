using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected const string BOX_STYLE = "box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;background-color: #EDEEF0;margin: 50px;padding: 30px;border-radius: 15px;";

        protected readonly ToshiDBContext _context;
        private readonly SessionStorage? _sessionStorage;

        protected readonly int? ID_USUARIO;
        protected readonly int? ID_ROL;
        protected readonly string? COD_USUARIO;
        protected readonly string? NOM_USUARIO;
        protected readonly string? USU_EMAIL;
        protected readonly string? COD_ROL;
        protected readonly bool ES_MOBILE;

        public RepositoryBase(ToshiDBContext context, SessionStorage? sessionStorage)
        {
            _context = context;
            _sessionStorage = sessionStorage;

            if (sessionStorage != null)
            {
                var gid_usuario = sessionStorage?.GetUser()?.IdUsuario;

                var userInfo = context.Usuario.FirstOrDefault(w => w.gid_usuario == gid_usuario);

                if (userInfo != null)
                {
                    var rolInfo = context.Rol.FirstOrDefault(w => w.id_rol == userInfo!.id_rol);

                    ID_USUARIO = userInfo?.id_usuario;
                    COD_USUARIO = userInfo?.cod_usuario;
                    NOM_USUARIO = $"{userInfo?.nom_usuario ?? ""} {userInfo?.ape_paterno ?? ""} {userInfo?.ape_materno ?? ""}".Trim();
                    USU_EMAIL = userInfo?.usu_email;
                    COD_ROL = rolInfo?.cod_rol;
                    ID_ROL = userInfo?.id_rol;
                    ES_MOBILE = _sessionStorage?.GetUser()?.EsMobile ?? false;
                }
            }
        }

        public static DateTime NOW
        {
            get
            {
                return BaseDomainModel.GetNow();
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.Set<T>().Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task DisableAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public void ThrowTrue(bool condition, string message)
        {
            if (condition)
            {
                throw new Exception(message);
            }
        }

        public void ThrowTrue(BaseDomainModel? model, string message)
        {
            ThrowTrue(!(model?.cod_estado ?? false), message);
        }

        public void ThrowActive(BaseDomainModel? model, string message)
        {
            ThrowTrue(model != null && model!.cod_estado == true, message);
        }

        public async Task<bool> ValidateAction(string cod_menu, ActionRol action)
        {
            var entity = await (
                from m in _context.Menu
                join rm in _context.RolMenu on m.id_menu equals rm.id_menu
                where rm.id_rol == ID_ROL
                && m.cod_menu == cod_menu
                && m.cod_estado == true
                && rm.cod_estado == true
                select rm
            ).FirstOrDefaultAsync();

            var allow_process = action switch
            {
                ActionRol.Create => entity?.ind_create,
                ActionRol.Read => entity?.ind_read,
                ActionRol.Update => entity?.ind_update,
                ActionRol.Delete => entity?.ind_delete,
                _ => null
            } ?? false;

            var extra = action switch
            {
                ActionRol.Create => "la creación",
                ActionRol.Read => "la lectura de datos",
                ActionRol.Update => "la actualización",
                ActionRol.Delete => "la eliminación",
                _ => "la acción"
            };

            ThrowTrue(!allow_process, $"No esta permitido realizar {extra} para este proceso");

            return entity?.ind_all ?? false;
        }

        public async Task<bool> IsValidAction(string cod_menu, ActionRol action)
        {
            try
            {
                await ValidateAction(cod_menu, action);

                return true;
            }
            catch
            {
                return false;

            }
        }

        public async Task<List<CodeTextDTO>> GetConfigs(string cod_config)
        {
            var items = await GetConfigsBD(cod_config);

            return items.Select(s => new CodeTextDTO(s.cod_detalle, s.nom_detalle)).ToList();
        }

        public async Task<List<ConfiguracionDetalleEntity>> GetConfigsBD(string cod_config)
        {
            var items = await (
              from c in _context.Configuracion
              join d in _context.ConfiguracionDetalle on c.id_config equals d.id_config
              where c.cod_config == cod_config && c.cod_estado == true && d.cod_estado == true
              orderby d.ord_config_det, d.nom_detalle
              select d).ToListAsync();

            if (cod_config == "TIPUSU" && items != null)
            {
                var item = items.FirstOrDefault(w => w.cod_detalle == "PROV");

                if (item != null) items!.Remove(item);
            }

            return items ?? [];
        }

        protected static string GenerarPassword(int longitud)
        {
            if (longitud < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");

            const string numeros = "0123456789";
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string especiales = "!@#$%^&*()-_=+<>?";

            // Garantizar que la contraseña contenga al menos un carácter de cada categoría
            char[] password = new char[longitud];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            password[0] = numeros[GetRandomInt(rng, numeros.Length)];
            password[1] = mayusculas[GetRandomInt(rng, mayusculas.Length)];
            password[2] = minusculas[GetRandomInt(rng, minusculas.Length)];
            password[3] = especiales[GetRandomInt(rng, especiales.Length)];

            // Llenar el resto de la contraseña con caracteres aleatorios de todos los conjuntos combinados
            string todos = numeros + mayusculas + minusculas + especiales;

            for (int i = 4; i < longitud; i++)
            {
                password[i] = todos[GetRandomInt(rng, todos.Length)];
            }

            // Mezclar los caracteres aleatoriamente para evitar que los primeros siempre sean de cada categoría
            return new string(password.OrderBy(_ => GetRandomInt(rng, longitud)).ToArray());
        }

        private static int GetRandomInt(RandomNumberGenerator rng, int max)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0) & int.MaxValue % max;
        }

        protected async Task<List<Destinatario>> CorreosOcultos()
        {
            var ocultos = await _context.ConfiguracionDetalle
                .Where(w => w.configuracion.cod_config == "OCULTOS" && w.cod_estado == true)
                .Select(s => new Destinatario { destino = s.des_email!, correo = s.val_email! })
                .ToListAsync();

            return ocultos;
        }

        protected async Task EnviarCorreo(EmailData data)
        {
            EmailSettings settings = data.settings!;

            // Envío de correo
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(settings.FromEmail, settings.DisplayName);
                message.Body = data.body;
                message.Subject = data.title;
                message.IsBodyHtml = true;

                foreach (var email in data.GetDestinatarios()) message.To.Add(email);
                foreach (var email in data.GetCopias()) message.CC.Add(email);
                foreach (var email in data.GetOcultos()) message.Bcc.Add(email);
                foreach (var archivo in data.GetArchivos()) message.Attachments.Add(new Attachment(archivo));

                SmtpClient smtp = new SmtpClient(settings.HostName, settings.PortNumber);

                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = settings.EnableSSL;
                smtp.Credentials = new NetworkCredential
                {
                    UserName = settings.UserName,
                    Password = settings.Password
                };

                await smtp.SendMailAsync(message);
            }
        }

        public async Task AddEntityAsync<D>(D entity) where D : BaseDomainModel
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.Set<D>().Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntityAsync<D>(D entity) where D : BaseDomainModel
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<D>().Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync<D>(D entity) where D : BaseDomainModel
        {
            _context.Entry(entity).State = EntityState.Deleted;
            // _context.Set<D>().Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> EnviadoDiario(int? id_usuario, string? gid_plantel, DateTime fecha)
        {
            var codigos = await CodigosPlantas.ObtenerPlanteles(_context, id_usuario, gid_plantel);
            var item = await _context.EnvioDiario.Where(w => codigos.Contains(w.campania!.id_plantel) && w.fec_envio.Date == fecha.Date).FirstOrDefaultAsync();

            return item?.ind_enviado ?? false;
        }

        protected string NewGuid() => Guid.NewGuid().ToString();

        public async Task<List<int>> GetPlanteles()
        {
            return await _context.UsuarioPlantel.GetActives().Where(w => w.id_usuario == ID_USUARIO).Select(s => s.id_plantel).ToListAsync();
        }
    }
}
