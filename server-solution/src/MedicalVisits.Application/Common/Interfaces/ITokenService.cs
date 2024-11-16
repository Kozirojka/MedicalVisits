using System.Security.Claims;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Common.Interfaces;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, string role);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
    