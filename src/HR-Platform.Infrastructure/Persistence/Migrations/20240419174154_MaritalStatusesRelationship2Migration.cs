using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MaritalStatusesRelationship2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_MaritalStatuses_MaritalStatusId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "Collaborators",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_MaritalStatuses_MaritalStatusId",
                table: "Collaborators",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_MaritalStatuses_MaritalStatusId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "Collaborators",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_MaritalStatuses_MaritalStatusId",
                table: "Collaborators",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id");
        }
    }
}
