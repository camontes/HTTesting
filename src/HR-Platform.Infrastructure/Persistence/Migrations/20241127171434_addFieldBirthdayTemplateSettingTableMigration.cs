using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addFieldBirthdayTemplateSettingTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomMessage",
                table: "BirthdayTemplateSettings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomTemplateName",
                table: "BirthdayTemplateSettings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomTemplateURL",
                table: "BirthdayTemplateSettings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomMessage",
                table: "BirthdayTemplateSettings");

            migrationBuilder.DropColumn(
                name: "CustomTemplateName",
                table: "BirthdayTemplateSettings");

            migrationBuilder.DropColumn(
                name: "CustomTemplateURL",
                table: "BirthdayTemplateSettings");
        }
    }
}
