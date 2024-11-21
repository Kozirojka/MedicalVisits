using System.Text.Json;
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using MedicalVisits.Models.Enums;


public class VisitRequest
{

    protected VisitRequest() { }

    private VisitRequest(string patientId, DateTime dateTime, string description, string address)
    {
        PatientId = patientId;
        DateTime = dateTime;
        Description = description;
        Address = address;
        Status = VisitStatus.Pending;
    }

    public int Id { get; private set; }
    public string PatientId { get; private set; }
    public ApplicationUser Patient { get; private set; }
    public string? DoctorId { get; private set; }
    public ApplicationUser? Doctor { get; private set; }
    public DateTime? DateTime { get; private set; }
    public DateTime? DateTimeEnd { get; private set; }
    public string Description { get; private set; }
    public string Address { get; private set; }
    public bool IsRegular { get; private set; }
    public bool HasMedicine { get; private set; }
    public VisitStatus Status { get; set; }
    public ICollection<WorkSchedule> WorkSchedules { get; set; }
    
    
   
    public static VisitRequest Create(string patientId, DateTime dateTime, string description, string address)
    {
        return new VisitRequest(patientId, dateTime, description, address);
    }

    public void AssignDoctor(string doctorId)
    {
        DoctorId = doctorId;
        Status = VisitStatus.Approved;
    }

    public void CompleteVisit()
    {
        Status = VisitStatus.Completed;
    }

    public void CancelVisit()
    {
        Status = VisitStatus.Cancelled;
    }

    public void SetIsRegular(bool isRegular)
    {
        IsRegular = isRegular;
    }
}

