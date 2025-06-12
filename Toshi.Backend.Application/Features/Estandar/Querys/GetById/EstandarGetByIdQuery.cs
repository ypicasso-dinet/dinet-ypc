using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Estandar;

namespace Toshi.Backend.Application.Features.Estandar.Querys.GetById
{
    public class EstandarGetByIdQuery : AppBaseCommand, IRequest<EstandarDTO?>
    {
        public string? id { get; set; }
    }
}
