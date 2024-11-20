using System.Text.Json;
using MedicalVisits.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Persistence.Configurations;

public class VisitRequestConfiguration : IEntityTypeConfiguration<VisitRequest>
{
    public void Configure(EntityTypeBuilder<VisitRequest> builder)
    {
        builder.HasOne(v => v.Patient)
            .WithMany()
            .HasForeignKey(v => v.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Doctor)
            .WithMany()
            .HasForeignKey(v => v.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}