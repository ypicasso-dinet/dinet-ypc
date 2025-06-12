using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Commands.UserAppend
{
    public class UserAppendCommandHandler : MediatR.IRequestHandler<UserAppendCommand, PlantelUserResponseDTO>
    {
        protected readonly IPlantelRepository repository;

        public UserAppendCommandHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PlantelUserResponseDTO> Handle(UserAppendCommand request, CancellationToken cancellationToken)
        {
            return await repository.UserAppend(request);
        }
    }
}
