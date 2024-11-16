using MediatR;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Auth.Commands.GenerateAccessToken;

public record GenerateAccessTokenCommand : IRequest<string>
{
    public ApplicationUser User { get; init; }
    public string Role { get; init; }
}
