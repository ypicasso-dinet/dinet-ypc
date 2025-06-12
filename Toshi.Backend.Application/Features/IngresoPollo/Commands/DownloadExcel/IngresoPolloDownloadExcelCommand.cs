using MediatR;
using System.Text.Json;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.DownloadExcel
{
    public record IngresoPolloDownloadExcelCommand(Dictionary<string, object> Payload) : IRequest<byte[]>;
}
