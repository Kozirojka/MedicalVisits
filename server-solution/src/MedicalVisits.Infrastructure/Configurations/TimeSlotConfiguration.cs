using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.HasKey(ts => ts.Id);

        builder.HasOne(ts => ts.AssignedVisit)
            .WithOne(v => v.TimeSlot)
            .HasForeignKey<VisitRequest>(v => v.TimeSlotId)
            .OnDelete(DeleteBehavior.Restrict);

        // Створюємо індекс для швидкого пошуку вільних слотів
        builder.HasIndex(ts => new { ts.ScheduleId, ts.Date, ts.Status });
    }
}