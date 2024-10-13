using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebApiLocationSearch.Repositories;
using WebApiLocationSearch.Services;

namespace WebApiLocationSearch.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYHEADER = "X-API-KEY";
        private readonly UserRepository _userRepository;

        public ApiKeyMiddleware(RequestDelegate next, UserRepository userRepository)
        {
            _next = next;
            _userRepository = userRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           context.Request.EnableBuffering(); 
            
            var endpoint = context.GetEndpoint();
            if (endpoint == null || endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);
                return;
            }
            
            if (!context.Request.Headers.TryGetValue(APIKEYHEADER, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }
            
            var user = _userRepository.GetUserByApiKey(extractedApiKey);
            if (user == null)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }
            
            context.Items["UserId"] = user.Id;

            await _next(context);
        }
    }
}