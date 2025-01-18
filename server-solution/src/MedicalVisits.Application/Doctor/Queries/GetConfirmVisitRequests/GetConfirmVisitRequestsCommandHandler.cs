using System.Net.Http.Json;
using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RouteResponse = MedicalVisits.Models.diraction.RouteResponse;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommandHandler : IRequestHandler<GetConfirmVisitRequestsCommand, RouteResponse>
{
    public readonly ApplicationDbContext _dbContext;
    public readonly UserManager<ApplicationUser> _userManager;
    public readonly IGeocodingService _geocodingService;
    private readonly IRouteService _routeService;
    public GetConfirmVisitRequestsCommandHandler(ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager, IGeocodingService geocodingService, HttpClient httpClient, IRouteService routeService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _geocodingService = geocodingService;
        _routeService = routeService;
    }
    
    public async Task<RouteResponse> Handle(GetConfirmVisitRequestsCommand request,
        CancellationToken cancellationToken)
    {
        

        var visits = _dbContext.TimeSlots
            .Where(ts => ts.WorkPlan.UserId == request.DoctorId)
            .Where(ts => ts.RequestId != null)
            .Include(ts => ts.Request)
            .ThenInclude(v => v.Patient)
            .Select(ts => ts.Request)
          .ToList();
        
        
        
        if (!visits.Any())
        {
            return new RouteResponse(); 
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
            var point = await _geocodingService.GeocodeAddressAsync(patient.Address);
            
            waypoints.Add(new Coordinate
            {
                Latitude = point.Latitude,
                Longitude = point.Longitude
            });
            
            patient.Latitude = point.Latitude;
            patient.Longitude = point.Longitude;
        }
        
        var doctor = await _userManager.FindByIdAsync(request.DoctorId);
        
        var startPointOfDoctor = await _geocodingService.GeocodeAddressAsync(doctor.Address);
        
        var resultOfOptimized = await _routeService.GetOptimizedRouteAsync(new Coordinate()
        {
            Latitude = startPointOfDoctor.Latitude,
            Longitude = startPointOfDoctor.Longitude
        }, waypoints);
                
        
        return resultOfOptimized;
    }
    
   

}
