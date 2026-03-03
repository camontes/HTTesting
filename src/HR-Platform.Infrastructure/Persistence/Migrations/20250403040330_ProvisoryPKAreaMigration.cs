using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProvisoryPKAreaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Areas_NoveltyTypeId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Areas_AreaId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AreaId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Forms_NoveltyTypeId",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "AuxId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AuxId",
                table: "Roles",
                column: "AuxId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_AuxId",
                table: "Forms",
                column: "AuxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Areas_AuxId",
                table: "Forms",
                column: "AuxId",
                principalTable: "Areas",
                principalColumn: "AuxId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Areas_AuxId",
                table: "Roles",
                column: "AuxId",
                principalTable: "Areas",
                principalColumn: "AuxId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Areas_AuxId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Areas_AuxId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AuxId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Forms_AuxId",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AreaId",
                table: "Roles",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_NoveltyTypeId",
                table: "Forms",
                column: "NoveltyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Areas_NoveltyTypeId",
                table: "Forms",
                column: "NoveltyTypeId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Areas_AreaId",
                table: "Roles",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
