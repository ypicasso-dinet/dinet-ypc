using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Estandar;

namespace Toshi.Backend.Application.Features.Estandar.Querys.GetById
{
    public class EstandarGetByIdQueryHandler : IRequestHandler<EstandarGetByIdQuery, EstandarDTO?>
    {
        protected readonly IEstandarRepository repository;

        public EstandarGetByIdQueryHandler(IEstandarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EstandarDTO?> Handle(EstandarGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
