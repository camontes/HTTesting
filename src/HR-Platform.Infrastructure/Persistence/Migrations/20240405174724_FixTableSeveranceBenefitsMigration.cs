using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTableSeveranceBenefitsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_SeveranceBenefits_SeveranceBenefitId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeveranceBenefitId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_SeveranceBenefits_SeveranceBenefitId",
                table: "Collaborators",
                column: "SeveranceBenefitId",
                principalTable: "SeveranceBenefits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_SeveranceBenefits_SeveranceBenefitId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeveranceBenefitId",
                table: "Collaborators",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_SeveranceBenefits_SeveranceBenefitId",
                table: "Collaborators",
                column: "SeveranceBenefitId",
                principalTable: "SeveranceBenefits",
                principalColumn: "Id");
        }
    }
}
