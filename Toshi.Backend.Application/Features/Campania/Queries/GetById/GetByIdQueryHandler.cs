using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Campania;

namespace Toshi.Backend.Application.Features.Campania.Queries.GetById
{
    public class GetByIdQueryHandler : MediatR.IRequestHandler<GetByIdQuery, CampaniaDTO?>
    {
        protected readonly ICampaniaRepository repository;

        public GetByIdQueryHandler(ICampaniaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CampaniaDTO?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetById(request);
        }
    }
}
