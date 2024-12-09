using MedicalVisits.Models.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne<ScheduleWorkPlan>(plan => plan.WorkPlan)
            .WithMany(x => x.TimeSlots)
            .HasForeignKey(x => x.WorkPlanId);
        
        builder.HasOne(x => x.Request).WithOne().HasForeignKey<TimeSlot>(x => x.RequestId); 
        
    }
}
