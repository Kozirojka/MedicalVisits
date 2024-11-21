using System.Text;
using System.Text.Json;
using MediatR;
using MedicalVisits.Application.Admin.Queries.GetAllDoctors;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetListOfNearestDoctorQueryHandler : IRequestHandler<GetListOfNearestDoctorQuery, List<GetListOfNearestDoctorQueryHandler.DoctorProfileWithDistance>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public GetListOfNearestDoctorQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    
    public async Task<List<DoctorProfileWithDistance>> Handle(GetListOfNearestDoctorQuery request, CancellationToken cancellationToken)
    {
        
        
        // Підготовка адреси пацієнта
        var patientAddress = $"{request.Address.Street}, {request.Address.City}, {request.Address.State}, {request.Address.Country}";
        var patientCoordinates = await GetCoordinatesFromAddress(patientAddress);

        
        Console.WriteLine($"Coordinates: {patientCoordinates}");
        if (patientCoordinates == null)
            throw new Exception("Не вдалося визначити координати пацієнта");
        
        
        
        //Повертаєм список лікарів, які живуть у цьому місті
        var doctors = await _dbContext.DoctorProfiles
            .Include(d => d.User)
            .Where(d => d.User.Address.Country == request.Address.Country 
                        && d.User.Address.City == request.Address.City)
            .ToListAsync();


        if (doctors == null)
        {
            throw new Exception("Не знайдено лікарів");
        }
        
        //Тепер потрібно визначити який лікар живе найближче
        var doctorDistances = new List<DoctorProfileWithDistance>();
        
        foreach (var doctor in doctors)
        {
            var doctorAddress = $"{doctor.User.Address.Street}, {doctor.User.Address.City}, {doctor.User.Address.Region}, {doctor.User.Address.Country}";
            var doctorCoordinates = await GetCoordinatesFromAddress(doctorAddress);

            if (doctorCoordinates == null)
                continue; // Пропускаємо лікарів, для яких не вдалося визначити координати

            // Розрахунок маршруту
            var distance = await GetDistanceBetweenCoordinates(patientCoordinates, doctorCoordinates);

            if (distance != null)
            {
                doctorDistances.Add(new DoctorProfileWithDistance
                {
                    Doctor = doctor,
                    Distance = distance.Value
                });
            }
        }
        
        // Сортування за відстанню
        return doctorDistances.OrderBy(d => d.Distance).ToList();
        
        //Тепер мені потрібно  створити запит який буде визначати який лікар живе найближче до
        //пацієнта, є декілька варіантів, перший це є за одни запит визначити,
        //а потім порівняти
        //Другий варіант це є за декілька зпитів визначити та порівняти
        
        
    }
    public class DoctorProfileWithDistance
    {
        public DoctorProfile Doctor { get; set; }
        public double Distance { get; set; } // Відстань у метрах
    }
    
    // Отримання координат для адреси
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
    
    // Отримання відстані між двома координатами
    private async Task<double?> GetDistanceBetweenCoordinates(List<double> startCoordinates, List<double> endCoordinates)
    {
        string apiKey = "5b3ce3597851110001cf62486b87172ca31c4ca19b59ce2e1f809ad6";
        string endpoint = "https://api.openrouteservice.org/v2/directions/driving-car";

        var requestBody = new
        {
            coordinates = new List<List<double>> { startCoordinates, endCoordinates },
            profile = "driving-car",
            format = "json"
        };

        using var client = new HttpClient();
        var response = await client.PostAsync(
            endpoint,
            new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        );

        if (!response.IsSuccessStatusCode) return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(jsonString);

        return json.RootElement
            .GetProperty("routes")[0]
            .GetProperty("summary")
            .GetProperty("distance")
            .GetDouble();
    }
}
