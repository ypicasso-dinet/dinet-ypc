using MediatR;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.DownloadExcel
{
    public record IngresoProductoDownloadExcelCommand(Dictionary<string, object> Payload) : IRequest<byte[]>;
}
