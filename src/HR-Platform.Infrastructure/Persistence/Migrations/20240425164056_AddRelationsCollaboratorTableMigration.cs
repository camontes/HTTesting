using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsCollaboratorTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_EconomicLevels_EconomicLevelId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<int>(
                name: "EconomicLevelId",
                table: "Collaborators",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_EconomicLevels_EconomicLevelId",
                table: "Collaborators",
                column: "EconomicLevelId",
                principalTable: "EconomicLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_EconomicLevels_EconomicLevelId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<int>(
                name: "EconomicLevelId",
                table: "Collaborators",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_EconomicLevels_EconomicLevelId",
                table: "Collaborators",
                column: "EconomicLevelId",
                principalTable: "EconomicLevels",
                principalColumn: "Id");
        }
    }
}
