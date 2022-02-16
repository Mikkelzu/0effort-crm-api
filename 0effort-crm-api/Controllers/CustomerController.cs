using Microsoft.AspNetCore.Mvc;
using _0effort_crm_api.Core;
using _0effort_crm_api.Models;
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
    [Authorize]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly AppSettings _appSettings;
        private readonly ICustomerRepository _db;
        private readonly IValidator<CreateOrUpdateCustomerDto> _modelValidator;

        public CustomerController(IOptions<AppSettings> appSettings, IDataService ds, IValidator<CreateOrUpdateCustomerDto> modelValidator)
        {
            _db = ds.Customers;
            _appSettings = appSettings.Value;
            _modelValidator = modelValidator;
        }


        // GET: api/<CustomerController>
        [HttpGet()]
        public IEnumerable<CustomerEntity> Get()
        {
            return _db.GetAll();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<CustomerEntity> Get(string id)
        {
           return await _db.GetCustomerByIdAsync(id);
        }

        // POST api/<CustomerController>
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
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _db.DeleteCustomerAsync(id);
        }
    }
}
