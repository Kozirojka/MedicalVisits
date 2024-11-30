using System.Net.Http.Json;
using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.Implementation;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommandHandler : IRequestHandler<GetConfirmVisitRequestsCommand, RouteResponse>
{
    public ApplicationDbContext _dbContext;
    public UserManager<ApplicationUser> _userManager;
    public IGeocodingService _geocodingService;
    private readonly HttpClient _httpClient;
    
    public GetConfirmVisitRequestsCommandHandler(ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager, IGeocodingService geocodingService, HttpClient httpClient)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _geocodingService = geocodingService;
        _httpClient = httpClient;
    }


    /*
     * Завдання середньої складності. Основні компоненти:
        База даних:
        Вибірка visitRequests з JOIN пацієнтів
        Фільтрація по даті та лікарю


        Geocoding:
        Інтеграція з Nominatim API
        Конвертація адрес в координати
        Обробка помилок та лімітів API


        Routing:
        Інтеграція з OSRM API
        Оптимізація маршруту
        Кодування polyline для фронтенду

        Основні виклики:
        Rate limiting API
        Кешування результатів геокодингу
        Обробка неточних адрес
        Оптимізація маршруту для великої кількості точок

        Рекомендую розбити на окремі сервіси:

        VisitService
        GeocodingService
        RoutingService
     */
    public async Task<RouteResponse> Handle(GetConfirmVisitRequestsCommand request,
        CancellationToken cancellationToken)
    {
        
    
        //TODO: Функція для пошуку пацієнтів які будуть в день вказаним лікарем
        //1. Потрібно зараз буде визначити по тим visitRequst, знаходження усіх пацієнтів
        //2. Тепер по адресам, потрібно звернутись до  OpenStreetMap -> потрібно повернути,адресу всіх корстувачів та їх координати
        //3. Коли ми визначили їх координати, нам потрібно тепер звернутись до api, щоб воно зробило нам маршрут машино
        //3.1. І повернула нам RouteResponceDto, у якому буде 
        //@return = Швидкість за яку проходить автомобіль, хешований шлях маршруту,
        //точки по яким буде проходити цей маршрут( для того, щоб там поставити іконки

        //TODO: звертати до сервера це не дуже вигідно завжди, особливо якщо це є віддалене api 
        //тому можна буде придумати якусь штуку, для того, щоб  кешувати
        //дані, або проходить сокро по табилці в ппошукуах конкретного
        //адресу, і брати його довготу і широту
        
        //TODO: потрібно почати з сервісів для розрахунку відстаней, почну з
        
        
        /*var result = await _dbContext.VisitRequests
            .Where(u => request.RouteRequest.DoctorId.Equals(u.DoctorId)) // Фільтруємо по лікарю
            .Where(u => u.Status == request.RouteRequest.status) // Фільтруємо по статусу
            .ToListAsync(cancellationToken);
        
       
        //1. Потрібно вибрати звідси домівки користувачів
        var addressOfRequests = await _dbContext.PatientProfiles
            .Select(p => new PatientAddressDto { 
                Address = p.User.Address,
                UserId = p.UserId 
            }).ToListAsync();*/
        
        // Отримали запити які є підтверджені
        var visits = await _dbContext.VisitRequests
            .Where(u => request.RouteRequest.DoctorId.Equals(u.DoctorId))
            .Where(u => u.Status == request.RouteRequest.status)
            .Include(v => v.Patient)
            .ToListAsync(cancellationToken);
        
        //Отримати адреси користувачів які живуть під цими запитами
        var patientAddresses = new List<PatientAddressDto>();
        foreach (var visit in visits)
        {
            var user = await _userManager.FindByIdAsync(visit.PatientId);
            if (user != null)
            {
                patientAddresses.Add(new PatientAddressDto
                {
                    Address = user.Address,
                    UserId = user.Id
                });
            }
        }

        //назначити для кожного користувача зі списку свій адрес де вони проживають
        foreach (var patient in patientAddresses)
        {   
            //2.
            //Отримуєм точки користувача
            var point = await _geocodingService.GetCoordinatesFromAddress(patient.Address);
            
            
            //ВСтавляєм точки проживання в наше dto
            patient.Latitude = point[1];
            patient.Longitude = point[0];
            
        }
        
        //тепер patientAddresses це є список, у якому є id пацієнта,адреси користувачів, широта та довгота
        //тепер настав час виконувати третій пункт 
        //TODO: не відомо як зберігати порядок користувачів  в сервісі
        var coordinates = patientAddresses.Select(p => new[] { p.Longitude, p.Latitude }).ToList();

        var requestBody = new { coordinates };
        
        
        //Todo: key set
        string apiUrl = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={"futureKey"}";

        var response = await _httpClient.PostAsJsonAsync(apiUrl, requestBody);
        var routeData = await response.Content.ReadFromJsonAsync<RouteResponse>();

        Console.WriteLine(routeData);
        
        
        // var coordinateToPatient = new Dictionary<string, PatientAddressDto>();
        // foreach (var coord in coordinates.Split('|'))
        // {
        //     var patient = AddressOfRequests.First(p => 
        //         $"{p.Longitude},{p.Latitude}" == coord);
        //     coordinateToPatient[coord] = patient;
        // }
        return null;
    }
    
   

}
