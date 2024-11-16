using MediatR;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Auth.Commands.GenerateRefreshToken;

public record UpdateRefreshTokenCommand : IRequest<string>
{
    public ApplicationUser User { get; init; }
    public string RefreshToken { get; init; }
    public DateTime ExpiryTime { get; init; }
}