using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetListParams
{
    public class GetListParamsQueryHandler : MediatR.IRequestHandler<GetListParamsQuery, IngresoPolloListParamsDTO>
    {
        protected readonly IIngresoPolloRepository repository;

        public GetListParamsQueryHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoPolloListParamsDTO> Handle(GetListParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetListParams(request);
        }
    }
}
