using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultContractTypes_DefaultContractT~",
                table: "CollaboratorContracts");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultContractTypeId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId",
                table: "CollaboratorContracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId1",
                table: "CollaboratorContracts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractTypes",
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
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_ContractTypeId1",
                table: "CollaboratorContracts",
                column: "ContractTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_CompanyId",
                table: "ContractTypes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_ContractTypes_ContractTypeId1",
                table: "CollaboratorContracts",
                column: "ContractTypeId1",
                principalTable: "ContractTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultContractTypes_DefaultContractT~",
                table: "CollaboratorContracts",
                column: "DefaultContractTypeId",
                principalTable: "DefaultContractTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_ContractTypes_ContractTypeId1",
                table: "CollaboratorContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultContractTypes_DefaultContractT~",
                table: "CollaboratorContracts");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_ContractTypeId1",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "ContractTypeId1",
                table: "CollaboratorContracts");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultContractTypeId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultContractTypes_DefaultContractT~",
                table: "CollaboratorContracts",
                column: "DefaultContractTypeId",
                principalTable: "DefaultContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
