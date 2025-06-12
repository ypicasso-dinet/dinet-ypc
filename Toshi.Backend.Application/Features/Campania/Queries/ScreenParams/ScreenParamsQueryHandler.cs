using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Campania;

namespace Toshi.Backend.Application.Features.Campania.Queries.ScreenParams
{
    public class ScreenParamsQueryHandler : MediatR.IRequestHandler<ScreenParamsQuery, CampaniaScreenParamsDTO>
    {
        protected readonly ICampaniaRepository repository;

        public ScreenParamsQueryHandler(ICampaniaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CampaniaScreenParamsDTO> Handle(ScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.ScreenParams(request);
        }
    }
}
