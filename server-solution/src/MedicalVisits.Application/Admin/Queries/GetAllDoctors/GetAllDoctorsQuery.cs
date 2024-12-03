using MediatR;

namespace MedicalVisits.Application.Admin.Queries.GetAllDoctors;

public class GetAllDoctorsQuery : IRequest<List<DoctorDto>>
{
// Можна додати фільтри
public string? SearchTerm { get; init; }
}