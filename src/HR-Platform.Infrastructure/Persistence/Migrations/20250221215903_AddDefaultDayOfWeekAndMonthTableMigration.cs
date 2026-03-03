using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultDayOfWeekAndMonthTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultDaysOfWeekId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultMonthId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultDaysOfWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultDaysOfWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultMonths", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultDaysOfWeeks",
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

            migrationBuilder.InsertData(
                table: "DefaultMonths",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Enero", "January" },
                    { 3, "Febrero", "February" },
                    { 4, "Marzo", "March" },
                    { 5, "Abril", "April" },
                    { 6, "Mayo", "May" },
                    { 7, "Junio", "June" },
                    { 8, "Julio", "July" },
                    { 9, "Agosto", "August" },
                    { 10, "Septiembre", "September" },
                    { 11, "Octubre", "October" },
                    { 12, "Noviembre", "November" },
                    { 13, "Diciembre", "December" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultDaysOfWeekId",
                table: "CollaboratorEducations",
                column: "DefaultDaysOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultMonthId",
                table: "CollaboratorEducations",
                column: "DefaultMonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultDaysOfWeeks_DefaultDaysOfWeek~",
                table: "CollaboratorEducations",
                column: "DefaultDaysOfWeekId",
                principalTable: "DefaultDaysOfWeeks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultMonths_DefaultMonthId",
                table: "CollaboratorEducations",
                column: "DefaultMonthId",
                principalTable: "DefaultMonths",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultDaysOfWeeks_DefaultDaysOfWeek~",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultMonths_DefaultMonthId",
                table: "CollaboratorEducations");

            migrationBuilder.DropTable(
                name: "DefaultDaysOfWeeks");

            migrationBuilder.DropTable(
                name: "DefaultMonths");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorEducations_DefaultDaysOfWeekId",
                table: "CollaboratorEducations");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorEducations_DefaultMonthId",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "DefaultDaysOfWeekId",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "DefaultMonthId",
                table: "CollaboratorEducations");
        }
    }
}
