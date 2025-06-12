using MediatR;
using Microsoft.AspNetCore.Hosting;
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.DownloadExcel
{
    public class IngresoProductoDownloadExcelCommandHandler : IRequestHandler<IngresoProductoDownloadExcelCommand, byte[]>
    {
        private readonly IWebHostEnvironment _env;
        protected readonly IIngresoProductoRepository _repository;

        public IngresoProductoDownloadExcelCommandHandler(IWebHostEnvironment env, IIngresoProductoRepository repository)
        {
            _env = env;
            _repository = repository;
        }

        public async Task<byte[]> Handle(IngresoProductoDownloadExcelCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GenerateExcelAsync(request.Payload, _env.WebRootPath);
        }
    }
}
