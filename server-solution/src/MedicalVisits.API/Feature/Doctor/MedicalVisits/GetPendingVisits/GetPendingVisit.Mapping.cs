using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Models.Dtos;

namespace MedicalVisits.API.Feature.Doctor.MedicalVisits.GetPendingVisits;

public static class MyApiRequestExtensions
{
    public static GetPendingRequestsForDoctorCommand MapToCommand(this DoctorRequestFilterDto request)
    {
        
        return new GetPendingRequestsForDoctorCommand(request);
    }
}