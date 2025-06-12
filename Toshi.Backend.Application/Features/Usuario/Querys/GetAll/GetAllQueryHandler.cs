using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Usuario;
using MediatR;

namespace Toshi.Backend.Application.Features.Usuario.Querys.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<UsuarioItemDTO>>
    {
        private readonly IUsuarioRepository repository;

        public GetAllQueryHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<UsuarioItemDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(request);
        }
    }
}
