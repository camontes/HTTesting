using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixProfessionalAdviceTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_ProfessionalAdvices_ProfessionalAdviceId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<string>(
                name: "NameAcronymsEnglish",
                table: "ProfessionalAdvices",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionalAdviceId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_ProfessionalAdvices_ProfessionalAdviceId",
                table: "Collaborators",
                column: "ProfessionalAdviceId",
                principalTable: "ProfessionalAdvices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_ProfessionalAdvices_ProfessionalAdviceId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<string>(
                name: "NameAcronymsEnglish",
                table: "ProfessionalAdvices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionalAdviceId",
                table: "Collaborators",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_ProfessionalAdvices_ProfessionalAdviceId",
                table: "Collaborators",
                column: "ProfessionalAdviceId",
                principalTable: "ProfessionalAdvices",
                principalColumn: "Id");
        }
    }
}
