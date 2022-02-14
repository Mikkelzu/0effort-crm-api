using _0effort_crm_api.Mongo.Entities;

namespace _0effort_crm_api.Contracts.DTO
{
    public class CustomerResponseModel : BaseResponseModel
    {
        public CustomerEntity Response { get; set; }
    }
}
