using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NotificationType1SeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "Message", "MessageEnglish" },
                values: new object[] { 1, "La solicitud al beneficio @1 ha sido @2. Para más información, consulte con el equipo encargado", "The application to the @1 benefit has been @2. For more information, please contact the team in charge" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
