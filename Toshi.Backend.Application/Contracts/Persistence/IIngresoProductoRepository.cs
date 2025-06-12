using Toshi.Backend.Application.Features.IngresoProducto.Commands.Create;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Delete;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Update;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetAll;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetById;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetListParams;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams;
using Toshi.Backend.Domain.DTO.IngresoProducto;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IIngresoProductoRepository : IAsyncRepository<IngresoProductoEntity>
    {
        Task<IngresoProductoListResponseDTO> GetAll(IngresoProductoGetAllQuery request);
        Task<IngresoProductoDTO?> GetById(IngresoProductoGetByIdQuery request);
        Task<string> Create(IngresoProductoCreateCommand request);
        Task<string> Update(IngresoProductoUpdateCommand request);
        Task<string> Delete(IngresoProductoDeleteCommand request);
        Task<IngresoProductoListParamsDTO> GetListParams(IngresoProductoGetListParamsQuery request);
        Task<IngresoProductoScreenParamsDTO> GetScreenParams(IngresoProductoGetScreenParamsQuery request);
        Task<IngresoProductoListCampaniaPorEstadoDTO> GetCampaniasByEstadoAsync(IngresoProductoGetCampaniasPorEstadoQuery request);
        Task<IngresoProductoGetTipoProductoPorProductoDTO> GetTipoProductoPorProducto(IngresoProductoGetTipoProductoPorProductoQuery request);
        Task<IngresoProductoGetUnidadMedidaPorProductoDTO> GetUnidadMedidaPorProducto(IngresoProductoGetUnidadMedidaPorProductoQuery request);
        Task<byte[]> GenerateExcelAsync(Dictionary<string, object> payload, string webRootPath);
    }
}
