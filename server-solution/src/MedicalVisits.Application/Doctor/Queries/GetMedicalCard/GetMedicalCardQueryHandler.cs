using MediatR;

namespace MedicalVisits.Application.Doctor.Queries.GetMedicalCard;

public class GetMedicalCardQueryHandler : IRequestHandler<GetMedicalCardQuery, byte[]>
{
    public async Task<byte[]> Handle(GetMedicalCardQuery request, CancellationToken cancellationToken)
    {
        // todo: зробити тут завтра код для того, щоб створювати пдф
        // він буде складатись з історії користувача,
        // а саме його запитів, оцінки лікаря ( в майбутньому планується реалізувати таку систему  Система контролю якості). 

        
        
        return null;
    }
}
