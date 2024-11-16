using MediatR;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;
using MedicalVisits.Application.Auth.Commands.RegisterPatient;
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
            var result = await _Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Error);

            return Ok(result.Response);
        }
        catch (Exception ex)
        {
            // Додайте логування
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}