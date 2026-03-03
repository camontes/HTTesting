using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TagId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TagId1",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_TagId1",
                table: "Collaborators",
                column: "TagId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CompanyId",
                table: "Tag",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Tag_TagId1",
                table: "Collaborators",
                column: "TagId1",
                principalTable: "Tag",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Tag_TagId1",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_TagId1",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "TagId1",
                table: "Collaborators");
        }
    }
}
