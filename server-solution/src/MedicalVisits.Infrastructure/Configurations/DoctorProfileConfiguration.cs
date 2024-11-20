using MedicalVisits.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
{
    public void Configure(EntityTypeBuilder<DoctorProfile> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<DoctorProfile>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Додаткові налаштування полів
        builder.Property(d => d.Specialization)
            .HasMaxLength(100)
            .IsRequired();
            
        // Інші налаштування...
    }
}