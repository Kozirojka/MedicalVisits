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
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<AuthResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.DriverRequest.Email) != null)
            return new AuthResult
            {
                Succeeded = false,
                Error = "Email is already registered"
            };


        
        
        var user = new ApplicationUser
        {
            UserName = request.DriverRequest.Email,
            Email = request.DriverRequest.Email,
            FirstName = request.DriverRequest.FirstName,
            LastName = request.DriverRequest.LastName,
        };

        var result = await _userManager.CreateAsync(user, request.DriverRequest.Password);

        if (!result.Succeeded)
            return new AuthResult 
            {
                Succeeded = false,
                Error = string.Join(", ", result.Errors.Select(e => e.Description))
            };

        await _userManager.AddToRoleAsync(user, "Patient");

        var token = await _mediator.Send(new GenerateAccessTokenCommand 
        { 
            User = user, 
            Role = "Patient" 
        }, cancellationToken);

        var refreshToken = Guid.NewGuid().ToString();
        await _mediator.Send(new UpdateRefreshTokenCommand 
        { 
            User = user,
            RefreshToken = refreshToken,
            ExpiryTime = DateTime.UtcNow.AddDays(7)
        }, cancellationToken);

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