using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixRiskTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_Companies_CompanyId",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_RiskTypes_RiskTypeId",
                table: "Risk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Risk",
                table: "Risk");

            migrationBuilder.RenameTable(
                name: "Risk",
                newName: "Risks");

            migrationBuilder.RenameColumn(
                name: "VideoURl",
                table: "Risks",
                newName: "VideoURL");

            migrationBuilder.RenameColumn(
                name: "ImagenName",
                table: "Risks",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "Despcription",
                table: "Risks",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Risk_RiskTypeId",
                table: "Risks",
                newName: "IX_Risks_RiskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Risk_CompanyId",
                table: "Risks",
                newName: "IX_Risks_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Risks",
                table: "Risks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_Companies_CompanyId",
                table: "Risks",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_RiskTypes_RiskTypeId",
                table: "Risks",
                column: "RiskTypeId",
                principalTable: "RiskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risks_Companies_CompanyId",
                table: "Risks");

            migrationBuilder.DropForeignKey(
                name: "FK_Risks_RiskTypes_RiskTypeId",
                table: "Risks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Risks",
                table: "Risks");

            migrationBuilder.RenameTable(
                name: "Risks",
                newName: "Risk");

            migrationBuilder.RenameColumn(
                name: "VideoURL",
                table: "Risk",
                newName: "VideoURl");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Risk",
                newName: "ImagenName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Risk",
                newName: "Despcription");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_RiskTypeId",
                table: "Risk",
                newName: "IX_Risk_RiskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_CompanyId",
                table: "Risk",
                newName: "IX_Risk_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Risk",
                table: "Risk",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_Companies_CompanyId",
                table: "Risk",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_RiskTypes_RiskTypeId",
                table: "Risk",
                column: "RiskTypeId",
                principalTable: "RiskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
