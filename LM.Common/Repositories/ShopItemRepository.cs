using LM.Shop.Service.Entities;
using MongoDB.Driver;

namespace LM.Shop.Service.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _dbCollection;
        private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder;

        public MongoRepository(IMongoDatabase db, string collectionName)
        {
            _dbCollection = db.GetCollection<T>(collectionName);
            _filterDefinitionBuilder = new FilterDefinitionBuilder<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _dbCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);

            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await _dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(T item)
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
