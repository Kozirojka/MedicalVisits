﻿namespace MedicalVisits.Models.Entities;

public class DoctorProfile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Specialization { get; set; }
    public string LicenseNumber { get; set; }
    // Інші специфічні поля лікаря
}