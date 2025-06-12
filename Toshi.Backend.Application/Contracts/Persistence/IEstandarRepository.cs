using Toshi.Backend.Application.Features.Estandar.Commands.Create;
using Toshi.Backend.Application.Features.Estandar.Commands.Delete;
using Toshi.Backend.Application.Features.Estandar.Commands.Update;
using Toshi.Backend.Application.Features.Estandar.Querys.GetAll;
using Toshi.Backend.Application.Features.Estandar.Querys.GetById;
using Toshi.Backend.Application.Features.Estandar.Querys.ScreenParams;
using Toshi.Backend.Domain.DTO.Estandar;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IEstandarRepository : IAsyncRepository<EstandarEntity>
    {
        Task<List<EstandarItemDTO>> GetAll(EstandarGetAllQuery request);
        Task<EstandarDTO?> GetById(EstandarGetByIdQuery request);
        Task<string> Create(EstandarCreateCommand request);
        Task<string> Update(EstandarUpdateCommand request);
        Task<string> Delete(EstandarDeleteCommand request);
        Task<EstandarScreenParamsDTO> ScreenParams(ScreenParamsQuery request);
    }
}
