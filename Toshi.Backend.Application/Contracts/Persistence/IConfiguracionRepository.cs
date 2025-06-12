using Toshi.Backend.Application.Features.Configuracion.Commands.Create;
using Toshi.Backend.Application.Features.Configuracion.Commands.Delete;
using Toshi.Backend.Application.Features.Configuracion.Commands.Update;
using Toshi.Backend.Application.Features.Configuracion.Querys.GetAll;
using Toshi.Backend.Application.Features.Configuracion.Querys.GetById;
using Toshi.Backend.Application.Features.Configuracion.Querys.ScreenParams;
using Toshi.Backend.Domain.DTO.Configuracion;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IConfiguracionRepository : IAsyncRepository<ConfiguracionEntity>
    {
        Task<List<ConfiguracionItemDTO>> GetAll(ConfiguracionGetAllQuery request);
        Task<ConfiguracionDTO?> GetById(ConfiguracionGetByIdQuery request);
        Task<string> Create(ConfiguracionCreateCommand request);
        Task<string> Update(ConfiguracionUpdateCommand request);
        Task<string> Delete(ConfiguracionDeleteCommand request);
        Task<ConfiguracionScreenParamsDTO> ScreenParams(ScreenParamsQuery request);
    }
}
