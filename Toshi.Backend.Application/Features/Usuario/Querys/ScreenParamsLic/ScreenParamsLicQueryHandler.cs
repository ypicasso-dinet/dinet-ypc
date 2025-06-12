using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Application.Features.Usuario.Querys.ScreenParamsLic
{
    public class ScreenParamsLicQueryHandler : MediatR.IRequestHandler<ScreenParamsLicQuery, List<CodeTextDTO>>
    {
        protected readonly IUsuarioRepository repository;

        public ScreenParamsLicQueryHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<CodeTextDTO>> Handle(ScreenParamsLicQuery request, CancellationToken cancellationToken)
        {
            return await repository.ScreenParamsLic(request);
        }
    }
}
