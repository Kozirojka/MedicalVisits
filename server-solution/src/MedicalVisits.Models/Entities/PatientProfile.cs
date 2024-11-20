namespace MedicalVisits.Models.Entities;

public class PatientProfile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Address { get; set; }
    // Інші специфічні поля пацієнта
}