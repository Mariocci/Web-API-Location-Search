using System.Collections.Generic;

namespace WebApiLocationSearch.Models;

public class GooglePlacesResponse
{
    public List<PlaceResult> Results { get; set; }
    public string Status { get; set; }
}

public class PlaceResult
{
    public Geometry Geometry { get; set; }
    public string Name { get; set; }
    public string Vicinity { get; set; }
    public List<string> Types { get; set; }
}

public class Geometry
{
    public Location Location { get; set; }
}

public class Location
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}
