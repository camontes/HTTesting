using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixBirthdayNotificationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Message", "MessageEnglish" },
                values: new object[] { "¡Feliz cumpleaños! @1. Disfruta de tu día al máximo y que todos tus sueños se hagan realidad.", "Happy birthday! @1. Enjoy your day to the fullest and may all your dreams come true." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Message", "MessageEnglish" },
                values: new object[] { "Feliz cumpleaños @1. Disfruta de tu día al máximo y que todos tus sueños se hagan realidad.", "Happy birthday @1. Enjoy your day to the fullest and may all your dreams come true." });
        }
    }
}
