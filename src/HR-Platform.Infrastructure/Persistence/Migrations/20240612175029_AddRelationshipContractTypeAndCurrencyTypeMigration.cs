using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipContractTypeAndCurrencyTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_DefaultContractTypeId",
                table: "CollaboratorContracts",
                column: "DefaultContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_DefaultCurrencyTypeId",
                table: "CollaboratorContracts",
                column: "DefaultCurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultContractTypes_DefaultContractT~",
                table: "CollaboratorContracts",
                column: "DefaultContractTypeId",
                principalTable: "DefaultContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultCurrencyTypes_DefaultCurrencyT~",
                table: "CollaboratorContracts",
                column: "DefaultCurrencyTypeId",
                principalTable: "DefaultCurrencyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultContractTypes_DefaultContractT~",
                table: "CollaboratorContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultCurrencyTypes_DefaultCurrencyT~",
                table: "CollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_DefaultContractTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_DefaultCurrencyTypeId",
                table: "CollaboratorContracts");
        }
    }
}
