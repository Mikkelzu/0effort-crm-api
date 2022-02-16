using _0effort_crm_api.Contracts.Repositories;
using _0effort_crm_api.Core;
using _0effort_crm_api.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace _0effort_crm_api.Auth
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _db;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IDataService ds)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _db = ds.Users;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null) ValidateJwtToken(context, token);
               // AttachUserToContext(context, ds, token);

            await _next(context);
        }

        private void ValidateJwtToken(HttpContext ctx, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return user id from JWT token if validation successful
                ctx.Items["User"] = _db.GetSingleAsync(x => x.Id == userId);
            }
            catch
            {
                // do nothing if validation fails
            }
        }
    }
}
