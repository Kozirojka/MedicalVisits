using MediatR;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;

public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateRefreshTokenCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        request.User.RefreshToken = request.RefreshToken;
        request.User.RefreshTokenExpiryTime = request.ExpiryTime;
        
        await _userManager.UpdateAsync(request.User);
        return Unit.Value;
    }
}