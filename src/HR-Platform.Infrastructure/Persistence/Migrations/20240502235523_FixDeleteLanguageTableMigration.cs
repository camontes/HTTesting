using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteLanguageTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorLanguages_Languages_LanguageId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorLanguages_LanguageId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "CollaboratorLanguages");

            migrationBuilder.AlterColumn<string>(
                name: "OtherLanguageNameEnglish",
                table: "CollaboratorLanguages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OtherLanguageName",
                table: "CollaboratorLanguages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "DefaultLanguageLevelId",
                table: "CollaboratorLanguages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultLanguageTypeId",
                table: "CollaboratorLanguages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLanguages_DefaultLanguageLevelId",
                table: "CollaboratorLanguages",
                column: "DefaultLanguageLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLanguages_DefaultLanguageTypeId",
                table: "CollaboratorLanguages",
                column: "DefaultLanguageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorLanguages_DefaultLanguageLevels_DefaultLanguage~",
                table: "CollaboratorLanguages",
                column: "DefaultLanguageLevelId",
                principalTable: "DefaultLanguageLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorLanguages_DefaultLanguageTypes_DefaultLanguageT~",
                table: "CollaboratorLanguages",
                column: "DefaultLanguageTypeId",
                principalTable: "DefaultLanguageTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorLanguages_DefaultLanguageLevels_DefaultLanguage~",
                table: "CollaboratorLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorLanguages_DefaultLanguageTypes_DefaultLanguageT~",
                table: "CollaboratorLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorLanguages_DefaultLanguageLevelId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorLanguages_DefaultLanguageTypeId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropColumn(
                name: "DefaultLanguageLevelId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropColumn(
                name: "DefaultLanguageTypeId",
                table: "CollaboratorLanguages");

            migrationBuilder.AlterColumn<string>(
                name: "OtherLanguageNameEnglish",
                table: "CollaboratorLanguages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherLanguageName",
                table: "CollaboratorLanguages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "CollaboratorLanguages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultLanguageLevelId = table.Column<int>(type: "integer", nullable: false),
                    DefaultLanguageTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_DefaultLanguageLevels_DefaultLanguageLevelId",
                        column: x => x.DefaultLanguageLevelId,
                        principalTable: "DefaultLanguageLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Languages_DefaultLanguageTypes_DefaultLanguageTypeId",
                        column: x => x.DefaultLanguageTypeId,
                        principalTable: "DefaultLanguageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLanguages_LanguageId",
                table: "CollaboratorLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_DefaultLanguageLevelId",
                table: "Languages",
                column: "DefaultLanguageLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_DefaultLanguageTypeId",
                table: "Languages",
                column: "DefaultLanguageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorLanguages_Languages_LanguageId",
                table: "CollaboratorLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
