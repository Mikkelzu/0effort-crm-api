

using _0effort_crm_api.Mongo.Entities;

namespace _0effort_crm_api.Contracts.DTO
{
    public class CreateOrUpdateOrderDto
    {
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
