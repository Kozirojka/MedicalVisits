using System.Text;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;
using MedicalVisits.Application.Auth.Commands.RegisterPatient;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Auth;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{

    public AuthController(IMediator mediator, UserManager<ApplicationUser> userManager) :
        base(mediator, userManager)
    {
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _UserManager.FindByEmailAsync(request.Email);
        if (user == null) return Unauthorized();

        var role = (await _UserManager.GetRolesAsync(user)).FirstOrDefault();

        var accessToken = await _Mediator.Send(new GenerateAccessTokenCommand 
        { 
            User = user, 
            Role = role 
        });

        var refreshToken = Guid.NewGuid().ToString();
        await _Mediator.Send(new UpdateRefreshTokenCommand
        {
            User = user,
            RefreshToken = refreshToken,
            ExpiryTime = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
    
    
    [HttpPost("Register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterPatientDto dto)
    {
        try 
        {
            var command = new CreatePatientCommand(dto);
            AuthResult result = await _Mediator.Send(command);

            if (result == null)
                return BadRequest("I fuck your life its null");

            return Ok("every thing is "  + result);
        }
        catch (Exception ex)
        {
            // Додайте логування
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost("Route")]
    public async Task<string> GetRoute(DirectionRequest request)
    {
        string apiKey = "5b3ce3597851110001cf62486b87172ca31c4ca19b59ce2e1f809ad6";
        string endpoint = "https://api.openrouteservice.org/v2/directions/driving-car";

        // var resultDto = new DirectionRequest()
        // {
        //     Coordinates = request.Coordinates,
        //     Format = request.Format,
        //     Profile = request.Profile
        // };
        
        var requestBody = @"
        {
            ""coordinates"": [
                [8.681495, 49.41461], 
                [8.687872, 49.420318]
            ],
            ""format"": ""json""
        }";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", apiKey);
        
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync(endpoint, content);
        var responseString = await response.Content.ReadAsStringAsync();
        
        return responseString;
    }
}

public class DirectionRequest
{
    public List<List<double>> Coordinates { get; set; } = new(); // Список координат [довгота, широта]
    public string Format { get; set; } = "json"; // Формат відповіді (завжди json)
    public string Profile { get; set; } = "driving-car";    
}