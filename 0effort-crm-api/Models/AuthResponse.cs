using _0effort_crm_api.Entities;

namespace _0effort_crm_api.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public AuthResponse(User user, string token)
        {
            Id = user.Id;
            Token = token;
        }
    }
}
