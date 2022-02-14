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


    public interface ICustomerRepository : IRepository<CustomerEntity>
    {
        Task<CustomerEntity> GetCustomerByIdAsync(string cutomerId);

        Task CreateCustomerAsync(CreateOrUpdateCustomerDto model);

        Task<CustomerEntity> UpdateCustomerAsync(string id, CreateOrUpdateCustomerDto model);

        Task DeleteCustomerAsync(string id);
    }

    public interface IUserRepository : IRepository<UserEntity>
    {

        Task<UserEntity> GetUserByUsernamePasswordCombo(string username, string password);

        // todo create these methods better

        //Task<UserEntity> GetUserByIdAsync(string userId);

        //Task CreateUserAsync(CreateOrUpdateUserDto model);

        //Task<UserEntity> UpdateUserAsync(string id, CreateOrUpdateUserDto model);

        //Task DeleteUserAsync(string id);
    }
}
