using MediatR;

namespace MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;

public class AssignDoctorToVisitCommand : IRequest<AssignmentResult> 
{
    public int VisitId { get; }
    public string DoctorId { get; }
    public int SlotTimeId { get; set;  }
    public AssignDoctorToVisitCommand(int visitId, string doctorId, int slotTimeId)
    {
        VisitId = visitId;
        DoctorId = doctorId;
        SlotTimeId = slotTimeId;
    }
}

public class AssignmentResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
