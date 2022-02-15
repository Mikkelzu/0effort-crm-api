using _0effort_crm_api.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Encodings;
using Microsoft.Extensions.Options;


namespace _0effort_crm_api.Auth
{
    public class JwtMongoDbMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMongoDbMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;

        }


        public async Task Invoke(HttpContext context)
        {

        }

    }
}
