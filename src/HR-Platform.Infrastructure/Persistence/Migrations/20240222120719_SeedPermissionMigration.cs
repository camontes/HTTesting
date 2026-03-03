using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "DescriptionEnglish", "Name", "NameEnglish", "PermissionGroupId", "ValidationString" },
                values: new object[,]
                {
                    { 1, "Permite visualizar la sección de bienestar con las opciones que estén disponibles", "Allows you to view the wellness section with the options that are available", "Visualizar la sección de bienestar", "View the wellness section", 1, "ViewWellness" },
                    { 2, "Permite visualizar sección del perfil sociodemográfico con las opciones que estén disponibles", "Allows you to view a section of the sociodemographic profile with the options that are available", "Visualizar la sección de perfil sociodemográfico", "View the sociodemographic profile section", 2, "ViewSocioDemo" },
                    { 3, "Permite visualizar sección de exámenes ocupacionales con las opciones que estén disponibles", "Allows you to view the occupational exam section with the options that are available", "Visualizar la sección de exámenes ocupacionales", "View the occupational exams section", 3, "ViewOccupational" },
                    { 4, "Permite visualizar sección de COPASST con las opciones que estén disponibles", "Allows you to view JHSC section with the options that are available", "Visualizar la sección de COPASST", "View the Joint Health and Safety Committee (JHSC) section", 3, "ViewJHSC" },
                    { 5, "Permite visualizar sección de configuración asociada con mi cuenta", "Allows to view the setting section associated with my account", "Visualizar sección de configuración", "View setting section", 4, "ViewAccountSettings" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
