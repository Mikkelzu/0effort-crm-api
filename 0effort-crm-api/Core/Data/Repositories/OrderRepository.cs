using _0effort_crm_api.Contracts;
using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Mongo.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace _0effort_crm_api.Core.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>(MongoCollectionNames.Orders);
        }

        public async Task CreateOrderAsync(CreateOrUpdateOrderDto model)
        {
            Order order = new()
            {
                OrderDate = model.OrderDate,
                OrderDescription = model.OrderDescription,
                CustomerId = model.Customer.Id,
                DeliveryAddress = model.DeliveryAddress,
                DeliveryPostcode = model.DeliveryPostcode,
                DeliveryCity = model.DeliveryCity,
                DeliveryCountry = model.DeliveryCountry,
            };

            await AddAsync(order);
        }

        public Task<Order> GetOrderByIdAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order[]> GetOrdersByMultipleIdsAsync(string[] orderIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMultipleOrdersAsync(string[] orderIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        #region IRepository Implementation
        public async Task AddAsync(Order obj)
        {
            await _orders.InsertOneAsync(obj);
        }

        public Task DeleteAsync(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            return _orders.AsQueryable();
        }

        public async Task<Order> GetSingleAsync(Expression<Func<Order, bool>> predicate)
        {
            var filter = Builders<Order>.Filter.Where(predicate);
            return (await _orders.FindAsync(filter)).FirstOrDefault();
        }

        public Task UpdateAsync(Order obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
