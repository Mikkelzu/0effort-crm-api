using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Core;
using _0effort_crm_api.Core.Data.Repositories;

namespace _0effort_crm_api.Services
{
    public interface IDataService
    {
        public ICustomerRepository Customers { get; }
        public IUserRepository Users { get; }
    }

    public class DataService: IDataService
    {
        private readonly MongoContext _dbContext;

        public DataService(MongoContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public ICustomerRepository Customers => new CustomerRepository(_dbContext.Database);
        public IUserRepository Users => new UserRepository(_dbContext.Database);
    }
}
