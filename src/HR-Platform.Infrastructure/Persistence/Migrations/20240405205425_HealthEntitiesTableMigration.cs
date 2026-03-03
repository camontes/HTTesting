using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class HealthEntitiesTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                table: "Positions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionEnglish",
                table: "Positions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Positions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "HealthEntityId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AddressStreetAddress = table.Column<string>(name: "Address_StreetAddress", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressCountryCode = table.Column<int>(name: "Address_CountryCode", type: "integer", nullable: false),
                    AddressCountry = table.Column<string>(name: "Address_Country", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressStateCode = table.Column<int>(name: "Address_StateCode", type: "integer", nullable: false),
                    AddressState = table.Column<string>(name: "Address_State", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressCityCode = table.Column<int>(name: "Address_CityCode", type: "integer", nullable: false),
                    AddressCity = table.Column<string>(name: "Address_City", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressZipCode = table.Column<string>(name: "Address_ZipCode", type: "character varying(100)", maxLength: 100, nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthEntity_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthEntity_CompanyId",
                table: "HealthEntity",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "HealthEntity");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "HealthEntityId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                table: "Positions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionEnglish",
                table: "Positions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Positions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);
        }
    }
}
