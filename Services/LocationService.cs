using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Repositories;

namespace WebApiLocationSearch.Services;

public class LocationService
{
    private readonly LocationRepository _locationRepository;
    private readonly HttpClient _httpClient;
    private readonly UserRepository _userRepository;

    public LocationService(LocationRepository locationRepository, HttpClient httpClient, UserRepository userRepository)
    {
        _locationRepository = locationRepository;
        _httpClient = httpClient;
        _userRepository = userRepository;
    }

    public void SaveFavorite(FavoriteLocation location)
    {
        if (location == null)
        {
            throw new ArgumentNullException(nameof(location));
        }
        var user = _userRepository.GetUserById(location.UserId);
        if (user == null)
        {
            throw new InvalidOperationException($"No user found with ID {location.UserId}");
        }
        
        location.User = user; 
        _locationRepository.SaveFavorite(location);
    }

    public List<FavoriteLocation> GetFavorites(int userId, string category, string search)
    {
        return _locationRepository.GetFavorites(userId, category, search);
    }

    public async Task<List<LocationModel>> SearchNearbyLocations(LocationSearchModel locationSearchModel)
    {
            var apiKey = "YOUR_GOOGLE_PLACES_API_KEY";
            var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={locationSearchModel.Latitude},{locationSearchModel.Longitude}&radius=1500&type={locationSearchModel.Radius}&key={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while fetching places from Google Places API");
            }
            
            var jsonString = await response.Content.ReadAsStringAsync();
            var locationResponse = JsonSerializer.Deserialize<GooglePlacesResponse>(jsonString);
            
            var locations = locationResponse.Results.Select(result => new LocationModel
            {
                Name = result.Name,
                Address = result.Vicinity,
                Latitude = result.Geometry.Location.Lat,
                Longitude = result.Geometry.Location.Lng,
                Category = result.Types != null && result.Types.Any() ? string.Join(", ", result.Types) : "Unknown"
            }).ToList();

            return locations;
        
    }
}