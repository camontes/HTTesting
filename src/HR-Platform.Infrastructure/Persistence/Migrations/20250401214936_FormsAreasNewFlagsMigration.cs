using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormsAreasNewFlagsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleInArea",
                table: "Forms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleWithEye",
                table: "Forms",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisibleInArea",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "IsVisibleWithEye",
                table: "Forms");
        }
    }
}
