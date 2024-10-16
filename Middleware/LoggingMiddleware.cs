using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Repositories;

namespace WebApiLocationSearch.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LoggingRepository _loggingRepository;

        public LoggingMiddleware(RequestDelegate next, LoggingRepository loggingRepository)
        {
            _next = next;
            _loggingRepository = loggingRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            string requestBody = string.Empty;
            if (context.Request.ContentLength > 0)
            {
                requestBody = await ReadRequestBodyAsync(context);
            }
            
            if (context.Request.Path == "/api/users/register" && context.Request.Method == "POST")
            {
                await LogRegistrationAsync(context, requestBody);
                
            }
            else
            {
                var originalResponseBodyStream = context.Response.Body;
                using var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                await _next(context);

                var responseBody = await ReadResponseBodyAsync(responseBodyStream);

                await LogRequestAndResponseAsync(context, requestBody, responseBody);

                responseBodyStream.Position = 0;
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            return body;
        }

        private async Task<string> ReadResponseBodyAsync(MemoryStream responseBodyStream)
        {
            responseBodyStream.Position = 0; 
            using var reader = new StreamReader(responseBodyStream, leaveOpen: true); 
            var body = await reader.ReadToEndAsync();
            responseBodyStream.Position = 0;
            return body;
        }

        private async Task LogRequestAndResponseAsync(HttpContext context, string requestBody, string responseBody)
        {
            int? userId = context.Items["UserId"] as int?;
            var requestLogEntry = new Log
            {
                Method = context.Request.Method,
                Endpoint = context.Request.Path,
                Body = requestBody,
                Timestamp = DateTime.UtcNow,
                LogType = "Request",
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                UserId = userId
            };

            await _loggingRepository.SaveLogAsync(requestLogEntry);

            var responseLogEntry = new Log
            {
                Method = context.Request.Method,
                Endpoint = context.Request.Path,
                Body = responseBody,
                Timestamp = DateTime.UtcNow,
                LogType = "Response",
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                UserId = userId
            };

            await _loggingRepository.SaveLogAsync(responseLogEntry);
        }
        
        private async Task LogRegistrationAsync(HttpContext context, string requestBody)
        {
            var registrationLogEntry = new Log
            {
                Method = "POST",
                Endpoint = "/login/register",
                Body = requestBody,
                Timestamp = DateTime.UtcNow,
                LogType = "Registration",
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                UserId = null
            };

            await _loggingRepository.SaveLogAsync(registrationLogEntry);
        }
    }
}
