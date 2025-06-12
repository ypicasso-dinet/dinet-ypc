using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetById
{
    public class IngresoPolloGetByIdQueryHandler : IRequestHandler<IngresoPolloGetByIdQuery, IngresoPolloDTO?>
    {
        protected readonly IIngresoPolloRepository repository;

        public IngresoPolloGetByIdQueryHandler(IIngresoPolloRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoPolloDTO?> Handle(IngresoPolloGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
