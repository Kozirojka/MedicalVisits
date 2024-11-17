using MediatR;
using MedicalVisits.Models.Dtos;
using Microsoft.AspNetCore.Http;

namespace MedicalVisits.Application.Patient.Command;

public record CreateVisitRequestCommand : IRequest<CreateVisitRequestResponse>
{
    public DateTime DateTime { get; init; }
    public DateTime DateTimeEnd { get; init; }
    public string Description { get; init; }
    public string Address { get; init; }
    public bool IsRegular { get; init; }
    public bool HasMedicine { get; init; }
    public string RequiredMedications { get; init; }
    public string PatientId { get; init; }

    public CreateVisitRequestCommand(VisitRequestDto dto, string patientId)
    {
        DateTime = dto.DateTime;
        DateTimeEnd = dto.DateTimeEnd;
        Description = dto.Description;
        Address = dto.Address;
        IsRegular = dto.IsRegular;
        HasMedicine = dto.HasMedicine;
        RequiredMedications = dto.RequiredMedications;
        PatientId = patientId;
    }
}


public record CreateVisitRequestResponse
{
    public int RequestId { get; init; }
    public string Message { get; init; }
}

