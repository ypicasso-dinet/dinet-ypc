using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Estandar;

namespace Toshi.Backend.Application.Features.Estandar.Querys.GetAll
{
    public class EstandarGetAllQueryHandler : IRequestHandler<EstandarGetAllQuery, List<EstandarItemDTO>>
    {
        protected readonly IEstandarRepository repository;

        public EstandarGetAllQueryHandler(IEstandarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<EstandarItemDTO>> Handle(EstandarGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
