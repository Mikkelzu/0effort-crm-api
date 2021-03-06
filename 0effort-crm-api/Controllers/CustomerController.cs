using Microsoft.AspNetCore.Mvc;
using _0effort_crm_api.Core;
using _0effort_crm_api.Auth;
using Microsoft.Extensions.Options;
using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Services;
using System.Net;
using _0effort_crm_api.Mongo.Entities;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0effort_crm_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _db;
        private readonly IValidator<CreateOrUpdateCustomerDto> _modelValidator;

        public CustomerController(IDataService ds, IValidator<CreateOrUpdateCustomerDto> modelValidator)
        {
            _db = ds.Customers;
            _modelValidator = modelValidator;
        }


        // GET: api/<CustomerController>
        [Authorize]
        [HttpGet()]
        public IEnumerable<Customer> Get()
        {
            return _db.GetAll();
        }

        // GET api/<CustomerController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Customer> Get(string id)
        {
           return await _db.GetCustomerByIdAsync(id);
        }

        // POST api/<CustomerController>
        [Authorize]
        [HttpPost]
        public async Task<BaseResponseModel> PostAsync([FromBody] CreateOrUpdateCustomerDto model)
        {
            var result = _modelValidator.Validate(model);

            if (!result.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new BaseResponseModel
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            await _db.CreateCustomerAsync(model);

            Response.StatusCode = (int)HttpStatusCode.Created;
            return new BaseResponseModel
            {
                IsSuccess = true
            };
        }

        // PUT api/<CustomerController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<CustomerResponseModel> Put(string id, [FromBody] CreateOrUpdateCustomerDto model)
        {
            var result = _modelValidator.Validate(model);

            if (!result.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new CustomerResponseModel
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            return new CustomerResponseModel
            {
                IsSuccess = true,
                Response = await _db.UpdateCustomerAsync(id, model)
            };
        }

        // DELETE api/<CustomerController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _db.DeleteCustomerAsync(id);
        }
    }
}
