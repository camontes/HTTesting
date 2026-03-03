using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DefaultFamilyCompositionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultFamilyCompositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultFamilyCompositions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultFamilyCompositions",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Esposo/a", "Husband/Wife" },
                    { 3, "Esposo/a e hijos", "Husband/Wife and children" },
                    { 4, "Hijos", "Children" },
                    { 5, "Madre/Padre y hermanos", "Mother/Father and siblings" },
                    { 6, "Padres", "Parents" },
                    { 7, "Hermanos", "Siblings" },
                    { 8, "Solo", "Alone" },
                    { 9, "Otro", "Other" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultFamilyCompositions");
        }
    }
}
