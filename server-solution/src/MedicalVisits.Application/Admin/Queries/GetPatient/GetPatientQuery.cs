using MediatR;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Admin.Queries.GetPatient;

public class GetPatientQuery : IRequest<List<PatientDto>>
{
    // Можна додати фільтри
    public string? SearchTerm { get; init; }
}

