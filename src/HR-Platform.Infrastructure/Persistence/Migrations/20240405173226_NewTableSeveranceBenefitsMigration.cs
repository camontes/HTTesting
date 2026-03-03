using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewTableSeveranceBenefitsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SeveranceBenefitId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeveranceBenefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeveranceBenefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeveranceBenefits_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_SeveranceBenefitId",
                table: "Collaborators",
                column: "SeveranceBenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_SeveranceBenefits_CompanyId",
                table: "SeveranceBenefits",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_SeveranceBenefits_SeveranceBenefitId",
                table: "Collaborators",
                column: "SeveranceBenefitId",
                principalTable: "SeveranceBenefits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_SeveranceBenefits_SeveranceBenefitId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "SeveranceBenefits");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_SeveranceBenefitId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "SeveranceBenefitId",
                table: "Collaborators");
        }
    }
}
