using MediatR;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;

public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateRefreshTokenCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = Guid.NewGuid().ToString();
        request.User.RefreshToken = refreshToken;
        request.User.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _userManager.UpdateAsync(request.User);
        return refreshToken;
    }
}