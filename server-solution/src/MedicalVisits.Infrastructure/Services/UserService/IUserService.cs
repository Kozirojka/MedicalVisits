using System.Security.Claims;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IUserService
{
    string? GetUserId(ClaimsPrincipal? user);
}
