using CRMdataLayer;
using CRMdataLayer.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;


namespace CRMdataLayer
{
    public class AppDBContext : DbContext
    {
        // Constructor with DbContextOptions
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        // Empty constructor (optional, for migrations)
        public AppDBContext()
        {
        }
        // Optional: Configure connection string for design-time (migrations)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // This is for migrations only
                optionsBuilder.UseSqlServer("Server =.; Database = CarRentalDB; user Id = sa; Password = 12345678; MultipleActiveResultSets = true; TrustServerCertificate = True; ");
            }
        }
        // DbSet properties
        public DbSet<Users> Users { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users configuration
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Customer configuration
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.LicenseNumber).HasMaxLength(20);
                entity.Property(e => e.LicenseType).HasMaxLength(50);
                entity.HasIndex(e => e.Phone).IsUnique();
                entity.HasIndex(e => e.Email);
                entity.HasIndex(e => e.IsActive);
            });

            // Vehicle configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PlateNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Make).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Color).HasMaxLength(30);
                entity.Property(e => e.VehicleType).HasMaxLength(30);
                entity.Property(e => e.Transmission).HasMaxLength(20);
                entity.Property(e => e.FuelType).HasMaxLength(20);
                entity.Property(e => e.VIN).HasMaxLength(17);
                entity.Property(e => e.EngineNumber).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Available");

                // Decimal properties with precision
                entity.Property(e => e.DailyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.WeeklyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MonthlyRate).HasColumnType("decimal(18,2)");

                entity.HasIndex(e => e.PlateNumber).IsUnique();
                entity.HasIndex(e => e.VIN).IsUnique().HasFilter("[VIN] IS NOT NULL");
                entity.HasIndex(e => e.IsActive);
                entity.HasIndex(e => e.IsAvailable);
                entity.HasIndex(e => e.Status);
            });

            // Rentals configuration - FIXED with decimal precision
            modelBuilder.Entity<Rentals>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configure all decimal properties with precision
                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18,2)");
                entity.Property(e => e.BalanceDue).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DailyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DamageFee).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Discount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.LateFee).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SubTotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");

                // Add indexes and other configurations as needed
                entity.HasIndex(e => e.CustomerId);
                entity.HasIndex(e => e.VehicleId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreatedAt);
            });

            // MAINTENANCE CONFIGURATION
            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configure decimal property
                entity.Property(e => e.ActualCost)
                    .HasColumnType("decimal(18,2)");

                // String properties with length
                entity.Property(e => e.MaintenanceType)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasDefaultValue("Scheduled");

                entity.Property(e => e.MechanicName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.MechanicPhone)
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasDefaultValue("System");

                // Default values
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Relationship
                entity.HasOne(m => m.Vehicle)
                    .WithMany(v => v.Maintenances)
                    .HasForeignKey(m => m.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.VehicleId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.ScheduledDate);
                entity.HasIndex(e => new { e.Status, e.ScheduledDate });
            });

           
        }
    }
}
