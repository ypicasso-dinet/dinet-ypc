using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Usuario;
using MediatR;

namespace Toshi.Backend.Application.Features.Usuario.Querys.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, UsuarioDTO?>
    {
        private readonly IUsuarioRepository repository;

        public GetByIdQueryHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UsuarioDTO?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetById(request);
        }
    }
}
