using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using MedicalVisits.Infrastructure.Services.Interfaces;
 using MedicalVisits.Models.Configurations;
 using MedicalVisits.Models.diraction;
 using MedicalVisits.Models.diraction.models;
 using Microsoft.Extensions.Options;
 using Newtonsoft.Json;
 using RouteResponse = MedicalVisits.Models.diraction.RouteResponse;
 
 namespace MedicalVisits.Infrastructure.Services.GoogleMapsApi;

 public class RouteService : IRouteService
 {
     private readonly HttpClient _httpClient;
     private readonly string _apiKey;

     public RouteService(HttpClient httpClient, IOptions<GoogleMapsServiceSettings> settings)
     {
         _httpClient = httpClient;
         _apiKey = settings.Value.ApiKey;

         _httpClient.DefaultRequestHeaders.Add("X-Goog-Api-Key", _apiKey);
         _httpClient.DefaultRequestHeaders.Add("X-Goog-FieldMask",
             "routes.optimizedIntermediateWaypointIndex,routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline");
     }

     public async Task<RouteResponse?> GetOptimizedRouteAsync(Coordinate start, List<Coordinate> waypoints)
     {
         var url = "https://routes.googleapis.com/directions/v2:computeRoutes";

         var request = new
         {
             origin = new
             {
                 location = new
                 {
                     latLng = new { latitude = start.Latitude, longitude = start.Longitude }
                 }
             },
             destination = new
             {
                 location = new
                 {
                     latLng = new { latitude = waypoints.Last().Latitude, longitude = waypoints.Last().Longitude }
                 }
             },
             intermediates = waypoints.Take(waypoints.Count - 1).Select(w => new
             {
                 location = new
                 {
                     latLng = new { latitude = w.Latitude, longitude = w.Longitude }
                 },
             }).ToList(),
             travelMode = "DRIVE",
             optimizeWaypointOrder = false,
             computeAlternativeRoutes = false,
             languageCode = "uk-UA"
         };

         try
         {
             var jsonRequest = JsonConvert.SerializeObject(request);
             var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

             var response = await _httpClient.PostAsync(url, requestContent);
             var responseContent = await response.Content.ReadAsStringAsync();

             if (!response.IsSuccessStatusCode)
             {
                 throw new Exception($"API Error: {responseContent}");
             }

             var routeResponse = JsonConvert.DeserializeObject<RouteResponse>(responseContent);

             return routeResponse;
         }
         catch (Exception ex)
         {
             throw new Exception($"Route optimization failed: {ex.Message}");
         }
     }

     public async Task<double> GetDistanceBetweenTwoPoints(Coordinate startPoint, Coordinate endPoint)
     {
         try
         {
             // Формування URL для запиту
             string url = $"https://routes.googleapis.com/directions/v2:computeRoutes";

             // Тіло запиту у форматі JSON
             var requestBody = new
             {
                 origin = new
                 {
                     location = new
                     {
                         latLng = new
                         {
                             latitude = startPoint.Latitude,
                             longitude = startPoint.Longitude
                         }
                     }
                 },
                 destination = new
                 {
                     location = new
                     {
                         latLng = new
                         {
                             latitude = endPoint.Latitude,
                             longitude = endPoint.Longitude
                         }
                     }
                 },
                 travelMode = "DRIVE",
                 routingPreference = "TRAFFIC_AWARE",
                 computeAlternativeRoutes = false
             };

             var response = await _httpClient.PostAsJsonAsync(url, requestBody);

             response.EnsureSuccessStatusCode();

             var result = await response.Content.ReadFromJsonAsync<GoogleMapsResponse>();

             if (result?.Routes == null || !result.Routes.Any())
             {
                 throw new Exception("Не знайдено маршрутів між вказаними точками");
             }

             return result.Routes.First().DistanceMeters / 1000.0;
         }
         catch (HttpRequestException ex)
         {
             throw new Exception($"Помилка при запиті до Google Maps API: {ex.Message}", ex);
         }
         catch (Exception ex)
         {
             throw new Exception($"Помилка при обчисленні відстані: {ex.Message}", ex);
         }
     }
 }


 // Клас для десеріалізації відповіді
 public class GoogleMapsResponse
 {
     public List<Route> Routes { get; set; }
 }

 public class Route
 {
     [JsonPropertyName("distanceMeters")]
     public double DistanceMeters { get; set; }
 }
