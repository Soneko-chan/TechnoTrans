using System.Windows;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Data.Interfaces;
using Data.SqlServer;

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
            return;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _dbContext?.Dispose();
            base.OnExit(e);
        }
    }
}