using _0effort_crm_api.Auth;
using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Core;
using _0effort_crm_api.Models;
using _0effort_crm_api.Mongo.Entities;
using _0effort_crm_api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace _0effort_crm_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private AppSettings _appSettings;
        private IUserRepository _db;
        private readonly IValidator<CreateOrUpdateUserDto> _modelValidator;

        public AuthController(IOptions<AppSettings> appSettings, IDataService ds, IValidator<CreateOrUpdateUserDto> modelValidator)
        {
            _appSettings = appSettings.Value;
            _modelValidator = modelValidator;
            _db = ds.Users;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<UserResponseModel> Login([FromBody] CreateOrUpdateUserDto model)
        {

            var result = _modelValidator.Validate(model);

            if (!result.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new UserResponseModel
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            var user = await _db.GetUserByUsernamePasswordCombo(model.Username, model.Password);

            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new UserResponseModel
                {
                    IsSuccess = false,
                    Error = "User not found"
                };
            }

            Response.StatusCode = (int)HttpStatusCode.Created;

            var token = GenerateJwtToken(user);

            return new UserResponseModel
            {
                IsSuccess = true,
                Token = token,
            };
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserResponseModel> Register([FromBody] CreateOrUpdateUserDto model)
        {
            // todo create a validator for unique username & email
            var result = _modelValidator.Validate(model);

            if (!result.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new UserResponseModel
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            await _db.CreateUserAsync(model);

            return new UserResponseModel
            {
                IsSuccess = true
            };
        }


        [HttpGet("user/{id}")]
        public async Task<UserEntity> GetUserById(string id)
        {
            return await _db.GetUserByIdAsync(id);
        }


        [HttpGet("users")]
        public IEnumerable<UserEntity> Get()
        {
            return _db.GetAll();
        }

        private string GenerateJwtToken(UserEntity user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
