using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetAll
{
    public class IngresoPolloGetAllQueryHandler : IRequestHandler<IngresoPolloGetAllQuery, IngresoPolloListResponseDTO>
    {
        protected readonly IIngresoPolloRepository repository;

        public IngresoPolloGetAllQueryHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoPolloListResponseDTO> Handle(IngresoPolloGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
