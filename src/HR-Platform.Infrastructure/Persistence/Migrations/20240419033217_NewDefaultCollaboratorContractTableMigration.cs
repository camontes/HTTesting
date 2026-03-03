using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultCollaboratorContractTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContract_Companies_CompanyId",
                table: "CollaboratorContract");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorContract",
                table: "CollaboratorContract");

            migrationBuilder.RenameTable(
                name: "CollaboratorContract",
                newName: "CollaboratorContracts");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorContract_CompanyId",
                table: "CollaboratorContracts",
                newName: "IX_CollaboratorContracts_CompanyId");

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorContractId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorContracts",
                table: "CollaboratorContracts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DefaultCollaboratorContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Arl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Bonus = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ContractType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultCollaboratorContracts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultCollaboratorContracts",
                columns: new[] { "Id", "Arl", "Bonus", "ContractType", "Salary" },
                values: new object[] { 1, "Ninguno", "Ninguno", "Ninguno", 0m });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_CollaboratorContractId",
                table: "Collaborators",
                column: "CollaboratorContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_Companies_CompanyId",
                table: "CollaboratorContracts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_CollaboratorContracts_CollaboratorContractId",
                table: "Collaborators",
                column: "CollaboratorContractId",
                principalTable: "CollaboratorContracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_Companies_CompanyId",
                table: "CollaboratorContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_CollaboratorContracts_CollaboratorContractId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "DefaultCollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_CollaboratorContractId",
                table: "Collaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorContracts",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "CollaboratorContractId",
                table: "Collaborators");

            migrationBuilder.RenameTable(
                name: "CollaboratorContracts",
                newName: "CollaboratorContract");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorContracts_CompanyId",
                table: "CollaboratorContract",
                newName: "IX_CollaboratorContract_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorContract",
                table: "CollaboratorContract",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContract_Companies_CompanyId",
                table: "CollaboratorContract",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
