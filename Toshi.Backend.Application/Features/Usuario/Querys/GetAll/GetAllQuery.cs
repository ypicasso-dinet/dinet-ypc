using MediatR;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Querys.GetAll
{
    public class GetAllQuery : IRequest<List<UsuarioItemDTO>>
    {
        public string? cod_usuario { get; set; }
        public string? nom_usuario { get; set; }
        public string? num_documento { get; set; }
        public string? cod_estado { get; set; }
        public string? id_plantel { get; set; }
    }
}
