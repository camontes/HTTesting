using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CollaboratorsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PersonalEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Document = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CellphoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AddressStreetAddress = table.Column<string>(name: "Address_StreetAddress", type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressCountryCode = table.Column<int>(name: "Address_CountryCode", type: "integer", nullable: false),
                    AddressCountry = table.Column<string>(name: "Address_Country", type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressStateCode = table.Column<int>(name: "Address_StateCode", type: "integer", nullable: false),
                    AddressState = table.Column<string>(name: "Address_State", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressCityCode = table.Column<int>(name: "Address_CityCode", type: "integer", nullable: false),
                    AddressCity = table.Column<string>(name: "Address_City", type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressZipCode = table.Column<string>(name: "Address_ZipCode", type: "character varying(100)", maxLength: 100, nullable: true),
                    OtherGender = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OtherMaritalStatus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OtherDocumentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SendNotificationsToPersonalEmail = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborators_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborators_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_BusinessEmail",
                table: "Collaborators",
                column: "BusinessEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_CompanyId",
                table: "Collaborators",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_RoleId",
                table: "Collaborators",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");
        }
    }
}
