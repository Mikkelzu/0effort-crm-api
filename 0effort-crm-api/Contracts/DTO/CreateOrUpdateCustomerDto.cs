using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _0effort_crm_api.Contracts.DTO
{
    public class CreateOrUpdateCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
