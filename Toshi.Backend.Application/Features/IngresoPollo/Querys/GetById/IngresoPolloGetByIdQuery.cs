using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetById
{
    public class IngresoPolloGetByIdQuery : AppBaseCommand, IRequest<IngresoPolloDTO?>
    {
        public string? id { get; set; }
    }
}
