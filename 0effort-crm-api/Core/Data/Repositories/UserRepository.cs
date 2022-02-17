using _0effort_crm_api.Contracts;
using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Mongo.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace _0effort_crm_api.Core.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>(MongoCollectionNames.Users);
        }

        public async Task<User> GetUserByUsernamePasswordCombo(string username, string password)
        {
            return await GetSingleAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await GetSingleAsync(x => x.Id == userId);
        }



        public async Task CreateUserAsync(CreateOrUpdateUserDto model)
        {
            User user = new()
            {
                Username = model.Username,
                Password = model.Password,
            };

            await AddAsync(user);
        }

        #region IRepository implementation
        public async Task AddAsync(User obj)
        {
            await _users.InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Expression<Func<User, bool>> predicate)
        {
            _ = await _users.DeleteOneAsync(predicate);
        }

        public IQueryable<User> GetAll()
        {
            return _users.AsQueryable();
        }

        public async Task<User> GetSingleAsync(Expression<Func<User, bool>> predicate)
        {
            var filter = Builders<User>.Filter.Where(predicate);
            return (await _users.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<User> UpdateAsync(User obj)
        {
            var filter = Builders<User>.Filter.Where(x => x.Id == obj.Id);

            var updateDefBuilder = Builders<User>.Update;
            var updateDef = updateDefBuilder.Combine(new UpdateDefinition<User>[]
            {
                updateDefBuilder.Set(x => x.Username, obj.Username),
                updateDefBuilder.Set(x => x.Password, obj.Password),
            });
            await _users.FindOneAndUpdateAsync(filter, updateDef);

            return await _users.FindOneAndReplaceAsync(x => x.Id == obj.Id, obj);
        }

        Task IRepository<User>.UpdateAsync(User obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
