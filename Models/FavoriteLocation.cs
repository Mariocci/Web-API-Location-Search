namespace WebApiLocationSearch.Models;

public class FavoriteLocation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Category { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}