using _0effort_crm_api.Auth;
using _0effort_crm_api.Core;
using _0effort_crm_api.Entities;
using _0effort_crm_api.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _0effort_crm_api.Services
{

    public interface IUserService
    {
        AuthResponse Authenticate(AuthenticateModel model);
        User GetById(int userId);
    }

    public class UserService : IUserService
    {
        private List<User> _users = new();

        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;

            _users = _context.Users.ToList();
        }

        public AuthResponse Authenticate(AuthenticateModel model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // on auth fail: null is returned because user is not found
            // on auth success: user object is returned

            if (user == null)
            {
                throw new AppException("Username or password is incorrect.");
            }

            // auth success
            var token = GenerateJwtToken(user);

            return new AuthResponse(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
    }

}