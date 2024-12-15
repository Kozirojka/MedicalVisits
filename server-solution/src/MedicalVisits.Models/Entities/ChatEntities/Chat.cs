using MedicalVisits.Models.Enums;

namespace MedicalVisits.Models.Entities.ChatEntities
{
    public class Chat
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatParticipants>? Participants { get; set; }

        public Chat()
        {
            Participants = new List<ChatParticipants>();
        }

        public static Chat CreatePrivateChat(string participantName, string userId, string participantId)
        {
            var chat = new Chat()
            {
                Name = participantName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                Type = "Private",
                Participants = new List<ChatParticipants>()
            };

            chat.Participants.Add(new ChatParticipants
            {
                UserId = userId,
                Role = ChatRoles.Owner
            });

            chat.Participants.Add(new ChatParticipants
            {
                UserId = participantId,
                Role = ChatRoles.Participant
            });

            return chat;
        }
    }
}
