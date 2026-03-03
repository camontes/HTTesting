using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addCollaboratorBrigadesInventoryTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorBrigadeInventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    BrigadeInventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityDelivered = table.Column<int>(type: "integer", nullable: false),
                    UnitMeasureId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Observations = table.Column<string>(type: "text", nullable: false),
                    EmailWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorBrigadeInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorBrigadeInventory_BrigadeInventories_BrigadeInve~",
                        column: x => x.BrigadeInventoryId,
                        principalTable: "BrigadeInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorBrigadeInventory_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorBrigadeInventory_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorBrigade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorBrigadeInventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorBrigade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorBrigade_CollaboratorBrigadeInventory_Collaborat~",
                        column: x => x.CollaboratorBrigadeInventoryId,
                        principalTable: "CollaboratorBrigadeInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorBrigade_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorBrigadeInventoryFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorBrigadeInventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UrlFile = table.Column<string>(type: "text", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorBrigadeInventoryFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorBrigadeInventoryFile_CollaboratorBrigadeInvento~",
                        column: x => x.CollaboratorBrigadeInventoryId,
                        principalTable: "CollaboratorBrigadeInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigade_CollaboratorBrigadeInventoryId",
                table: "CollaboratorBrigade",
                column: "CollaboratorBrigadeInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigade_CollaboratorId",
                table: "CollaboratorBrigade",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigadeInventory_BrigadeInventoryId",
                table: "CollaboratorBrigadeInventory",
                column: "BrigadeInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigadeInventory_CompanyId",
                table: "CollaboratorBrigadeInventory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigadeInventory_UnitMeasureId",
                table: "CollaboratorBrigadeInventory",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigadeInventoryFile_CollaboratorBrigadeInvento~",
                table: "CollaboratorBrigadeInventoryFile",
                column: "CollaboratorBrigadeInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorBrigade");

            migrationBuilder.DropTable(
                name: "CollaboratorBrigadeInventoryFile");

            migrationBuilder.DropTable(
                name: "CollaboratorBrigadeInventory");
        }
    }
}
