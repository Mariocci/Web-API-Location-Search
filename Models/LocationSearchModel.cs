namespace WebApiLocationSearch.Models;

public class LocationSearchModel
{
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public int Radius { get; set; }
    public string Filter { get; set; }
}