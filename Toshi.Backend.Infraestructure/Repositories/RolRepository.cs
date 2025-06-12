using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Rol.Commands.Create;
using Toshi.Backend.Application.Features.Rol.Commands.Delete;
using Toshi.Backend.Application.Features.Rol.Commands.SaveAcciones;
using Toshi.Backend.Application.Features.Rol.Commands.Update;
using Toshi.Backend.Application.Features.Rol.Querys.GetAll;
using Toshi.Backend.Application.Features.Rol.Querys.GetById;
using Toshi.Backend.Application.Features.Rol.Querys.Opciones;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Rol;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class RolRepository : RepositoryBase<RolEntity>, IRolRepository
    {
        const string MENU_CODE = "FEAT-1200";

        public RolRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<string> Create(RolCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Rol.Where(w => w.cod_rol == request.cod_rol).FirstOrDefaultAsync();

                var existing = await _context.Rol.Where(w => w.cod_rol == request.cod_rol).FirstOrDefaultAsync();

                ThrowTrue(existing != null, "El código de rol ya se encuentra registrado");

                if (record == null)
                {
                    record = new RolEntity
                    {
                        cod_rol = request.cod_rol!.ToUpper(),
                        nom_rol = request.nom_rol!,
                        gid_rol = Guid.NewGuid().ToString()
                    };

                    await AddAsync(record);
                }
                else
                {
                    record.cod_rol = record.cod_rol.ToUpper();

                    await UpdateAsync(record);
                }

                scope.Complete();
            }

            return $"Rol {request.nom_rol} creado satisfactoriamente.";
        }

        public async Task<string> Delete(RolDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            var record = await _context.Rol.Where(w => w.gid_rol == request.id).FirstOrDefaultAsync();

            ThrowTrue(record, "El rol indicado no se encuentra habilitado");

            await DeleteAsync(record!);

            return "Rol eliminado satisfactoriamente";
        }

        public async Task<List<RolItemDTO>> GetAll(GetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var records = await _context.Rol
                .Where(w =>
                    (string.IsNullOrEmpty(request.cod_rol) || w.cod_rol.Contains(request.cod_rol)) &&
                    (string.IsNullOrEmpty(request.nom_rol) || w.nom_rol.Contains(request.nom_rol))
                )
                .OrderBy(o => o.nom_rol)
                .Select(s => new RolItemDTO()
                {
                    id = s.gid_rol,
                    cod_rol = s.cod_rol,
                    nom_rol = s.nom_rol,
                    nom_estado = s.cod_estado.ToStatus()
                }).ToListAsync();

            return records;
        }

        public async Task<RolDTO?> GetById(GetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.Rol
                .Where(w => w.gid_rol == request.id)
                .Select(s => new RolDTO()
                {
                    id = s.gid_rol,
                    cod_rol = s.cod_rol,
                    nom_rol = s.nom_rol,
                    nom_estado = s.cod_estado.ToStatus()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> Update(RolUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            var record = await _context.Rol.Where(w => w.gid_rol == request.id).FirstOrDefaultAsync();

            ThrowTrue(record == null, "El rol indicado no se encuentra habilitado");

            var existing = await _context.Rol.Where(w => w.cod_rol == request.cod_rol && w.gid_rol != request.id).FirstOrDefaultAsync();

            ThrowTrue(existing != null, "El código de rol ya se encuentra registrado");

            //record!.cod_rol = request!.cod_rol!.ToUpper();
            record!.nom_rol = request!.nom_rol!;

            await UpdateAsync(record!);

            return "Rol actualizado satisfactoriamente";
        }

        public async Task<List<RolMenuDTO>> Opciones(OpcionesQuery request)
        {
            var source = new List<RolMenuDTO>();

            var rol = await _context.Rol.Where(w => w.gid_rol == request.id).FirstOrDefaultAsync();

            ThrowTrue(rol, "El rol no se encuentra registrado");

            source = await (
                from m in _context.Menu.Where(w => w.cod_estado == true)
                join p in _context.Menu on m.id_padre equals p.id_menu
                join rm in _context.RolMenu.Where(w => w.id_rol == rol!.id_rol) on m.id_menu equals rm.id_menu into dmr
                from df in dmr.DefaultIfEmpty()
                orderby p.ord_menu, m.ord_menu
                select new RolMenuDTO
                {
                    cod_menu = m.cod_menu,
                    nom_menu = p.tit_menu,
                    tit_opcion = m.tit_menu,
                    ind_select = df == null ? false : df.ind_read,
                    ind_create = df == null ? false : df.ind_create,
                    ind_update = df == null ? false : df.ind_update,
                    ind_delete = df == null ? false : df.ind_delete,
                    ind_all = df == null ? false : df.ind_all,
                }
            ).ToListAsync();

            return source;
        }

        public async Task<string> SaveAcciones(RolSaveAccionesCommand request)
        {
            var rol = await _context.Rol.Where(w => w.gid_rol == request.id_rol).FirstOrDefaultAsync();

            ThrowTrue(rol, "El rol indicado no se encuentra habilitado");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var item in request.opciones!)
                {
                    var menu = await _context.Menu.Where(w => w.cod_menu == item.cod_menu).FirstOrDefaultAsync();

                    ThrowTrue(menu, "EL menu no se encuentra registrado");

                    var id_rol = rol!.id_rol;
                    var id_menu = menu!.id_menu;

                    var accion = await _context.RolMenu.Where(w => w.id_rol == id_rol && w.id_menu == id_menu).FirstOrDefaultAsync();

                    if (accion == null)
                    {
                        accion = new RolMenuEntity
                        {
                            id_rol = id_rol,
                            id_menu = id_menu,
                            //------------------------------
                            ind_create = item.ind_create,
                            ind_read = item.ind_select,
                            ind_update = item.ind_update,
                            ind_delete = item.ind_delete,
                            ind_all = item.ind_all,
                        };

                        await _context.RolMenu.AddAsync(accion);
                    }
                    else
                    {
                        accion.ind_create = item.ind_create;
                        accion.ind_read = item.ind_select;
                        accion.ind_update = item.ind_update;
                        accion.ind_delete = item.ind_delete;
                        accion.ind_all = item.ind_all;

                        _context.RolMenu.Update(accion);
                    }

                    await _context.SaveChangesAsync();
                }

                scope.Complete();
            }

            return "Acciones actualizadas satisfactoriamente";
        }
    }
}
