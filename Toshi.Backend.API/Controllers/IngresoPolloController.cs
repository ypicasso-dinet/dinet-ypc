using MediatR;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Common;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Create;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Delete;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.DownloadExcel;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Update;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetAll;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetById;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetListParams;
using Toshi.Backend.Application.Features.IngresoPollo.Querys.GetScreenParams;
using Toshi.Backend.Domain.DTO.IngresoPollo;


namespace Toshi.Backend.API.Controllers
{
    public class IngresoPolloController : AppBaseController
    {

        public IngresoPolloController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetAll")]
        public async Task<IngresoPolloListResponseDTO> GetAll([FromQuery] IngresoPolloGetAllQuery command) => await _mediator.Send(command);

        [HttpGet("GetById")]
        public async Task<IngresoPolloDTO?> GetById([FromQuery] IngresoPolloGetByIdQuery command) => await _mediator.Send(command);

        [HttpPost()]
        public async Task<string> Create([FromBody] IngresoPolloCreateCommand command) => await _mediator.Send(command);

        [HttpPost("CreateFromMobile")]
        public async Task<string> CreateFromMobile(List<IFormFile> files, [FromForm] string data)
        {
            var command = Newtonsoft.Json.JsonConvert.DeserializeObject<IngresoPolloCreateCommand>(data);

            if (files != null && files.Count > 0)
            {
                var uploadPath = Path.Combine("wwwroot/ipbb");

                Directory.CreateDirectory(uploadPath);

                command!.imagenes = new List<IngresoPolloImagen>();

                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                    var filePath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);

                    await file.CopyToAsync(stream);

                    string imageUrl = $"{Request.Scheme}://{Request.Host}/ipbb/{fileName}";

                    command!.imagenes.Add(new IngresoPolloImagen
                    {
                        nom_imagen = file.FileName,
                        url_imagen = imageUrl
                    });
                }
            }

            return await _mediator.Send(command!);
        }

        [HttpPut()]
        public async Task<string> Update([FromBody] IngresoPolloUpdateCommand command) => await _mediator.Send(command);

        [HttpPut("UpdateFromMobile")]
        public async Task<string> UpdateFromMobile(List<IFormFile> files, [FromForm] string data)
        {
            var command = Newtonsoft.Json.JsonConvert.DeserializeObject<IngresoPolloUpdateCommand>(data);

            //if (files == null || files.Count == 0)
            //    throw new Exception("Se debde enviar por lo menos una imagen");

            var uploadPath = Path.Combine("wwwroot/ipbb");

            Directory.CreateDirectory(uploadPath);

            if (command!.imagenes == null)
                command!.imagenes = new List<IngresoPolloImagen>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                var filePath = Path.Combine(uploadPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(stream);

                string imageUrl = $"{Request.Scheme}://{Request.Host}/ipbb/{fileName}";

                command!.imagenes.Add(new IngresoPolloImagen
                {
                    nom_imagen = file.FileName,
                    url_imagen = imageUrl
                });
            }

            return await _mediator.Send(command!);
        }

        [HttpDelete()]
        public async Task<string> Delete([FromQuery] IngresoPolloDeleteCommand command) => await _mediator.Send(command);

        [HttpGet("ListParams")]
        public async Task<IngresoPolloListParamsDTO?> ListParams([FromQuery] GetListParamsQuery command) => await _mediator.Send(command);

        [HttpGet("ScreenParams")]
        public async Task<IngresoPolloScreenParamsDTO?> ScreenParams([FromQuery] GetScreenParamsQuery command) => await _mediator.Send(command);

        [HttpPost("DownloadExcel")]
        public async Task<IActionResult> GenerateExcel([FromBody] Dictionary<string, object> payload)
        {
            var result = await _mediator.Send(new IngresoPolloDownloadExcelCommand(payload));
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Listado-IPBB.xlsx");
        }

    }
}
