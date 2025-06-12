using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Estandar;

namespace Toshi.Backend.Application.Features.Estandar.Querys.GetAll
{
    public class EstandarGetAllQuery : AppBaseCommand, IRequest<List<EstandarItemDTO>>
    {
        // Request Properties
        public string? cod_lote { get; set; }
        public string? val_edad { get; set; }
        public string? cod_sexo { get; set; }
    }
}
