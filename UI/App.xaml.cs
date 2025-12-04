using Data.Interfaces;
using Data.SqlServer;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;

namespace UI
{
    public partial class App : Application
    {
        private IRepairRequestRepository _repairRequestRepository = null;
        private TechnoTransDbContext _dbContext = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.database.json")
                .Build();

            var factory = new TechnoTransDbContextFactory();
            _dbContext = factory.CreateDbContext(configuration);

            _dbContext.Database.Migrate();

            _repairRequestRepository = new RepairRequestRepository(_dbContext);

            SeedInitData();

            var mainWindow = new MainWindow(_repairRequestRepository);
            mainWindow.Show();
        }

        private void SeedInitData()
        {
            if (_dbContext.RepairRequests.Any())
            {
                return; 
            }

            var random = new Random();
            var carTypes = new[] { "Седан", "Универсал", "Хэтчбек", "Внедорожник", "Минивэн", "Купе" };
            var carModels = new[]
            {
        "Mercedes C-class", "Audi A4", "BMW 3 Series", "Toyota Camry",
        "Honda Accord", "Volkswagen Passat", "Hyundai Sonata",
        "Skoda Octavia", "Kia Optima"
    };
            var problems = new[]
            {
        "Не заводится двигатель", "Стук в подвеске", "Проблемы с тормозами",
        "Течет масло", "Не работает кондиционер", "Проблемы с коробкой передач",
        "Электронные неисправности", "Кузовной ремонт"
    };
            var clientNames = new[]
            {
        "Иванов Иван", "Петров Петр", "Сидоров Алексей", "Кузнецова Мария",
        "Смирнов Дмитрий", "Попова Анна", "Васильев Сергей", "Новикова Екатерина",
        "Федоров Андрей", "Морозова Ольга", "Волков Павел"
    };
            var mechanics = new[]
            {
        "Петров П.А.", "Сидоров М.В.", "Козлов И.С.", "Никитин А.П.", "Фомин Д.К."
    };
            var comments = new[]
            {
        "Требуется диагностика", "Клиент ждет звонка", "Необходима замена деталей",
        "Ремонт по гарантии", "Срочный ремонт", "Обычный сервис"
    };
            var spareParts = new[]
            {
        "Тормозные колодки, диски", "Аккумулятор, свечи", "Фильтры, масло",
        "Амортизаторы, пружины", "ШРУС, сайлентблоки", "Генератор, стартер",
        "Датчики, проводка"
    };

            for (int i = 0; i < 500; i++)
            {
                var repairRequest = new RepairRequest
                {
                    CreationDate = DateTime.Now.AddDays(-random.Next(60)),
                    CarType = carTypes[random.Next(carTypes.Length)],
                    CarModel = carModels[random.Next(carModels.Length)],
                    ProblemDescription = problems[random.Next(problems.Length)],
                    ClientName = clientNames[random.Next(clientNames.Length)],
                    PhoneNumber = "+79" + random.Next(10000000, 99999999).ToString(),
                    Status = (RepairRequestStatus)random.Next(4), 

                    
                    ResponsibleMechanic = random.NextDouble() > 0.3 ?
                        mechanics[random.Next(mechanics.Length)] : null,

                    
                    Comments = random.NextDouble() > 0.4 ?
                        comments[random.Next(comments.Length)] : null,

                    
                    SpareParts = random.NextDouble() > 0.5 ?
                        spareParts[random.Next(spareParts.Length)] : null
                };

                _dbContext.RepairRequests.Add(repairRequest);
            }

            _dbContext.SaveChanges();
        }
        

        protected override void OnExit(ExitEventArgs e)
        {
            
            _dbContext?.Dispose();
            base.OnExit(e);
        }
    }
}