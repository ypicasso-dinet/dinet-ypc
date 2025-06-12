using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetById
{
    public class IngresoProductoGetByIdQueryHandler : IRequestHandler<IngresoProductoGetByIdQuery, IngresoProductoDTO?>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoGetByIdQueryHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoProductoDTO?> Handle(IngresoProductoGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
