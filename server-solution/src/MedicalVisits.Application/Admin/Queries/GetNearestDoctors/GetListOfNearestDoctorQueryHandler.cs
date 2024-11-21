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
    
    
    /*
     * Рекомендації для оптимізації:
Кешування результатів: Зберігайте результати API-запитів, щоб уникнути повторних звернень.
Групування запитів: Використовуйте Matrix API для розрахунку між кількома точками одночасно, а не поодинокі запити.
Оптимізація даних: Скоротіть кількість точок або уточніть географічні зони для запитів.
     */
    public async Task<List<DoctorProfileWithDistance>> Handle(GetListOfNearestDoctorQuery request, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Red; // Встановлюємо червоний колір тексту
        Console.OutputEncoding = Encoding.UTF8;
        // Підготовка адреси пацієнта
        
        //Працює 100 процентів, якщо брати вулиці англійською мовою,
        //щоб найти, наприклад вулицю княгині
        //ольги потрібно у вулиці ввести 
        // Kniahyni Olhy Street
        
        var patientAddress = $"{request.Address.Street}, {request.Address.City}, {request.Address.State}, {request.Address.Country}";

        //Console.WriteLine($"{patientAddress} patient adresses");
        
        var patientCoordinates = await GetCoordinatesFromAddress(patientAddress);

        
        
            //Console.WriteLine($"Coordinates: {patientCoordinates[0]} + {patientCoordinates[1]} -------------------------------");
        if (patientCoordinates == null)
            throw new Exception("Do not can find the coodicnate if user");
        
        
        
        //Повертаєм список лікарів, які живуть у цьому місті
        var doctors = await _dbContext.DoctorProfiles
            .Include(d => d.User)
            .Where(d => d.User.Address.Country == request.Address.Country 
                        && d.User.Address.City == request.Address.City)
            .ToListAsync();


        //foreach (var doctor in doctors)
         //   Console.WriteLine(doctor.User.FirstName);
        
        
        if (doctors == null)
        {
            throw new Exception("Не знайдено лікарів");
        }
        
        //Тепер потрібно визначити який лікар живе найближче
        var doctorDistances = new List<DoctorProfileWithDistance>();
        
        foreach (var doctor in doctors)
        {
            int i = 0; 
            
            var doctorAddress = $"{doctor.User.Address.Street}, {doctor.User.Address.City}, {doctor.User.Address.Region}, {doctor.User.Address.Country}";
            var doctorCoordinates = await GetCoordinatesFromAddress(doctorAddress);


           // Console.WriteLine($"{doctorAddress}, {doctorCoordinates}");
           // Console.WriteLine($"{doctorCoordinates[0]} +++++++{doctorCoordinates[1]}");
            
            
            if (doctorCoordinates == null)
                continue;

            // Розрахунок маршруту
            var distance = await GetDistanceBetweenCoordinates(patientCoordinates, doctorCoordinates);

            Console.WriteLine("Дистанція: " + distance);
            
            if (distance != null)
            {
                doctorDistances.Add(new DoctorProfileWithDistance
                {
                    Doctor = doctor,
                    Distance = distance.Value
                });
            }

            i++;
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
