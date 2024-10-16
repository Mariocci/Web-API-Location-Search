using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "e724ae57-2b23-4115-a384-16b79a574072"; 

        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/locationHub", options =>
            {
                options.Headers.Add("X-API-KEY", apiKey); 
            })
            .Build();

        connection.On<string>("ReceiveLocationsUpdate", (location) =>
        {
            Console.WriteLine($"Locations update received: {location}");
        });

        await connection.StartAsync();
        Console.WriteLine("Connected to SignalR hub.");

        Console.ReadLine();
    }
}
