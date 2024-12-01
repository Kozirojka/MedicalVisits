using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models;
using MedicalVisits.Models.diraction;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers;

public class TestController : BaseController
{
    private readonly IGGeocodingService _geocodingService;
    private readonly IRouteOptimizationService _routeService;
    
    [HttpPost("optimize")]
    public async Task<ActionResult<OptimizedRoute>> OptimizeRoute([FromBody] RouteOptimizationRequest request)
    {
        try
        {
            if (request?.StartPoint == null || request.Waypoints == null || !request.Waypoints.Any())
            {
                return BadRequest("Неправильні дані запиту: відсутня початкова точка або точки маршруту");
            }

            // Передаємо об'єкти Coordinate напряму
            var optimizedRoute = await _routeService.GetOptimizedRouteAsync(request.StartPoint, request.Waypoints);
        
            return Ok(optimizedRoute);
        }
        catch (Exception ex)
        {
            return BadRequest($"Помилка при обробці маршруту: {ex.Message}");
        }
    }
    public TestController(IMediator mediator, UserManager<ApplicationUser> userManager, IGGeocodingService geocodingService, IRouteOptimizationService routeService) 
        : base(mediator, userManager)
    {
        _geocodingService = geocodingService;
        _routeService = routeService;
    }

    [HttpGet("/test/{region}/{city}/{street}/{house}")]
    public async Task<ActionResult> GetAddress(
        string region,
        string city,
        string street,
        string house)
    {
        try 
        {
            // Створюємо об'єкт Address з параметрів URL
            // Для України як країни і порожнього номера квартири
            var address = new Address(
                city: Uri.UnescapeDataString(city),
                street: Uri.UnescapeDataString(street),
                building: Uri.UnescapeDataString(house),
                region: Uri.UnescapeDataString(region),
                country: "Україна",
                apartment: ""
            );

            var result = await _geocodingService.GeocodeAddressAsync(address);
            
            return Ok(new
            {
                Address = address.ToString(),
                Coordinates = result.ToString()
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Помилка в параметрах адреси: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Помилка при обробці адреси: {ex.Message}");
        }
    }
    
    
    
    
}

