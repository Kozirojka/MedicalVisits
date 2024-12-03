using System.Net.Http.Json;
using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommandHandler : IRequestHandler<GetConfirmVisitRequestsCommand, RouteResponse>
{
    public ApplicationDbContext _dbContext;
    public UserManager<ApplicationUser> _userManager;
    public IGGeocodingService _geocodingService;
    private readonly HttpClient _httpClient;
    private readonly IRouteOptimizationService _routeOptimizationService;
    public GetConfirmVisitRequestsCommandHandler(ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager, IGGeocodingService geocodingService, HttpClient httpClient, IRouteOptimizationService routeOptimizationService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _geocodingService = geocodingService;
        _httpClient = httpClient;
        _routeOptimizationService = routeOptimizationService;
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
        
        
        //todo: в майбутньому потрібно завести можливість пошуку за графіком
        var visits = await _dbContext.VisitRequests
            .Where(u => request.DoctorId == u.DoctorId)
            .Where(u => u.Status == request.status)
            .Include(v => v.Patient)
            .ToListAsync(cancellationToken);
        
        if (!visits.Any())
        {
            return new RouteResponse(); // або null, залежно від вашої логіки
        }
        
        List<Coordinate> waypoints = new List<Coordinate>();

        
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

        foreach (var patient in patientAddresses)
        {   
            //2.
            //Отримуєм точки користувача
            var point = await _geocodingService.GeocodeAddressAsync(patient.Address);
            
            
            waypoints.Add(new Coordinate
            {
                Latitude = point.Latitude,
                Longitude = point.Longitude
            });
            
            //Doing:
            patient.Latitude = point.Latitude;
            patient.Longitude = point.Longitude;
        }
        
        var Doctor = await _userManager.FindByIdAsync(request.DoctorId);
        
        //todo: зробити так, щоб в програмі використвоувався один стиль координат клас "Coordinate"
        var startPointOfDoctor = await _geocodingService.GeocodeAddressAsync(Doctor.Address);
        
        var resultOfOptimized = await _routeOptimizationService.GetOptimizedRouteAsync(new Coordinate()
        {
            Latitude = startPointOfDoctor.Latitude,
            Longitude = startPointOfDoctor.Longitude
        }, waypoints);
                
        
        return null;
    }
    
   

}
