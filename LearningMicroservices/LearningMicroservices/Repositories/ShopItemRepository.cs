using LM.Shop.Service.Entities;
using MongoDB.Driver;

namespace LM.Shop.Service.Repositories
{
    public class ShopItemRepository : IShopItemRepository
    {
        private const string _collectionName = "ShopItems";

        private readonly IMongoCollection<ShopItem> _dbCollection;
        private readonly FilterDefinitionBuilder<ShopItem> _filterDefinitionBuilder;

        public ShopItemRepository(IMongoDatabase db)
        {
            _dbCollection = db.GetCollection<ShopItem>(_collectionName);
            _filterDefinitionBuilder = new FilterDefinitionBuilder<ShopItem>();
        }

        public async Task<IReadOnlyCollection<ShopItem>> GetAllAsync()
        {
            return await _dbCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
        }

        public async Task<ShopItem> GetAsync(Guid id)
        {
            var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);

            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ShopItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await _dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(ShopItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, item.Id);
            await _dbCollection.ReplaceOneAsync(filter, item);
        }

        public async Task RemoveAsync(Guid id)
        {
            var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}
