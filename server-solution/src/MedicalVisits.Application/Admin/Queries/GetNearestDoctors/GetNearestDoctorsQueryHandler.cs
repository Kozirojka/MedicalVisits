using System.Text;
using System.Text.Json;
using MediatR;
using MedicalVisits.Application.Admin.Queries.GetAllDoctors;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.GoogleMapsApi;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.diraction;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetNearestDoctorsQueryHandler : IRequestHandler<GetNearestDoctorsQuery, List<DoctorProfileWithDistance>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IGeocodingService _geocodingService; 
    private readonly IRouteService _routeService;
    
    public GetNearestDoctorsQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext, IGeocodingService geocodingService, IRouteService routeService)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _geocodingService = geocodingService;
        _routeService = routeService;
    }
    
    
    /*
     * Рекомендації для оптимізації:
Кешування результатів: Зберігайте результати API-запитів, щоб уникнути повторних звернень.
Групування запитів: Використовуйте Matrix API для розрахунку між кількома точками одночасно, а не поодинокі запити.
Оптимізація даних: Скоротіть кількість точок або уточніть географічні зони для запитів.
     */
    public async Task<List<DoctorProfileWithDistance>> Handle(GetNearestDoctorsQuery request, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.OutputEncoding = Encoding.UTF8;
        var visitRequest = await _dbContext.VisitRequests
            .Include(v => v.Patient) 
            .ThenInclude(p => p.Address) 
            .FirstOrDefaultAsync(v => v.Id ==  request.requestId, cancellationToken);


        if (visitRequest == null)
        {
            Console.WriteLine("-----------------No Visit Request------------------------------------------------------");
        }
        else
        {
            Console.WriteLine(visitRequest.Description + "------------------------------");
        }
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

            var distance = _routeService.GetDistanceBetweenTwoPoints(new Coordinate()
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
                    DoctorId = doctor.UserId,
                    FirstName = doctor.User.FirstName,
                    LastName = doctor.User.LastName,
                    Specialization = doctor.Specialization,
                    Distance = distance.Result
                });
            }

            i++;
        }
        
        return doctorDistances.OrderBy(d => d.Distance).ToList();
        
    }
    
}
