using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Campania;

namespace Toshi.Backend.Application.Features.Campania.Queries.GetAll
{
    public class GetAllQueryHandler : MediatR.IRequestHandler<GetAllQuery, List<CampaniaItemDTO>>
    {
        protected readonly ICampaniaRepository repository;

        public GetAllQueryHandler(ICampaniaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<CampaniaItemDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(request);
        }
    }
}
