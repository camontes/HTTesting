using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInFormsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumNumberCharactersAnswer",
                table: "FormQuestionsTypes");

            migrationBuilder.AddColumn<string>(
                name: "NumberReference",
                table: "Forms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberReference",
                table: "Forms");

            migrationBuilder.AddColumn<int>(
                name: "MaximumNumberCharactersAnswer",
                table: "FormQuestionsTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
