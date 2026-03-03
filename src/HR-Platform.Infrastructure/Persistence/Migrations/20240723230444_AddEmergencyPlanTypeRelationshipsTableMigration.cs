using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmergencyPlanTypeRelationshipsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyPlanTypeId",
                table: "RiskTypeMains",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyPlanTypeId1",
                table: "RiskTypeMains",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyPlanMainName",
                table: "EmergencyPlanTypes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EmergencyPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmergencyPlanTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    VideoURL = table.Column<string>(type: "text", nullable: false),
                    VideoName = table.Column<string>(type: "text", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyPlans_EmergencyPlanTypes_EmergencyPlanTypeId",
                        column: x => x.EmergencyPlanTypeId,
                        principalTable: "EmergencyPlanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DefaultEmergencyPlanTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 8, "Otras actividades", "Other activities" });

            migrationBuilder.CreateIndex(
                name: "IX_RiskTypeMains_EmergencyPlanTypeId1",
                table: "RiskTypeMains",
                column: "EmergencyPlanTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyPlans_EmergencyPlanTypeId",
                table: "EmergencyPlans",
                column: "EmergencyPlanTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskTypeMains_EmergencyPlanTypes_EmergencyPlanTypeId1",
                table: "RiskTypeMains",
                column: "EmergencyPlanTypeId1",
                principalTable: "EmergencyPlanTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskTypeMains_EmergencyPlanTypes_EmergencyPlanTypeId1",
                table: "RiskTypeMains");

            migrationBuilder.DropTable(
                name: "EmergencyPlans");

            migrationBuilder.DropIndex(
                name: "IX_RiskTypeMains_EmergencyPlanTypeId1",
                table: "RiskTypeMains");

            migrationBuilder.DeleteData(
                table: "DefaultEmergencyPlanTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "EmergencyPlanTypeId",
                table: "RiskTypeMains");

            migrationBuilder.DropColumn(
                name: "EmergencyPlanTypeId1",
                table: "RiskTypeMains");

            migrationBuilder.DropColumn(
                name: "EmergencyPlanMainName",
                table: "EmergencyPlanTypes");
        }
    }
}
