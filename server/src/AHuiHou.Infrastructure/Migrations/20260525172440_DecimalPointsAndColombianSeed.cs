using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AHuiHou.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DecimalPointsAndColombianSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "point_transactions",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "loyalty_wallets",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Café de especialidad, tés, frappés y más");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Sándwiches artesanales, repostería y bowls");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Planes de acceso a espacios de trabajo");

            migrationBuilder.UpdateData(
                table: "membership_types",
                keyColumn: "Id",
                keyValue: 1,
                column: "MonthlyFee",
                value: 149900m);

            migrationBuilder.UpdateData(
                table: "membership_types",
                keyColumn: "Id",
                keyValue: 2,
                column: "MonthlyFee",
                value: 89900m);

            migrationBuilder.UpdateData(
                table: "membership_types",
                keyColumn: "Id",
                keyValue: 3,
                column: "MonthlyFee",
                value: 39900m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 5500m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 7500m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 9000m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Americano", 5000m });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Prensa Francesa", 8500m });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Mocaccino", 9500m });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Té Chai Latte", 8000m });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 8, 1, "Cold Brew", 10000m },
                    { 9, 1, "Frappé de Café", 12500m },
                    { 10, 1, "Matcha Latte", 11000m },
                    { 11, 2, "Croissant Artesanal", 7000m },
                    { 12, 2, "Torta de Almojábana", 8500m },
                    { 13, 2, "Sándwich de Pollo", 18000m },
                    { 14, 2, "Sándwich Artesanal", 20000m },
                    { 15, 2, "Bagel con Salmón", 22000m },
                    { 16, 2, "Ensalada César", 16500m },
                    { 17, 2, "Bowl de Frutas", 15000m },
                    { 18, 2, "Pastel de Chocolate", 12000m },
                    { 19, 3, "Coworking - Hora", 8000m },
                    { 20, 3, "Coworking - Medio Día", 25000m },
                    { 21, 3, "Coworking - Día Completo", 45000m },
                    { 22, 3, "Coworking - Semana", 180000m }
                });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "Café Americano + Croissant artesanal por solo $10.000. Válido hasta las 11am.", "DESAYUNO10K" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "2x1 en Cappuccinos de 4pm a 6pm. Trae a tu colega y solo pagas uno.");

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Todos los lunes, 20% de descuento en cualquier latte. Tu semana empieza mejor.");

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DiscountCode", "Title" },
                values: new object[] { "Día completo de coworking por solo $35.000 (antes $45.000). Café ilimitado incluido.", "COWORK35K", "Pase Coworking Día" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "Regístrate y recibe un espresso o americano de bienvenida totalmente gratis.", "BIENVENIDO" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "30% de descuento en planes de coworking con carnet vigente. WiFi + café ilimitado.", "ESTUDIANTE30" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Cualquier té + pastry a mitad de precio de 3pm a 5pm. La hora del té perfecta.", "Té + Pastelería 50% Off" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "DiscountCode", "Title" },
                values: new object[] { "4 horas de coworking en sala privada + 4 cafés por solo $80.000. Ideal para brainstorms.", "TEAM80K", "Sala para Equipos" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Cada sábado, prueba un café de origen colombiano con 15% de descuento. Conoce nuestro café.", "Sábados de Origen Único" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "Trae a un amigo nuevo y ambos ganan 100 puntos de lealtad extra en su primera compra.", "AMIGO100" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Jueves: toma un libro de nuestra biblioteca y recibe un espresso cortesía. Lectura + café.");

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "DiscountCode", "Title" },
                values: new object[] { "3 piezas de pastelería + 2 americanos por solo $22.000. Perfecto para compartir.", "PASTRY22K", "Pack Repostería x3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "point_transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Balance",
                table: "loyalty_wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldDefaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Café, té, smoothies y más");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Sándwiches, pasteles y snacks");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Acceso a espacios de trabajo");

            migrationBuilder.UpdateData(
                table: "membership_types",
                keyColumn: "Id",
                keyValue: 1,
                column: "MonthlyFee",
                value: 99.99m);

            migrationBuilder.UpdateData(
                table: "membership_types",
                keyColumn: "Id",
                keyValue: 2,
                column: "MonthlyFee",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "membership_types",
                keyColumn: "Id",
                keyValue: 3,
                column: "MonthlyFee",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 3.50m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 4.50m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 5.00m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Sándwich de Pollo", 8.50m });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Croissant", 3.50m });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Coworking - Hora", 5.00m });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Coworking - Día", 25.00m });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "Café + Croissant recién horneado por solo $6.00. Disponible hasta las 11am.", "DESAYUNO6" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "2x1 en todos los Cappuccinos de 4pm a 6pm. El after work perfecto.");

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Todos los lunes, 20% de descuento en cualquier Latte. Empieza la semana con energía.");

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DiscountCode", "Title" },
                values: new object[] { "Acceso ilimitado a nuestro espacio de coworking por $25. Incluye café de cortesía.", "COWORKDAY", "Pase Coworking - Día" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "Regístrate y recibe un café de bienvenida totalmente gratis. ¡Te esperamos!", "WELCOMECOFFEE" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "30% de descuento en coworking para estudiantes con credencial vigente. WiFi + café ilimitado.", "STUDENT30" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Cualquier té + pastry a mitad de precio de 3pm a 5pm. El plan perfecto para la merienda.", "Tarde de Té & Pastelería" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "DiscountCode", "Title" },
                values: new object[] { "Reserva nuestra sala para tu equipo. 4 horas de coworking + 4 cafés por solo $40.", "TEAM40", "Reunión de Equipo" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Cada sábado, prueba un café de origen único con 15% de descuento. Granos seleccionados.", "Sábados de Especialidad" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "DiscountCode" },
                values: new object[] { "Trae a un amigo y ambos obtienen 50 puntos de lealtad extra en su primera compra juntos.", "AMIGO50" });

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Todos los jueves, lleva un libro prestado de nuestra biblioteca y recibe un espresso cortesía.");

            migrationBuilder.UpdateData(
                table: "promotions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "DiscountCode", "Title" },
                values: new object[] { "3 piezas de pastelería artesanal + 2 cafés americanos por $14. Para compartir o darte un gusto.", "PASTRYPACK", "Pack Repostería" });
        }
    }
}
