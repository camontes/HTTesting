using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipContractTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_ContractTypes_ContractTypeId1",
                table: "CollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_ContractTypeId1",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "ContractTypeId1",
                table: "CollaboratorContracts");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_ContractTypeId",
                table: "CollaboratorContracts",
                column: "ContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_ContractTypes_ContractTypeId",
                table: "CollaboratorContracts",
                column: "ContractTypeId",
                principalTable: "ContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_ContractTypes_ContractTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_ContractTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId1",
                table: "CollaboratorContracts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_ContractTypeId1",
                table: "CollaboratorContracts",
                column: "ContractTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_ContractTypes_ContractTypeId1",
                table: "CollaboratorContracts",
                column: "ContractTypeId1",
                principalTable: "ContractTypes",
                principalColumn: "Id");
        }
    }
}
