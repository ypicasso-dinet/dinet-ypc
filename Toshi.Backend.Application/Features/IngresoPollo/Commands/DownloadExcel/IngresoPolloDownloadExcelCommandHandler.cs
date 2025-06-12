using MediatR;
using OfficeOpenXml;
using System.Drawing;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.DownloadExcel
{

    public class IngresoPolloDownloadExcelCommandHandler : IRequestHandler<IngresoPolloDownloadExcelCommand, byte[]>
    {
        private readonly IWebHostEnvironment _env;
        protected readonly IIngresoPolloRepository _repository;

        public IngresoPolloDownloadExcelCommandHandler(IWebHostEnvironment env, IIngresoPolloRepository repository)
        {
            _env = env;
            _repository = repository;
        }

        public async Task<byte[]> Handle(IngresoPolloDownloadExcelCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GenerateExcelAsync(request.Payload, _env.WebRootPath);
        }
    }
}
