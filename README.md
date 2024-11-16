logic of the structure

MySolution.sln
├── src/
│   ├── MySolution.API/                # API контролери, конфігурація
│   │   ├── Controllers/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── MySolution.Application/        # Бізнес логіка, CQRS
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Interfaces/
│   │   └── Services/
│   │
│   ├── MySolution.Domain/            # Доменні моделі, інтерфейси
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Interfaces/
│   │
│   └── MySolution.Infrastructure/    # База даних, зовнішні сервіси
│       ├── Persistence/
│       │   ├── Configurations/
│       │   └── ApplicationDbContext.cs
│       └── Repositories/
