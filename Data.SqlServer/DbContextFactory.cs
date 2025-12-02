using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.SqlServer;
public class TechnoTransDbContextFactory : IDesignTimeDbContextFactory<TechnoTransDbContext>
{
    public TechnoTransDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.database.json")
            .Build();

        return CreateDbContext(configuration);
    }

    public TechnoTransDbContext CreateDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionBuilder = new DbContextOptionsBuilder<TechnoTransDbContext>();

        optionBuilder.UseSqlServer(connectionString);

        return new TechnoTransDbContext(optionBuilder.Options);
    }
    
    
}

