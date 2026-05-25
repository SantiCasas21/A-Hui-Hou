using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AHuiHou.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePromotionsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Café + Croissant recién horneado por solo $6.00. Disponible hasta las 11am.", "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=800" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "2x1 en todos los Cappuccinos de 4pm a 6pm. El after work perfecto.", "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?w=800", "After Office 2x1" });

            migrationBuilder.InsertData(
                table: "promotions",
                columns: new[] { "Id", "Description", "DiscountCode", "EndDate", "ImageUrl", "IsActive", "StartDate", "Title" },
                values: new object[,]
                {
                    { 3, "Todos los lunes, 20% de descuento en cualquier Latte. Empieza la semana con energía.", "LUNESLATTE", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lunes de Latte" },
                    { 4, "Acceso ilimitado a nuestro espacio de coworking por $25. Incluye café de cortesía.", "COWORKDAY", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pase Coworking - Día" },
                    { 5, "Regístrate y recibe un café de bienvenida totalmente gratis. ¡Te esperamos!", "WELCOMECOFFEE", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Primer Café Gratis" },
                    { 6, "30% de descuento en coworking para estudiantes con credencial vigente. WiFi + café ilimitado.", "STUDENT30", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1434030216411-0b793f4b4173?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plan Estudiante" },
                    { 7, "Cualquier té + pastry a mitad de precio de 3pm a 5pm. El plan perfecto para la merienda.", "TEATIME50", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tarde de Té & Pastelería" },
                    { 8, "Reserva nuestra sala para tu equipo. 4 horas de coworking + 4 cafés por solo $40.", "TEAM40", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1600880292203-757bb62b4baf?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Reunión de Equipo" },
                    { 9, "Cada sábado, prueba un café de origen único con 15% de descuento. Granos seleccionados.", "SABADO15", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1442512595331-e89e73853f31?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sábados de Especialidad" },
                    { 10, "Trae a un amigo y ambos obtienen 50 puntos de lealtad extra en su primera compra juntos.", "AMIGO50", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1529333166437-7750a6dd5a70?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Amigo Referido" },
                    { 11, "Todos los jueves, lleva un libro prestado de nuestra biblioteca y recibe un espresso cortesía.", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1513475382585-d06e58bc1ae2?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Café & Libro" },
                    { 12, "3 piezas de pastelería artesanal + 2 cafés americanos por $14. Para compartir o darte un gusto.", "PASTRYPACK", new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1509365465985-25d11c17e812?w=800", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pack Repostería" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Café + Croissant por $6.00", "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=600" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "2x1 en Cappuccinos (4pm - 6pm)", "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?w=600", "After Office" });
        }
    }
}
