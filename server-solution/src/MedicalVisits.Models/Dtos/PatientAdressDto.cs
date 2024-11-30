namespace MedicalVisits.Models.Dtos;

public class PatientAddressDto
{
    public Address Address { get; set; }
    public string UserId { get; set; }
    
    
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}