using LM.Shop.Service.Entities;

namespace LM.Shop.Service.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T item);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T item);
    }
}