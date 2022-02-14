using _0effort_crm_api.Mongo.Entities;

namespace _0effort_crm_api.Contracts.DTO
{
    public class CreateOrUpdateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
