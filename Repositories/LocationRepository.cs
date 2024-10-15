using System.Collections.Generic;
using System.Linq;
using WebApiLocationSearch.Data;
using WebApiLocationSearch.Models;

namespace WebApiLocationSearch.Repositories;

public class LocationRepository
{
    private readonly AppDbContext _dbContext;

    public LocationRepository(AppDbContext context)
    {   
        _dbContext = context;
    }

    public List<FavoriteLocation> GetFavorites(int userId, string category, string search)
    {
        return _dbContext.FavoriteLocations.Where(f => f.UserId == userId 
                                                       && (category == null || f.Category == category)
                                                       && (search == null || f.Name.Contains(search)))
                                                        .ToList();
    }

    public void SaveFavorite(FavoriteLocation location)
    {
        _dbContext.FavoriteLocations.Add(location);
        _dbContext.SaveChanges();
    }
}