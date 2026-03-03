using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewEmergencyPlanTypeRelationshipsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskTypeMains_EmergencyPlanTypes_EmergencyPlanTypeId1",
                table: "RiskTypeMains");

            migrationBuilder.DropIndex(
                name: "IX_RiskTypeMains_EmergencyPlanTypeId1",
                table: "RiskTypeMains");

            migrationBuilder.DropColumn(
                name: "EmergencyPlanTypeId1",
                table: "RiskTypeMains");

            migrationBuilder.CreateIndex(
                name: "IX_RiskTypeMains_EmergencyPlanTypeId",
                table: "RiskTypeMains",
                column: "EmergencyPlanTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskTypeMains_EmergencyPlanTypes_EmergencyPlanTypeId",
                table: "RiskTypeMains",
                column: "EmergencyPlanTypeId",
                principalTable: "EmergencyPlanTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskTypeMains_EmergencyPlanTypes_EmergencyPlanTypeId",
                table: "RiskTypeMains");

            migrationBuilder.DropIndex(
                name: "IX_RiskTypeMains_EmergencyPlanTypeId",
                table: "RiskTypeMains");

            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyPlanTypeId1",
                table: "RiskTypeMains",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskTypeMains_EmergencyPlanTypeId1",
                table: "RiskTypeMains",
                column: "EmergencyPlanTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskTypeMains_EmergencyPlanTypes_EmergencyPlanTypeId1",
                table: "RiskTypeMains",
                column: "EmergencyPlanTypeId1",
                principalTable: "EmergencyPlanTypes",
                principalColumn: "Id");
        }
    }
}
