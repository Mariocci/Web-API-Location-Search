using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApiLocationSearch.Hubs;

public class LocationHub : Hub
{
    public async Task SendLocationUpdate(string locationName)
    {
        await Clients.All.SendAsync("ReceiveLocationsUpdate", locationName);
    }
}