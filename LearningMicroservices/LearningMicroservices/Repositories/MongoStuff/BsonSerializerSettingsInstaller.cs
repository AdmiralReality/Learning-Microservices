using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LM.Shop.Service.Repositories.MongoStuff
{
    public static class BsonSerializerSettingsInstaller
    {
        public static void Install()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
        }
    }
}
