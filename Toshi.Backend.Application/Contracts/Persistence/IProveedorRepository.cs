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
using Toshi.Backend.Domain.DTO.Proveedor;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IProveedorRepository : IAsyncRepository<ProveedorEntity>
    {
        Task<List<ProveedorItemDTO>> GetAll(ProveedorGetAllQuery request);
        Task<ProveedorDTO?> GetById(ProveedorGetByIdQuery request);
        Task<ProveedorCreateResponseDTO> Create(ProveedorCreateCommand request);
        Task<string> Update(ProveedorUpdateCommand request);
        Task<string> Delete(ProveedorDeleteCommand request);
        Task<ProveedorUpsertParamsDTO> UpsertParams(UpsertParamsQuery request);
        Task<ProveedorPersonalResponseDTO> PersonalUpdate(PersonalUpdateCommand request);
        Task<ProveedorPersonalResponseDTO> PersonalDelete(PersonalDeleteCommand request);
        Task<ProveedorPersonalResponseDTO> PersonalCreate(PersonalCreateCommand request);
        Task<List<ProveedorRolPersonalDTO>> PersonalByRol(PersonalByRolQuery request);
        Task<ProveedorPersonaDTO?> BuscarPersona(BuscarPersonaCommand request);
    }
}
