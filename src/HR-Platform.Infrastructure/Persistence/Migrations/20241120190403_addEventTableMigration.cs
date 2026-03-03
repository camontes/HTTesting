using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEventTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultTimeZones_DefaultTimeZoneId",
                table: "CollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_DefaultTimeZoneId",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "DefaultTimeZoneId",
                table: "CollaboratorContracts");

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TimeZoneId = table.Column<int>(type: "integer", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmailCreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsDeletedEvent = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_DefaultTimeZones_TimeZoneId",
                        column: x => x.TimeZoneId,
                        principalTable: "DefaultTimeZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationSent = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorEvents_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorEvents_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEvents_CollaboratorId",
                table: "CollaboratorEvents",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEvents_EventId",
                table: "CollaboratorEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CompanyId",
                table: "Event",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventTypeId",
                table: "Event",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TimeZoneId",
                table: "Event",
                column: "TimeZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorEvents");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.AddColumn<int>(
                name: "DefaultTimeZoneId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_DefaultTimeZoneId",
                table: "CollaboratorContracts",
                column: "DefaultTimeZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultTimeZones_DefaultTimeZoneId",
                table: "CollaboratorContracts",
                column: "DefaultTimeZoneId",
                principalTable: "DefaultTimeZones",
                principalColumn: "Id");
        }
    }
}
