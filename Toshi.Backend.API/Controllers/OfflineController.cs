using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.Offline.Queries.GetInitialInfo;
using Toshi.Backend.Domain.DTO.Offline;

namespace Toshi.Backend.API.Controllers
{
    public class OfflineController : AppBaseController
    {
        public OfflineController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetInitialInfo")]
        public async Task<OfflineResponseDTO> GetInitialInfo([FromQuery] GetInitialInfoQuery command) => await _mediator.Send(command);
    }
}
