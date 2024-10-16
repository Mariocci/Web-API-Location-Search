using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WebApiLocationSearch.Hubs;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Services;

namespace WebApiLocationSearch.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly LocationService _locationService;
    private readonly IHubContext<LocationHub> _hubContext;

    public LocationController(LocationService locationService, IHubContext<LocationHub> hubContext)
    {
        _locationService = locationService;
        _hubContext = hubContext;
    }
    
    [HttpPost("search-locations")]
    public async Task<IActionResult> GetLocations([FromBody] LocationSearchModel model)
    {   
        if (model.Latitude < -90 || model.Latitude > 90 || model.Longitude < -180 || model.Longitude > 180)
        {
            return BadRequest(new { message = "Invalid latitude or longitude." });
        }
        if (model.Radius <= 0)
        {
            return BadRequest(new { message = "Invalid radius. It must be greater than zero." });
        }
        var locations = await _locationService.SearchNearbyLocations(model);
        
        if (locations == null || !locations.Any())
        {
            return NotFound(new { message = "No locations found." });
        }
        var jsonLocations = JsonSerializer.Serialize(locations);
        await _hubContext.Clients.All.SendAsync("ReceiveLocationsUpdate", jsonLocations);
        return Ok(locations);
    }
    [HttpPut("save-favorite")]
    public IActionResult SaveFavorite([FromBody] FavoriteLocation model)
    {
        _locationService.SaveFavorite(model);
        return Ok();
    }
    
    [HttpGet("get-favorites")]
    public IActionResult GetFavoriteLocations(int userId, string category = null, string search = null)
    {
        var favorites = _locationService.GetFavorites(userId, category, search);
        return Ok(favorites);
    }
    

    
}

