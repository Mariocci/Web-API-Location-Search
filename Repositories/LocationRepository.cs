using System.Collections.Generic;
using System.Linq;
using WebApiLocationSearch.Data;
using WebApiLocationSearch.Models;

namespace WebApiLocationSearch.Repositories;

public class LocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {   
        context = _context;
    }

    public List<FavoriteLocation> GetFavorites(int userId, string category, string search)
    {
        return _context.FavoriteLocations.Where(f => f.UserId == userId 
                                                     && (category == null || f.Category == category)
                                                     && (search == null || f.Name.Contains(search)))
                                                        .ToList();
    }

    public void SaveFavorite(FavoriteLocation location)
    {
        _context.FavoriteLocations.Add(location);
        _context.SaveChanges();
    }
}