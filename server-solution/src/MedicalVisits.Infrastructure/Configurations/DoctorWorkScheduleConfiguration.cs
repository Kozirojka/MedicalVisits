using MedicalVisits.Models.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class DoctorWorkScheduleConfiguration : IEntityTypeConfiguration<DoctorWorkSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorWorkSchedule> builder)
    {
        builder.HasKey(d => d.Id);
        
        builder.HasOne(s => s.Doctor)
            .WithMany()
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.HasMany(s => s.TimeSlots)
            .WithOne(s => s.Schedule)
            .HasForeignKey(s => s.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
