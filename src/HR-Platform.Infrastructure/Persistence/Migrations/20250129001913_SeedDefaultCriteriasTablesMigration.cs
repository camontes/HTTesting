using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultCriteriasTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultEvaluationCriteriaValue");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EvaluationCriteriaType");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "EvaluationCriteriaType");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropColumn(
                name: "NameEnglish",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "DefaultEvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "DefaultEvaluationCriteria");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "DefaultEvaluationCriteriaScore",
                newName: "UpperScore");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "EvaluationCriteriaType",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultEvaluationCriteriaId",
                table: "DefaultEvaluationCriteriaScore",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LowerScore",
                table: "DefaultEvaluationCriteriaScore",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationCriteriaTypeId",
                table: "DefaultEvaluationCriteria",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "DefaultEvaluationCriteria",
                columns: new[] { "Id", "Description", "DescriptionEnglish", "EvaluationCriteriaTypeId", "IsDeleteable", "IsEditable", "Name", "NameEnglish", "Percentage" },
                values: new object[,]
                {
                    { 1, "Evaluar si el empleado ha cumplido con las metas y objetivos establecidos para su cargo. Esto incluye la cantidad de trabajo realizado, la calidad y la eficiencia.", "Evaluate whether the employee has met the goals and objectives established for his or her position. This includes quantity of work performed, quality and efficiency.", 1, false, false, "Cumplimiento de metas y objetivos", "Achievement of goals and objectives", 25 },
                    { 2, "Evaluar el dominio de las habilidades técnicas requeridas para el cargo, como el manejo de herramientas.", "Evaluate the mastery of the technical skills required for the position, such as tool handling.", 1, false, false, "Competencias técnicas", "Technical competence", 25 },
                    { 3, "Cumplimiento del horario de trabajo, respeto por los tiempos establecidos y asistencia regular a sus compromisos.", "Adherence to the work schedule, respect for established times and regular attendance to commitments.", 1, false, false, "Puntualidad y cumplimiento de horario ", "Punctuality and compliance with schedule", 25 },
                    { 4, "Evaluar si el empleado entrega sus proyectos y tareas dentro de los plazos establecidos y con el nivel de calidad esperado", "Evaluate whether the employee delivers projects and tasks within the established deadlines and with the expected level of quality.", 1, false, false, "Entrega de proyectos y tareas", "Punctuality and compliance with schedule", 25 },
                    { 5, "Evaluar cómo el empleado se integra al equipo, colabora con sus compañeros y contribuye al buen ambiente laboral.", "Evaluate how the employee integrates into the team, collaborates with colleagues and contributes to a good work environment.", 2, false, false, "Trabajo en equipo y colaboración", "Teamwork and collaboration", 40 },
                    { 6, "Capacidad del empleado para adaptarse a cambios, nuevas herramientas o metodologías de trabajo, y responder adecuadamente a situaciones imprevistas.", "Employee's ability to adapt to changes, new tools or work methodologies, and to respond appropriately to unforeseen situations.", 2, false, false, "Adaptabilidad y flexibilidad", "Adaptability and flexibility", 30 },
                    { 7, "Evaluar la actitud general del empleado hacia su trabajo y si toma la iniciativa para resolver problemas o mejorar procesos sin esperar indicaciones.", "Evaluate the employee's general attitude towards his or her work and whether he or she takes the initiative to solve problems or improve processes without waiting for directions.", 2, false, false, "Actitud y proactividad", "Attitude and proactivity", 30 }
                });

            migrationBuilder.InsertData(
                table: "DefaultEvaluationCriteriaScore",
                columns: new[] { "Id", "DefaultEvaluationCriteriaId", "Description", "DescriptionEnglish", "IsDeleteable", "IsEditable", "LowerScore", "UpperScore" },
                values: new object[,]
                {
                    { 1, 1, "No cumple con las metas establecidas.", "Does not meet established goals.", false, false, 0, 0 },
                    { 2, 1, "Cumple algunas metas, pero necesita mejorar.", "Meets some goals, but needs improvement.", false, false, 0, 0 },
                    { 3, 1, "Cumple con la mayoría de las metas.", "Meets some goals, but needs improvement.", false, false, 0, 0 },
                    { 4, 1, "Excede las expectativas, superando las metas.", "Exceeds expectations, exceeding goals.", false, false, 0, 0 },
                    { 5, 2, "Habilidades técnicas insuficientes.", "Insufficient technical skills.", false, false, 0, 0 },
                    { 6, 2, "Buen dominio, pero requiere mejorar.", "Good domain, but requires improvement.", false, false, 0, 0 },
                    { 7, 2, "Buen nivel técnico, adecuado para su posición.", "Good technical level, adequate for position.", false, false, 0, 0 },
                    { 8, 2, "Experto en su área técnica, superando las expectativas.", "Expert in technical area, exceeding expectations.", false, false, 0, 0 },
                    { 9, 3, "Frecuentemente incumple con los plazos.", "Frequently fails to meet deadlines.", false, false, 0, 0 },
                    { 10, 3, "Cumple con los plazos la mayor parte del tiempo.", "Meets deadlines most of the time.", false, false, 0, 0 },
                    { 11, 3, "Cumple siempre con los plazos establecidos.", "Always meets deadlines.", false, false, 0, 0 },
                    { 12, 3, "Entrega antes de los plazos y con alta calidad", "Delivery before deadlines and with high quality", false, false, 0, 0 },
                    { 13, 4, "Alta tasa de ausencias y/o impuntualidad.", "High rate of absenteeism and/or lateness.", false, false, 0, 0 },
                    { 14, 4, "Algunas ausencias y retrasos ocasionales.", "Some occasional absences and delays.", false, false, 0, 0 },
                    { 15, 4, "Cumple regularmente con su horario.", "Regularly meets schedule.", false, false, 0, 0 },
                    { 16, 4, "Excelente asistencia y puntualidad.", "Excellent attendance and punctuality.", false, false, 0, 0 },
                    { 17, 5, "Dificultades frecuentes para trabajar en equipo.", "Difficulties adapting to changes", false, false, 0, 0 },
                    { 18, 5, "Colabora ocasionalmente, pero podría mejorar.", "Collaborates occasionally, but could improve.", false, false, 0, 0 },
                    { 19, 5, "Colabora efectivamente con su equipo.", "Collaborates effectively with his team.", false, false, 0, 0 },
                    { 20, 5, "Excelente trabajo en equipo, fomenta la colaboración.", "Excellent teamwork, encourages collaboration.", false, false, 0, 0 },
                    { 21, 6, "Dificultades para adaptarse a cambios", "Difficulties adapting to changes", false, false, 0, 0 },
                    { 22, 6, "Se adapta, pero requiere orientación constante.", "Adapts, but requires constant guidance.", false, false, 0, 0 },
                    { 23, 6, "Se adapta fácilmente a nuevos cambios.", "Adapts easily to new changes", false, false, 0, 0 },
                    { 24, 6, "Sobresale en situaciones cambiantes, demostrando gran flexibilidad.", "Excels in changing situations, demonstrating great flexibility.", false, false, 0, 0 },
                    { 25, 7, "Actitud negativa o falta de proactividad.", "Negative attitude or lack of proactivity.", false, false, 0, 0 },
                    { 26, 7, "Actitud positiva, pero proactividad limitada.", "Positive attitude, but limited proactivity.", false, false, 0, 0 },
                    { 27, 7, "Buena actitud y proactividad constante.", "Good attitude and constant proactivity.", false, false, 0, 0 },
                    { 28, 7, "Sobresaliente, siempre toma la iniciativa y busca mejoras.", "Outstanding, always takes initiative and seeks improvement.", false, false, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "EvaluationCriteriaType",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish", "Value" },
                values: new object[,]
                {
                    { 1, false, false, "Criterio objetivo", "Objective criteria", 70 },
                    { 2, false, false, "Criterio subjetivo", "Subjective criteria", 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteria",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "EvaluationCriteriaType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EvaluationCriteriaType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Value",
                table: "EvaluationCriteriaType");

            migrationBuilder.DropColumn(
                name: "DefaultEvaluationCriteriaId",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropColumn(
                name: "LowerScore",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropColumn(
                name: "EvaluationCriteriaTypeId",
                table: "DefaultEvaluationCriteria");

            migrationBuilder.RenameColumn(
                name: "UpperScore",
                table: "DefaultEvaluationCriteriaScore",
                newName: "Score");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EvaluationCriteriaType",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "EvaluationCriteriaType",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "DefaultEvaluationCriteriaScore",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "DefaultEvaluationCriteriaScore",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DefaultEvaluationCriteriaScore",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                table: "DefaultEvaluationCriteriaScore",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "DefaultEvaluationCriteria",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "DefaultEvaluationCriteria",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DefaultEvaluationCriteriaValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    ObjectiveCriteriaValue = table.Column<int>(type: "integer", nullable: false),
                    SubjectiveCriteriaValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultEvaluationCriteriaValue", x => x.Id);
                });
        }
    }
}
