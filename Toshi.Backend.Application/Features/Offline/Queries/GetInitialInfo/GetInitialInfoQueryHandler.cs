using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Offline;

namespace Toshi.Backend.Application.Features.Offline.Queries.GetInitialInfo
{
    public class GetInitialInfoQueryHandler : MediatR.IRequestHandler<GetInitialInfoQuery, OfflineResponseDTO>
    {
        protected readonly IOfflineRepository repository;

        public GetInitialInfoQueryHandler(IOfflineRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OfflineResponseDTO> Handle(GetInitialInfoQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetInitialInfo(request);
        }
    }
}
