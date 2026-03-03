using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BackToOldNameAreaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Areas_AuxId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Areas_AuxId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "AuxId",
                table: "Roles",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_AuxId",
                table: "Roles",
                newName: "IX_Roles_AreaId");

            migrationBuilder.RenameColumn(
                name: "AuxId",
                table: "Forms",
                newName: "NoveltyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_AuxId",
                table: "Forms",
                newName: "IX_Forms_NoveltyTypeId");

            migrationBuilder.RenameColumn(
                name: "AuxId",
                table: "Areas",
                newName: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Areas_NoveltyTypeId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Areas_AreaId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Roles",
                newName: "AuxId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_AreaId",
                table: "Roles",
                newName: "IX_Roles_AuxId");

            migrationBuilder.RenameColumn(
                name: "NoveltyTypeId",
                table: "Forms",
                newName: "AuxId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_NoveltyTypeId",
                table: "Forms",
                newName: "IX_Forms_AuxId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Areas",
                newName: "AuxId");

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
    }
}
