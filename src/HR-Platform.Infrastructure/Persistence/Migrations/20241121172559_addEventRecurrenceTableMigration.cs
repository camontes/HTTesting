using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEventRecurrenceTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultEventReplays_DefaultEventRepla~",
                table: "CollaboratorContracts");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_DefaultEventReplayId",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "DefaultEventReplayId",
                table: "CollaboratorContracts");

            migrationBuilder.CreateTable(
                name: "EventRecurrence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventReplayTypeId = table.Column<int>(type: "integer", nullable: false),
                    Interval = table.Column<int>(type: "integer", nullable: false),
                    RecurrenceEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRecurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRecurrence_DefaultEventReplays_EventReplayTypeId",
                        column: x => x.EventReplayTypeId,
                        principalTable: "DefaultEventReplays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRecurrence_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRecurrence_EventId",
                table: "EventRecurrence",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRecurrence_EventReplayTypeId",
                table: "EventRecurrence",
                column: "EventReplayTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRecurrence");

            migrationBuilder.AddColumn<int>(
                name: "DefaultEventReplayId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_DefaultEventReplayId",
                table: "CollaboratorContracts",
                column: "DefaultEventReplayId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultEventReplays_DefaultEventRepla~",
                table: "CollaboratorContracts",
                column: "DefaultEventReplayId",
                principalTable: "DefaultEventReplays",
                principalColumn: "Id");
        }
    }
}
