using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentManagementTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentManagementFileTypeId = table.Column<int>(type: "integer", nullable: false),
                    Other = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UrlFile = table.Column<string>(type: "text", nullable: false),
                    EmailWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlPhotoWhoChangedByTH = table.Column<string>(type: "text", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentManagement_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentManagement_DocumentManagementFileTypes_DocumentMana~",
                        column: x => x.DocumentManagementFileTypeId,
                        principalTable: "DocumentManagementFileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentManagement_CollaboratorId",
                table: "DocumentManagement",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentManagement_DocumentManagementFileTypeId",
                table: "DocumentManagement",
                column: "DocumentManagementFileTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentManagement");
        }
    }
}
