using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetAll
{
    public class IngresoProductoGetAllQueryCommandHandler : IRequestHandler<IngresoProductoGetAllQuery, IngresoProductoListResponseDTO>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoGetAllQueryCommandHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoProductoListResponseDTO> Handle(IngresoProductoGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
