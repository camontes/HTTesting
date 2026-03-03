using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixJobAreasTableNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobAreas_Companies_CompanyId",
                table: "jobAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_jobAreas",
                table: "jobAreas");

            migrationBuilder.RenameTable(
                name: "jobAreas",
                newName: "JobAreas");

            migrationBuilder.RenameIndex(
                name: "IX_jobAreas_CompanyId",
                table: "JobAreas",
                newName: "IX_JobAreas_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAreas",
                table: "JobAreas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAreas_Companies_CompanyId",
                table: "JobAreas",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAreas_Companies_CompanyId",
                table: "JobAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAreas",
                table: "JobAreas");

            migrationBuilder.RenameTable(
                name: "JobAreas",
                newName: "jobAreas");

            migrationBuilder.RenameIndex(
                name: "IX_JobAreas_CompanyId",
                table: "jobAreas",
                newName: "IX_jobAreas_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_jobAreas",
                table: "jobAreas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_jobAreas_Companies_CompanyId",
                table: "jobAreas",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
