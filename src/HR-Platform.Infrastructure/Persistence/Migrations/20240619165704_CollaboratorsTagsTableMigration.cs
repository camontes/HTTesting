using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CollaboratorsTagsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Tag_TagId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_TagId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Collaborators");

            migrationBuilder.CreateTable(
                name: "CollaboratorTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorTag_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTag_CollaboratorId",
                table: "CollaboratorTag",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTag_TagId",
                table: "CollaboratorTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTag");

            migrationBuilder.AddColumn<Guid>(
                name: "TagId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_TagId",
                table: "Collaborators",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Tag_TagId",
                table: "Collaborators",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
