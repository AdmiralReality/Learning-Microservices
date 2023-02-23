using LM.Shop.Service.Entities;
using LM.Shop.Service.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace LM.Shop.Service.Repositories
{
    public static class MongoDbInstaller
    {
        public static void Install(IServiceCollection serivces, IConfiguration configuration)
        {
            ConfigSerializers(serivces, configuration);
            AddDependencies(serivces, configuration);
        }

        private static void ConfigSerializers(IServiceCollection serivces, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
        }

        private static void AddDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddSingleton(ServiceProvider => {
                
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString); // TODO get db string from configuration
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            services.AddSingleton<IShopItemRepository, ShopItemRepository>();
        }
    }
}
