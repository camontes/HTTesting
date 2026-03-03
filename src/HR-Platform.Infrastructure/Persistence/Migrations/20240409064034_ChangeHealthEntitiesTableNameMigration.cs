using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHealthEntitiesTableNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthEntity_Companies_CompanyId",
                table: "HealthEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthEntity",
                table: "HealthEntity");

            migrationBuilder.RenameTable(
                name: "HealthEntity",
                newName: "HealthEntities");

            migrationBuilder.RenameIndex(
                name: "IX_HealthEntity_CompanyId",
                table: "HealthEntities",
                newName: "IX_HealthEntities_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthEntities",
                table: "HealthEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntities_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthEntities_Companies_CompanyId",
                table: "HealthEntities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntities_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthEntities_Companies_CompanyId",
                table: "HealthEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthEntities",
                table: "HealthEntities");

            migrationBuilder.RenameTable(
                name: "HealthEntities",
                newName: "HealthEntity");

            migrationBuilder.RenameIndex(
                name: "IX_HealthEntities_CompanyId",
                table: "HealthEntity",
                newName: "IX_HealthEntity_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthEntity",
                table: "HealthEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthEntity_Companies_CompanyId",
                table: "HealthEntity",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
