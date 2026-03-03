using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReduceCollaboratorsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "CellphoneNumber",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Collaborators");

            migrationBuilder.AlterColumn<string>(
                name: "Address_StreetAddress",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Superadministrador TH", "Superadministrator RH" });

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Administrador TH", "RH Administrator" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address_StreetAddress",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "Collaborators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Collaborators",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CellphoneNumber",
                table: "Collaborators",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Collaborators",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "SuperAdministrador", "SuperAdministrator" });

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Administrador", "Administrator" });
        }
    }
}
