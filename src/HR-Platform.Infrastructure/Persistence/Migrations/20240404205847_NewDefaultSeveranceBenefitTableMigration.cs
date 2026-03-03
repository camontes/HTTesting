using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultSeveranceBenefitTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultSeveranceBenefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultSeveranceBenefits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultSeveranceBenefits",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Colfondos", "Colfondos" },
                    { 3, "Colpensiones", "Colpensiones" },
                    { 4, "FNA", "FNA" },
                    { 5, "IMSS", "IMSS" },
                    { 6, "Old Mutual/Skandia", "Old Mutual/Skandia" },
                    { 7, "Porvenir", "Porvenir" },
                    { 8, "Protección", "Protección" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultSeveranceBenefits");
        }
    }
}
