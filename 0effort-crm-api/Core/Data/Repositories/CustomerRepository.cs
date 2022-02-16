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
        private readonly IMongoCollection<Customer> _customers;

        public CustomerRepository(IMongoDatabase database)
        {
            _customers = database.GetCollection<Customer>(MongoCollectionNames.Customers);
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await GetSingleAsync(x => x.Id == customerId);
        }

        public async Task CreateCustomerAsync(CreateOrUpdateCustomerDto model)
        {
            Customer customer = new()
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

        public async Task<Customer> UpdateCustomerAsync(string id, CreateOrUpdateCustomerDto model)
        {
            Customer customer = new()
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

        public async Task AddAsync(Customer obj)
        {
            await _customers.InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Expression<Func<Customer, bool>> predicate)
        {
            _ = await _customers.DeleteOneAsync(predicate);
        }

        public IQueryable<Customer> GetAll()
        {
            return _customers.AsQueryable();
        }

        public async Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> predicate)
        {
            var filter = Builders<Customer>.Filter.Where(predicate);
            return (await _customers.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<Customer> UpdateAsync(Customer obj)
        {
            var filter = Builders<Customer>.Filter.Where(x => x.Id == obj.Id);

            var updateDefBuilder = Builders<Customer>.Update;
            var updateDef = updateDefBuilder.Combine(new UpdateDefinition<Customer>[]
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

        Task IRepository<Customer>.UpdateAsync(Customer obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
