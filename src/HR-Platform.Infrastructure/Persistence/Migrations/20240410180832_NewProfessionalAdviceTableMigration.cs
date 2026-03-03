using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewProfessionalAdviceTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultProfessionalAdvice",
                table: "DefaultProfessionalAdvice");

            migrationBuilder.RenameTable(
                name: "DefaultProfessionalAdvice",
                newName: "DefaultProfessionalAdvices");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionalAdviceId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultProfessionalAdvices",
                table: "DefaultProfessionalAdvices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProfessionalAdvices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameAcronyms = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    NameAcronymsEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalAdvices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionalAdvices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ProfessionalAdviceId",
                table: "Collaborators",
                column: "ProfessionalAdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalAdvices_CompanyId",
                table: "ProfessionalAdvices",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_ProfessionalAdvices_ProfessionalAdviceId",
                table: "Collaborators",
                column: "ProfessionalAdviceId",
                principalTable: "ProfessionalAdvices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_ProfessionalAdvices_ProfessionalAdviceId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "ProfessionalAdvices");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_ProfessionalAdviceId",
                table: "Collaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultProfessionalAdvices",
                table: "DefaultProfessionalAdvices");

            migrationBuilder.DropColumn(
                name: "ProfessionalAdviceId",
                table: "Collaborators");

            migrationBuilder.RenameTable(
                name: "DefaultProfessionalAdvices",
                newName: "DefaultProfessionalAdvice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultProfessionalAdvice",
                table: "DefaultProfessionalAdvice",
                column: "Id");
        }
    }
}
