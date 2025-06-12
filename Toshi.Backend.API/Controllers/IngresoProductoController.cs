using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Create;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Delete;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.DownloadExcel;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Update;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetAll;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetById;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetListParams;
using Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams;
using Toshi.Backend.Domain.DTO.Campania;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.API.Controllers
{
    public class IngresoProductoController : AppBaseController
    {
        public IngresoProductoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetAll")]
        public async Task<IngresoProductoListResponseDTO> GetAll([FromQuery] IngresoProductoGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<IngresoProductoDTO?> GetById([FromQuery] IngresoProductoGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] IngresoProductoCreateCommand command) => await _mediator.Send(command);

        [HttpPut()]
        public async Task<string> Update([FromBody] IngresoProductoUpdateCommand command) => await _mediator.Send(command);

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] IngresoProductoDeleteCommand command) => await _mediator.Send(command);

        [HttpGet("ListParams")]
        public async Task<IngresoProductoListParamsDTO?> ListParams([FromQuery] IngresoProductoGetListParamsQuery command) => await _mediator.Send(command);

        [HttpGet("ScreenParams")]
        public async Task<IngresoProductoScreenParamsDTO?> ScreenParams([FromQuery] IngresoProductoGetScreenParamsQuery command) => await _mediator.Send(command);

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
