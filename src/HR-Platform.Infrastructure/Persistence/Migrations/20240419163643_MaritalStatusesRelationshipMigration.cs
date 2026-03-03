using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MaritalStatusesRelationshipMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaritalStatusId",
                table: "Collaborators",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MaritalStatuses",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Solter@", "Single" },
                    { 3, "Casad@", "Married" },
                    { 4, "Unión libre", "Civil union" },
                    { 5, "Separad@", "Divorced" },
                    { 6, "Viud@", "Widow/Widower" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_MaritalStatusId",
                table: "Collaborators",
                column: "MaritalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_MaritalStatuses_MaritalStatusId",
                table: "Collaborators",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_MaritalStatuses_MaritalStatusId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_MaritalStatusId",
                table: "Collaborators");

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "MaritalStatusId",
                table: "Collaborators");
        }
    }
}
