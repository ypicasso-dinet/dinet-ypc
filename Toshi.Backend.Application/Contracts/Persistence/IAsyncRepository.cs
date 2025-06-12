using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task DisableAsync(T entity);


        Task AddEntityAsync<D>(D entity) where D : BaseDomainModel;
        Task UpdateEntityAsync<D>(D entity) where D : BaseDomainModel;
        Task DeleteEntityAsync<D>(D entity) where D : BaseDomainModel;
    }
}
