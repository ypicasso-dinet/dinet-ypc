using Toshi.Backend.Application.Features.Producto.Commands.Create;
using Toshi.Backend.Application.Features.Producto.Commands.Delete;
using Toshi.Backend.Application.Features.Producto.Commands.Update;
using Toshi.Backend.Application.Features.Producto.Querys.GetAll;
using Toshi.Backend.Application.Features.Producto.Querys.GetById;
using Toshi.Backend.Application.Features.Producto.Querys.ScreenParams;
using Toshi.Backend.Domain.DTO.Producto;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IProductoRepository : IAsyncRepository<ProductoEntity>
    {
        Task<List<ProductoItemDTO>> GetAll(ProductoGetAllQuery request);
        Task<ProductoDTO?> GetById(ProductoGetByIdQuery request);
        Task<string> Create(ProductoCreateCommand request);
        Task<string> Update(ProductoUpdateCommand request);
        Task<string> Delete(ProductoDeleteCommand request);
        Task<ProductoScreenParamsDTO> ScreenParams(ScreenParamsQuery request);
    }
}
