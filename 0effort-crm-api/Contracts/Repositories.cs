using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Mongo.Entities;
using System.Linq.Expressions;

namespace _0effort_crm_api.Contracts.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }


    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(string cutomerId);

        Task CreateCustomerAsync(CreateOrUpdateCustomerDto model);

        Task<Customer> UpdateCustomerAsync(string id, CreateOrUpdateCustomerDto model);

        Task DeleteCustomerAsync(string id);
    }

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernamePasswordCombo(string username, string password);

        Task<User> GetUserByIdAsync(string userId);

        Task CreateUserAsync(CreateOrUpdateUserDto model);

        //Task<UserEntity> UpdateUserAsync(string id, CreateOrUpdateUserDto model);

        // Task DeleteUserAsync(string id);
    }

    public interface IOrderRepository : IRepository<Order>
    {
        Task CreateOrderAsync(CreateOrUpdateOrderDto model);

        Task<Order> GetOrderByIdAsync(string orderId);

        Task<List<Order>> GetOrdersFromCustomerId(string customerId);

        Task<Order[]> GetOrdersByMultipleIdsAsync(string[] orderIds);

        Task DeleteOrderAsync(string orderId);

        Task DeleteMultipleOrdersAsync(string[] orderIds);
    }
}
