using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _0effort_crm_api.Mongo.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public string CustomerId { get; set; }

        /**
         * Optionals?
        */
        public string? DeliveryAddress { get; set; }
        public string? DeliveryCity { get; set; }
        public string? DeliveryPostcode { get; set; }
        public string? DeliveryCountry { get; set; }
    }
}
