using MediatR;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Dtos.AuthDto;

namespace MedicalVisits.Application.Admin.Queries.GetAllUser;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
}