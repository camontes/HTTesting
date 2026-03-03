using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixIsVisibleFormMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisibleInArea",
                table: "Forms");

            migrationBuilder.RenameColumn(
                name: "IsVisibleWithEye",
                table: "Forms",
                newName: "IsVisible");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "Forms",
                newName: "IsVisibleWithEye");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleInArea",
                table: "Forms",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
