using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEducationTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyTypes_DefaultStudyTypeId",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_EducationalLevels_EducationalLevelId",
                table: "CollaboratorEducations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndEducationDate",
                table: "CollaboratorEducations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "EducationalLevelId",
                table: "CollaboratorEducations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "EducationFile",
                table: "CollaboratorEducations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DefaultStudyTypeId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultStudyAreaId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "EducationFileName",
                table: "CollaboratorEducations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationFileURL",
                table: "CollaboratorEducations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DefaultEducationStages",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameEnglish",
                value: "Deferred");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations",
                column: "DefaultStudyAreaId",
                principalTable: "DefaultStudyAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyTypes_DefaultStudyTypeId",
                table: "CollaboratorEducations",
                column: "DefaultStudyTypeId",
                principalTable: "DefaultStudyTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_EducationalLevels_EducationalLevelId",
                table: "CollaboratorEducations",
                column: "EducationalLevelId",
                principalTable: "EducationalLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyTypes_DefaultStudyTypeId",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_EducationalLevels_EducationalLevelId",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "EducationFileName",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "EducationFileURL",
                table: "CollaboratorEducations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndEducationDate",
                table: "CollaboratorEducations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EducationalLevelId",
                table: "CollaboratorEducations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EducationFile",
                table: "CollaboratorEducations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DefaultStudyTypeId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DefaultStudyAreaId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "DefaultEducationStages",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameEnglish",
                value: "deferred");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations",
                column: "DefaultStudyAreaId",
                principalTable: "DefaultStudyAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyTypes_DefaultStudyTypeId",
                table: "CollaboratorEducations",
                column: "DefaultStudyTypeId",
                principalTable: "DefaultStudyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_EducationalLevels_EducationalLevelId",
                table: "CollaboratorEducations",
                column: "EducationalLevelId",
                principalTable: "EducationalLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
