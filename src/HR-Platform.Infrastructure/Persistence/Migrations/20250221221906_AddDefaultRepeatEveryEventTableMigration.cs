using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRepeatEveryEventTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultRepeatEveryEventId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultRepeatEveryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultRepeatEveryEvents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultRepeatEveryEvents",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Lunes", "Monday" },
                    { 3, "Martes", "Tuesday" },
                    { 4, "Miércoles", "Wednesday" },
                    { 5, "Jueves", "Thursday" },
                    { 6, "Viernes", "Friday" },
                    { 7, "Sáabado", "Saturday" },
                    { 8, "Domingo", "Sunday" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultRepeatEveryEventId",
                table: "CollaboratorEducations",
                column: "DefaultRepeatEveryEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultRepeatEveryEvents_DefaultRepe~",
                table: "CollaboratorEducations",
                column: "DefaultRepeatEveryEventId",
                principalTable: "DefaultRepeatEveryEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultRepeatEveryEvents_DefaultRepe~",
                table: "CollaboratorEducations");

            migrationBuilder.DropTable(
                name: "DefaultRepeatEveryEvents");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorEducations_DefaultRepeatEveryEventId",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "DefaultRepeatEveryEventId",
                table: "CollaboratorEducations");
        }
    }
}
