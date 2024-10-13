using System;

namespace WebApiLocationSearch.Models;

public class Log
{
    public int LogId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Method { get; set; }
    public string Endpoint { get; set; }
    public string IpAddress { get; set; }
    public string Body { get; set; } 
    public string LogType { get; set; }
    
    public int? UserId { get; set; }
    public User User { get; set; }
    
}