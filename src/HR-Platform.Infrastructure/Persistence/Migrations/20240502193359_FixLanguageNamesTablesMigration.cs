using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixLanguageNamesTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_DefaultLanguageLevel_DefaultLanguageLevelId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_DefaultLanguageType_DefaultLanguageTypeId",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultLanguageType",
                table: "DefaultLanguageType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultLanguageLevel",
                table: "DefaultLanguageLevel");

            migrationBuilder.RenameTable(
                name: "DefaultLanguageType",
                newName: "DefaultLanguageTypes");

            migrationBuilder.RenameTable(
                name: "DefaultLanguageLevel",
                newName: "DefaultLanguageLevels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultLanguageTypes",
                table: "DefaultLanguageTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultLanguageLevels",
                table: "DefaultLanguageLevels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_DefaultLanguageLevels_DefaultLanguageLevelId",
                table: "Languages",
                column: "DefaultLanguageLevelId",
                principalTable: "DefaultLanguageLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_DefaultLanguageTypes_DefaultLanguageTypeId",
                table: "Languages",
                column: "DefaultLanguageTypeId",
                principalTable: "DefaultLanguageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_DefaultLanguageLevels_DefaultLanguageLevelId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_DefaultLanguageTypes_DefaultLanguageTypeId",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultLanguageTypes",
                table: "DefaultLanguageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultLanguageLevels",
                table: "DefaultLanguageLevels");

            migrationBuilder.RenameTable(
                name: "DefaultLanguageTypes",
                newName: "DefaultLanguageType");

            migrationBuilder.RenameTable(
                name: "DefaultLanguageLevels",
                newName: "DefaultLanguageLevel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultLanguageType",
                table: "DefaultLanguageType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultLanguageLevel",
                table: "DefaultLanguageLevel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_DefaultLanguageLevel_DefaultLanguageLevelId",
                table: "Languages",
                column: "DefaultLanguageLevelId",
                principalTable: "DefaultLanguageLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_DefaultLanguageType_DefaultLanguageTypeId",
                table: "Languages",
                column: "DefaultLanguageTypeId",
                principalTable: "DefaultLanguageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
