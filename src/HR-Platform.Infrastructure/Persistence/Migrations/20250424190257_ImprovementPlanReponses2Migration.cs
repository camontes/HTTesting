using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImprovementPlanReponses2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImprovementPlanResponse_ImprovementPlanTasks_ImprovementPla~",
                table: "ImprovementPlanResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_ImprovementPlanResponseFile_ImprovementPlanResponse_Improve~",
                table: "ImprovementPlanResponseFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImprovementPlanResponseFile",
                table: "ImprovementPlanResponseFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImprovementPlanResponse",
                table: "ImprovementPlanResponse");

            migrationBuilder.DropIndex(
                name: "IX_ImprovementPlanResponse_ImprovementPlanTaskId",
                table: "ImprovementPlanResponse");

            migrationBuilder.RenameTable(
                name: "ImprovementPlanResponseFile",
                newName: "ImprovementPlanResponseFiles");

            migrationBuilder.RenameTable(
                name: "ImprovementPlanResponse",
                newName: "ImprovementPlanResponses");

            migrationBuilder.RenameIndex(
                name: "IX_ImprovementPlanResponseFile_ImprovementPlanResponseId",
                table: "ImprovementPlanResponseFiles",
                newName: "IX_ImprovementPlanResponseFiles_ImprovementPlanResponseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImprovementPlanResponseFiles",
                table: "ImprovementPlanResponseFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImprovementPlanResponses",
                table: "ImprovementPlanResponses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanResponses_ImprovementPlanTaskId",
                table: "ImprovementPlanResponses",
                column: "ImprovementPlanTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImprovementPlanResponseFiles_ImprovementPlanResponses_Impro~",
                table: "ImprovementPlanResponseFiles",
                column: "ImprovementPlanResponseId",
                principalTable: "ImprovementPlanResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImprovementPlanResponses_ImprovementPlanTasks_ImprovementPl~",
                table: "ImprovementPlanResponses",
                column: "ImprovementPlanTaskId",
                principalTable: "ImprovementPlanTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImprovementPlanResponseFiles_ImprovementPlanResponses_Impro~",
                table: "ImprovementPlanResponseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ImprovementPlanResponses_ImprovementPlanTasks_ImprovementPl~",
                table: "ImprovementPlanResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImprovementPlanResponses",
                table: "ImprovementPlanResponses");

            migrationBuilder.DropIndex(
                name: "IX_ImprovementPlanResponses_ImprovementPlanTaskId",
                table: "ImprovementPlanResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImprovementPlanResponseFiles",
                table: "ImprovementPlanResponseFiles");

            migrationBuilder.RenameTable(
                name: "ImprovementPlanResponses",
                newName: "ImprovementPlanResponse");

            migrationBuilder.RenameTable(
                name: "ImprovementPlanResponseFiles",
                newName: "ImprovementPlanResponseFile");

            migrationBuilder.RenameIndex(
                name: "IX_ImprovementPlanResponseFiles_ImprovementPlanResponseId",
                table: "ImprovementPlanResponseFile",
                newName: "IX_ImprovementPlanResponseFile_ImprovementPlanResponseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImprovementPlanResponse",
                table: "ImprovementPlanResponse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImprovementPlanResponseFile",
                table: "ImprovementPlanResponseFile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanResponse_ImprovementPlanTaskId",
                table: "ImprovementPlanResponse",
                column: "ImprovementPlanTaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImprovementPlanResponse_ImprovementPlanTasks_ImprovementPla~",
                table: "ImprovementPlanResponse",
                column: "ImprovementPlanTaskId",
                principalTable: "ImprovementPlanTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImprovementPlanResponseFile_ImprovementPlanResponse_Improve~",
                table: "ImprovementPlanResponseFile",
                column: "ImprovementPlanResponseId",
                principalTable: "ImprovementPlanResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
