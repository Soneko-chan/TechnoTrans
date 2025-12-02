using Microsoft.EntityFrameworkCore;
using Domain;

namespace Data.SqlServer
{
    public class TechnoTransDbContext : DbContext
    {
        // Добавьте этот конструктор для миграций
        public TechnoTransDbContext()
        {
        }

        public TechnoTransDbContext(DbContextOptions<TechnoTransDbContext> options)
            : base(options)
        {
        }

        public DbSet<RepairRequest> RepairRequests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TechnoTransDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepairRequest>(entity =>
            {
                entity.ToTable("RepairRequests");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreationDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.CarType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CarModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProblemDescription)
                    .IsRequired()
                    .HasMaxLength(int.MaxValue);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<int>() // Конвертация enum в int
                    .HasDefaultValue(RepairRequestStatus.New);

                // Nullable поля
                entity.Property(e => e.ResponsibleMechanic)
                    .HasMaxLength(200)
                    .IsRequired(false); // Разрешаем NULL

                entity.Property(e => e.Comments)
                    .HasMaxLength(int.MaxValue)
                    .IsRequired(false);

                entity.Property(e => e.SpareParts)
                    .HasMaxLength(int.MaxValue)
                    .IsRequired(false);
            });
        }
    }
}