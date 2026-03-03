using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftskillTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultSoftSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultSoftSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorSoftSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultSoftSkillId = table.Column<int>(type: "integer", nullable: true),
                    OtherLanguageName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherLanguageNameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorSoftSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorSoftSkills_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorSoftSkills_DefaultSoftSkills_DefaultSoftSkillId",
                        column: x => x.DefaultSoftSkillId,
                        principalTable: "DefaultSoftSkills",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DefaultSoftSkills",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Adaptación al cambio", "Adaptation to change" },
                    { 3, "Análisis numérico", "Numerical analysis" },
                    { 4, "Aprendizaje rápido", "Fast learning" },
                    { 5, "Autogestión", "Self-management" },
                    { 6, "Compromiso", "Commitment" },
                    { 7, "Creatividad", "Creativity" },
                    { 8, "Empatía", "Empathy" },
                    { 9, "Escucha asertiva", "Assertive listening" },
                    { 10, "Gestión de conflictos", "Conflict management" },
                    { 11, "Influencia", "Influence" },
                    { 12, "Iniciativa", "Initiative" },
                    { 13, "Liderazgo", "Leadership" },
                    { 14, "Motivación", "Motivation" },
                    { 15, "Negociación", "Negotiation" },
                    { 16, "Orientación al detalle", "Detail orientation" },
                    { 17, "Orientación al logro", "Achievement orientation" },
                    { 18, "Pensamiento crítico", "Critical thinking" },
                    { 19, "Persistencia", "Persistence" },
                    { 20, "Planeación", "Planning" },
                    { 21, "Proactividad", "Proactivity" },
                    { 22, "Receptividad", "Receptivity" },
                    { 23, "Resiliencia", "Resilience" },
                    { 24, "Resolución de problemas", "Problem resolution" },
                    { 25, "Responsabilidad", "Responsibility" },
                    { 26, "Servicio al cliente", "Customer Service" },
                    { 27, "Sociabilidad", "Sociability" },
                    { 28, "Tolerancia a la frustración", "Tolerance to frustration" },
                    { 29, "Toma de decisiones", "Decision making" },
                    { 30, "Trabajo en equipo", "Teamwork" },
                    { 31, "Otro", "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorSoftSkills_CollaboratorId",
                table: "CollaboratorSoftSkills",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorSoftSkills_DefaultSoftSkillId",
                table: "CollaboratorSoftSkills",
                column: "DefaultSoftSkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorSoftSkills");

            migrationBuilder.DropTable(
                name: "DefaultSoftSkills");
        }
    }
}
