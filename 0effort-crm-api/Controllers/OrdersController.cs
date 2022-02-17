using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Mongo.Entities;
using _0effort_crm_api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0effort_crm_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository _db;
        private readonly IValidator<CreateOrUpdateOrderDto> _modelValidator; // todo create validator

        public OrdersController(IDataService ds)
        {
            _db = ds.Orders;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _db.GetAll();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<Order> Get(string id)
        {
            return await _db.GetOrderByIdAsync(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<BaseResponseModel> Post([FromBody] CreateOrUpdateOrderDto model)
        {
            //todo create validator

            await _db.CreateOrderAsync(model);

            Response.StatusCode = (int)HttpStatusCode.Created;
            return new BaseResponseModel
            {
                IsSuccess = true,
            };
        }

        // GET api/<OrdersController>/customer/1
        [HttpGet("customer/{customerId}")]
        public async Task<List<Order>> GetAllByCustomerId(string customerId)
        {
            var orders =  await _db.GetOrdersFromCustomerId(customerId);

            if (orders.Count == 0)
            {
                return new List<Order>();
            }

            return orders;
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
