using MediatR;

namespace MedicalVisits.Application.Admin.Command.AttachVisitToDoctor;

public class AttachVisitToDoctorCommand : IRequest<resultOfAttach>
{
    public int VisitRequestId { get; init; }
    public string DoctorId { get; init; }
}


public class resultOfAttach
{
    public bool Result { get; set; }
    public string Message { get; set; }
}