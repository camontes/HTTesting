using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class HealthEntitiesRelationshipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "HealthEntityId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "HealthEntityId",
                table: "Collaborators",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntity",
                principalColumn: "Id");
        }
    }
}
