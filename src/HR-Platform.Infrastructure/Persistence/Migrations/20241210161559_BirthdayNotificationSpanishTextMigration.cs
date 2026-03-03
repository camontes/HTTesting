using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BirthdayNotificationSpanishTextMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Message", "MessageEnglish" },
                values: new object[] { "La solicitud al beneficio @1 ha sido @2. Para más información, consulte con el equipo encargado.", "The application to the @1 benefit has been @2. For more information, please contact the team in charge." });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Message", "MessageEnglish" },
                values: new object[] { 2, "Feliz cumpleaños @1. Disfruta de tu día al máximo y que todos tus sueños se hagan realidad.", "Happy birthday @1. Enjoy your day to the fullest and may all your dreams come true." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Message", "MessageEnglish" },
                values: new object[] { "La solicitud al beneficio @1 ha sido @2. Para más información, consulte con el equipo encargado", "The application to the @1 benefit has been @2. For more information, please contact the team in charge" });
        }
    }
}
