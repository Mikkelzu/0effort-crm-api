using Microsoft.AspNetCore.Mvc;
using _0effort_crm_api.Core;
using _0effort_crm_api.Models;
using _0effort_crm_api.Auth;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0effort_crm_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public CustomerController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }


        // GET: api/<CustomerController>
        [Authorize]
        [HttpGet]
        public List<Customer> Get()
        {

            return _context.Customers.ToList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            var customer = _context.Customers.Find(id);

            return customer!;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
