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
    public DbSet<Promotion> Promotions => Set<Promotion>();

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
            e.Property(x => x.Balance).HasColumnType("decimal(10,2)").HasDefaultValue(0m);
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
            e.Property(x => x.Amount).HasColumnType("decimal(10,2)").IsRequired();
            e.Property(x => x.TransactionType).HasMaxLength(50);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(x => x.Wallet)
                .WithMany(w => w.PointTransactions)
                .HasForeignKey(x => x.WalletId);
        });

        // Promotion
        modelBuilder.Entity<Promotion>(e =>
        {
            e.ToTable("promotions");
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Description).IsRequired().HasMaxLength(500);
            e.Property(x => x.ImageUrl).HasMaxLength(500);
            e.Property(x => x.DiscountCode).HasMaxLength(50);
            e.Property(x => x.StartDate).IsRequired();
            e.Property(x => x.EndDate).IsRequired();
            e.Property(x => x.IsActive).HasDefaultValue(true);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MembershipType>().HasData(
            new MembershipType { Id = 1, Name = "VIP", DiscountRate = 15.00m, MonthlyFee = 149900m },
            new MembershipType { Id = 2, Name = "Diamante", DiscountRate = 10.00m, MonthlyFee = 89900m },
            new MembershipType { Id = 3, Name = "Estudiante", DiscountRate = 5.00m, MonthlyFee = 39900m }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Bebidas", Description = "Café de especialidad, tés, frappés y más" },
            new Category { Id = 2, Name = "Comida", Description = "Sándwiches artesanales, repostería y bowls" },
            new Category { Id = 3, Name = "Coworking", Description = "Planes de acceso a espacios de trabajo" }
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
            // --- Bebidas calientes ---
            new Product { Id = 1, CategoryId = 1, Name = "Espresso", Price = 5500m, PointsAwarded = 0 },
            new Product { Id = 2, CategoryId = 1, Name = "Cappuccino", Price = 7500m, PointsAwarded = 0 },
            new Product { Id = 3, CategoryId = 1, Name = "Latte Macchiato", Price = 9000m, PointsAwarded = 0 },
            new Product { Id = 4, CategoryId = 1, Name = "Americano", Price = 5000m, PointsAwarded = 0 },
            new Product { Id = 5, CategoryId = 1, Name = "Prensa Francesa", Price = 8500m, PointsAwarded = 0 },
            new Product { Id = 6, CategoryId = 1, Name = "Mocaccino", Price = 9500m, PointsAwarded = 0 },
            new Product { Id = 7, CategoryId = 1, Name = "Té Chai Latte", Price = 8000m, PointsAwarded = 0 },
            // --- Bebidas frías ---
            new Product { Id = 8, CategoryId = 1, Name = "Cold Brew", Price = 10000m, PointsAwarded = 0 },
            new Product { Id = 9, CategoryId = 1, Name = "Frappé de Café", Price = 12500m, PointsAwarded = 0 },
            new Product { Id = 10, CategoryId = 1, Name = "Matcha Latte", Price = 11000m, PointsAwarded = 0 },
            // --- Comida ---
            new Product { Id = 11, CategoryId = 2, Name = "Croissant Artesanal", Price = 7000m, PointsAwarded = 0 },
            new Product { Id = 12, CategoryId = 2, Name = "Torta de Almojábana", Price = 8500m, PointsAwarded = 0 },
            new Product { Id = 13, CategoryId = 2, Name = "Sándwich de Pollo", Price = 18000m, PointsAwarded = 0 },
            new Product { Id = 14, CategoryId = 2, Name = "Sándwich Artesanal", Price = 20000m, PointsAwarded = 0 },
            new Product { Id = 15, CategoryId = 2, Name = "Bagel con Salmón", Price = 22000m, PointsAwarded = 0 },
            new Product { Id = 16, CategoryId = 2, Name = "Ensalada César", Price = 16500m, PointsAwarded = 0 },
            new Product { Id = 17, CategoryId = 2, Name = "Bowl de Frutas", Price = 15000m, PointsAwarded = 0 },
            new Product { Id = 18, CategoryId = 2, Name = "Pastel de Chocolate", Price = 12000m, PointsAwarded = 0 },
            // --- Coworking ---
            new Product { Id = 19, CategoryId = 3, Name = "Coworking - Hora", Price = 8000m, PointsAwarded = 0 },
            new Product { Id = 20, CategoryId = 3, Name = "Coworking - Medio Día", Price = 25000m, PointsAwarded = 0 },
            new Product { Id = 21, CategoryId = 3, Name = "Coworking - Día Completo", Price = 45000m, PointsAwarded = 0 },
            new Product { Id = 22, CategoryId = 3, Name = "Coworking - Semana", Price = 180000m, PointsAwarded = 0 }
        );

        modelBuilder.Entity<Promotion>().HasData(
            new Promotion
            {
                Id = 1,
                Title = "Combo Desayuno",
                Description = "Café Americano + Croissant artesanal por solo $10.000. Válido hasta las 11am.",
                ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=800",
                DiscountCode = "DESAYUNO10K",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 2,
                Title = "After Office 2x1",
                Description = "2x1 en Cappuccinos de 4pm a 6pm. Trae a tu colega y solo pagas uno.",
                ImageUrl = "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?w=800",
                DiscountCode = "AFTER2X1",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 3,
                Title = "Lunes de Latte",
                Description = "Todos los lunes, 20% de descuento en cualquier latte. Tu semana empieza mejor.",
                ImageUrl = "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=800",
                DiscountCode = "LUNESLATTE",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 4,
                Title = "Pase Coworking Día",
                Description = "Día completo de coworking por solo $35.000 (antes $45.000). Café ilimitado incluido.",
                ImageUrl = "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800",
                DiscountCode = "COWORK35K",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 5,
                Title = "Primer Café Gratis",
                Description = "Regístrate y recibe un espresso o americano de bienvenida totalmente gratis.",
                ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=800",
                DiscountCode = "BIENVENIDO",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 6,
                Title = "Plan Estudiante",
                Description = "30% de descuento en planes de coworking con carnet vigente. WiFi + café ilimitado.",
                ImageUrl = "https://images.unsplash.com/photo-1434030216411-0b793f4b4173?w=800",
                DiscountCode = "ESTUDIANTE30",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 7,
                Title = "Té + Pastelería 50% Off",
                Description = "Cualquier té + pastry a mitad de precio de 3pm a 5pm. La hora del té perfecta.",
                ImageUrl = "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=800",
                DiscountCode = "TEATIME50",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 8,
                Title = "Sala para Equipos",
                Description = "4 horas de coworking en sala privada + 4 cafés por solo $80.000. Ideal para brainstorms.",
                ImageUrl = "https://images.unsplash.com/photo-1600880292203-757bb62b4baf?w=800",
                DiscountCode = "TEAM80K",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 9,
                Title = "Sábados de Origen Único",
                Description = "Cada sábado, prueba un café de origen colombiano con 15% de descuento. Conoce nuestro café.",
                ImageUrl = "https://images.unsplash.com/photo-1442512595331-e89e73853f31?w=800",
                DiscountCode = "SABADO15",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 10,
                Title = "Amigo Referido",
                Description = "Trae a un amigo nuevo y ambos ganan 100 puntos de lealtad extra en su primera compra.",
                ImageUrl = "https://images.unsplash.com/photo-1529333166437-7750a6dd5a70?w=800",
                DiscountCode = "AMIGO100",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 11,
                Title = "Café & Libro",
                Description = "Jueves: toma un libro de nuestra biblioteca y recibe un espresso cortesía. Lectura + café.",
                ImageUrl = "https://images.unsplash.com/photo-1513475382585-d06e58bc1ae2?w=800",
                DiscountCode = null,
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            },
            new Promotion
            {
                Id = 12,
                Title = "Pack Repostería x3",
                Description = "3 piezas de pastelería + 2 americanos por solo $22.000. Perfecto para compartir.",
                ImageUrl = "https://images.unsplash.com/photo-1509365465985-25d11c17e812?w=800",
                DiscountCode = "PASTRY22K",
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                IsActive = true
            }
        );
    }
}

