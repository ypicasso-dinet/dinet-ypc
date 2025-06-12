using Toshi.Backend.Application.Features.Usuario.Commands.Create;
using Toshi.Backend.Application.Features.Usuario.Commands.Delete;
using Toshi.Backend.Application.Features.Usuario.Commands.DeleteLicencia;
using Toshi.Backend.Application.Features.Usuario.Commands.Update;
using Toshi.Backend.Application.Features.Usuario.Commands.UpsertLicencia;
using Toshi.Backend.Application.Features.Usuario.Querys.GetAll;
using Toshi.Backend.Application.Features.Usuario.Querys.GetById;
using Toshi.Backend.Application.Features.Usuario.Querys.ScreenParams;
using Toshi.Backend.Application.Features.Usuario.Querys.ScreenParamsLic;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Usuario;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IUsuarioRepository : IAsyncRepository<UsuarioEntity>
    {
        Task<UsuarioCreateResponseDTO> Create(UsuarioCreateCommand request);
        Task<string> Update(UsuarioUpdateCommand request);
        Task<string> Delete(UsuarioDeleteCommand request);
        Task<List<UsuarioItemDTO>> GetAll(GetAllQuery request);
        Task<UsuarioDTO?> GetById(GetByIdQuery request);
        Task<UsuarioScreenParamsDTO> ScreenParams(ScreenParamsQuery request);
        Task<List<CodeTextDTO>> ScreenParamsLic(ScreenParamsLicQuery request);
        Task<UsuarioLicenciaResponseDTO> UpsertLicencia(UpsertLicenciaCommand request);
        Task<string> DeleteLicencia(UsuarioDeleteLicenciaCommand request);
    }
}
