using _0effort_crm_api.Contracts;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Mongo.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace _0effort_crm_api.Core.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<UserEntity>(MongoCollectionNames.Users);
        }

        public async Task<UserEntity> GetUserByUsernamePasswordCombo(string username, string password)
        {
            return await GetSingleAsync(x => x.Username == username && x.Password == password);
        }

        public Task AddAsync(UserEntity obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserEntity> GetAll()
        {
            return _users.AsQueryable();
        }

        public async Task<UserEntity> GetSingleAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            var filter = Builders<UserEntity>.Filter.Where(predicate);
            return (await _users.FindAsync(filter)).FirstOrDefault();
        }

        public Task UpdateAsync(UserEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
