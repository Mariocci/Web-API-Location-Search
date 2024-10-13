using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApiLocationSearch.Data;
using WebApiLocationSearch.Models;

namespace WebApiLocationSearch.Repositories;

public class LoggingRepository  
{
    private readonly AppDbContext _dbContext;
    
    public LoggingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveLogAsync(Log log)
    {
        await _dbContext.Logs.AddAsync(log);
        await _dbContext.SaveChangesAsync();
    }
    public async Task LogUserRegistration(User user, HttpContext context)
    {
        var logEntry = new Log
        {
            Method = "POST",
            Endpoint = "/login/register",
            Body = $"User registered: {user.Username}",
            Timestamp = DateTime.UtcNow,
            LogType = "Request",
            IpAddress = context.Connection.RemoteIpAddress?.ToString()
        };

        await SaveLogAsync(logEntry);
    }
}