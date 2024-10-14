using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Repositories;

namespace WebApiLocationSearch.Services;

public class LocationService
{
    private readonly LocationRepository _locationRepository;

    public LocationService(LocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public void SaveFavorite(FavoriteLocation location)
    {
        _locationRepository.SaveFavorite(location);
    }

    public List<FavoriteLocation> GetFavorites(int userId, string category, string search)
    {
        return _locationRepository.GetFavorites(userId, category, search);
    }

    public async Task<List<LocationModel>> SearchNearbyLocations(LocationSearchModel locationSearchModel)
    {
        return null;//TODO
    }
}