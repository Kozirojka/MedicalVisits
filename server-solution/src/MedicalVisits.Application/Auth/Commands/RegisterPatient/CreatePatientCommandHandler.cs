using MediatR;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;
using MedicalVisits.Models.Auth;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Auth.Commands.RegisterPatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, AuthResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;
    public CreatePatientCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }



    public async Task<AuthResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
{
    // Перевірка request
    if (request == null || request.DriverRequest == null)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = "Request is null"
        };
    }

    // Перевірка полів request
    if (string.IsNullOrWhiteSpace(request.DriverRequest.Email) || 
        string.IsNullOrWhiteSpace(request.DriverRequest.Password) ||
        string.IsNullOrWhiteSpace(request.DriverRequest.FirstName) ||
        string.IsNullOrWhiteSpace(request.DriverRequest.LastName))
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = "All fields are required"
        };
    }

    if (await _userManager.FindByEmailAsync(request.DriverRequest.Email) != null)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = "Email is already registered"
        };
    }

    var user = new ApplicationUser
    {
        UserName = request.DriverRequest.Email,
        Email = request.DriverRequest.Email,
        FirstName = request.DriverRequest.FirstName,
        LastName = request.DriverRequest.LastName
    };
    var result = await _userManager.CreateAsync(user, request.DriverRequest.Password);
    if (!result.Succeeded)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = string.Join(", ", result.Errors.Select(e => e.Description))
        };
    }

    var roleResult = await _userManager.AddToRoleAsync(user, "Patient");
    if (!roleResult.Succeeded)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = "Failed to assign role"
        };
    }

    // Генерація access token
    string token;
    try
    {
        token = await _mediator.Send(new GenerateAccessTokenCommand
        {
            User = user,
            Role = "Patient"
        }, cancellationToken);
    }
    catch (Exception ex)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = $"Failed to generate access token: {ex.Message}"
        };
    }

    // Генерація refresh token
    string refreshToken;
    try
    {
        refreshToken = await _mediator.Send(new UpdateRefreshTokenCommand
        {
            User = user,
            RefreshToken = Guid.NewGuid().ToString(),
            ExpiryTime = DateTime.UtcNow.AddDays(7)
        }, cancellationToken);
    }
    catch (Exception ex)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = $"Failed to generate refresh token: {ex.Message}"
        };
    }

    // Оновлення користувача з refresh token
    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    var updateResult = await _userManager.UpdateAsync(user);
    if (!updateResult.Succeeded)
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = "Failed to update user with refresh token"
        };
    }

    // Фінальна перевірка перед поверненням результату
    if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
    {
        return new AuthResult
        {
            Succeeded = false,
            Error = "Token generation failed"
        };
    }

    // Успішне завершення
    return new AuthResult
    {
        Succeeded = true,
        Response = new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token,
            RefreshToken = refreshToken,
            Role = "Patient",
            FirstName = user.FirstName,
            LastName = user.LastName
        }
    };
}
}