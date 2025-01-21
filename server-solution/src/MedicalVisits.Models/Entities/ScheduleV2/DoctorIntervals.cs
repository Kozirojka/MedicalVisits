namespace MedicalVisits.Models.Entities.ScheduleV2;

public class DoctorIntervals
{
    public int Id { get; set; }
    public int? DoctorId { get; set; }
    public DoctorProfile Doctor { get; set; }
    
    public DateTime StartInterval { get; set; }
    public DateTime EndInterval { get; set; }
    
    public DoctorSchedules DoctorSchedules { get; set; }
    public int DoctorScheduleId { get; set; }
    
    public VisitRequest? VisitRequest { get; set; }
    public int? VisitRequestId { get; set; }



    public void SetVisitRequest(int RequestId)
    {
        VisitRequestId = RequestId;
    }
}