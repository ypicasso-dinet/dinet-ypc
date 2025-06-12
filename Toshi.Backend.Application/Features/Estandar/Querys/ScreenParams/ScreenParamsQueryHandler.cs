using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Estandar;

namespace Toshi.Backend.Application.Features.Estandar.Querys.ScreenParams
{
    public class ScreenParamsQueryHandler : MediatR.IRequestHandler<ScreenParamsQuery, EstandarScreenParamsDTO>
    {
        protected readonly IEstandarRepository repository;

        public ScreenParamsQueryHandler(IEstandarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EstandarScreenParamsDTO> Handle(ScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.ScreenParams(request);
        }
    }
}
