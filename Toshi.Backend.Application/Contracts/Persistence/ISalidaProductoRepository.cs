using Toshi.Backend.Application.Features.SalidaProducto.Commands.Create;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Delete;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Update;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetAll;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetById;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetListParams;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetScreenParams;
using Toshi.Backend.Domain.DTO.SalidaProducto;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface ISalidaProductoRepository : IAsyncRepository<SalidaProductoEntity>
    {
        Task<SalidaProductoGetAllResponseDTO> GetAll(SalidaProductoGetAllQuery request);
        Task<SalidaProductoDTO?> GetById(SalidaProductoGetByIdQuery request);
        Task<string> Create(SalidaProductoCreateCommand request);
        Task<string> Update(SalidaProductoUpdateCommand request);
        Task<string> Delete(SalidaProductoDeleteCommand request);
        Task<SalidaProductoListParamsDTO> GetListParams(GetSalidaListParamsQuery request);
        Task<SalidaProductoScreenParamsDTO> GetScreenParams(GetSalidaScreenParamsQuery request);
    }
}
