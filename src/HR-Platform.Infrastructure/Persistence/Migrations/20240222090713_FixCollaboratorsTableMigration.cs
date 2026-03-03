using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCollaboratorsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherDocumentType",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "OtherGender",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "OtherMaritalStatus",
                table: "Collaborators");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Collaborators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignationId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CVFile",
                table: "Collaborators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                table: "Collaborators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntranceDate",
                table: "Collaborators",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_AreaId",
                table: "Collaborators",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_AssignationId",
                table: "Collaborators",
                column: "AssignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_DocumentTypeId",
                table: "Collaborators",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Areas_AreaId",
                table: "Collaborators",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Assignations_AssignationId",
                table: "Collaborators",
                column: "AssignationId",
                principalTable: "Assignations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_DocumentTypes_DocumentTypeId",
                table: "Collaborators",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Areas_AreaId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Assignations_AssignationId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_DocumentTypes_DocumentTypeId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_AreaId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_AssignationId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_DocumentTypeId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "AssignationId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "CVFile",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "EntranceDate",
                table: "Collaborators");

            migrationBuilder.AddColumn<string>(
                name: "OtherDocumentType",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtherGender",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtherMaritalStatus",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
