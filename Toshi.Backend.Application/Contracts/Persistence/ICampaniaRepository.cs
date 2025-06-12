using Toshi.Backend.Application.Features.Campania.Commands.CerrarCampania;
using Toshi.Backend.Application.Features.Campania.Commands.CreateCampania;
using Toshi.Backend.Application.Features.Campania.Queries.GetAll;
using Toshi.Backend.Application.Features.Campania.Queries.GetById;
using Toshi.Backend.Application.Features.Campania.Queries.ScreenParams;
using Toshi.Backend.Domain.DTO.Campania;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface ICampaniaRepository : IAsyncRepository<CampaniaEntity>
    {
        Task<string> CerrarCampania(CerrarCampaniaCommand request);
        Task<string> CreateCampania(CreateCampaniaCommand request);
        Task<List<CampaniaItemDTO>> GetAll(GetAllQuery request);
        Task<CampaniaDTO?> GetById(GetByIdQuery request);
        Task<CampaniaScreenParamsDTO> ScreenParams(ScreenParamsQuery request);
    }
}
