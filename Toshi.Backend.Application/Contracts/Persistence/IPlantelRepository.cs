using Toshi.Backend.Application.Features.Plantel.Commands.Create;
using Toshi.Backend.Application.Features.Plantel.Commands.Delete;
using Toshi.Backend.Application.Features.Plantel.Commands.Update;
using Toshi.Backend.Application.Features.Plantel.Commands.UserAppend;
using Toshi.Backend.Application.Features.Plantel.Commands.UserDelete;
using Toshi.Backend.Application.Features.Plantel.Querys.GetAll;
using Toshi.Backend.Application.Features.Plantel.Querys.GetById;
using Toshi.Backend.Application.Features.Plantel.Querys.ListParams;
using Toshi.Backend.Application.Features.Plantel.Querys.UpsertParams;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Plantel;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IPlantelRepository : IAsyncRepository<PlantelEntity>
    {
        Task<List<PlantelItemDTO>> GetAll(PlantelGetAllQuery request);
        Task<PlantelDTO?> GetById(PlantelGetByIdQuery request);
        Task<StatusResponse> Create(PlantelCreateCommand request);
        Task<string> Update(PlantelUpdateCommand request);
        Task<string> Delete(PlantelDeleteCommand request);
        Task<PlantelListParamsDTO> ListParams(ListParamsQuery request);
        Task<PlantelUpsertParamsDTO> UpsertParams(UpsertParamsQuery request);
        Task<PlantelUserResponseDTO> UserDelete(UserDeleteCommand request);
        Task<PlantelUserResponseDTO> UserAppend(UserAppendCommand request);
    }
}
