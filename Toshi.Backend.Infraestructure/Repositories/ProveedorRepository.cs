using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;
//------------------------------------------------------
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.Proveedor.Commands.Create;
using Toshi.Backend.Application.Features.Proveedor.Commands.Delete;
using Toshi.Backend.Application.Features.Proveedor.Commands.PersonalCreate;
using Toshi.Backend.Application.Features.Proveedor.Commands.PersonalDelete;
using Toshi.Backend.Application.Features.Proveedor.Commands.PersonalUpdate;
using Toshi.Backend.Application.Features.Proveedor.Commands.Update;
using Toshi.Backend.Application.Features.Proveedor.Querys.BuscarPersona;
using Toshi.Backend.Application.Features.Proveedor.Querys.GetAll;
using Toshi.Backend.Application.Features.Proveedor.Querys.GetById;
using Toshi.Backend.Application.Features.Proveedor.Querys.UpsertParams;
using Toshi.Backend.Application.Features.Proveedor.Querys.UsersByRol;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Proveedor;
using Toshi.Backend.Domain.Entities;
using Toshi.Backend.Domain.Enums;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Utilities;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public class ProveedorRepository : RepositoryBase<ProveedorEntity>, IProveedorRepository
    {
        const string MENU_CODE = "FEAT-1900";

        public ProveedorRepository(ToshiDBContext context, SessionStorage sessionStorage) : base(context, sessionStorage)
        {
        }

        public async Task<List<ProveedorItemDTO>> GetAll(ProveedorGetAllQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var records = await _context.Proveedor
                .Where(w =>
                    (string.IsNullOrEmpty(request.ruc_proveedor) || w.ruc_proveedor.Contains(request.ruc_proveedor)) &&
                    (string.IsNullOrEmpty(request.nom_proveedor) || w.nom_proveedor.Contains(request.nom_proveedor))
                )
                .Select(s => new ProveedorItemDTO()
                {
                    // Specifying field's
                    id = s.gid_proveedor,
                    nom_proveedor = s.nom_proveedor,
                    ruc_proveedor = s.ruc_proveedor,
                    nom_estado = s.cod_estado.ToStatus()
                })
                .OrderBy(o => o.nom_proveedor)
                .ToListAsync();

            return records;
        }

        public async Task<ProveedorDTO?> GetById(ProveedorGetByIdQuery request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Read);

            var allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);

            var record = await _context.Proveedor
                .Where(w => w.gid_proveedor == request.id)
                .Select(s => new ProveedorDTO()
                {
                    // Specifying field's    
                    id_proveedor = s.gid_proveedor,
                    nom_proveedor = s.nom_proveedor,
                    ruc_proveedor = s.ruc_proveedor,
                    nom_estado = s.cod_estado.ToStatus(),
                    //-------------------------------------
                    allow_update = allowUpdate
                })
                .FirstOrDefaultAsync();

            if (record != null)
            {
                //record.roles = await _context.Rol
                //.Where(w => w.cod_estado == true && w.cod_rol != "SADMIN")
                //.OrderBy(o => o.nom_rol)
                //.Select(s => new ProveedorRolDTO
                //{
                //    id_rol = s.gid_rol,
                //    cod_rol = s.cod_rol,
                //    nom_rol = s.nom_rol,
                //    personal = new List<ProveedorRolPersonalDTO>()
                //})
                //.ToListAsync();

                //foreach (var rol in record.roles)
                //{
                //    rol.personal = await GetPersonal(record!.id_proveedor!, rol.cod_rol!);
                //}

                record!.personal = await GetPersonal(record!.id_proveedor!, "");
            }

            return record;
        }

        public async Task<ProveedorCreateResponseDTO> Create(ProveedorCreateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Create);

            var gid_proveedor = string.Empty;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var ruc_proveedor = request.ruc_proveedor!.Trim();
                var record = await _context.Proveedor.Where(w => w.ruc_proveedor == ruc_proveedor).FirstOrDefaultAsync();

                ThrowTrue(record != null && record.cod_estado == true, "El RUC indicado ya se encuentra registrado");

                if (record == null)
                {
                    record = new ProveedorEntity
                    {
                        gid_proveedor = Guid.NewGuid().ToString(),
                        ruc_proveedor = ruc_proveedor,
                        nom_proveedor = request.nom_proveedor!
                    };

                    await AddAsync(record);
                }
                else
                {
                    record.ruc_proveedor = ruc_proveedor;
                    record.nom_proveedor = request.nom_proveedor!;

                    await UpdateAsync(record!);
                }

                scope.Complete();

                gid_proveedor = record!.gid_proveedor;
            }

            return new ProveedorCreateResponseDTO { message = $"Proveedor creado satisfactoriamente.", data = gid_proveedor };
        }

        public async Task<string> Update(ProveedorUpdateCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Update);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var ruc_proveedor = request.ruc_proveedor!.Trim();
                var record = await _context.Proveedor.Where(w => w.gid_proveedor == request.id_proveedor).FirstOrDefaultAsync();

                ThrowTrue(record == null, "El proveedor no se encuentra registrado");

                var existente = await _context.Proveedor.Where(w => w.ruc_proveedor == ruc_proveedor && w.id_proveedor != record!.id_proveedor).FirstOrDefaultAsync();

                ThrowTrue(existente != null, "El RUC indicado ya se encuentra registrado");

                record!.ruc_proveedor = ruc_proveedor;
                record!.nom_proveedor = request.nom_proveedor!;

                await UpdateAsync(record!);

                scope.Complete();
            }

            return "Proveedor actualizado satisfactoriamente";
        }

        public async Task<string> Delete(ProveedorDeleteCommand request)
        {
            await ValidateAction(MENU_CODE, ActionRol.Delete);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.Proveedor.Where(w => w.gid_proveedor == request.id).FirstOrDefaultAsync();

                ThrowTrue(record, "El proveedor no se encuentra registrado");

                await DeleteAsync(record!);

                scope.Complete();
            }

            return "Proveedor eliminado satisfactoriamente";
        }

        public async Task<ProveedorUpsertParamsDTO> UpsertParams(UpsertParamsQuery request)
        {
            var source = new ProveedorUpsertParamsDTO();

            source.tipos_documento = await GetConfigs("TIPDOC");

            source.planteles = await _context.Plantel
                .Where(w => w.cod_estado == true)
                .OrderBy(o => o.nom_plantel)
                .Select(p => new CodeTextDTO
                {
                    code = p.gid_plantel,
                    text = p.nom_plantel
                })
                .ToListAsync();

            source.roles = await _context.Rol
                .Where(w => w.cod_estado == true && w.cod_rol != "SADMIN")
                .OrderBy(o => o.nom_rol)
                .Select(s => new CodeTextDTO
                {
                    code = s.gid_rol,
                    text = s.nom_rol
                })
                .ToListAsync();

            return source;
        }

        private async Task<List<ProveedorRolPersonalDTO>> GetPersonal(string gid_proveedor, string cod_rol)
        {
            var allowUpdate = await IsValidAction(MENU_CODE, ActionRol.Update);
            var allowDelete = await IsValidAction(MENU_CODE, ActionRol.Delete);

            return await (
                from p in _context.Persona
                join pp in _context.ProveedorPersonal on p.id_persona equals pp.id_persona
                join prv in _context.Proveedor on pp.id_proveedor equals prv.id_proveedor
                join td in _context.ConfiguracionDetalle on new { cod_detalle = p.tip_documento, cod_config = "TIPDOC" } equals new { td.cod_detalle, td.configuracion.cod_config }
                where
                   //p.rol.cod_rol == cod_rol
                   prv.gid_proveedor == gid_proveedor
                && pp.cod_estado == true
                && p.cod_estado == true
                // && td.cod_estado == true
                orderby p.nom_persona, p.ape_paterno, p.ape_materno
                select new ProveedorRolPersonalDTO
                {
                    //id_personal = p.gid_personal,
                    //cod_personal = p.cod_personal,
                    //nom_personal = $"{p.nom_personal ?? ""} {p.ape_paterno ?? ""} {p.ape_materno ?? ""}".Trim(),
                    //tip_documento = td.nom_detalle,
                    //num_documento = p.num_documento,
                    //nom_turno = p.ind_turno.ToTurno()

                    id_personal = pp.gid_proveedor_personal,
                    //-------------------------------------------------
                    cod_personal = p.cod_persona,
                    nom_personal = p.nom_persona,
                    ape_paterno = p.ape_paterno,
                    ape_materno = p.ape_materno,
                    //fec_nacimiento = p.fec_nacimiento.ToDate(),
                    tip_documento = p.tip_documento,
                    num_documento = p.num_documento,
                    //per_email = p.per_email,
                    num_telefono = pp.num_telefono,
                    //id_rol = p.rol.gid_rol,
                    //id_plantel = p.plantel.gid_plantel,
                    id_proveedor = prv.gid_proveedor,
                    //ind_turno = p.ind_turno,
                    //nom_turno = p.ind_turno.ToTurno(),
                    nom_estado = p.cod_estado.ToStatus(),
                    //------------------------------------------
                    allow_update = allowUpdate,
                    allow_delete = allowDelete,
                })
                .ToListAsync();
        }

        public async Task<ProveedorPersonalResponseDTO> PersonalCreate(PersonalCreateCommand request)
        {
            var response = new ProveedorPersonalResponseDTO();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var persona = await _context.Persona.FirstOrDefaultAsync(w => w.tip_documento == request.tip_documento && w.num_documento == request.num_documento && w.gid_persona != request.id_persona);
                ThrowTrue(persona != null && persona.cod_estado == true, "El tipo y número de documento ya se encuentra registrado");

                var proveedor = await _context.Proveedor.FirstOrDefaultAsync(w => w.gid_proveedor == request.id_proveedor);
                ThrowTrue(proveedor, "El proveedor no se encuentra habilitado");

                if (persona == null)
                {
                    persona = new PersonaEntity
                    {
                        //id_persona = request!.id_persona,
                        gid_persona = NewGuid(),
                        cod_persona = $"{request!.tip_documento}-{request!.num_documento}",
                        nom_persona = request!.nom_personal!,
                        ape_paterno = request!.ape_paterno!,
                        ape_materno = request!.ape_materno,
                        tip_documento = request!.tip_documento!,
                        num_documento = request!.num_documento!,
                    };

                    await _context.Persona.AddAsync(persona);
                    await _context.SaveChangesAsync();
                }

                var id_persona = persona?.id_persona ?? 0;
                var record = await _context.ProveedorPersonal.Where(w => w.id_proveedor == proveedor!.id_proveedor && w.id_persona == id_persona).FirstOrDefaultAsync();
                var append = record == null;

                //var rol = await _context.Rol.FirstOrDefaultAsync(w => w.gid_rol == request.id_rol);
                //ThrowTrue(rol, "El rol no se encuentra habilitado");

                //var plantel = await _context.Plantel.FirstOrDefaultAsync(w => w.gid_plantel == request.id_plantel);
                //ThrowTrue(plantel, "El plantel no se encuentra habilitado");

                if (append)
                    record = new ProveedorPersonalEntity
                    {
                        id_persona = persona!.id_persona,
                        id_proveedor = proveedor!.id_proveedor,
                        gid_proveedor_personal = NewGuid()
                    };
                else
                    ThrowTrue(record!.id_proveedor != proveedor!.id_proveedor, "El personal no corresponde al proveedor indicado");

                //record!.cod_persona = request!.cod_personal!;
                //record!.nom_personal = request!.nom_personal!;
                //record!.ape_paterno = request!.ape_paterno ?? "";
                //record!.ape_materno = request!.ape_materno ?? "";
                //record!.fec_nacimiento = request!.fec_nacimiento.ToDate();
                //record!.tip_documento = request!.tip_documento ?? "";
                //record!.num_documento = request!.num_documento ?? "";
                //record!.per_email = request!.per_email;
                record!.num_telefono = request!.num_telefono;
                //record!.id_rol = rol!.id_rol;
                //record!.id_plantel = plantel!.id_plantel;
                //record!.id_proveedor = proveedor!.id_proveedor;
                //record!.ind_turno = request!.ind_turno!.Value;

                if (append)
                    await AddEntityAsync(record!);
                else
                    await UpdateEntityAsync(record!);

                response.message = "Personal agregado satisfactoriamente";
                //response.data = await GetPersonal(proveedor!.gid_proveedor, rol!.cod_rol);
                response.data = await GetPersonal(proveedor!.gid_proveedor, "");

                scope.Complete();
            }

            return response;
        }

        public async Task<ProveedorPersonalResponseDTO> PersonalUpdate(PersonalUpdateCommand request)
        {
            var response = new ProveedorPersonalResponseDTO();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.ProveedorPersonal.FirstOrDefaultAsync(w => w.gid_proveedor_personal == request.id_personal);
                ThrowTrue(record == null, "El personal no se encuentra habilitado para su eliminación");

                var persona = await _context.Persona.FirstOrDefaultAsync(w => w.id_persona == record!.id_persona);
                ThrowTrue(persona, "El personal no se encuentra disponible");

                ThrowTrue(!(persona!.tip_documento == request.tip_documento && persona!.num_documento == request.num_documento), "El personal enviado no corresponde al tipo y número de documento");

                var proveedor = await _context.Proveedor.FirstOrDefaultAsync(w => w.gid_proveedor == request.id_proveedor);
                ThrowTrue(proveedor, "El proveedor no se encuentra habilitado");
                ThrowTrue(proveedor!.id_proveedor != record!.id_proveedor, "El personal no corresponde al proveedor indicado");

                //var rol = await _context.Rol.FirstOrDefaultAsync(w => w.gid_rol == request.id_rol);
                //ThrowTrue(rol, "El rol no se encuentra habilitado");

                //var plantel = await _context.Plantel.FirstOrDefaultAsync(w => w.gid_plantel == request.id_plantel);
                //ThrowTrue(plantel, "El plantel no se encuentra habilitado");

                //record!.cod_personal = request!.cod_personal!;
                persona!.nom_persona = request!.nom_personal!;
                persona!.ape_paterno = request!.ape_paterno!;
                persona!.ape_materno = request!.ape_materno;
                persona!.tip_documento = request!.tip_documento!;
                persona!.num_documento = request!.num_documento!;

                await UpdateEntityAsync(persona!);

                //record!.fec_nacimiento = request!.fec_nacimiento.ToDate();

                //record!.per_email = request!.per_email;
                record!.num_telefono = request!.num_telefono;
                //record!.id_rol = rol!.id_rol;
                //record!.id_plantel = plantel!.id_plantel;
                //record!.id_proveedor = proveedor!.id_proveedor;
                //record!.ind_turno = request!.ind_turno!.Value;

                await UpdateEntityAsync(record!);

                response.message = "Personal actualizado satisfactoriamente";
                //response.data = await GetPersonal(proveedor!.gid_proveedor, rol!.cod_rol);
                response.data = await GetPersonal(proveedor!.gid_proveedor, "");

                scope.Complete();
            }

            return response;
        }

        public async Task<ProveedorPersonalResponseDTO> PersonalDelete(PersonalDeleteCommand request)
        {
            var response = new ProveedorPersonalResponseDTO();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _context.ProveedorPersonal.FirstOrDefaultAsync(w => w.gid_proveedor_personal == request.id_personal);
                ThrowTrue(record, "El personal no se encuentra habilitado para su eliminación");

                var proveedor = await _context.Proveedor.FirstOrDefaultAsync(w => w.gid_proveedor == request.id_proveedor);
                ThrowTrue(proveedor, "El proveedor no se encuentra habilitado");
                ThrowTrue(proveedor!.id_proveedor != record!.id_proveedor, "El personal no corresponde al proveedor indicado");

                //var rol = await _context.Rol.FirstOrDefaultAsync(w => w.gid_rol == request.id_rol);
                //ThrowTrue(rol, "El rol no se encuentra habilitado");
                //ThrowTrue(rol!.id_rol != record!.id_rol, "El rol no corresponde al personal indicado");

                await DeleteEntityAsync(record!);

                response.message = "Personal eliminado satisfactoriamente";
                //response.data = await GetPersonal(proveedor!.gid_proveedor, rol!.cod_rol);
                response.data = await GetPersonal(proveedor!.gid_proveedor, "");

                scope.Complete();
            }

            return response;
        }

        public async Task<List<ProveedorRolPersonalDTO>> PersonalByRol(PersonalByRolQuery request)
        {
            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(w => w.gid_proveedor == request.id_proveedor && w.cod_estado == true);
            ThrowTrue(proveedor, "El proveedor no se encuentra habilitado");

            var rol = await _context.Rol.FirstOrDefaultAsync(w => w.gid_rol == request.id_rol && w.cod_estado == true);
            ThrowTrue(rol, "El rol no se encuentra habilitado");

            var records = await GetPersonal(proveedor!.gid_proveedor, rol!.cod_rol);

            return records;
        }

        public async Task<ProveedorPersonaDTO?> BuscarPersona(BuscarPersonaCommand request)
        {
            var persona = await _context.Persona
                .Where(w =>
                    w.tip_documento == request.tip_documento &&
                    w.num_documento == request.num_documento
                )
                .Select(s => new ProveedorPersonaDTO
                {
                    id_persona = s.gid_persona,
                    cod_persona = s.cod_persona,
                    nom_persona = s.nom_persona,
                    ape_paterno = s.ape_paterno,
                    ape_materno = s.ape_materno,
                    tip_documento = s.tip_documento,
                    num_documento = s.num_documento,
                })
                .FirstOrDefaultAsync();



            return persona;
        }
    }
}
