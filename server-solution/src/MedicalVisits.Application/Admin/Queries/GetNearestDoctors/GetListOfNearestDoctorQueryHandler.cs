using System.Text;
using System.Text.Json;
using MediatR;
using MedicalVisits.Application.Admin.Queries.GetAllDoctors;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.GoogleMapsApi;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetListOfNearestDoctorQueryHandler : IRequestHandler<GetListOfNearestDoctorQuery, List<GetListOfNearestDoctorQueryHandler.DoctorProfileWithDistance>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IGGeocodingService _geocodingService; 
    private readonly IRouteOptimizationService _routeOptimizationService;
    
    public GetListOfNearestDoctorQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext, IGGeocodingService geocodingService, IRouteOptimizationService routeOptimizationService)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _geocodingService = geocodingService;
        _routeOptimizationService = routeOptimizationService;
    }
    
    
    /*
     * Рекомендації для оптимізації:
Кешування результатів: Зберігайте результати API-запитів, щоб уникнути повторних звернень.
Групування запитів: Використовуйте Matrix API для розрахунку між кількома точками одночасно, а не поодинокі запити.
Оптимізація даних: Скоротіть кількість точок або уточніть географічні зони для запитів.
     */
    public async Task<List<DoctorProfileWithDistance>> Handle(GetListOfNearestDoctorQuery request, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.OutputEncoding = Encoding.UTF8;
        var visitRequest = await _dbContext.VisitRequests
            .Include(v => v.Patient) // Завантаження пов'язаного пацієнта
            .ThenInclude(p => p.Address) // Завантаження адреси пацієнта (якщо потрібно)
            .FirstOrDefaultAsync(v => v.Id ==  request.requestId, cancellationToken);


        if (visitRequest == null)
        {
            Console.WriteLine("-----------------No Visit Request------------------------------------------------------");
        }
        else
        {
            Console.WriteLine(visitRequest.Description + "------------------------------");
        }
        // Отримати пацієнта
        var patientId = visitRequest.PatientId;
        var patient = await _userManager.FindByIdAsync(patientId);
        
        var patientAddress = patient?.Address;
        
        
         
        var patientCoordinates = await _geocodingService.GeocodeAddressAsync(patientAddress);

        
        
        
            //Console.WriteLine($"Coordinates: {patientCoordinates[0]} + {patientCoordinates[1]} -------------------------------");
        if (patientCoordinates.Latitude == 0 && patientCoordinates.Longitude == 0)
            throw new Exception("Do not can find the coordinate if user");
        
        
        
        var doctors = await _dbContext.DoctorProfiles
            .Include(d => d.User)
            .Where(d => d.User.Address.Country == patientAddress.Country 
                        && d.User.Address.City == patientAddress.City)
            .ToListAsync(cancellationToken: cancellationToken);


        
        if (doctors == null)
        {
            throw new Exception("Не знайдено лікарів");
        }
        
        var doctorDistances = new List<DoctorProfileWithDistance>();
        
        foreach (var doctor in doctors)
        {
            int i = 0; 
            
            var doctorCoordinates = await _geocodingService.GeocodeAddressAsync(doctor.User.Address);

            
            if (doctorCoordinates.Longitude == 0 && doctorCoordinates.Latitude == 0)
                continue;

            var distance = _routeOptimizationService.GetDistanceBetweenTwoPoints(new Coordinate()
            {
                Latitude = patientCoordinates.Latitude,
                Longitude = patientCoordinates.Longitude
                
            }, new Coordinate()
            {
                Latitude = doctorCoordinates.Latitude,
                Longitude = doctorCoordinates.Longitude
            });

            Console.WriteLine("Дистанція: " + distance);
            
            if (distance.Result != null)
            {
                doctorDistances.Add(new DoctorProfileWithDistance
                {
                    Doctor = doctor,
                    Distance = distance.Result
                });
            }

            i++;
        }
        
        return doctorDistances.OrderBy(d => d.Distance).ToList();
        
    }
    public class DoctorProfileWithDistance
    {
        public DoctorProfile Doctor { get; set; }
        public double Distance { get; set; } 
    }
    
    private async Task<List<double>?> GetCoordinatesFromAddress(string address)
    {
        string apiKey = "5b3ce3597851110001cf62486b87172ca31c4ca19b59ce2e1f809ad6";
        string geocodeUrl = $"https://api.openrouteservice.org/geocode/search?api_key={apiKey}&text={Uri.EscapeDataString(address)}";

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
    
    private async Task<double?> GetDistanceBetweenCoordinates(List<double> startCoordinates, List<double> endCoordinates)
    {
        string apiKey = "5b3ce3597851110001cf62486b87172ca31c4ca19b59ce2e1f809ad6";
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
