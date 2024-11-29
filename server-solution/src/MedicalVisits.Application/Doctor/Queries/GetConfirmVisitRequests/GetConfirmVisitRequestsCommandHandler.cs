using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommandHandler : IRequestHandler<GetConfirmVisitRequestsCommand, RouteResponseDto>
{
    public ApplicationDbContext _dbContext;
    public UserManager<ApplicationUser> _userManager;


    public GetConfirmVisitRequestsCommandHandler(ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }


    /*
     * Завдання середньої складності. Основні компоненти:
        База даних:
        Вибірка visitRequests з JOIN пацієнтів
        Фільтрація по даті та лікарю


        Geocoding:
        Інтеграція з Nominatim API
        Конвертація адрес в координати
        Обробка помилок та лімітів API


        Routing:
        Інтеграція з OSRM API
        Оптимізація маршруту
        Кодування polyline для фронтенду

        Основні виклики:
        Rate limiting API
        Кешування результатів геокодингу
        Обробка неточних адрес
        Оптимізація маршруту для великої кількості точок

        Рекомендую розбити на окремі сервіси:

        VisitService
        GeocodingService
        RoutingService
     */
    public async Task<RouteResponseDto> Handle(GetConfirmVisitRequestsCommand request,
        CancellationToken cancellationToken)
    {
        // Отримали запити які є підтверджені
        var result = await _dbContext.VisitRequests
            .Where(u => request.RouteRequest.DoctorId.Equals(u.DoctorId)) // Фільтруємо по лікарю
            .Where(u => u.Status == request.RouteRequest.status) // Фільтруємо по статусу
            .ToListAsync(cancellationToken);

        //TODO: Функція для пошуку пацієнтів які будуть в день вказаним лікарем
        //1. Потрібно зараз буде визначити по тим visitRequst, знаходження усіх пацієнтів
        //2. Тепер по адресам, потрібно звернутись до  OpenStreetMap -> потрібно повернути,адресу всіх корстувачів та їх координати
        //3. Коли ми визначили їх координати, нам потрібно тепер звернутись до api, щоб воно зробило нам маршрут машино
        //3.1. І повернула нам RouteResponceDto, у якому буде 
        //@return = Швидкість за яку проходить автомобіль, хешований шлях маршруту,
        //точки по яким буде проходити цей маршрут( для того, щоб там поставити іконки

        //TODO: звертати до сервера це не дуже вигідно завжди, особливо якщо це є віддалене api 
        //тому можна буде придумати якусь штуку, для того, щоб  кешувати
        //дані, або проходить сокро по табилці в ппошукуах конкретного
        //адресу, і брати його довготу і широту
        
        //TODO: потрібно почати з сервісів для розрахунку відстаней, почну з
        return null;
    }
}
