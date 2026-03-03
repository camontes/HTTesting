using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultProfessionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultProfessionId",
                table: "CollaboratorLanguages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultProfessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultProfessions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultProfessions",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Administración aeronáutica", "Aeronautical administration" },
                    { 3, "Administración agroindustrial", "Agro-industrial administration" },
                    { 4, "Administración agropecuaria", "Agricultural and livestock administration" },
                    { 5, "Administración comercial y de mercadeo", "Commercial and marketing administration" },
                    { 6, "Administración de aerolíneas", "Airline management" },
                    { 7, "Administración de bienes raíces", "Real estate administration" },
                    { 8, "Administración de empresas", "Business administration" },
                    { 9, "Administración de negocios", "Business management" },
                    { 10, "Administración de obras civiles", "Civil Works Administration" },
                    { 11, "Administración de personal", "Personnel administration" },
                    { 12, "Administración de seguros", "Insurance administration" },
                    { 13, "Administración de servicios", "Service administration" },
                    { 14, "Administración de sistemas informáticos", "IT systems administration" },
                    { 15, "Administración de transporte", "Transportation administration" },
                    { 16, "Administración financiera", "Financial administration" },
                    { 17, "Administración hospitalaria", "Hospital administration" },
                    { 18, "Administración industrial", "Industrial administration" },
                    { 19, "Administración pública", "Public administration" },
                    { 20, "Administración tributaria", "Tax administration" },
                    { 21, "Administración turística hotelera", "Hotel and tourism administration" },
                    { 22, "Bachillerato Académico", "High School" },
                    { 23, "Bachillerato comercial", "Business high school degree" },
                    { 24, "Bachillerato técnico", "Technical high school degree" },
                    { 25, "Ciencias políticas y gobierno", "Political science and government" },
                    { 26, "Comercio internacional", "International commerce" },
                    { 27, "Comunicación publicitaria", "Advertising Communication" },
                    { 28, "Comunicación social y periodismo", "Social communication and journalism" },
                    { 29, "Contaduría", "Accounting" },
                    { 30, "Derecho", "Law" },
                    { 31, "Diseño gráfico", "Graphic design" },
                    { 32, "Diseño industrial", "Industrial design" },
                    { 33, "Docente", "Teacher" },
                    { 34, "Doctorado en administración", "Doctorate in administration" },
                    { 35, "Doctorado en economía", "Doctorate in Economics" },
                    { 36, "Doctorado en humanidades", "Doctorate in Humanities" },
                    { 37, "Economía", "Economics" },
                    { 38, "Especialización en administración de empresas", "Specialization in business administration" },
                    { 39, "Especialización en alta gerencia", "Specialization in senior management" },
                    { 40, "Especialización en educación bilingüe", "Specialization in bilingual education" },
                    { 41, "Especialización en finanzas", "Specialization in finance" },
                    { 42, "Especialización en gerencia de empresas constructoras", "Specialization in construction company management" },
                    { 43, "Especialización en gerencia de la calidad", "Specialization in quality management" },
                    { 44, "Especialización en gerencia de proyectos", "Specialization in project management" },
                    { 45, "Especialización en gerencia del talento humano", "Specialization in human talent management" },
                    { 46, "Especialización en gerencia logística internacional", "Specialization in international logistics management" },
                    { 47, "Especialización en gerencia prospectiva y estrategia", "Specialization in prospective management and strategy" },
                    { 48, "Especialización en gerencia tributaria", "Specialization in tax management" },
                    { 49, "Especialización en gestión de servicios de tecnologías de información", "Specialization in Information Technology Services Management" },
                    { 50, "Especialización en legislación aduanera", "Specialization in customs legislation" },
                    { 51, "Especialización en mercadeo", "Specialization in marketing" },
                    { 52, "Especialización en negocios internacionales e integración económica", "Specialization in international business and economic integration" },
                    { 53, "Especialización en proyectos de desarrollo", "Specialization in development projects" },
                    { 54, "Estadista", "Statistician" },
                    { 55, "Finanzas, relaciones internacionales y gobierno", "Finance, international relations and government" },
                    { 56, "Finanzas y gobierno", "Finance and government" },
                    { 57, "Gobierno y relaciones internacionales", "Government and international relations" },
                    { 58, "Ingeniería administrativa", "Administrative engineering" },
                    { 59, "Ingeniería comercial", "Commercial Engineering" },
                    { 60, "Ingeniería de energías", "Energy Engineering" },
                    { 61, "Ingeniería de mercados", "Market engineering" },
                    { 62, "Ingeniería de procesos", "Process engineering" },
                    { 63, "Ingeniería de producción", "Production engineering" },
                    { 64, "Ingeniería de redes y telecomunicaciones", "Network and telecommunications engineering" },
                    { 65, "Ingeniería de sistemas en computación", "Computer systems engineering" },
                    { 66, "Ingeniería de software", "Software engineering" },
                    { 67, "Ingeniería de telecomunicaciones", "Telecommunications engineering" },
                    { 68, "Ingeniería eléctrica", "Electrical engineering" },
                    { 69, "Ingeniería electromecánica", "Electromechanical engineering" },
                    { 70, "Ingeniería electrónica", "Electronics engineering" },
                    { 71, "Ingeniería industrial", "Industrial engineering" },
                    { 72, "Ingeniería mecánica", "Mechanical engineering" },
                    { 73, "Ingeniería mecatrónica", "Mechatronics Engineering" },
                    { 74, "Ingeniería química", "Chemical engineering" },
                    { 75, "Ingeniería telemática", "Telematics engineering" },
                    { 76, "Licenciatura en inglés", "Bachelor's degree in English" },
                    { 77, "Maestría en administración de empresa", "Master of Business Administration" },
                    { 78, "Maestría en derecho", "Master's Degree in Law" },
                    { 79, "Maestría en desarrollo humano", "Master's degree in human development" },
                    { 80, "Maestría en dirección de marketing", "Master's degree in marketing management" },
                    { 81, "Maestría en economía", "Master's degree in economics" },
                    { 82, "Maestría en emprendimiento e innovación", "Master's degree in entrepreneurship and innovation" },
                    { 83, "Maestría en finanzas corporativas", "Master's degree in corporate finance" },
                    { 84, "Maestría gestión integral de la calidad y la productividad", "Master's degree in integrated quality and productivity management" },
                    { 85, "Maestría en ingeniería industrial", "Master's degree in industrial engineering" },
                    { 86, "Maestría en mercadeo", "Master's degree in marketing" },
                    { 87, "Maestría en innovación", "Master's degree in innovation" },
                    { 88, "Negocios internacionales", "International business" },
                    { 89, "Planeación y desarrollo social", "Planning and social development" },
                    { 90, "Profesional en logística", "Professional in logistics" },
                    { 91, "Profesional en marketing y negocios internacionales", "Marketing and international business professional" },
                    { 92, "Psicología", "Psychology" },
                    { 93, "Publicidad y mercadeo", "Advertising and marketing" },
                    { 94, "Secretariado", "Secretarial work" },
                    { 95, "Sociología", "Sociology" },
                    { 96, "Técnico en administración de personal", "Personnel administration technician" },
                    { 97, "Técnico seguridad industrial", "Industrial safety technician" },
                    { 98, "Técnico sistemas de computación", "Computer systems technician" },
                    { 99, "Técnico en profesional de producción", "Production professional technician" },
                    { 100, "Técnico en desarrollo y mantenimiento de software", "Technician in software development and maintenance" },
                    { 101, "Técnico en gestión contable", "Technician in accounting management" },
                    { 102, "Técnico en gestión empresarial", "Technician in business management" },
                    { 103, "Técnico en logística", "Logistics technician" },
                    { 104, "Técnico en sistemas de computación", "Computer systems technician" },
                    { 105, "Técnico de mantenimiento", "Maintenance technician" },
                    { 106, "Técnico profesional en producción", "Professional production technician" },
                    { 107, "Técnico en mantenimiento", "Maintenance technician" },
                    { 108, "Tecnología en banca y finanzas", "Banking and finance technology" },
                    { 109, "Tecnología en comercio internacional", "International trade technology" },
                    { 110, "Tecnología en desarrollo de sistemas informáticos", "Computer systems development technology" },
                    { 111, "Tecnología en electricidad industrial", "Industrial electricity technology" },
                    { 112, "Tecnología en electrónica", "Electronics Technology" },
                    { 113, "Tecnología en gestión administrativa", "Administrative Management Technology" },
                    { 114, "Tecnología en gestión comercial", "Commercial management technology" },
                    { 115, "Tecnología en gestión de mercadeo", "Marketing Management Technology" },
                    { 116, "Tecnología en gestión de sistemas de telecomunicaciones", "Telecommunication Systems Management Technology" },
                    { 117, "Tecnología en implementación de sistemas electrónicos industriales", "Industrial electronic systems implementation technology" },
                    { 118, "Otra", "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLanguages_DefaultProfessionId",
                table: "CollaboratorLanguages",
                column: "DefaultProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorLanguages_DefaultProfessions_DefaultProfessionId",
                table: "CollaboratorLanguages",
                column: "DefaultProfessionId",
                principalTable: "DefaultProfessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorLanguages_DefaultProfessions_DefaultProfessionId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropTable(
                name: "DefaultProfessions");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorLanguages_DefaultProfessionId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropColumn(
                name: "DefaultProfessionId",
                table: "CollaboratorLanguages");
        }
    }
}
