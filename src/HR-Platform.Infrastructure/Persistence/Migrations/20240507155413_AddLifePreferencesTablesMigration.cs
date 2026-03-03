using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLifePreferencesTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultLifePreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultLifePreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorLifePreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultLifePreferenceId = table.Column<int>(type: "integer", nullable: true),
                    OtherLanguageName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherLanguageNameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorLifePreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorLifePreferences_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorLifePreferences_DefaultLifePreferences_DefaultL~",
                        column: x => x.DefaultLifePreferenceId,
                        principalTable: "DefaultLifePreferences",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DefaultLifePreferences",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 1, "Ninguno", "None" });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLifePreferences_CollaboratorId",
                table: "CollaboratorLifePreferences",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLifePreferences_DefaultLifePreferenceId",
                table: "CollaboratorLifePreferences",
                column: "DefaultLifePreferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorLifePreferences");

            migrationBuilder.DropTable(
                name: "DefaultLifePreferences");
        }
    }
}
