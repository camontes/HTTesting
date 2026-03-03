using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PensionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PensionId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pensions",
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
                    table.PrimaryKey("PK_Pensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pensions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_PensionId",
                table: "Collaborators",
                column: "PensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pensions_CompanyId",
                table: "Pensions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Pensions_PensionId",
                table: "Collaborators",
                column: "PensionId",
                principalTable: "Pensions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Pensions_PensionId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "Pensions");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_PensionId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "PensionId",
                table: "Collaborators");
        }
    }
}
