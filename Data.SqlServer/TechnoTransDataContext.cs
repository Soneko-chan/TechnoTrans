using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class TechnoTransDbContext : DbContext
{
    public TechnoTransDbContext() { }

    public TechnoTransDbContext(DbContextOptions<TechnoTransDbContext> options)
        : base(options)
    {
    }

    

    public DbSet<RepairRequest> RepairRequests { get; set; }
}