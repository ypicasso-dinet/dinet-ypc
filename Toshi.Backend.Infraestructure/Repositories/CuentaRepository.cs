using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Cuenta.Commands.ChangePassword;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class CuentaRepository : RepositoryBase<UsuarioEntity>, ICuentaRepository
    {
        private readonly EncryptionService _encryption;

        public CuentaRepository(ToshiDBContext context, SessionStorage? sessionStorage, EncryptionService encryption) : base(context, sessionStorage)
        {
            _encryption = encryption;
        }

        public async Task<string> ChangePassword(ChangePasswordCommand request)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var usuario = await _context.Usuario.Where(w => w.id_usuario == ID_USUARIO).FirstOrDefaultAsync();

                ThrowTrue(usuario, "El usuario no se encuentra registrado");
                var pss_decrypt = _encryption.Decrypt(usuario!.pwd_usuario);
                ThrowTrue(pss_decrypt != request.old_password, "La contraseña actual es incorrecta");

                // Validación de 3 ultimas contraseñas
                var ultimas = await _context.Contrasenia
                    .Where(w => w.id_usuario == usuario!.id_usuario)
                    .OrderByDescending(o => o.fec_insert)
                    .Select(s => _encryption.Decrypt(s.pwd_usuario))
                    .Take(3)
                    .ToListAsync();

                ThrowTrue(ultimas.Contains(request.new_password!), "Debe ingresar una contraseña diferente a las 3 últimas usadas");

                usuario.pwd_usuario = _encryption.Encrypt(request.new_password!);
                usuario.ind_repassword = false;

                await UpdateAsync(usuario);

                var contrasenia = new ContraseniaEntity { id_usuario = usuario!.id_usuario, pwd_usuario = usuario!.pwd_usuario };

                await _context.Contrasenia.AddAsync(contrasenia);
                await _context.SaveChangesAsync();

                scope.Complete();
            }

            return "Contraseña actualizada satisfactoriamente";
        }
    }
}
