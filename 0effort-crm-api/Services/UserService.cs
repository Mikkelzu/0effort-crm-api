using _0effort_crm_api.Core;
using _0effort_crm_api.Entities;

namespace _0effort_crm_api.Services
{

    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {


        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>();

        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            _users = _context.Users.ToList();

            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // on auth fail: null is returned because user is not found
            // on auth success: user object is returned
            return user;
        }
    }
}
