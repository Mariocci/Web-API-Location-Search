using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Services;

namespace WebApiLocationSearch.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly LocationService _locationService;

    public LocationController(LocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpPost("search-locations")]
    public async Task<IActionResult> GetLocations([FromBody] LocationSearchModel model)
    {
        var locations = await _locationService.SearchNearbyLocations(model);
        return Ok(locations);
    }
    [HttpPost("save-favorite")]
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

