using _0effort_crm_api.Contracts;
using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Mongo.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace _0effort_crm_api.Core.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<CustomerEntity> _customers;

        public CustomerRepository(IMongoDatabase database)
        {
            _customers = database.GetCollection<CustomerEntity>(MongoCollectionNames.Customers);
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(string customerId)
        {
            return await GetSingleAsync(x => x.Id == customerId);
        }

        public async Task CreateCustomerAsync(CreateOrUpdateCustomerDto model)
        {
            CustomerEntity customer = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                City = model.City,
                Postcode = model.Postcode,
                Country = model.Country,
                Phone = model.Phone,
            };

            await AddAsync(customer);
        }

        public async Task<CustomerEntity> UpdateCustomerAsync(string id, CreateOrUpdateCustomerDto model)
        {
            CustomerEntity customer = new()
            {
                Id = id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email= model.Email,
                Address = model.Address,
                City = model.City,
                Postcode = model.Postcode,
                Country = model.Country,
                Phone = model.Phone,
            };

            return await UpdateAsync(customer);
        }


        public async Task DeleteCustomerAsync(string id)
        {
            await DeleteAsync(x => x.Id == id);
        }

        #region IRepository implementation

        public async Task AddAsync(CustomerEntity obj)
        {
            await _customers.InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Expression<Func<CustomerEntity, bool>> predicate)
        {
            _ = await _customers.DeleteOneAsync(predicate);
        }

        public IQueryable<CustomerEntity> GetAll()
        {
            return _customers.AsQueryable();
        }

        public async Task<CustomerEntity> GetSingleAsync(Expression<Func<CustomerEntity, bool>> predicate)
        {
            var filter = Builders<CustomerEntity>.Filter.Where(predicate);
            return (await _customers.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<CustomerEntity> UpdateAsync(CustomerEntity obj)
        {
            var filter = Builders<CustomerEntity>.Filter.Where(x => x.Id == obj.Id);

            var updateDefBuilder = Builders<CustomerEntity>.Update;
            var updateDef = updateDefBuilder.Combine(new UpdateDefinition<CustomerEntity>[]
            {
                updateDefBuilder.Set(x => x.FirstName, obj.FirstName),
                updateDefBuilder.Set(x => x.LastName, obj.LastName),
                updateDefBuilder.Set(x => x.Email, obj.Email),
                updateDefBuilder.Set(x => x.Address, obj.Address),
                updateDefBuilder.Set(x => x.City, obj.City),
                updateDefBuilder.Set(x => x.Postcode, obj.Postcode),
                updateDefBuilder.Set(x => x.City, obj.City),
                updateDefBuilder.Set(x => x.Phone, obj.Phone)
            });
            await _customers.FindOneAndUpdateAsync(filter, updateDef);

            return await _customers.FindOneAndReplaceAsync(x => x.Id == obj.Id, obj);
        }

        Task IRepository<CustomerEntity>.UpdateAsync(CustomerEntity obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
