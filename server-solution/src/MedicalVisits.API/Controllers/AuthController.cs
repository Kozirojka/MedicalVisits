using MediatR;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;
using MedicalVisits.Models.Auth;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return Unauthorized();

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        // Генеруємо access token
        var accessToken = await _mediator.Send(new GenerateAccessTokenCommand 
        { 
            User = user, 
            Role = role 
        });

        // Генеруємо та зберігаємо refresh token
        var refreshToken = Guid.NewGuid().ToString();
        await _mediator.Send(new UpdateRefreshTokenCommand
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
}