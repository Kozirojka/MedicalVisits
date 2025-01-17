using MediatR;

namespace MedicalVisits.Application.Admin.Command.AttachVisitToDoctor;

public class AttachVisitToDoctorCommand : IRequest<ResultOfAttach>
{
    public int VisitRequestId { get; init; }
    public string DoctorId { get; init; }
}


public class ResultOfAttach
{
    public bool Result { get; set; }
    public string Message { get; set; }
}