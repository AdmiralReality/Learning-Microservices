using LM.Shop.Service.Entities;

namespace LM.Shop.Service.Repositories
{
    public interface IShopItemRepository
    {
        Task CreateAsync(ShopItem item);
        Task<IReadOnlyCollection<ShopItem>> GetAllAsync();
        Task<ShopItem> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(ShopItem item);
    }
}