using MedicalVisits.Models.Entities.ChatEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class ChatParticipantsConfiguration : IEntityTypeConfiguration<ChatParticipants>
{
    public void Configure(EntityTypeBuilder<ChatParticipants> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(cp => cp.Chat)
            .WithMany(c => c.Participants)
            .HasForeignKey(cp => cp.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(cp => cp.User)
            .WithMany(u => u.Chats)
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
