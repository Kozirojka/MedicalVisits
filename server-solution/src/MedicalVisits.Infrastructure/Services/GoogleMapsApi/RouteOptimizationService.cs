using System.Text;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Configurations;
using MedicalVisits.Models.diraction;
using MedicalVisits.Models.diraction.models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RouteResponse = MedicalVisits.Models.diraction.RouteResponse;

namespace MedicalVisits.Infrastructure.Services.GoogleMapsApi;

public class RouteOptimizationService : IRouteOptimizationService
{
   private readonly HttpClient _httpClient;
   private readonly string _apiKey;

   public RouteOptimizationService(HttpClient httpClient, IOptions<GoogleMapsServiceSettings> settings)
   {
       _httpClient = httpClient;
       _apiKey = settings.Value.ApiKey;
       
       // Встановлюємо заголовки один раз в конструкторі
       _httpClient.DefaultRequestHeaders.Add("X-Goog-Api-Key", _apiKey);
       _httpClient.DefaultRequestHeaders.Add("X-Goog-FieldMask",
           "routes.duration,routes.distanceMeters,routes.optimizedIntermediateWaypointIndex,routes.legs.steps,routes.polyline.encodedPolyline");
   }

   public async Task<OptimizedRoute> GetOptimizedRouteAsync(Coordinate start, List<Coordinate> waypoints)
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
               }
           }).ToList(),
           travelMode = "DRIVE",
           optimizeWaypointOrder = true,
           computeAlternativeRoutes = false,
           routeModifiers = new
           {
               avoidTolls = false,
               avoidHighways = false
           },
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
           return ParseRouteResponse(routeResponse, start, waypoints);
       }
       catch (Exception ex)
       {
           throw new Exception($"Route optimization failed: {ex.Message}");
       }
   }

   
   private OptimizedRoute ParseRouteResponse(RouteResponse response, 
       Coordinate start,
       List<Coordinate> waypoints)
   {
       var route = response.Routes.FirstOrDefault();
       if (route == null)
           throw new Exception("No route found");

       // Отримуємо оптимізований порядок точок
       var optimizedWaypoints = new List<Coordinate>();
       optimizedWaypoints.Add(start);

       foreach (var index in route.OptimizedIntermediateWaypointIndex)
       {
           optimizedWaypoints.Add(waypoints[index]);
       }

       return new OptimizedRoute
       {
           OrderedWaypoints = optimizedWaypoints,
           TotalDistance = route.DistanceMeters,
           TotalDuration = route.Duration,
           Steps = route.Legs.SelectMany(l => l.Steps.Select(s => s.NavigationInstruction)).ToList()
       };
   }
}