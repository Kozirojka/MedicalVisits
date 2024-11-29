using MediatR;

namespace MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;

public class AssignDoctorToVisitCommand : IRequest<AssignmentResult>  // Changed return type to a proper result
{
    // Properties to store the assignment details
    public int VisitId { get; }
    public string DoctorId { get; }

    public AssignDoctorToVisitCommand(int visitId, string doctorId)
    {
        VisitId = visitId;
        DoctorId = doctorId;
    }
}

// Result class to return assignment outcome
public class AssignmentResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
