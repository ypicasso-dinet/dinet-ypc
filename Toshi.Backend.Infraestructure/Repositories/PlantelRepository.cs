using Azure;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Transactions;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Plantel.Commands.Create;
using Toshi.Backend.Application.Features.Plantel.Commands.Delete;
using Toshi.Backend.Application.Features.Plantel.Commands.Update;
using Toshi.Backend.Application.Features.Plantel.Commands.UserAppend;
using Toshi.Backend.Application.Features.Plantel.Commands.UserDelete;
using Toshi.Backend.Application.Features.Plantel.Querys.GetAll;
using Toshi.Backend.Application.Features.Plantel.Querys.GetById;
using Toshi.Backend.Application.Features.Plantel.Querys.ListParams;
using Toshi.Backend.Application.Features.Plantel.Querys.UpsertParams;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Plantel;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class PlantelRepository : RepositoryBase<PlantelEntity>, IPlantelRepository
    {
        const string MENU_CODE = "FEAT-1700";

        public PlantelRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<List<PlantelItemDTO>> GetAll(PlantelGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var roles = await _context.Rol.Where(w => w.cod_estado == true).ToListAsync();

            var planteles = await _context.Plantel
                .Where(w =>
                    (string.IsNullOrEmpty(request.cod_plantel) || w.cod_plantel.Contains(request.cod_plantel)) &&
                    (string.IsNullOrEmpty(request.nom_plantel) || w.nom_plantel.Contains(request.nom_plantel)) &&
                    (string.IsNullOrEmpty(request.cod_estado) || w.cod_estado == request.cod_estado.ToBool())
                ).ToListAsync();


            var records = new List<PlantelItemDTO>();

            foreach (var item in planteles)
            {
                var data = new PlantelItemDTO();

                data.Add("id_plantel", item.gid_plantel);
                data.Add("cod_plantel", item.cod_plantel);
                data.Add("nom_plantel", item.nom_plantel);

                foreach (var rol in roles)
                {
                    var usuarios = await _context.UsuarioPlantel.Where(w => w.id_plantel == item.id_plantel && w.usuario.id_rol == rol.id_rol && w.cod_estado == true).ToListAsync();

                    data.Add(rol.cod_rol, usuarios.Count());
                }

                data.Add("cod_estado", item.cod_estado);
                data.Add("nom_estado", item.cod_estado.ToStatus());
                data.Add("tip_plantel", item.ind_interno == true ? "Plantel Interno" : "Plantel Externo");

                records.Add(data);
            }

            return records;
        }

        public async Task<PlantelDTO?> GetById(PlantelGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var record = await _context.Plantel
                .Where(w => w.gid_plantel == request.id)
                .Select(s => new PlantelDTO()
                {
                    // Specifying field's    
                    id_plantel = s.gid_plantel,
                    cod_plantel = s.cod_plantel,
                    nom_plantel = s.nom_plantel,
                    nom_estado = s.cod_estado.ToStatus(),
                    ind_interno = s.ind_interno,
                    roles = _context.Rol.Where(w => w.cod_estado == true && w.cod_rol != "SADMIN").OrderBy(o => o.nom_rol).Select(r => new PlantelRolDTO
                    {
                        id_rol = r.gid_rol,
                        cod_rol = r.cod_rol,
                        nom_rol = r.nom_rol,
                        usuarios = _context.UsuarioPlantel
                            .Where(w => w.id_plantel == s.id_plantel && w.usuario.rol.cod_rol == r.cod_rol && w.cod_estado == true)
                            .Select(u => new PlantelUsuarioDTO
                            {
                                id_usuario = u.usuario.gid_usuario,
                                nom_usuario = $"{u.usuario.nom_usuario} {u.usuario.ape_paterno} {u.usuario.ape_materno}".Trim(),
                                tip_documento = u.usuario.tip_documento,
                                num_documento = u.usuario.num_documento,
                                //ind_turno = u.ind_turno,
                                nom_turno = u.usuario.ind_turno.ToTurno(),
                                tip_usuario = u.usuario.tip_usuario,
                                nom_proveedor = u.usuario.proveedor == null ? "" : u.usuario.proveedor.nom_proveedor,
                                ruc_proveedor = u.usuario.proveedor == null ? "" : u.usuario.proveedor.ruc_proveedor,
                                cod_estado = u.cod_estado,
                                nom_estado = u.cod_estado.ToStatus()
                            }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<StatusResponse> Create(PlantelCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            var gid = string.Empty;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Plantel.FirstOrDefaultAsync(w => w.cod_plantel == request.cod_plantel);

                ThrowActive(record, "El código ingresado ya se encuentra registrado");

                var append = record == null;

                if (append) { record = new PlantelEntity { gid_plantel = Guid.NewGuid().ToString() }; }

                record!.cod_plantel = request.cod_plantel!;
                record!.nom_plantel = request.nom_plantel!;

                if (append)
                    await AddAsync(record);
                else
                    await UpdateAsync(record);

                scope.Complete();

                gid = record.gid_plantel;
            }

            return new StatusResponse("Plantel creado satisfactoriamente.", gid);
        }

        public async Task<string> Update(PlantelUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Plantel.FirstOrDefaultAsync(w => w.gid_plantel == request.id_plantel);

                ThrowTrue(record == null, "El plantel indicado no se encuentra registrado");

                var existente = await _context.Plantel.FirstOrDefaultAsync(w => w.cod_plantel == request.cod_plantel && w.id_plantel != record!.id_plantel);

                ThrowActive(existente, "El código de plantel ya se encuentra registrado");

                record!.cod_plantel = request.cod_plantel!;
                record!.nom_plantel = request.nom_plantel!;
                record!.ind_interno = request.ind_interno!.Value;

                await UpdateAsync(record);

                scope.Complete();
            }

            return "Plantel actualizado satisfactoriamente";
        }

        public async Task<string> Delete(PlantelDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Plantel.Where(w => w.gid_plantel == request.id).FirstOrDefaultAsync();

                ThrowTrue(record == null, "El plantel no se encuentra registrado");

                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Plantel eliminado satisfactoriamente";
        }

        public async Task<PlantelListParamsDTO> ListParams(ListParamsQuery request)
        {
            var source = new PlantelListParamsDTO();

            source.estados = await GetConfigs("ESTADOS");

            source.roles = await _context.Rol.Where(w => w.cod_estado == true && w.cod_rol != "SADMIN").OrderBy(o => o.nom_rol).Select(s => new PlantelRolDTO
            {
                id_rol = s.gid_rol,
                cod_rol = s.cod_rol,
                nom_rol = s.nom_rol
            }).ToListAsync();

            return source;
        }

        public async Task<PlantelUpsertParamsDTO> UpsertParams(UpsertParamsQuery request)
        {
            await ValidateUpsert();

            var plantel = await _context.Plantel.FirstAsync(w => w.gid_plantel == request.id_plantel);

            ThrowTrue(plantel == null, "El plantel no se encuentra disponible");


            var source = new PlantelUpsertParamsDTO();

            source.roles = await _context.Rol.Where(w => w.cod_estado == true && w.cod_rol != "SADMIN").OrderBy(o => o.nom_rol).Select(s => new PlantelRolDTO
            {
                id_rol = s.gid_rol,
                cod_rol = s.cod_rol,
                nom_rol = s.nom_rol
            }).ToListAsync();

            source.usuarios = await _context.Usuario
                .Where(w => w.cod_estado == true)
                .OrderBy(o => o.nom_usuario)
                .ThenBy(o => o.ape_paterno)
                .ThenBy(o => o.ape_materno)
                .Select(u => new PlantelUsuarioDTO
                {
                    id_usuario = u.gid_usuario,
                    nom_usuario = $"{u.nom_usuario} {u.ape_paterno} {u.ape_materno}".Trim(),
                    cod_rol = u.rol.cod_rol,
                    tip_documento = u.tip_documento,
                    num_documento = u.num_documento,
                    //ind_turno = u.usuario_planteles.First(w => w.id_plantel == plantel!.id_plantel && w.cod_estado == true).ind_turno,
                    //nom_turno = u.usuario_planteles.First(w => w.id_plantel == plantel!.id_plantel && w.cod_estado == true).ind_turno.ToTurno(),
                    tip_usuario = u.tip_usuario,
                    nom_proveedor = u.proveedor == null ? "" : u.proveedor.nom_proveedor,
                    ruc_proveedor = u.proveedor == null ? "" : u.proveedor.ruc_proveedor,
                    cod_estado = u.cod_estado,
                    nom_estado = u.cod_estado.ToStatus()
                }).ToListAsync();

            return source;
        }

        private async Task ValidateUpsert()
        {
            try
            {
                await ValidateAction(MENU_CODE, ActionRol.Create);
            }
            catch
            {
                await ValidateAction(MENU_CODE, ActionRol.Update);
            }
        }

        public async Task<PlantelUserResponseDTO> UserAppend(UserAppendCommand request)
        {
            await ValidateUpsert();

            var response = new PlantelUserResponseDTO();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var rol = await _context.Rol.FirstAsync(w => w.gid_rol == request.id_rol);
                var plantel = await _context.Plantel.FirstAsync(w => w.gid_plantel == request.id_plantel);


                ThrowTrue(rol, "El rol indicado no se encuentra registrado");
                ThrowTrue(plantel, "El plantel indicado no se encuentra registrado");
                //ThrowTrue(usuario, "El usuario indicado no se encuentra registrado");

                var errores = new List<string>();
                var usuarios = new List<UsuarioEntity>();

                foreach (var item in request.usuarios!)
                {
                    var usuario = await _context.Usuario.FirstAsync(w => w.gid_usuario == item.id_usuario);

                    if (!usuario?.cod_estado ?? false)
                    {
                        errores.Add($"El usuario {item.nom_usuario} no se encuentra registrado");
                        continue;
                    }

                    if (rol!.id_rol != usuario!.id_rol)
                    {
                        errores.Add($"El rol indicado no pertener al rol del usuario {item.nom_usuario}");
                        continue;
                    }

                    usuarios.Add(usuario);
                }

                if (errores.Count > 0)
                    throw new Exception(string.Join("\r\n", errores));

                foreach (var usuario in usuarios)
                {
                    var record = await _context.UsuarioPlantel.FirstOrDefaultAsync(w => w.id_plantel == plantel!.id_plantel && w.id_usuario == usuario!.id_usuario);

                    if (record == null)
                    {
                        record = new UsuarioPlantelEntity
                        {
                            id_plantel = plantel!.id_plantel,
                            id_usuario = usuario!.id_usuario
                        };

                        await _context.UsuarioPlantel.AddAsync(record!);
                    }
                    else
                    {
                        _context.UsuarioPlantel.Update(record!);
                    }
                }

                await _context.SaveChangesAsync();

                response.data = await GetUsers(plantel!.id_plantel, rol!.id_rol);
                response.message = "Los usuarios han sido agregados satisfactoriamente";

                scope.Complete();
            }

            return response;
        }

        public async Task<PlantelUserResponseDTO> UserDelete(UserDeleteCommand request)
        {
            await ValidateUpsert();

            var response = new PlantelUserResponseDTO();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var rol = await _context.Rol.FirstAsync(w => w.gid_rol == request.id_rol);
                var plantel = await _context.Plantel.FirstAsync(w => w.gid_plantel == request.id_plantel);
                var usuario = await _context.Usuario.FirstAsync(w => w.gid_usuario == request.id_usuario);

                ThrowTrue(rol, "El rol indicado no se encuentra registrado");
                ThrowTrue(plantel, "El plantel indicado no se encuentra registrado");
                ThrowTrue(usuario, "El usuario indicado no se encuentra registrado");


                var record = await _context.UsuarioPlantel.FirstOrDefaultAsync(w => w.id_plantel == plantel!.id_plantel && w.id_usuario == usuario!.id_usuario);

                ThrowTrue(record == null, "El usuario no se encuentra para el plantel indicado");

                _context.UsuarioPlantel.Entry(record!).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                scope.Complete();

                response.data = await GetUsers(plantel!.id_plantel, rol!.id_rol);
                response.message = "El usuario ha sido eliminado satisfactoriamente";
            }

            return response;
        }

        private async Task<List<PlantelUsuarioDTO>> GetUsers(int id_plantel, int id_rol)
        {
            var source = await _context.UsuarioPlantel
                    .Where(w => w.id_plantel == id_plantel && w.usuario.id_rol == id_rol && w.cod_estado == true)
                    .Select(u => new PlantelUsuarioDTO
                    {
                        id_usuario = u.usuario.gid_usuario,
                        nom_usuario = $"{u.usuario.nom_usuario} {u.usuario.ape_paterno} {u.usuario.ape_materno}".Trim(),
                        tip_documento = u.usuario.tip_documento,
                        num_documento = u.usuario.num_documento,
                        cod_rol = u.usuario.rol.cod_rol,
                        //ind_turno = u.ind_turno,
                        //nom_turno = u.ind_turno.ToTurno(),
                        tip_usuario = u.usuario.tip_usuario,
                        nom_proveedor = u.usuario.proveedor == null ? "" : u.usuario.proveedor.nom_proveedor,
                        ruc_proveedor = u.usuario.proveedor == null ? "" : u.usuario.proveedor.ruc_proveedor,
                        cod_estado = u.cod_estado,
                        nom_estado = u.cod_estado.ToStatus()
                    }).ToListAsync();

            return source;
        }
    }
}
