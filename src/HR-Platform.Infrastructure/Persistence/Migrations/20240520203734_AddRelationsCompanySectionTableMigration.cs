using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsCompanySectionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Roles_CompanySectionId",
                table: "Roles",
                column: "CompanySectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_CompanySections_CompanySectionId",
                table: "Roles",
                column: "CompanySectionId",
                principalTable: "CompanySections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_CompanySections_CompanySectionId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_CompanySectionId",
                table: "Roles");
        }
    }
}
