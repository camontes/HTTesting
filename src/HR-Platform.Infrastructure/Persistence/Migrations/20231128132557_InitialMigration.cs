using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressStreetAddress = table.Column<string>(name: "Address_StreetAddress", type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressCountryCode = table.Column<int>(name: "Address_CountryCode", type: "integer", nullable: false),
                    AddressCountry = table.Column<string>(name: "Address_Country", type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressStateCode = table.Column<int>(name: "Address_StateCode", type: "integer", nullable: false),
                    AddressState = table.Column<string>(name: "Address_State", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressCityCode = table.Column<int>(name: "Address_CityCode", type: "integer", nullable: false),
                    AddressCity = table.Column<string>(name: "Address_City", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressZipCode = table.Column<string>(name: "Address_ZipCode", type: "character varying(10)", maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Email",
                table: "Companies",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
