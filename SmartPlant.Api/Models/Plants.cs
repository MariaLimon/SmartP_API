using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class Plant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;} = string.Empty;

        [BsonElement("Users")] // Nombre del campo que almacenará el ID del usuario
        public ObjectId UserId { get; set; } = ObjectId.Empty;

        [BsonElement("NamePlant")]
        public string NamePlant {get; set;} = string.Empty;

        [BsonElement("TypePlant")]
        public string TypePlant {get; set;} = string.Empty;

    }
}