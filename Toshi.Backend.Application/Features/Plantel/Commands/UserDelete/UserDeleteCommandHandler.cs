using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Commands.UserDelete
{
    public class UserDeleteCommandHandler : MediatR.IRequestHandler<UserDeleteCommand, PlantelUserResponseDTO>
    {
        protected readonly IPlantelRepository repository;

        public UserDeleteCommandHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PlantelUserResponseDTO> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.UserDelete(request);
        }
    }
}
