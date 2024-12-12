using MedicalVisits.Models.Enums;

namespace MedicalVisits.Models.Entities.ChatEntities;

public class ChatParticipants
{
    public int Id { get; set; }

    public int ChatId { get; set; }
    public Chat Chat { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public ChatRoles Role { get; set; } 
}