using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AHuiHou.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsQuietZone = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "membership_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DiscountRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    MonthlyFee = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DiscountCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AreaId = table.Column<int>(type: "integer", nullable: false),
                    TableNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    HasOutlet = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tables_areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PointsAwarded = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loyalty_wallets",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loyalty_wallets", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_loyalty_wallets_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipTypeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_memberships_membership_types_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "membership_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_memberships_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservations_tables_TableId",
                        column: x => x.TableId,
                        principalTable: "tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "point_transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    TransactionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_point_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_point_transactions_loyalty_wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "loyalty_wallets",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_point_transactions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "areas",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Piso 1" },
                    { 2, "Terraza" }
                });

            migrationBuilder.InsertData(
                table: "areas",
                columns: new[] { "Id", "IsQuietZone", "Name" },
                values: new object[] { 3, true, "Sala Silenciosa" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Café, té, smoothies y más", "Bebidas" },
                    { 2, "Sándwiches, pasteles y snacks", "Comida" },
                    { 3, "Acceso a espacios de trabajo", "Coworking" }
                });

            migrationBuilder.InsertData(
                table: "membership_types",
                columns: new[] { "Id", "DiscountRate", "MonthlyFee", "Name" },
                values: new object[,]
                {
                    { 1, 15.00m, 99.99m, "VIP" },
                    { 2, 10.00m, 59.99m, "Diamante" },
                    { 3, 5.00m, 29.99m, "Estudiante" }
                });

            migrationBuilder.InsertData(
                table: "promotions",
                columns: new[] { "Id", "Description", "DiscountCode", "EndDate", "ImageUrl", "IsActive", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "Café + Croissant por $6.00", "DESAYUNO6", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=600", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Combo Desayuno" },
                    { 2, "2x1 en Cappuccinos (4pm - 6pm)", "AFTER2X1", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?w=600", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "After Office" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Espresso", 3.50m },
                    { 2, 1, "Cappuccino", 4.50m },
                    { 3, 1, "Latte Macchiato", 5.00m },
                    { 4, 2, "Sándwich de Pollo", 8.50m },
                    { 5, 2, "Croissant", 3.50m },
                    { 6, 3, "Coworking - Hora", 5.00m },
                    { 7, 3, "Coworking - Día", 25.00m }
                });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "Id", "AreaId", "Capacity", "HasOutlet", "TableNumber" },
                values: new object[,]
                {
                    { 1, 1, 2, true, "A1" },
                    { 2, 1, 4, true, "A2" }
                });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "Id", "AreaId", "Capacity", "TableNumber" },
                values: new object[] { 3, 1, 2, "A3" });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "Id", "AreaId", "Capacity", "HasOutlet", "TableNumber" },
                values: new object[] { 4, 2, 6, true, "T1" });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "Id", "AreaId", "Capacity", "TableNumber" },
                values: new object[] { 5, 2, 4, "T2" });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "Id", "AreaId", "Capacity", "HasOutlet", "TableNumber" },
                values: new object[,]
                {
                    { 6, 3, 1, true, "S1" },
                    { 7, 3, 2, true, "S2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_point_transactions_UserId",
                table: "point_transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_point_transactions_WalletId",
                table: "point_transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_TableId_StartTime_EndTime",
                table: "reservations",
                columns: new[] { "TableId", "StartTime", "EndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_UserId",
                table: "reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tables_AreaId",
                table: "tables",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_user_memberships_MembershipTypeId",
                table: "user_memberships",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_user_memberships_UserId",
                table: "user_memberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "point_transactions");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "user_memberships");

            migrationBuilder.DropTable(
                name: "loyalty_wallets");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "membership_types");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "areas");
        }
    }
}
