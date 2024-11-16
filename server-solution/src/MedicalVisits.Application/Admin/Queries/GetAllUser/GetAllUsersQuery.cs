using MediatR;
using MedicalVisits.Models.Dtos;

namespace MedicalVisits.Application.Admin.Queries.GetAllUser;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
    // Можна додати параметри для пагінації якщо потрібно
    // public int? PageNumber { get; set; }
    // public int? PageSize { get; set; }
}