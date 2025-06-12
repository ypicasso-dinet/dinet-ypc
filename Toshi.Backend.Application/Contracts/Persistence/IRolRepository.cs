using Toshi.Backend.Application.Features.Rol.Commands.Create;
using Toshi.Backend.Application.Features.Rol.Commands.Delete;
using Toshi.Backend.Application.Features.Rol.Commands.SaveAcciones;
using Toshi.Backend.Application.Features.Rol.Commands.Update;
using Toshi.Backend.Application.Features.Rol.Querys.GetAll;
using Toshi.Backend.Application.Features.Rol.Querys.GetById;
using Toshi.Backend.Application.Features.Rol.Querys.Opciones;
using Toshi.Backend.Domain.DTO.Rol;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IRolRepository : IAsyncRepository<RolEntity>
    {
        Task<string> Create(RolCreateCommand request);
        Task<string> Update(RolUpdateCommand request);
        Task<string> Delete(RolDeleteCommand request);
        Task<List<RolItemDTO>> GetAll(GetAllQuery request);
        Task<RolDTO?> GetById(GetByIdQuery request);
        Task<List<RolMenuDTO>> Opciones(OpcionesQuery request);
        Task<string> SaveAcciones(RolSaveAccionesCommand request);
    }
}
