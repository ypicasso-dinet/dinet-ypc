using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetScreenParams
{
    public class GetScreenParamsQueryHandler : MediatR.IRequestHandler<GetScreenParamsQuery, IngresoPolloScreenParamsDTO>
    {
        protected readonly IIngresoPolloRepository repository;

        public GetScreenParamsQueryHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoPolloScreenParamsDTO> Handle(GetScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetScreenParams(request);
        }
    }
}
