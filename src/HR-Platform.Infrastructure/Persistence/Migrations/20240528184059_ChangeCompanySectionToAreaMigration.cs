using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCompanySectionToAreaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_CompanySections_CompanySectionId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "CompanySections");

            migrationBuilder.RenameColumn(
                name: "CompanySectionId",
                table: "Roles",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_CompanySectionId",
                table: "Roles",
                newName: "IX_Roles_AreaId");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Operaciones", "Operations" },
                    { 3, "Talento Humano", "Human Talent" },
                    { 4, "Infraestrcutura", "Infrastructure" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Areas_AreaId",
                table: "Roles",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Areas_AreaId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Roles",
                newName: "CompanySectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_AreaId",
                table: "Roles",
                newName: "IX_Roles_CompanySectionId");

            migrationBuilder.CreateTable(
                name: "CompanySections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySections", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CompanySections",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Operaciones", "Operations" },
                    { 3, "Talento Humano", "Human Talent" },
                    { 4, "Infraestrcutura", "Infrastructure" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_CompanySections_CompanySectionId",
                table: "Roles",
                column: "CompanySectionId",
                principalTable: "CompanySections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
