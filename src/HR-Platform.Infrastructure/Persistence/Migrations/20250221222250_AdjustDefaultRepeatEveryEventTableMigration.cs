using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDefaultRepeatEveryEventTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "DefaultDaysOfWeeks",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Sábado");

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Día", "Day" });

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Semana", "Week" });

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Mes", "Month" });

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Año", "Year" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultDaysOfWeeks",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Sáabado");

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Lunes", "Monday" });

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Martes", "Tuesday" });

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Miércoles", "Wednesday" });

            migrationBuilder.UpdateData(
                table: "DefaultRepeatEveryEvents",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Jueves", "Thursday" });

            migrationBuilder.InsertData(
                table: "DefaultRepeatEveryEvents",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 6, "Viernes", "Friday" },
                    { 7, "Sáabado", "Saturday" },
                    { 8, "Domingo", "Sunday" }
                });
        }
    }
}
