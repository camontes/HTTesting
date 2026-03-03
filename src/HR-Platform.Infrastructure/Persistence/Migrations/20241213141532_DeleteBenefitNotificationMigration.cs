using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBenefitNotificationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Message", "MessageEnglish" },
                values: new object[] { 3, "La  solicitud al beneficio @1 @2 ha sido <em>Eliminada</em>. Para más información consulte con el equipo encargado.", "The request to the @1 @2 has been <em>Deleted</em>. For more information, please contact the team in charge." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
