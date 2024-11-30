using System.Text;
using System.Text.Json;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models;
using MedicalVisits.Models.Configurations;
using Microsoft.Extensions.Options;

namespace MedicalVisits.Infrastructure.Services.Implementation;

public class GeocodingService : IGeocodingService
{
    public readonly OpenRouteServiceSettings _settings;

    public GeocodingService(IOptions<OpenRouteServiceSettings> settings)
    {
        _settings = settings.Value;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="address"> $"{doctor.User.Address.Street},
    /// {doctor.User.Address.City}, {doctor.User.Address.Region}, {doctor.User.Address.Country}";</param>
    /// <returns></returns>
     public async Task<List<double>?> GetCoordinatesFromAddress(Address _address)
     {
         string address =_address.ToString();
         
        string geocodeUrl = $"https://api.openrouteservice.org/geocode/search?api_key={_settings.ApiKey}&text={Uri.EscapeDataString(address)}";

        using var client = new HttpClient();
        var response = await client.GetAsync(geocodeUrl);
        if (!response.IsSuccessStatusCode) return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(jsonString);

        var coordinates = json.RootElement
            .GetProperty("features")[0]
            .GetProperty("geometry")
            .GetProperty("coordinates");

        return new List<double> { coordinates[0].GetDouble(), coordinates[1].GetDouble() };
    }

   

    public async Task<double?> GetDistanceBetweenCoordinates(List<double> startCoordinates, List<double> endCoordinates)
    {
        string apiKey = _settings.ApiKey;
        string endpoint = "https://api.openrouteservice.org/v2/directions/driving-car";

        // Логування координат
        Console.WriteLine($"Start Coordinates: {startCoordinates[0]}, {startCoordinates[1]}");
        Console.WriteLine($"End Coordinates: {endCoordinates[0]}, {endCoordinates[1]}");

        var requestBody = new
        {
            coordinates = new List<List<double>> { startCoordinates, endCoordinates },
            profile = "driving-car",
            format = "json"
        };

        using var client = new HttpClient();

        
        
        //отірбно запам'ятати, про те, щоб потрібно в хедер деколи закидати автентифікацію, та ключ по якому ми
        //доступаємось до якогось апі
        client.DefaultRequestHeaders.Add("Authorization", apiKey);

        var response = await client.PostAsync(
            endpoint,
            new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        );

        var directionsString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Directions Response: " + directionsString);

        if (!response.IsSuccessStatusCode) return null;

        var json = JsonDocument.Parse(directionsString);

        if (json.RootElement.TryGetProperty("routes", out var routes) && routes.GetArrayLength() > 0)
        {
            var distance = routes[0].GetProperty("summary").GetProperty("distance").GetDouble();
            return distance;
        }
        else
        {
            Console.WriteLine("No route found or invalid response.");
            return null;
        }
    }
}
