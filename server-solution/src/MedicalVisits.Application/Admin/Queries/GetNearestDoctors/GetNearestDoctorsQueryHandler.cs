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
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetNearestDoctorsQueryHandler : IRequestHandler<GetNearestDoctorsQuery, List<DoctorProfileWithDistance>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IGeocodingService _geocodingService;
    private readonly IRouteService _routeService;
    private readonly ILogger<GetNearestDoctorsQueryHandler> _logger;

    public GetNearestDoctorsQueryHandler(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext,
        IGeocodingService geocodingService,
        IRouteService routeService,
        ILogger<GetNearestDoctorsQueryHandler> logger)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _geocodingService = geocodingService;
        _routeService = routeService;
        _logger = logger;
    }

    public async Task<List<DoctorProfileWithDistance>> Handle(GetNearestDoctorsQuery request, CancellationToken cancellationToken)
    {
        var visitRequest = await _dbContext.VisitRequests
            .Include(v => v.Patient)
            .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(v => v.Id == request.requestId, cancellationToken);

        if (visitRequest == null)
        {
            _logger.LogWarning("Visit request with ID {RequestId} not found.", request.requestId);
            return new List<DoctorProfileWithDistance>();
        }

        var patient = await _userManager.FindByIdAsync(visitRequest.PatientId);
        var patientAddress = patient?.Address ?? throw new Exception("Patient address not found.");

        var patientCoordinates = await _geocodingService.GeocodeAddressAsync(patientAddress);
        ValidateCoordinates(patientCoordinates, "Patient coordinates not found.");

        var doctors = await _dbContext.DoctorProfiles
            .Include(d => d.User)
            .Where(d => d.User.Address.Country == patientAddress.Country &&
                        d.User.Address.City == patientAddress.City)
            .ToListAsync(cancellationToken);

        if (!doctors.Any())
        {
            _logger.LogWarning("No doctors found in the city {City}.", patientAddress.City);
            return new List<DoctorProfileWithDistance>();
        }

        var doctorDistances = await CalculateDistancesAsync(doctors, patientCoordinates);

        return doctorDistances.OrderBy(d => d.Distance).ToList();
    }

    private async Task<List<DoctorProfileWithDistance>> CalculateDistancesAsync(
        List<DoctorProfile> doctors,
        Coordinate patientCoordinates)
    {
        var doctorDistances = new List<DoctorProfileWithDistance>();

        foreach (var doctor in doctors)
        {
            var doctorCoordinates = await _geocodingService.GeocodeAddressAsync(doctor.User.Address);
            if (!IsValidCoordinates(doctorCoordinates)) continue;

            var distance = await _routeService.GetDistanceBetweenTwoPoints(patientCoordinates, doctorCoordinates);
            if (distance == null) continue;

            doctorDistances.Add(new DoctorProfileWithDistance
            {
                DoctorId = doctor.UserId,
                FirstName = doctor.User.FirstName,
                LastName = doctor.User.LastName,
                Specialization = doctor.Specialization,
                Distance = distance
            });
        }

        return doctorDistances;
    }

    private void ValidateCoordinates(Coordinate coordinates, string errorMessage)
    {
        if (!IsValidCoordinates(coordinates))
        {
            throw new Exception(errorMessage);
        }
    }

    private bool IsValidCoordinates(Coordinate coordinates)
    {
        return coordinates.Latitude != 0 && coordinates.Longitude != 0;
    }
}
