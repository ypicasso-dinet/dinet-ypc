using Toshi.Backend.Application.Features.IngresoPollo.Commands.Create;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Delete;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Update;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetAll;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetById;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetListParams;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetScreenParams;
using Toshi.Backend.Domain.DTO.IngresoPollo;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IIngresoPolloRepository : IAsyncRepository<IngresoPolloEntity>
    {
        Task<IngresoPolloListResponseDTO> GetAll(IngresoPolloGetAllQuery request);
        Task<IngresoPolloDTO?> GetById(IngresoPolloGetByIdQuery request);
        Task<string> Create(IngresoPolloCreateCommand request);
        Task<string> Update(IngresoPolloUpdateCommand request);
        Task<string> Delete(IngresoPolloDeleteCommand request);
        Task<IngresoPolloListParamsDTO> GetListParams(GetListParamsQuery request);
        Task<IngresoPolloScreenParamsDTO> GetScreenParams(GetScreenParamsQuery request);
        Task<byte[]> GenerateExcelAsync(Dictionary<string, object> payload, string webRootPath);
    }
}
