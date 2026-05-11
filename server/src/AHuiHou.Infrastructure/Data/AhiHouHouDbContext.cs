using AHuiHou.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Data;

public class AHuiHouDbContext : DbContext
{
    public AHuiHouDbContext(DbContextOptions<AHuiHouDbContext> options) : base(options) { }

    public DbSet<MembershipType> MembershipTypes => Set<MembershipType>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserMembership> UserMemberships => Set<UserMembership>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<LoyaltyWallet> LoyaltyWallets => Set<LoyaltyWallet>();
    public DbSet<PointTransaction> PointTransactions => Set<PointTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // MembershipType
        modelBuilder.Entity<MembershipType>(e =>
        {
            e.ToTable("membership_types");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(50);
            e.Property(x => x.DiscountRate).HasColumnType("decimal(5,2)").HasDefaultValue(0);
            e.Property(x => x.MonthlyFee).HasColumnType("decimal(10,2)");
        });

        // Category
        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("categories");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(50);
            e.Property(x => x.Description);
        });

        // Area
        modelBuilder.Entity<Area>(e =>
        {
            e.ToTable("areas");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(50);
            e.Property(x => x.IsQuietZone).HasDefaultValue(false);
        });

        // User
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.HasKey(x => x.Id);
            e.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            e.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).IsRequired().HasMaxLength(150);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.PasswordHash).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // UserMembership
        modelBuilder.Entity<UserMembership>(e =>
        {
            e.ToTable("user_memberships");
            e.HasKey(x => x.Id);
            e.Property(x => x.StartDate).IsRequired();
            e.Property(x => x.IsActive).HasDefaultValue(true);
            e.HasOne(x => x.User)
                .WithMany(u => u.Memberships)
                .HasForeignKey(x => x.UserId);
            e.HasOne(x => x.MembershipType)
                .WithMany(m => m.UserMemberships)
                .HasForeignKey(x => x.MembershipTypeId);
        });

        // Table
        modelBuilder.Entity<Table>(e =>
        {
            e.ToTable("tables");
            e.HasKey(x => x.Id);
            e.Property(x => x.TableNumber).IsRequired().HasMaxLength(10);
            e.Property(x => x.Capacity).IsRequired();
            e.Property(x => x.HasOutlet).HasDefaultValue(true);
            e.HasOne(x => x.Area)
                .WithMany(a => a.Tables)
                .HasForeignKey(x => x.AreaId);
        });

        // Reservation
        modelBuilder.Entity<Reservation>(e =>
        {
            e.ToTable("reservations");
            e.HasKey(x => x.Id);
            e.Property(x => x.StartTime).IsRequired();
            e.Property(x => x.EndTime).IsRequired();
            e.Property(x => x.Status).HasMaxLength(20).HasDefaultValue("Pending");
            e.HasOne(x => x.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(x => x.UserId);
            e.HasOne(x => x.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(x => x.TableId);
            e.HasIndex(x => new { x.TableId, x.StartTime, x.EndTime });
        });

        // Product
        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("products");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Price).HasColumnType("decimal(10,2)");
            e.Property(x => x.PointsAwarded).HasDefaultValue(0);
            e.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId);
        });

        // LoyaltyWallet
        modelBuilder.Entity<LoyaltyWallet>(e =>
        {
            e.ToTable("loyalty_wallets");
            e.HasKey(x => x.UserId);
            e.Property(x => x.Balance).HasDefaultValue(0);
            e.Property(x => x.LastUpdate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(x => x.User)
                .WithOne(u => u.LoyaltyWallet)
                .HasForeignKey<LoyaltyWallet>(x => x.UserId);
        });

        // PointTransaction
        modelBuilder.Entity<PointTransaction>(e =>
        {
            e.ToTable("point_transactions");
            e.HasKey(x => x.Id);
            e.Property(x => x.Amount).IsRequired();
            e.Property(x => x.TransactionType).HasMaxLength(50);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(x => x.Wallet)
                .WithMany(w => w.PointTransactions)
                .HasForeignKey(x => x.WalletId);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MembershipType>().HasData(
            new MembershipType { Id = 1, Name = "VIP", DiscountRate = 15.00m, MonthlyFee = 99.99m },
            new MembershipType { Id = 2, Name = "Diamante", DiscountRate = 10.00m, MonthlyFee = 59.99m },
            new MembershipType { Id = 3, Name = "Estudiante", DiscountRate = 5.00m, MonthlyFee = 29.99m }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Bebidas", Description = "Café, té, smoothies y más" },
            new Category { Id = 2, Name = "Comida", Description = "Sándwiches, pasteles y snacks" },
            new Category { Id = 3, Name = "Coworking", Description = "Acceso a espacios de trabajo" }
        );

        modelBuilder.Entity<Area>().HasData(
            new Area { Id = 1, Name = "Piso 1", IsQuietZone = false },
            new Area { Id = 2, Name = "Terraza", IsQuietZone = false },
            new Area { Id = 3, Name = "Sala Silenciosa", IsQuietZone = true }
        );

        modelBuilder.Entity<Table>().HasData(
            new Table { Id = 1, AreaId = 1, TableNumber = "A1", Capacity = 2, HasOutlet = true },
            new Table { Id = 2, AreaId = 1, TableNumber = "A2", Capacity = 4, HasOutlet = true },
            new Table { Id = 3, AreaId = 1, TableNumber = "A3", Capacity = 2, HasOutlet = false },
            new Table { Id = 4, AreaId = 2, TableNumber = "T1", Capacity = 6, HasOutlet = true },
            new Table { Id = 5, AreaId = 2, TableNumber = "T2", Capacity = 4, HasOutlet = false },
            new Table { Id = 6, AreaId = 3, TableNumber = "S1", Capacity = 1, HasOutlet = true },
            new Table { Id = 7, AreaId = 3, TableNumber = "S2", Capacity = 2, HasOutlet = true }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, CategoryId = 1, Name = "Espresso", Price = 3.50m, PointsAwarded = 0 },
            new Product { Id = 2, CategoryId = 1, Name = "Cappuccino", Price = 4.50m, PointsAwarded = 0 },
            new Product { Id = 3, CategoryId = 1, Name = "Latte Macchiato", Price = 5.00m, PointsAwarded = 0 },
            new Product { Id = 4, CategoryId = 2, Name = "Sándwich de Pollo", Price = 8.50m, PointsAwarded = 0 },
            new Product { Id = 5, CategoryId = 2, Name = "Croissant", Price = 3.50m, PointsAwarded = 0 },
            new Product { Id = 6, CategoryId = 3, Name = "Coworking - Hora", Price = 5.00m, PointsAwarded = 0 },
            new Product { Id = 7, CategoryId = 3, Name = "Coworking - Día", Price = 25.00m, PointsAwarded = 0 }
        );
    }
}

