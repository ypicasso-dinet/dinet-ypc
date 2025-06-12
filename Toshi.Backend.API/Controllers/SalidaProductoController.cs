using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.DownloadExcel;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Create;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Delete;
using Toshi.Backend.Application.Features.SalidaProducto.Commands.Update;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetAll;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetById;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetListParams;
using Toshi.Backend.Application.Features.SalidaProducto.Queries.GetScreenParams;
using Toshi.Backend.Domain.DTO.IngresoProducto;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.API.Controllers
{
    public class SalidaProductoController : AppBaseController
    {
        public SalidaProductoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ListParams")]
        public async Task<SalidaProductoListParamsDTO> ListParams([FromQuery] GetSalidaListParamsQuery command) => await _mediator.Send(command);

        [HttpGet("ScreenParams")]
        public async Task<SalidaProductoScreenParamsDTO> ScreenParams([FromQuery] GetSalidaScreenParamsQuery command) => await _mediator.Send(command);


        [HttpGet("GetAll")]
        public async Task<SalidaProductoGetAllResponseDTO> GetAll([FromQuery] SalidaProductoGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<SalidaProductoDTO?> GetById([FromQuery] SalidaProductoGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] SalidaProductoCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] SalidaProductoUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] SalidaProductoDeleteCommand command) => await _mediator.Send(command);

        [HttpGet("ListCampaniasPorEstado")]
        public async Task<IngresoProductoListCampaniaPorEstadoDTO> ListCampaniasPorEstado([FromQuery] IngresoProductoGetCampaniasPorEstadoQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("GetTipoProductoPorProducto")]
        public async Task<IngresoProductoGetTipoProductoPorProductoDTO> GetTipoProductoPorProducto([FromQuery] IngresoProductoGetTipoProductoPorProductoQuery query) => await _mediator.Send(query);

        [HttpGet("GetUnidadMedidaPorProducto")]
        public async Task<IngresoProductoGetUnidadMedidaPorProductoDTO> GetUnidadMedidaPorProducto([FromQuery] IngresoProductoGetUnidadMedidaPorProductoQuery query) => await _mediator.Send(query);

        [HttpPost("DownloadExcel")]
        public async Task<IActionResult> GenerateExcel([FromBody] Dictionary<string, object> payload)
        {
            var result = await _mediator.Send(new IngresoProductoDownloadExcelCommand(payload));
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Listado-IPBB.xlsx");
        }
    }
}
