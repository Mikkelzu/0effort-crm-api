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
        private readonly IMongoCollection<UserEntity> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<UserEntity>(MongoCollectionNames.Users);
        }

        public async Task<UserEntity> GetUserByUsernamePasswordCombo(string username, string password)
        {
            return await GetSingleAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<UserEntity> GetUserByIdAsync(string userId)
        {
            return await GetSingleAsync(x => x.Id == userId);
        }

        public async Task CreateUserAsync(CreateOrUpdateUserDto model)
        {
            UserEntity user = new()
            {
                Username = model.Username,
                Password = model.Password,
            };

            await AddAsync(user);
        }

        #region IRepository implementation
        public async Task AddAsync(UserEntity obj)
        {
            await _users.InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            _ = await _users.DeleteOneAsync(predicate);
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

        public async Task<UserEntity> UpdateAsync(UserEntity obj)
        {
            var filter = Builders<UserEntity>.Filter.Where(x => x.Id == obj.Id);

            var updateDefBuilder = Builders<UserEntity>.Update;
            var updateDef = updateDefBuilder.Combine(new UpdateDefinition<UserEntity>[]
            {
                updateDefBuilder.Set(x => x.Username, obj.Username),
                updateDefBuilder.Set(x => x.Password, obj.Password),
            });
            await _users.FindOneAndUpdateAsync(filter, updateDef);

            return await _users.FindOneAndReplaceAsync(x => x.Id == obj.Id, obj);
        }

        Task IRepository<UserEntity>.UpdateAsync(UserEntity obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
