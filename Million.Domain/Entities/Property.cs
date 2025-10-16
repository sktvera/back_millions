
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.Domain.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("idOwner")]
        public string IdOwner { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }

       
        [BsonElement("image")]
        public string Image { get; set; } = string.Empty;

        [BsonElement("imageKey")]
        public string? ImageKey { get; set; }

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }
    }
}
