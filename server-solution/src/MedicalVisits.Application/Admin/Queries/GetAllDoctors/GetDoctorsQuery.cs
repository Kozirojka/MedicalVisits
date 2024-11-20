using MediatR;

namespace MedicalVisits.Application.Admin.Queries.GetAllDoctors;

public class GetDoctorsQuery : IRequest<List<DoctorDto>>
{
// Можна додати фільтри
public string? SearchTerm { get; init; }
}