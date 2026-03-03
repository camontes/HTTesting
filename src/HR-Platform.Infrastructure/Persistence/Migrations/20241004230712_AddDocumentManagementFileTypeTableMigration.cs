using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentManagementFileTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentManagementFileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentManagementFileTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DocumentManagementFileTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "HV", "CV" },
                    { 3, "Documento de identidad al 150%", "Identity card at 150%" },
                    { 4, "Copia del pasaporte", "Copy of passport" },
                    { 5, "Foto Formal 3*4", "Formal photo 3*4" },
                    { 6, "Certificados de estudio", "Certificates of study" },
                    { 7, "Certificados de educación no formal", "Certificates of non-formal education" },
                    { 8, "Copia de tarjeta profesional", "Copy of professional card" },
                    { 9, "Certificados laborales", "Labor certificates" },
                    { 10, "Referencias personales", "Personal references" },
                    { 11, "Certificado de pensiones", "Pension certificate" },
                    { 12, "Certificado de cesantías", "Severance pay certificate" },
                    { 13, "Certificado de EPS", "EPS Certificate" },
                    { 14, "Certificado de Cuenta Bancaria", "Bank Account Certificate" },
                    { 15, "Informe de selección", "Selection report" },
                    { 16, "Autorización de tratamiento de datos personales", "Authorization to process personal data" },
                    { 17, "Validación de antecedentes", "Background validation" },
                    { 18, "RUT", "RUT" },
                    { 19, "Solicitud de personal", "Personnel application" },
                    { 20, "Propuesta laboral", "Job offer" },
                    { 21, "Aceptación de propuesta laboral", "Acceptance of job offer" },
                    { 22, "Referencias laborales", "Job references" },
                    { 23, "Cartas referencias personales", "Letters personal references" },
                    { 24, "Validación académica", "Academic validation" },
                    { 25, "Novedad de ingreso", "New entry" },
                    { 26, "Otro", "Other" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentManagementFileTypes");
        }
    }
}
