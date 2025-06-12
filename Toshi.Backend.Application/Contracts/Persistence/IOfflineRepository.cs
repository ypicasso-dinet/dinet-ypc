using Toshi.Backend.Application.Features.Offline.Queries.GetInitialInfo;
using Toshi.Backend.Domain.DTO.Offline;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IOfflineRepository : IAsyncRepository<PlantelEntity>
    {
        Task<OfflineResponseDTO> GetInitialInfo(GetInitialInfoQuery request);
    }
}
