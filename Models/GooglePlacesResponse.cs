using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApiLocationSearch.Models
{
    public class GooglePlacesResponse
    {
        [JsonProperty("results")] public List<PlaceResult> Results { get; set; }

        [JsonProperty("next_page_token")] public string NextPageToken { get; set; }

        [JsonProperty("html_attributions")] public List<string> HtmlAttributions { get; set; }
    }

    public class PlaceResult
    {
        [JsonProperty("geometry")] public Geometry Geometry { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("vicinity")] public string Vicinity { get; set; }

        [JsonProperty("types")] public List<string> Types { get; set; }

        [JsonProperty("icon")] public string Icon { get; set; }

        [JsonProperty("photos")] public List<Photo> Photos { get; set; }

        [JsonProperty("place_id")] public string PlaceId { get; set; }

        [JsonProperty("business_status")] public string BusinessStatus { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")] public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")] public double Lat { get; set; }

        [JsonProperty("lng")] public double Lng { get; set; }
    }

    public class Photo
    {
        [JsonProperty("height")] public int Height { get; set; }

        [JsonProperty("width")] public int Width { get; set; }

        [JsonProperty("photo_reference")] public string PhotoReference { get; set; }

        [JsonProperty("html_attributions")] public List<string> HtmlAttributions { get; set; }
    }
}