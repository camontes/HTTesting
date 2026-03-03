using HR_Platform.Domain.Areas;
using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.CollaboratorStates;
using HR_Platform.Domain.DefaultAreas;
using HR_Platform.Domain.DefaultAssignations;
using HR_Platform.Domain.DefaultBanks;
using HR_Platform.Domain.DefaultBrigadeAdjustments;
using HR_Platform.Domain.DefaultCollaboratorContracts;
using HR_Platform.Domain.DefaultContractTypes;
using HR_Platform.Domain.DefaultCurrencyTypes;
using HR_Platform.Domain.DefaultDaysOfWeeks;
using HR_Platform.Domain.DefaultEducationalLevels;
using HR_Platform.Domain.DefaultEducationStages;
using HR_Platform.Domain.DefaultEmergencyPlanTypes;
using HR_Platform.Domain.DefaultEvaluationCriterias;
using HR_Platform.Domain.DefaultEvaluationCriteriaScores;
using HR_Platform.Domain.DefaultEventReplays;
using HR_Platform.Domain.DefaultEventTypes;
using HR_Platform.Domain.DefaultFamilyCompositions;
using HR_Platform.Domain.DefaultFileTypes;
using HR_Platform.Domain.DefaultKnowledgeLevels;
using HR_Platform.Domain.DefaultLanguageLevels;
using HR_Platform.Domain.DefaultLanguageTypes;
using HR_Platform.Domain.DefaultLifePreferences;
using HR_Platform.Domain.DefaultMonths;
using HR_Platform.Domain.DefaultPensions;
using HR_Platform.Domain.DefaultPositions;
using HR_Platform.Domain.DefaultProfessionalAdvices;
using HR_Platform.Domain.DefaultProfessions;
using HR_Platform.Domain.DefaultQuestionTypes;
using HR_Platform.Domain.DefaultRepeatEveryEvents;
using HR_Platform.Domain.DefaultRiskTypes;
using HR_Platform.Domain.DefaultRoles;
using HR_Platform.Domain.DefaultSeveranceBenefits;
using HR_Platform.Domain.DefaultSoftSkills;
using HR_Platform.Domain.DefaultStudyAreas;
using HR_Platform.Domain.DefaultStudyTypes;
using HR_Platform.Domain.DefaultTags;
using HR_Platform.Domain.DefaultTechnologyNames;
using HR_Platform.Domain.DefaultTimeZones;
using HR_Platform.Domain.DefaultTypeAccounts;
using HR_Platform.Domain.DocumentManagementFileTypes;
using HR_Platform.Domain.DocumentTypes;
using HR_Platform.Domain.EconomicLevels;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.FormAnswerGroupStates;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.MaritalStatuses;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.PermissionGroups;
using HR_Platform.Domain.Permissions;
using HR_Platform.Domain.PriorityNovelties;
using HR_Platform.Domain.SurveyQuestionMandatoryTypes;
using HR_Platform.Domain.SurveyQuestionTypes;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HR_Platform.Infrastructure.Persistence.Seed;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        /* Seed AssignationTypes */
        SeedAssignationType(1, "Personal interno", "Internal staff", modelBuilder);
        SeedAssignationType(2, "Personal externo", "External staff", modelBuilder);

        /* Seed Collaborator States */
        SeedCollaboratorStates(1, "Activo", "Active", modelBuilder);
        SeedCollaboratorStates(2, "Suspendido", "Suspended", modelBuilder);

        /* Seed Default Question Type */
        SeedDefaultQuestionType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultQuestionType(2, "Opción múltiple", "Multiple choice", modelBuilder);
        SeedDefaultQuestionType(3, "Única opción", "Only option", modelBuilder);
        SeedDefaultQuestionType(4, "Texto largo", "Long text", modelBuilder);
        SeedDefaultQuestionType(5, "Texto corto", "Short text", modelBuilder);
        SeedDefaultQuestionType(6, "Calificación", "Qualification", modelBuilder);
        SeedDefaultQuestionType(7, "Listado", "Listing", modelBuilder);


        /* Seed Default Collaborator Contract */
        SeedDefaultCollaboratorContract(1, "Ninguno", "Ninguno", "Ninguno", 0, modelBuilder);

        /* Seed Default Assignations */
        SeedDefaultAssignation(1, "Personal interno", "Internal staff", modelBuilder);
        SeedDefaultAssignation(2, "Personal externo", "External staff", modelBuilder);

        /* Seed Defaul File Type */
        SeedDefaultFileType(1, "Certificado ", "Certificate", modelBuilder);
        SeedDefaultFileType(2, "Informe de resultados", "Report of results", modelBuilder);
        SeedDefaultFileType(3, "Otro ", "Other", modelBuilder);

        /* Seed Default Areas */
        SeedDefaultArea(1, "Ninguno", "None", modelBuilder);
        SeedDefaultArea(2, "Operaciones", "Operations", modelBuilder);
        SeedDefaultArea(3, "Talento Humano", "Human Talent", modelBuilder);
        SeedDefaultArea(4, "Infraestructura", "Infrastructure", modelBuilder);

        /* Seed Default Month */
        SeedDefaultMonth(1, "Ninguno", "None", modelBuilder);
        SeedDefaultMonth(2, "Enero", "January", modelBuilder);
        SeedDefaultMonth(3, "Febrero", "February", modelBuilder);
        SeedDefaultMonth(4, "Marzo", "March", modelBuilder);
        SeedDefaultMonth(5, "Abril", "April", modelBuilder);
        SeedDefaultMonth(6, "Mayo", "May", modelBuilder);
        SeedDefaultMonth(7, "Junio", "June", modelBuilder);
        SeedDefaultMonth(8, "Julio", "July", modelBuilder);
        SeedDefaultMonth(9, "Agosto", "August", modelBuilder);
        SeedDefaultMonth(10, "Septiembre", "September", modelBuilder);
        SeedDefaultMonth(11, "Octubre", "October", modelBuilder);
        SeedDefaultMonth(12, "Noviembre", "November", modelBuilder);
        SeedDefaultMonth(13, "Diciembre", "December", modelBuilder);

        /* Seed Default Days of Week*/
        SeedDefaultDaysOfWeek(1, "Ninguno", "None", modelBuilder);
        SeedDefaultDaysOfWeek(2, "Lunes", "Monday", modelBuilder);
        SeedDefaultDaysOfWeek(3, "Martes", "Tuesday", modelBuilder);
        SeedDefaultDaysOfWeek(4, "Miércoles", "Wednesday", modelBuilder);
        SeedDefaultDaysOfWeek(5, "Jueves", "Thursday", modelBuilder);
        SeedDefaultDaysOfWeek(6, "Viernes", "Friday", modelBuilder);
        SeedDefaultDaysOfWeek(7, "Sábado", "Saturday", modelBuilder);
        SeedDefaultDaysOfWeek(8, "Domingo", "Sunday", modelBuilder);

        /* Seed Default Repeat Every Event*/
        SeedDefaultRepeatEveryEvent(1, "Ninguno", "None", modelBuilder);
        SeedDefaultRepeatEveryEvent(2, "Día", "Day", modelBuilder);
        SeedDefaultRepeatEveryEvent(3, "Semana", "Week", modelBuilder);
        SeedDefaultRepeatEveryEvent(4, "Mes", "Month", modelBuilder);
        SeedDefaultRepeatEveryEvent(5, "Año", "Year", modelBuilder);

        /* Seed Default Event Type*/
        SeedDefaultEventType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultEventType(2, "Cumpleaños", "Birthday", modelBuilder);
        SeedDefaultEventType(3, "Feedback", "Feedback", modelBuilder);
        SeedDefaultEventType(4, "Pausas activas", "Active breaks", modelBuilder);
        SeedDefaultEventType(5, "Reunión", "Meeting", modelBuilder);
        SeedDefaultEventType(6, "Otro", "Other", modelBuilder);

        /* Seed Default Brigade Adjustment */
        SeedDefaultBrigadeAdjustment(1, "Ninguno", "None", modelBuilder);
        SeedDefaultBrigadeAdjustment(2, "Brigada de primeros auxilios", "First aid brigade", modelBuilder);
        SeedDefaultBrigadeAdjustment(3, "Brigada contraincendios", "Fire Brigade", modelBuilder);
        SeedDefaultBrigadeAdjustment(4, "Brigada de evacuación", "Evacuation brigade", modelBuilder);

        /* Seed Default Tag */
        SeedDefaultTag(1, "Colaborador Interno", "Internal Collaborator", modelBuilder);
        SeedDefaultTag(2, "Colaborador Externo", "External Collaborator", modelBuilder);

        /* Seed Default Banks */
        SeedDefaultBank(1, "Bancolombia", "Bancolombia", modelBuilder);
        SeedDefaultBank(2, "BBVA", "BBVA", modelBuilder);
        SeedDefaultBank(3, "Scotia Bank-Colpatria", "Scotia Bank-Colpatria", modelBuilder);
        SeedDefaultBank(4, "Popular", "Popular", modelBuilder);
        SeedDefaultBank(5, "Banco de Bogotá", "Banco de Bogotá", modelBuilder);
        SeedDefaultBank(6, "Ninguno", "None", modelBuilder);

        /* Seed Default EducationalLevel */
        SeedDefaultEducationalLevel(1, "Ninguno", "None", modelBuilder);
        SeedDefaultEducationalLevel(2, "Técnico", "Technical", modelBuilder);
        SeedDefaultEducationalLevel(3, "Tecnólogo", "Technologist", modelBuilder);
        SeedDefaultEducationalLevel(4, "Pregrado", "College degree", modelBuilder);
        SeedDefaultEducationalLevel(5, "Posgrado", "Postgraduate degree", modelBuilder);


        /* Seed Default Event Replay */
        SeedDefaultEventReplay(1, "Ninguno", "None", modelBuilder);
        SeedDefaultEventReplay(2, "No se repite", "Not repeated", modelBuilder);
        SeedDefaultEventReplay(3, "Cada día laborable (lunes - viernes)", "Every working day (Monday - Friday)", modelBuilder);
        SeedDefaultEventReplay(4, "Diariamente", "Daily", modelBuilder);
        SeedDefaultEventReplay(5, "Semanalmente", "Weekly", modelBuilder);
        SeedDefaultEventReplay(6, "Mensualmente", "Monthly", modelBuilder);
        SeedDefaultEventReplay(7, "Anualmente", "Annually", modelBuilder);
        SeedDefaultEventReplay(8, "Personalizado", "Customized", modelBuilder);


        /* Seed Risk Type*/
        SeedDefaultRiskType(1, "Riesgos naturales", "Natural risks", modelBuilder);
        SeedDefaultRiskType(2, "Riesgos biológicos", "Biological risks", modelBuilder);
        SeedDefaultRiskType(3, "Riesgo físico", "Physical risks", modelBuilder);
        SeedDefaultRiskType(4, "Riesgo químico", "Chemical risks", modelBuilder);
        SeedDefaultRiskType(5, "Riesgo psicosocial", "Psychosocial risks", modelBuilder);
        SeedDefaultRiskType(6, "Riesgo biomecánico", "Biomechanical risks", modelBuilder);
        SeedDefaultRiskType(7, "Ninguno", "None", modelBuilder);
        SeedDefaultRiskType(8, "Riesgo de condiciones de seguridad", "Risk of safety conditions", modelBuilder);

        /* Seed Default Emergency Plan Type*/
        SeedDefaultEmergencyPlanType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultEmergencyPlanType(2, "Niveles de riesgo", "Risk levels", modelBuilder);
        SeedDefaultEmergencyPlanType(3, "Punto de encuentro ", "Meeting point", modelBuilder);
        SeedDefaultEmergencyPlanType(4, "Rutas de evacuación", "Evacuation routes", modelBuilder);
        SeedDefaultEmergencyPlanType(5, "En caso de emergencia", "In case of emergency", modelBuilder);
        SeedDefaultEmergencyPlanType(6, "Kit de emergencia", "Emergency kit", modelBuilder);
        SeedDefaultEmergencyPlanType(7, "Simulacros", "Drills", modelBuilder);
        SeedDefaultEmergencyPlanType(8, "Otras actividades", "Other activities", modelBuilder);

    /* Seed Default Family Compositions */
        SeedDefaultFamilyComposition(1, "Ninguno", "None", modelBuilder);
        SeedDefaultFamilyComposition(2, "Esposo/a", "Husband/Wife", modelBuilder);
        SeedDefaultFamilyComposition(3, "Esposo/a e hijos", "Husband/Wife and children", modelBuilder);
        SeedDefaultFamilyComposition(4, "Hijos", "Children", modelBuilder);
        SeedDefaultFamilyComposition(5, "Madre/Padre y hermanos", "Mother/Father and siblings", modelBuilder);
        SeedDefaultFamilyComposition(6, "Padres", "Parents", modelBuilder);
        SeedDefaultFamilyComposition(7, "Hermanos", "Siblings", modelBuilder);
        SeedDefaultFamilyComposition(8, "Solo", "Alone", modelBuilder);
        SeedDefaultFamilyComposition(9, "Otro", "Other", modelBuilder);

        /* SeedDefault Study Type */
        SeedDefaultStudyType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultStudyType(2, "Educación formal", "Formal education", modelBuilder);
        SeedDefaultStudyType(3, "Educación complementaria", "Complementary education", modelBuilder);
        SeedDefaultStudyType(4, "Certificaciones", "Certifications", modelBuilder);


        /* SeedDefault Study AssignationType */
        SeedDefaultStudyArea(1, "Ninguno", "None", modelBuilder);
        SeedDefaultStudyArea(2, "Ciencias administrativas, económicas y financieras", "Management, economic and financial sciences", modelBuilder);
        SeedDefaultStudyArea(3, "Ciencias de la salud", "Health sciences", modelBuilder);
        SeedDefaultStudyArea(4, "Ciencias sociales y humanas", "Social and human sciences", modelBuilder);
        SeedDefaultStudyArea(5, "Diseño", "Design", modelBuilder);
        SeedDefaultStudyArea(6, "Comunicación", "Communication", modelBuilder);
        SeedDefaultStudyArea(7, "Ingeniería y tecnología", "Engineering and technology", modelBuilder);
        SeedDefaultStudyArea(8, "Educación", "Education", modelBuilder);
        SeedDefaultStudyArea(9, "Derecho", "Law", modelBuilder);
        SeedDefaultStudyArea(10, "Empresarial", "Business", modelBuilder);
        SeedDefaultStudyArea(11, "Otra", "Other", modelBuilder);

        /* Seed Default Roles */
        SeedDefaultRole(1, "Superadministrador TH", "Superadministrator HR", modelBuilder);
        SeedDefaultRole(2, "Administrador TH", "HR Administrator", modelBuilder);
        SeedDefaultRole(3, "Colaborador", "Employee", modelBuilder);

        /* Seed Default Education Stage */
        SeedDefaultEducationStage(1, "Actualmente Estudiando", "Currently Studying", modelBuilder);
        SeedDefaultEducationStage(2, "Aplazado", "Deferred", modelBuilder);
        SeedDefaultEducationStage(3, "Sin Finalizar", "Unfinished", modelBuilder);

        /* Seed Default Type Account */
        SeedDefaultTypeAccount(1, "Ninguno", "None", modelBuilder);
        SeedDefaultTypeAccount(2, "Ahorros", "Savings", modelBuilder);
        SeedDefaultTypeAccount(3, "Corriente", "Current", modelBuilder);
        SeedDefaultTypeAccount(4, "Deel", "Deel", modelBuilder);

        /* Seed Default Contract Type */
        SeedDefaultContractType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultContractType(2, "Indefinido", "Indefinite", modelBuilder);
        SeedDefaultContractType(3, "Aprendizaje", "Apprenticeship ", modelBuilder);

        #region SeedDocumentManagementFileType
        /* Seed Document Management File Type */
        SeedDocumentManagementFileType(1, "Ninguno", "None", modelBuilder);
        SeedDocumentManagementFileType(2, "HV", "CV", modelBuilder);
        SeedDocumentManagementFileType(3, "Documento de identidad al 150%", "Identity card at 150%", modelBuilder);
        SeedDocumentManagementFileType(4, "Copia del pasaporte", "Copy of passport", modelBuilder);
        SeedDocumentManagementFileType(5, "Foto Formal 3*4", "Formal photo 3*4", modelBuilder);
        SeedDocumentManagementFileType(6, "Certificados de estudio", "Certificates of study", modelBuilder);
        SeedDocumentManagementFileType(7, "Certificados de educación no formal", "Certificates of non-formal education", modelBuilder);
        SeedDocumentManagementFileType(8, "Copia de tarjeta profesional", "Copy of professional card", modelBuilder);
        SeedDocumentManagementFileType(9, "Certificados laborales", "Labor certificates", modelBuilder);
        SeedDocumentManagementFileType(10, "Referencias personales", "Personal references", modelBuilder);
        SeedDocumentManagementFileType(11, "Certificado de pensiones", "Pension certificate", modelBuilder);
        SeedDocumentManagementFileType(12, "Certificado de cesantías", "Severance pay certificate", modelBuilder);
        SeedDocumentManagementFileType(13, "Certificado de EPS", "EPS Certificate", modelBuilder);
        SeedDocumentManagementFileType(14, "Certificado de Cuenta Bancaria", "Bank Account Certificate", modelBuilder);
        SeedDocumentManagementFileType(15, "Informe de selección", "Selection report", modelBuilder);
        SeedDocumentManagementFileType(16, "Autorización de tratamiento de datos personales", "Authorization to process personal data", modelBuilder);
        SeedDocumentManagementFileType(17, "Validación de antecedentes", "Background validation", modelBuilder);
        SeedDocumentManagementFileType(18, "RUT", "RUT", modelBuilder);
        SeedDocumentManagementFileType(19, "Solicitud de personal", "Personnel application", modelBuilder);
        SeedDocumentManagementFileType(20, "Propuesta laboral", "Job offer", modelBuilder);
        SeedDocumentManagementFileType(21, "Aceptación de propuesta laboral", "Acceptance of job offer", modelBuilder);
        SeedDocumentManagementFileType(22, "Referencias laborales", "Job references", modelBuilder);
        SeedDocumentManagementFileType(23, "Cartas referencias personales", "Letters personal references", modelBuilder);
        SeedDocumentManagementFileType(24, "Validación académica", "Academic validation", modelBuilder);
        SeedDocumentManagementFileType(25, "Novedad de ingreso", "New entry", modelBuilder);
        SeedDocumentManagementFileType(26, "Otro", "Other", modelBuilder);
        #endregion

        #region SeedDefaultCurrencyType
        /* Seed Default Currency Type */
        SeedDefaultCurrencyType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultCurrencyType(2, "AED (Emiratos Árabes Unidos Dirham)", "AED (United Arab Emirates Dirham)", modelBuilder);
        SeedDefaultCurrencyType(3, "AFN (Afganistán Afgani)", "AFN (Afghanistan Afghani)", modelBuilder);
        SeedDefaultCurrencyType(4, "ALL (Albania Lek)", "ALL (Albania Lek)", modelBuilder);
        SeedDefaultCurrencyType(5, "AMD (Armenia Dram)", "AMD (Armenia Dram)", modelBuilder);
        SeedDefaultCurrencyType(6, "ANG (Antillas Holandesas Florín)", "ANG (Netherlands Antilles Florin)", modelBuilder);
        SeedDefaultCurrencyType(7, "AOA (Angola Kwanza)", "AOA (Angola Kwanza)", modelBuilder);
        SeedDefaultCurrencyType(8, "ARS (Peso argentino)", "ARS (Argentine Peso)", modelBuilder);
        SeedDefaultCurrencyType(9, "ATS (EURO) (Austria Chelín)", "ATS (EURO) (Austria Shilling)", modelBuilder);
        SeedDefaultCurrencyType(10, "AUD (Dólar australiano)", "AUD (Australian Dollar)", modelBuilder);
        SeedDefaultCurrencyType(11, "AWG (Aruba Florín)", "AWG (Aruban Florin)", modelBuilder);
        SeedDefaultCurrencyType(12, "AZN (Azerbaiyán Nuevo Manat)", "AZN (Azerbaijan New Manat)", modelBuilder);
        SeedDefaultCurrencyType(13, "BAM (Bosnia Marco)", "BAM (Bosnia Marka)", modelBuilder);
        SeedDefaultCurrencyType(14, "BBD (Barbados Dólar)", "BBD (Barbados Dollar)", modelBuilder);
        SeedDefaultCurrencyType(15, "BDT (Bangladesh Taka)", "BDT (Bangladesh Taka)", modelBuilder);
        SeedDefaultCurrencyType(16, "BEF (EURO) (Bélgica Franco)", "BEF (EURO) (Belgium Franc)", modelBuilder);
        SeedDefaultCurrencyType(17, "BGN (Bulgaria Lev)", "BGN (Bulgaria Lev)", modelBuilder);
        SeedDefaultCurrencyType(18, "BHD (Bahréin Dinar)", "BHD (Bahrain Dinar)", modelBuilder);
        SeedDefaultCurrencyType(19, "BIF (Burundi Franco)", "BIF (Burundi Franc)", modelBuilder);
        SeedDefaultCurrencyType(20, "BMD (Bermuda Dólar)", "BMD (Bermuda Dollar)", modelBuilder);
        SeedDefaultCurrencyType(21, "BND (Brunéi Dólar)", "BND (Brunei Dollar)", modelBuilder);
        SeedDefaultCurrencyType(22, "BOB (Bolivia Boliviano)", "BOB (Bolivian Boliviano)", modelBuilder);
        SeedDefaultCurrencyType(23, "BRL (Real brasileño)", "BRL (Brazilian Real)", modelBuilder);
        SeedDefaultCurrencyType(24, "BSD (Bahamas Dólar)", "BSD (Bahamas Dollar)", modelBuilder);
        SeedDefaultCurrencyType(25, "BTN (Bután Ngultrum)", "BTN (Bhutanese Ngultrum)", modelBuilder);
        SeedDefaultCurrencyType(26, "BWP (Botsuana Pula)", "BWP (Botswana Pula)", modelBuilder);
        SeedDefaultCurrencyType(27, "BYR (Bielorrusia Rublo)", "BYR (Belarusian Rouble)", modelBuilder);
        SeedDefaultCurrencyType(28, "BZD (Belice Dólar)", "BZD (Belize Dollar)", modelBuilder);
        SeedDefaultCurrencyType(29, "CAD (Canadá Dólar)", "CAD (Canada Dollar)", modelBuilder);
        SeedDefaultCurrencyType(30, "CDF (Congo Franco)", "CDF (Congo Franc)", modelBuilder);
        SeedDefaultCurrencyType(31, "CHF (Franco suizo)", "CHF (Swiss Franc)", modelBuilder);
        SeedDefaultCurrencyType(32, "CLP (Peso chileno)", "CLP (Chilean Peso)", modelBuilder);
        SeedDefaultCurrencyType(33, "CNY (China Yuan/Renminbi)", "CNY (China Yuan/Renminbi)", modelBuilder);
        SeedDefaultCurrencyType(34, "COP (Peso colombiano)", "COP (Colombian Peso)", modelBuilder);
        SeedDefaultCurrencyType(35, "CRC (Costa Rica Colón)", "CRC (Costa Rican Colon)", modelBuilder);
        SeedDefaultCurrencyType(36, "CUC (Cuba Peso convertible)", "CUC (Cuban Convertible Peso)", modelBuilder);
        SeedDefaultCurrencyType(37, "CUP (Peso cubano)", "CUP (Cuban Peso)", modelBuilder);
        SeedDefaultCurrencyType(38, "CVE (Cabo Verde Escudo)", "CVE (Cape Verde Escudo)", modelBuilder);
        SeedDefaultCurrencyType(39, "CYP (EURO) (Chipre Libra)", "CYP (EURO) (Cyprus Pound)", modelBuilder);
        SeedDefaultCurrencyType(40, "CZK (República Checa Corona)", "CZK (Czech Republic Koruna)", modelBuilder);
        SeedDefaultCurrencyType(41, "DJF (Yibuti Franco)", "DJF (Djibouti Franc)", modelBuilder);
        SeedDefaultCurrencyType(42, "DKK (Dinamarca Corona)", "DKK (Denmark Krone)", modelBuilder);
        SeedDefaultCurrencyType(43, "DMK (EURO) (Alemania Marco)", "DMK (EURO) (Germany Mark)", modelBuilder);
        SeedDefaultCurrencyType(44, "DOP (República Dominicana Peso)", "DOP (Dominican Republic Peso)", modelBuilder);
        SeedDefaultCurrencyType(45, "DZD (Argelia Dinar)", "DZD (Algerian Dinar)", modelBuilder);
        SeedDefaultCurrencyType(46, "EEK (EURO) (Estonia Corona)", "EEK (EURO) (Estonia Kroon)", modelBuilder);
        SeedDefaultCurrencyType(47, "EGP (Egipto Libra)", "EGP (Egypt Pound)", modelBuilder);
        SeedDefaultCurrencyType(48, "ESP (EURO) (España Peseta)", "ESP (EURO) (Spain Peseta)", modelBuilder);
        SeedDefaultCurrencyType(49, "ETB (Etiopía Birr)", "ETB (Ethiopia Birr)", modelBuilder);
        SeedDefaultCurrencyType(50, "EUR (Euro)", "EUR (Euro)", modelBuilder);
        SeedDefaultCurrencyType(51, "FIM (EURO) (Finlandia Marco)", "FIM (EURO) (Finland Mark)", modelBuilder);
        SeedDefaultCurrencyType(52, "FJD (Fiji Dólar)", "FJD (Fiji Dollar)", modelBuilder);
        SeedDefaultCurrencyType(53, "FKP (Islas Falkland Libra)", "FKP (Falkland Islands Pound)", modelBuilder);
        SeedDefaultCurrencyType(54, "GBP (Gran Bretaña Libra esterlina)", "GBP (Great Britain Pound Sterling)", modelBuilder);
        SeedDefaultCurrencyType(55, "GEL (Georgia Lari)", "GEL (Georgia Lari)", modelBuilder);
        SeedDefaultCurrencyType(56, "GHS (Ghana Nuevo Cedi)", "GHS (Ghana New Cedi)", modelBuilder);
        SeedDefaultCurrencyType(57, "GIP (Gibraltar Libra)", "GIP (Gibraltar Pound)", modelBuilder);
        SeedDefaultCurrencyType(58, "GMD (Gambia Dalasi)", "GMD (Gambia Dalasi)", modelBuilder);
        SeedDefaultCurrencyType(59, "GNF (Guinea Franco)", "GNF (Guinea Franc)", modelBuilder);
        SeedDefaultCurrencyType(60, "GRD (EURO) (Grecia Dracma)", "GRD (EURO) (Greece Drachma)", modelBuilder);
        SeedDefaultCurrencyType(61, "GTQ (Guatemala Quetzal)", "GTQ (Guatemala Quetzal)", modelBuilder);
        SeedDefaultCurrencyType(62, "GYD (Guyana Dólar)", "GYD (Guyana Dollar)", modelBuilder);
        SeedDefaultCurrencyType(63, "HKD (Hong Kong Dólar)", "HKD (Hong Kong Dollar)", modelBuilder);
        SeedDefaultCurrencyType(64, "HNL (Honduras Lempira)", "HNL (Honduras Lempira)", modelBuilder);
        SeedDefaultCurrencyType(65, "HRK (Croacia Kuna)", "HRK (Croatia Kuna)", modelBuilder);
        SeedDefaultCurrencyType(66, "HTG (Haití Gourde)", "HTG (Haiti Gourde)", modelBuilder);
        SeedDefaultCurrencyType(67, "HUF (Hungría Forinto)", "HUF (Hungary Forint)", modelBuilder);
        SeedDefaultCurrencyType(68, "IDR (Indonesia Rupia)", "IDR (Indonesia Rupiah)", modelBuilder);
        SeedDefaultCurrencyType(69, "IED (EURO) (Irlanda Libra)", "IED (EURO) (Ireland Pound)", modelBuilder);
        SeedDefaultCurrencyType(70, "ILS (Israel Nuevo Séquel)", "ILS (Israel New Baku)", modelBuilder);
        SeedDefaultCurrencyType(71, "INR (India Rupia)", "INR (India Rupee)", modelBuilder);
        SeedDefaultCurrencyType(72, "IQD (Irak Dinar)", "IQD (Iraqi Dinar)", modelBuilder);
        SeedDefaultCurrencyType(73, "IRR (Irán Rial)", "IRR (Iranian Rial)", modelBuilder);
        SeedDefaultCurrencyType(74, "ISK (Islandia Corona)", "ISK (Iceland Krona)", modelBuilder);
        SeedDefaultCurrencyType(75, "ITL (EURO) (Italia Lira)", "ITL (EURO) (Italy Lira)", modelBuilder);
        SeedDefaultCurrencyType(76, "JMD (Jamaica Dólar)", "JMD (Jamaica Dollar)", modelBuilder);
        SeedDefaultCurrencyType(77, "JOD (Jordania Dinar)", "JOD (Jordan Dinar)", modelBuilder);
        SeedDefaultCurrencyType(78, "JPY (Japón Yen)", "JPY (Japan Yen)", modelBuilder);
        SeedDefaultCurrencyType(79, "KES (Kenia Chelín)", "KES (Kenya Shilling)", modelBuilder);
        SeedDefaultCurrencyType(80, "KGS (Kirguistán Som)", "KGS (Kyrgyzstan Som)", modelBuilder);
        SeedDefaultCurrencyType(81, "KHR (Camboya Riel)", "KHR (Cambodian Riel)", modelBuilder);
        SeedDefaultCurrencyType(82, "KMF (Comoras Franco)", "KMF (Comoros Franc)", modelBuilder);
        SeedDefaultCurrencyType(83, "KPW (Corea del Norte Won)", "KPW (North Korea Won)", modelBuilder);
        SeedDefaultCurrencyType(84, "KRW (Corea del Sur Won)", "KRW (South Korea Won)", modelBuilder);
        SeedDefaultCurrencyType(85, "KWD (Kuwait Dinar)", "KWD (Kuwait Dinar)", modelBuilder);
        SeedDefaultCurrencyType(86, "KYD (Islas Caimán Dólar)", "KYD (Cayman Islands Dollar)", modelBuilder);
        SeedDefaultCurrencyType(87, "KZT (Kazajistán Tenge)", "KZT (Kazakhstan Tenge)", modelBuilder);
        SeedDefaultCurrencyType(88, "LAK (Laos Kip)", "LAK (Lao Kip)", modelBuilder);
        SeedDefaultCurrencyType(89, "LBP (Líbano Libra)", "LBP (Lebanon Pound)", modelBuilder);
        SeedDefaultCurrencyType(90, "LKR (Sri Lanka Rupia)", "LKR (Sri Lanka Rupee)", modelBuilder);
        SeedDefaultCurrencyType(91, "LRD (Liberia Dólar)", "LRD (Liberia Dollar)", modelBuilder);
        SeedDefaultCurrencyType(92, "LSL (Lesotho Loti)", "LSL (Lesotho Loti)", modelBuilder);
        SeedDefaultCurrencyType(93, "LTL (EURO) (Lituania Litas)", "LTL (EURO) (Lithuania Litas)", modelBuilder);
        SeedDefaultCurrencyType(94, "LUF (EURO) (Luxemburgo Franco)", "LUF (EURO) (Luxembourg Franc)", modelBuilder);
        SeedDefaultCurrencyType(95, "LVL (EURO) (Letonia Lats)", "LVL (EURO) (Latvia Lats)", modelBuilder);
        SeedDefaultCurrencyType(96, "LYD (Libia Dinar)", "LYD (Libyan Dinar)", modelBuilder);
        SeedDefaultCurrencyType(97, "MAD (Marruecos Dirham)", "MAD (Moroccan Dirham)", modelBuilder);
        SeedDefaultCurrencyType(98, "MDL (Moldavia Leu)", "MDL (Moldova Leu)", modelBuilder);
        SeedDefaultCurrencyType(99, "MGA (Madagascar Ariary)", "MGA (Madagascar Ariary)", modelBuilder);
        SeedDefaultCurrencyType(100, "MKD (Macedonia Denar)", "MKD (Macedonia Denar)", modelBuilder);
        SeedDefaultCurrencyType(101, "MMK (Myanmar Kyat)", "MMK (Myanmar Kyat)", modelBuilder);
        SeedDefaultCurrencyType(102, "MNT (Mongolia Tugrik)", "MNT (Mongolia Tugrik)", modelBuilder);
        SeedDefaultCurrencyType(103, "MOP (Macao Pataca)", "MOP (Macao Pataca)", modelBuilder);
        SeedDefaultCurrencyType(104, "MRO (Mauritania Ouguiya)", "MRO (Mauritania Ouguiya)", modelBuilder);
        SeedDefaultCurrencyType(105, "MTL (EURO) (Malta Lira)", "MTL (EURO) (Malta Lira)", modelBuilder);
        SeedDefaultCurrencyType(106, "MUR (Mauricio Rupia)", "MUR (Mauritius Rupee)", modelBuilder);
        SeedDefaultCurrencyType(107, "MVR (Maldivas Rufiyaa)", "MVR (Maldives Rufiyaa)", modelBuilder);
        SeedDefaultCurrencyType(108, "MWK (Malawi Kwacha)", "MWK (Malawi Kwacha)", modelBuilder);
        SeedDefaultCurrencyType(109, "MXN (Peso mexicano)", "MXN (Mexican Peso)", modelBuilder);
        SeedDefaultCurrencyType(110, "MYR (Malasia Ringgit)", "MYR (Malaysia Ringgit)", modelBuilder);
        SeedDefaultCurrencyType(111, "MZN (Mozambique Nuevo Metical)", "MZN (Mozambique New Metical)", modelBuilder);
        SeedDefaultCurrencyType(112, "NAD (Namibia Dólar)", "NAD (Namibia Dollar)", modelBuilder);
        SeedDefaultCurrencyType(113, "NGN (Nigeria Naira)", "NGN (Nigeria Naira)", modelBuilder);
        SeedDefaultCurrencyType(114, "NIO (Nicaragua Córdoba)", "NIO (Nicaragua Cordoba)", modelBuilder);
        SeedDefaultCurrencyType(115, "NLG (EURO) (Países bajos Florín)", "NLG (EURO) (Netherlands Florin)", modelBuilder);
        SeedDefaultCurrencyType(116, "NOK (Noruega Corona)", "NOK (Norwegian Krone)", modelBuilder);
        SeedDefaultCurrencyType(117, "NPR (Nepal Rupia)", "NPR (Nepal Rupee)", modelBuilder);
        SeedDefaultCurrencyType(118, "NZD (Nueva Zelanda Dólar)", "NZD (New Zealand Dollar)", modelBuilder);
        SeedDefaultCurrencyType(119, "OMR (Omán Rial)", "OMR (Oman Rial)", modelBuilder);
        SeedDefaultCurrencyType(120, "PAB (Panamá Balboa)", "PAB (Panama Balboa)", modelBuilder);
        SeedDefaultCurrencyType(121, "PEN (Perú Nuevo Sol)", "PEN (Peru Nuevo Sol)", modelBuilder);
        SeedDefaultCurrencyType(122, "PGK (Papúa Nueva Guinea Kina)", "PGK (Papua New Guinea Kina)", modelBuilder);
        SeedDefaultCurrencyType(123, "PHP (Peso filipino)", "PHP (Philippines Peso)", modelBuilder);
        SeedDefaultCurrencyType(124, "PKR (Pakistán Rupia)", "PKR (Pakistan Rupee)", modelBuilder);
        SeedDefaultCurrencyType(125, "PLN (Polonia Zloty)", "PLN (Poland Zloty)", modelBuilder);
        SeedDefaultCurrencyType(126, "PTE (EURO) (Portugal Escudo)", "PTE (EURO) (Portugal Escudo)", modelBuilder);
        SeedDefaultCurrencyType(127, "PYG (Paraguay Guaraní)", "PYG (Paraguay Guarani)", modelBuilder);
        SeedDefaultCurrencyType(128, "QAR (Catar Rial)", "QAR (Qatar Rial)", modelBuilder);
        SeedDefaultCurrencyType(129, "RON (Rumania Nuevo Lei)", "RON (Romania New Lei)", modelBuilder);
        SeedDefaultCurrencyType(130, "RSD (Serbia Dinar)", "RSD (Serbia Dinar)", modelBuilder);
        SeedDefaultCurrencyType(131, "RUB (Rusia Rublo)", "RUB (Russian Rouble)", modelBuilder);
        SeedDefaultCurrencyType(132, "RWF (Ruanda Franco)", "RWF (Rwanda Franc)", modelBuilder);
        SeedDefaultCurrencyType(133, "SAR (Arabia Saudita Rial)", "SAR (Saudi Arabian Rial)", modelBuilder);
        SeedDefaultCurrencyType(134, "SBD (Islas Salomón Dólar)", "SBD (Solomon Islands Dollar)", modelBuilder);
        SeedDefaultCurrencyType(135, "SCR (Seychelles Rupia)", "SCR (Seychelles Rupee)", modelBuilder);
        SeedDefaultCurrencyType(136, "SDG (Sudán Libra)", "SDG (Sudan Pound)", modelBuilder);
        SeedDefaultCurrencyType(137, "SEK (Suecia Corona)", "SEK (Swedish Krona)", modelBuilder);
        SeedDefaultCurrencyType(138, "SGD (Singapur Dólar)", "SGD (Singapore Dollar)", modelBuilder);
        SeedDefaultCurrencyType(139, "SHP (Santa Helena Libra)", "SHP (Saint Helena Pound)", modelBuilder);
        SeedDefaultCurrencyType(140, "SIT (EURO) (Eslovenia Tolar)", "SIT (EURO) (Slovenia Tolar)", modelBuilder);
        SeedDefaultCurrencyType(141, "SKK (EURO) (Eslovaquia Koruna)", "SKK (EURO) (Slovakia Koruna)", modelBuilder);
        SeedDefaultCurrencyType(142, "SLL (Sierra Leona Leone)", "SLL (Sierra Leone Leone)", modelBuilder);
        SeedDefaultCurrencyType(143, "SOS (Somalía Chelín)", "SOS (Somali Shilling)", modelBuilder);
        SeedDefaultCurrencyType(144, "SRD (Suriname Dólar)", "SRD (Suriname Dollar)", modelBuilder);
        SeedDefaultCurrencyType(145, "STD (Santo Tomé y Príncipe Dobra)", "STD (Sao Tome & Principe Dobra)", modelBuilder);
        SeedDefaultCurrencyType(146, "SVC (El Salvador Colón)", "SVC (El Salvador Colon)", modelBuilder);
        SeedDefaultCurrencyType(147, "SYP (Siria Libra)", "SYP (Syria Pound)", modelBuilder);
        SeedDefaultCurrencyType(148, "SZL (Suazilandia Lilangeni)", "SZL (Swaziland Lilangeni)", modelBuilder);
        SeedDefaultCurrencyType(149, "THB (Tailandia Baht)", "THB (Thailand Baht)", modelBuilder);
        SeedDefaultCurrencyType(150, "TMM (Turkmenistán Manat)", "TMM (Turkmenistan Manat)", modelBuilder);
        SeedDefaultCurrencyType(151, "TND (Túnez Dinar)", "TND (Tunisia Dinar)", modelBuilder);
        SeedDefaultCurrencyType(152, "TOP (Tonga Pa'anga)", "TOP (Tonga Pa'anga)", modelBuilder);
        SeedDefaultCurrencyType(153, "TRY (Turquía Nueva Lira)", "TRY (Turkey New Lira)", modelBuilder);
        SeedDefaultCurrencyType(154, "TTD (Trinidad y Tobago Dólar)", "TTD (Trinidad & Tobago Dollar)", modelBuilder);
        SeedDefaultCurrencyType(155, "TWD (Taiwán Dólar)", "TWD (Taiwan Dollar)", modelBuilder);
        SeedDefaultCurrencyType(156, "TZS (Tanzania Chelín)", "TZS (Tanzania Shilling)", modelBuilder);
        SeedDefaultCurrencyType(157, "UAH (Ucrania Hryvnia)", "UAH (Ukraine Hryvnia)", modelBuilder);
        SeedDefaultCurrencyType(158, "UGX (Uganda Chelín)", "UGX (Uganda Shilling)", modelBuilder);
        SeedDefaultCurrencyType(159, "USD (EE.UU. Dólar)", "USD (U.S. Dollar)", modelBuilder);
        SeedDefaultCurrencyType(160, "UYU (Peso uruguayo)", "UYU (Uruguayan Peso)", modelBuilder);
        SeedDefaultCurrencyType(161, "VEB (Venezuela Bolívar)", "VEB (Venezuela Bolivar)", modelBuilder);
        SeedDefaultCurrencyType(162, "VND (Vietnam Dong)", "VND (Vietnam Dong)", modelBuilder);
        SeedDefaultCurrencyType(163, "VUV (Vanuatu Vatu)", "VUV (Vanuatu Vatu)", modelBuilder);
        SeedDefaultCurrencyType(164, "WST (Samoa Tala)", "WST (Samoa Tala)", modelBuilder);
        SeedDefaultCurrencyType(165, "XAF (CFA Franco BEAC)", "XAF (CFA Franc BEAC)", modelBuilder);
        SeedDefaultCurrencyType(166, "XCD (Caribe oriental Dólar)", "XCD (Eastern Caribbean Dollar)", modelBuilder);
        SeedDefaultCurrencyType(167, "XOF (CFA Franco BCEAO)", "XOF (CFA Franc BCEAO)", modelBuilder);
        SeedDefaultCurrencyType(168, "XPF (CFP Franco)", "XPF (CFA Franc Franc CFP)", modelBuilder);
        SeedDefaultCurrencyType(169, "YER (Yemen Rial)", "YER (Yemen Rial)", modelBuilder);
        SeedDefaultCurrencyType(170, "ZAR (Sudáfrica Rand)", "ZAR (South African Rand)", modelBuilder);
        SeedDefaultCurrencyType(171, "ZMK (Zambia Kwacha)", "ZMK (Zambia Kwacha)", modelBuilder);
        SeedDefaultCurrencyType(172, "ZWD (Zimbabue Dólar)", "ZWD (Zimbabwe Dollar)", modelBuilder);
        #endregion

        #region SeedDefaultProfession
        /* Seed Default Profession */
        SeedDefaultProfession(1, "Ninguno", "None", modelBuilder);
        SeedDefaultProfession(2, "Administración aeronáutica", "Aeronautical administration", modelBuilder);
        SeedDefaultProfession(3, "Administración agroindustrial", "Agro-industrial administration", modelBuilder);
        SeedDefaultProfession(4, "Administración agropecuaria", "Agricultural and livestock administration", modelBuilder);
        SeedDefaultProfession(5, "Administración comercial y de mercadeo", "Commercial and marketing administration", modelBuilder);
        SeedDefaultProfession(6, "Administración de aerolíneas", "Airline management", modelBuilder);
        SeedDefaultProfession(7, "Administración de bienes raíces", "Real estate administration", modelBuilder);
        SeedDefaultProfession(8, "Administración de empresas", "Business administration", modelBuilder);
        SeedDefaultProfession(9, "Administración de negocios", "Business management", modelBuilder);
        SeedDefaultProfession(10, "Administración de obras civiles", "Civil Works Administration", modelBuilder);
        SeedDefaultProfession(11, "Administración de personal", "Personnel administration", modelBuilder);
        SeedDefaultProfession(12, "Administración de seguros", "Insurance administration", modelBuilder);
        SeedDefaultProfession(13, "Administración de servicios", "Service administration", modelBuilder);
        SeedDefaultProfession(14, "Administración de sistemas informáticos", "IT systems administration", modelBuilder);
        SeedDefaultProfession(15, "Administración de transporte", "Transportation administration", modelBuilder);
        SeedDefaultProfession(16, "Administración financiera", "Financial administration", modelBuilder);
        SeedDefaultProfession(17, "Administración hospitalaria", "Hospital administration", modelBuilder);
        SeedDefaultProfession(18, "Administración industrial", "Industrial administration", modelBuilder);
        SeedDefaultProfession(19, "Administración pública", "Public administration", modelBuilder);
        SeedDefaultProfession(20, "Administración tributaria", "Tax administration", modelBuilder);
        SeedDefaultProfession(21, "Administración turística hotelera", "Hotel and tourism administration", modelBuilder);
        SeedDefaultProfession(22, "Bachillerato Académico", "High School", modelBuilder);
        SeedDefaultProfession(23, "Bachillerato comercial", "Business high school degree", modelBuilder);
        SeedDefaultProfession(24, "Bachillerato técnico", "Technical high school degree", modelBuilder);
        SeedDefaultProfession(25, "Ciencias políticas y gobierno", "Political science and government", modelBuilder);
        SeedDefaultProfession(26, "Comercio internacional", "International commerce", modelBuilder);
        SeedDefaultProfession(27, "Comunicación publicitaria", "Advertising Communication", modelBuilder);
        SeedDefaultProfession(28, "Comunicación social y periodismo", "Social communication and journalism", modelBuilder);
        SeedDefaultProfession(29, "Contaduría", "Accounting", modelBuilder);
        SeedDefaultProfession(30, "Derecho", "Law", modelBuilder);
        SeedDefaultProfession(31, "Diseño gráfico", "Graphic design", modelBuilder);
        SeedDefaultProfession(32, "Diseño industrial", "Industrial design", modelBuilder);
        SeedDefaultProfession(33, "Docente", "Teacher", modelBuilder);
        SeedDefaultProfession(34, "Doctorado en administración", "Doctorate in administration", modelBuilder);
        SeedDefaultProfession(35, "Doctorado en economía", "Doctorate in Economics", modelBuilder);
        SeedDefaultProfession(36, "Doctorado en humanidades", "Doctorate in Humanities", modelBuilder);
        SeedDefaultProfession(37, "Economía", "Economics", modelBuilder);
        SeedDefaultProfession(38, "Especialización en administración de empresas", "Specialization in business administration", modelBuilder);
        SeedDefaultProfession(39, "Especialización en alta gerencia", "Specialization in senior management", modelBuilder);
        SeedDefaultProfession(40, "Especialización en educación bilingüe", "Specialization in bilingual education", modelBuilder);
        SeedDefaultProfession(41, "Especialización en finanzas", "Specialization in finance", modelBuilder);
        SeedDefaultProfession(42, "Especialización en gerencia de empresas constructoras", "Specialization in construction company management", modelBuilder);
        SeedDefaultProfession(43, "Especialización en gerencia de la calidad", "Specialization in quality management", modelBuilder);
        SeedDefaultProfession(44, "Especialización en gerencia de proyectos", "Specialization in project management", modelBuilder);
        SeedDefaultProfession(45, "Especialización en gerencia del talento humano", "Specialization in human talent management", modelBuilder);
        SeedDefaultProfession(46, "Especialización en gerencia logística internacional", "Specialization in international logistics management", modelBuilder);
        SeedDefaultProfession(47, "Especialización en gerencia prospectiva y estrategia", "Specialization in prospective management and strategy", modelBuilder);
        SeedDefaultProfession(48, "Especialización en gerencia tributaria", "Specialization in tax management", modelBuilder);
        SeedDefaultProfession(49, "Especialización en gestión de servicios de tecnologías de información", "Specialization in Information Technology Services Management", modelBuilder);
        SeedDefaultProfession(50, "Especialización en legislación aduanera", "Specialization in customs legislation", modelBuilder);
        SeedDefaultProfession(51, "Especialización en mercadeo", "Specialization in marketing", modelBuilder);
        SeedDefaultProfession(52, "Especialización en negocios internacionales e integración económica", "Specialization in international business and economic integration", modelBuilder);
        SeedDefaultProfession(53, "Especialización en proyectos de desarrollo", "Specialization in development projects", modelBuilder);
        SeedDefaultProfession(54, "Estadista", "Statistician", modelBuilder);
        SeedDefaultProfession(55, "Finanzas, relaciones internacionales y gobierno", "Finance, international relations and government", modelBuilder);
        SeedDefaultProfession(56, "Finanzas y gobierno", "Finance and government", modelBuilder);
        SeedDefaultProfession(57, "Gobierno y relaciones internacionales", "Government and international relations", modelBuilder);
        SeedDefaultProfession(58, "Ingeniería administrativa", "Administrative engineering", modelBuilder);
        SeedDefaultProfession(59, "Ingeniería comercial", "Commercial Engineering", modelBuilder);
        SeedDefaultProfession(60, "Ingeniería de energías", "Energy Engineering", modelBuilder);
        SeedDefaultProfession(61, "Ingeniería de mercados", "Market engineering", modelBuilder);
        SeedDefaultProfession(62, "Ingeniería de procesos", "Process engineering", modelBuilder);
        SeedDefaultProfession(63, "Ingeniería de producción", "Production engineering", modelBuilder);
        SeedDefaultProfession(64, "Ingeniería de redes y telecomunicaciones", "Network and telecommunications engineering", modelBuilder);
        SeedDefaultProfession(65, "Ingeniería de sistemas en computación", "Computer systems engineering", modelBuilder);
        SeedDefaultProfession(66, "Ingeniería de software", "Software engineering", modelBuilder);
        SeedDefaultProfession(67, "Ingeniería de telecomunicaciones", "Telecommunications engineering", modelBuilder);
        SeedDefaultProfession(68, "Ingeniería eléctrica", "Electrical engineering", modelBuilder);
        SeedDefaultProfession(69, "Ingeniería electromecánica", "Electromechanical engineering", modelBuilder);
        SeedDefaultProfession(70, "Ingeniería electrónica", "Electronics engineering", modelBuilder);
        SeedDefaultProfession(71, "Ingeniería industrial", "Industrial engineering", modelBuilder);
        SeedDefaultProfession(72, "Ingeniería mecánica", "Mechanical engineering", modelBuilder);
        SeedDefaultProfession(73, "Ingeniería mecatrónica", "Mechatronics Engineering", modelBuilder);
        SeedDefaultProfession(74, "Ingeniería química", "Chemical engineering", modelBuilder);
        SeedDefaultProfession(75, "Ingeniería telemática", "Telematics engineering", modelBuilder);
        SeedDefaultProfession(76, "Licenciatura en inglés", "Bachelor's degree in English", modelBuilder);
        SeedDefaultProfession(77, "Maestría en administración de empresa", "Master of Business Administration", modelBuilder);
        SeedDefaultProfession(78, "Maestría en derecho", "Master's Degree in Law", modelBuilder);
        SeedDefaultProfession(79, "Maestría en desarrollo humano", "Master's degree in human development", modelBuilder);
        SeedDefaultProfession(80, "Maestría en dirección de marketing", "Master's degree in marketing management", modelBuilder);
        SeedDefaultProfession(81, "Maestría en economía", "Master's degree in economics", modelBuilder);
        SeedDefaultProfession(82, "Maestría en emprendimiento e innovación", "Master's degree in entrepreneurship and innovation", modelBuilder);
        SeedDefaultProfession(83, "Maestría en finanzas corporativas", "Master's degree in corporate finance", modelBuilder);
        SeedDefaultProfession(84, "Maestría gestión integral de la calidad y la productividad", "Master's degree in integrated quality and productivity management", modelBuilder);
        SeedDefaultProfession(85, "Maestría en ingeniería industrial", "Master's degree in industrial engineering", modelBuilder);
        SeedDefaultProfession(86, "Maestría en mercadeo", "Master's degree in marketing", modelBuilder);
        SeedDefaultProfession(87, "Maestría en innovación", "Master's degree in innovation", modelBuilder);
        SeedDefaultProfession(88, "Negocios internacionales", "International business", modelBuilder);
        SeedDefaultProfession(89, "Planeación y desarrollo social", "Planning and social development", modelBuilder);
        SeedDefaultProfession(90, "Profesional en logística", "Professional in logistics", modelBuilder);
        SeedDefaultProfession(91, "Profesional en marketing y negocios internacionales", "Marketing and international business professional", modelBuilder);
        SeedDefaultProfession(92, "Psicología", "Psychology", modelBuilder);
        SeedDefaultProfession(93, "Publicidad y mercadeo", "Advertising and marketing", modelBuilder);
        SeedDefaultProfession(94, "Secretariado", "Secretarial work", modelBuilder);
        SeedDefaultProfession(95, "Sociología", "Sociology", modelBuilder);
        SeedDefaultProfession(96, "Técnico en administración de personal", "Personnel administration technician", modelBuilder);
        SeedDefaultProfession(97, "Técnico seguridad industrial", "Industrial safety technician", modelBuilder);
        SeedDefaultProfession(98, "Técnico sistemas de computación", "Computer systems technician", modelBuilder);
        SeedDefaultProfession(99, "Técnico en profesional de producción", "Production professional technician", modelBuilder);
        SeedDefaultProfession(100, "Técnico en desarrollo y mantenimiento de software", "Technician in software development and maintenance", modelBuilder);
        SeedDefaultProfession(101, "Técnico en gestión contable", "Technician in accounting management", modelBuilder);
        SeedDefaultProfession(102, "Técnico en gestión empresarial", "Technician in business management", modelBuilder);
        SeedDefaultProfession(103, "Técnico en logística", "Logistics technician", modelBuilder);
        SeedDefaultProfession(104, "Técnico en sistemas de computación", "Computer systems technician", modelBuilder);
        SeedDefaultProfession(105, "Técnico de mantenimiento", "Maintenance technician", modelBuilder);
        SeedDefaultProfession(106, "Técnico profesional en producción", "Professional production technician", modelBuilder);
        SeedDefaultProfession(107, "Técnico en mantenimiento", "Maintenance technician", modelBuilder);
        SeedDefaultProfession(108, "Tecnología en banca y finanzas", "Banking and finance technology", modelBuilder);
        SeedDefaultProfession(109, "Tecnología en comercio internacional", "International trade technology", modelBuilder);
        SeedDefaultProfession(110, "Tecnología en desarrollo de sistemas informáticos", "Computer systems development technology", modelBuilder);
        SeedDefaultProfession(111, "Tecnología en electricidad industrial", "Industrial electricity technology", modelBuilder);
        SeedDefaultProfession(112, "Tecnología en electrónica", "Electronics Technology", modelBuilder);
        SeedDefaultProfession(113, "Tecnología en gestión administrativa", "Administrative Management Technology", modelBuilder);
        SeedDefaultProfession(114, "Tecnología en gestión comercial", "Commercial management technology", modelBuilder);
        SeedDefaultProfession(115, "Tecnología en gestión de mercadeo", "Marketing Management Technology", modelBuilder);
        SeedDefaultProfession(116, "Tecnología en gestión de sistemas de telecomunicaciones", "Telecommunication Systems Management Technology", modelBuilder);
        SeedDefaultProfession(117, "Tecnología en implementación de sistemas electrónicos industriales", "Industrial electronic systems implementation technology", modelBuilder);
        SeedDefaultProfession(118, "Otra", "Other", modelBuilder);
        #endregion

        #region SeedDefaultLifePreference
        /* Seed Default Life Preference */
        SeedDefaultLifePreference(1, "Ninguno", "None", modelBuilder);
        SeedDefaultLifePreference(2, "Actuar", "Act", modelBuilder);
        SeedDefaultLifePreference(3, "Acuarios", "Aquariums", modelBuilder);
        SeedDefaultLifePreference(4, "Aerobic", "Aerobic", modelBuilder);
        SeedDefaultLifePreference(5, "Aeromodelismo", "Aeromodelling", modelBuilder);
        SeedDefaultLifePreference(6, "Aerostación", "Aerostation", modelBuilder);
        SeedDefaultLifePreference(7, "Aikido", "Aikido", modelBuilder);
        SeedDefaultLifePreference(8, "Airsoft", "Airsoft", modelBuilder);
        SeedDefaultLifePreference(9, "Ajedrez", "Chess", modelBuilder);
        SeedDefaultLifePreference(10, "Animación", "Animation", modelBuilder);
        SeedDefaultLifePreference(11, "Aquagym", "Aquagym", modelBuilder);
        SeedDefaultLifePreference(12, "Aromaterapia", "Aromatherapy", modelBuilder);
        SeedDefaultLifePreference(13, "Arte digital", "Digital art", modelBuilder);
        SeedDefaultLifePreference(14, "Arte", "Art", modelBuilder);
        SeedDefaultLifePreference(15, "Asociaciones", "Associations", modelBuilder);
        SeedDefaultLifePreference(16, "Astrología", "Astrology", modelBuilder);
        SeedDefaultLifePreference(17, "Astronomía", "Astronomy", modelBuilder);
        SeedDefaultLifePreference(18, "Atletismo", "Athletics", modelBuilder);
        SeedDefaultLifePreference(19, "Audiolibros", "Audiobooks", modelBuilder);
        SeedDefaultLifePreference(20, "Autocaravanas", "Motorhomes", modelBuilder);
        SeedDefaultLifePreference(21, "Automovilismo", "Motoring", modelBuilder);
        SeedDefaultLifePreference(22, "Aviación deportiva", "Sport Aviation", modelBuilder);
        SeedDefaultLifePreference(23, "Avicultura", "Aviculture", modelBuilder);
        SeedDefaultLifePreference(24, "Avistamiento de aves", "Bird watching", modelBuilder);
        SeedDefaultLifePreference(25, "Backgammon", "Backgammon", modelBuilder);
        SeedDefaultLifePreference(26, "Badminton", "Badminton", modelBuilder);
        SeedDefaultLifePreference(27, "Bailar", "Dancing", modelBuilder);
        SeedDefaultLifePreference(28, "Baloncesto", "Basketball", modelBuilder);
        SeedDefaultLifePreference(29, "Balonmano", "Handball", modelBuilder);
        SeedDefaultLifePreference(30, "Banda o grupo musicales", "Band or musical group", modelBuilder);
        SeedDefaultLifePreference(31, "Barcos de motor", "Motor boats", modelBuilder);
        SeedDefaultLifePreference(32, "Barranquismo", "Canyoning", modelBuilder);
        SeedDefaultLifePreference(33, "Batik y estampación de tejidos", "Batik and fabric printing", modelBuilder);
        SeedDefaultLifePreference(34, "Beisbol", "Baseball", modelBuilder);
        SeedDefaultLifePreference(35, "Belleza y estética", "Beauty and esthetics", modelBuilder);
        SeedDefaultLifePreference(36, "Biatlon", "Biathlon", modelBuilder);
        SeedDefaultLifePreference(37, "Bibliofilia", "Bibliophilia", modelBuilder);
        SeedDefaultLifePreference(38, "Bicicleta(ciclismo)", "Bicycle(cycling)", modelBuilder);
        SeedDefaultLifePreference(39, "Bicicleta de montaña", "Mountain biking", modelBuilder);
        SeedDefaultLifePreference(40, "Bikejoring", "Bikejoring", modelBuilder);
        SeedDefaultLifePreference(41, "Billar", "Billiards", modelBuilder);
        SeedDefaultLifePreference(42, "Bingo", "Bingo", modelBuilder);
        SeedDefaultLifePreference(43, "Blog(y videoblog)", "Blog(and videoblog)", modelBuilder);
        SeedDefaultLifePreference(44, "BMX", "BMX", modelBuilder);
        SeedDefaultLifePreference(45, "Bodyboard", "Bodyboard", modelBuilder);
        SeedDefaultLifePreference(46, "Bolos(bowling, boliche)", "Bowling", modelBuilder);
        SeedDefaultLifePreference(47, "Bonsai(arboles enanos)", "Bonsai(dwarf trees)", modelBuilder);
        SeedDefaultLifePreference(48, "Bordado sobre papel", "Embroidery on paper", modelBuilder);
        SeedDefaultLifePreference(49, "Boxeo", "Boxing", modelBuilder);
        SeedDefaultLifePreference(50, "Bricolaje o diy", "DIY", modelBuilder);
        SeedDefaultLifePreference(51, "Buceo", "Diving", modelBuilder);
        SeedDefaultLifePreference(52, "Caballo(equitación)", "Horse(horseback riding)", modelBuilder);
        SeedDefaultLifePreference(53, "Caligrafía y lettering", "Calligraphy and lettering", modelBuilder);
        SeedDefaultLifePreference(54, "Campismo", "Camping", modelBuilder);
        SeedDefaultLifePreference(55, "Canaricultura", "Dog breeding", modelBuilder);
        SeedDefaultLifePreference(56, "Canicross", "Canicross", modelBuilder);
        SeedDefaultLifePreference(57, "Cantar", "Singing", modelBuilder);
        SeedDefaultLifePreference(58, "Capoeira", "Capoeira", modelBuilder);
        SeedDefaultLifePreference(59, "Carpintería", "Carpentry", modelBuilder);
        SeedDefaultLifePreference(60, "Carreras por montaña", "Mountain racing", modelBuilder);
        SeedDefaultLifePreference(61, "Carrovela y blokart", "Carrovela and blokart", modelBuilder);
        SeedDefaultLifePreference(62, "Casas de muñecas", "Doll houses", modelBuilder);
        SeedDefaultLifePreference(63, "Cata de cerveza", "Beer tasting", modelBuilder);
        SeedDefaultLifePreference(64, "Cata de vinos", "Wine tasting", modelBuilder);
        SeedDefaultLifePreference(65, "Caza(deportiva)", "Hunting(sport)", modelBuilder);
        SeedDefaultLifePreference(66, "Cerámica", "Ceramics", modelBuilder);
        SeedDefaultLifePreference(67, "Cerveza(fabricación)", "Beer(brewing)", modelBuilder);
        SeedDefaultLifePreference(68, "Cestería", "Basketry", modelBuilder);
        SeedDefaultLifePreference(69, "Cetrería", "Falconry", modelBuilder);
        SeedDefaultLifePreference(70, "Chalkpaint(pintura de tiza)", "Chalkpaint(chalk painting)", modelBuilder);
        SeedDefaultLifePreference(71, "Chi kung o qui gong", "Chi kung or qui gong", modelBuilder);
        SeedDefaultLifePreference(72, "Cianotipia", "Cyanotype", modelBuilder);
        SeedDefaultLifePreference(73, "Cicloturismo", "Cycling", modelBuilder);
        SeedDefaultLifePreference(74, "Cine", "Cinema", modelBuilder);
        SeedDefaultLifePreference(75, "Cocina", "Cooking", modelBuilder);
        SeedDefaultLifePreference(76, "Coleccionismo(antigüedades)", "Collecting(antiques)", modelBuilder);
        SeedDefaultLifePreference(77, "Colorear libros", "Coloring books", modelBuilder);
        SeedDefaultLifePreference(78, "Cometas(volar cometas)", "Kites(kite flying)", modelBuilder);
        SeedDefaultLifePreference(79, "Comics(creación de)", "Comics(creating)", modelBuilder);
        SeedDefaultLifePreference(80, "Comics(lectura de)", "Comics(reading)", modelBuilder);
        SeedDefaultLifePreference(81, "Comidista(foodie)", "Foodie", modelBuilder);
        SeedDefaultLifePreference(82, "Composición musical", "Musical composition", modelBuilder);
        SeedDefaultLifePreference(83, "Compostaje", "Composting", modelBuilder);
        SeedDefaultLifePreference(84, "Comprar", "Shopping", modelBuilder);
        SeedDefaultLifePreference(85, "Conducción de automóviles", "Automobile driving", modelBuilder);
        SeedDefaultLifePreference(86, "Conducción de motocicletas", "Motorcycle driving", modelBuilder);
        SeedDefaultLifePreference(87, "Conferencias(asistir a)", "Lectures(attend)", modelBuilder);
        SeedDefaultLifePreference(88, "Coro", "Choir", modelBuilder);
        SeedDefaultLifePreference(89, "Correr(running)", "Running", modelBuilder);
        SeedDefaultLifePreference(90, "Cosmética", "Cosmetics", modelBuilder);
        SeedDefaultLifePreference(91, "Costura, corte y confección", "Sewing, cutting and tailoring", modelBuilder);
        SeedDefaultLifePreference(92, "Cristal teñido", "Stained glass", modelBuilder);
        SeedDefaultLifePreference(93, "Croquet", "Croquet", modelBuilder);
        SeedDefaultLifePreference(94, "Cruceros(viajes de)", "Cruising(travel)", modelBuilder);
        SeedDefaultLifePreference(95, "Cubo de rubik(rompecabezas)", "Rubik's cube (puzzles)", modelBuilder);
        SeedDefaultLifePreference(96, "Cuero(creaciones con)", "Leather(creations with)", modelBuilder);
        SeedDefaultLifePreference(97, "Culturismo(body building)", "Bodybuilding(body building)", modelBuilder);
        SeedDefaultLifePreference(98, "Curling", "Curling", modelBuilder);
        SeedDefaultLifePreference(99, "Customización de ropa", "Clothing customization", modelBuilder);
        SeedDefaultLifePreference(100, "Customización y restauración de bicicletas", "Customization and restoration of bicycles", modelBuilder);
        SeedDefaultLifePreference(101, "Damas(juego de)", "Checkers(checkers game)", modelBuilder);
        SeedDefaultLifePreference(102, "Danza aérea", "Aerial dance", modelBuilder);
        SeedDefaultLifePreference(103, "Dardos(lanzar dardos)", "Darts(throwing darts)", modelBuilder);
        SeedDefaultLifePreference(104, "Decoración de interiores", "Interior decoration", modelBuilder);
        SeedDefaultLifePreference(105, "Decoupage(decoración de superficies)", "Decoupage(surface decoration)", modelBuilder);
        SeedDefaultLifePreference(106, "Deporte(asistir a espectáculos deportivos)", "Sport(attending sporting events)", modelBuilder);
        SeedDefaultLifePreference(107, "Deporte(ver deporte)", "Sports(watching sports)", modelBuilder);
        SeedDefaultLifePreference(108, "Deportes de fantasía(liga de fantasía)", "Fantasy sports(fantasy league)", modelBuilder);
        SeedDefaultLifePreference(109, "Diario(escribir un)", "Journaling(writing a diary)", modelBuilder);
        SeedDefaultLifePreference(110, "Dibujo artístico", "Artistic drawing", modelBuilder);
        SeedDefaultLifePreference(111, "Dioramas(y belenes)", "Dioramas(and nativity scenes)", modelBuilder);
        SeedDefaultLifePreference(112, "Dirigir, entrenar, gestionar.", "Directing, coaching, managing", modelBuilder);
        SeedDefaultLifePreference(113, "Disc golf", "Disc golf", modelBuilder);
        SeedDefaultLifePreference(114, "Diseño de ropa(moda)", "Clothing design(fashion)", modelBuilder);
        SeedDefaultLifePreference(115, "Diseño y creación de páginas web", "Design and creation of web pages", modelBuilder);
        SeedDefaultLifePreference(116, "Dj(disk jockey)", "Dj(disc jockey)", modelBuilder);
        SeedDefaultLifePreference(117, "Documentales(afición a los)", "Documentaries(hobby)", modelBuilder);
        SeedDefaultLifePreference(118, "Domino", "Domino", modelBuilder);
        SeedDefaultLifePreference(119, "Electrónica", "Electronics", modelBuilder);
        SeedDefaultLifePreference(120, "Encuadernación de libros", "Book binding", modelBuilder);
        SeedDefaultLifePreference(121, "Enmarcar cuadros", "Picture framing", modelBuilder);
        SeedDefaultLifePreference(122, "Escalada", "Climbing", modelBuilder);
        SeedDefaultLifePreference(123, "Escribir literatura", "Writing literature", modelBuilder);
        SeedDefaultLifePreference(124, "Escultura", "Sculpting", modelBuilder);
        SeedDefaultLifePreference(125, "Esgrima", "Fencing", modelBuilder);
        SeedDefaultLifePreference(126, "Esmaltes sobre metal(al fuego)", "Enamels on metal(on fire)", modelBuilder);
        SeedDefaultLifePreference(127, "Espeleología", "Speleology", modelBuilder);
        SeedDefaultLifePreference(128, "Esports", "Esports", modelBuilder);
        SeedDefaultLifePreference(129, "Esquí alpino", "Alpine skiing", modelBuilder);
        SeedDefaultLifePreference(130, "Esquí de fondo o nórdico", "Cross-country or Nordic skiing", modelBuilder);
        SeedDefaultLifePreference(131, "Esquí náutico o acuático", "Water skiing or water skiing", modelBuilder);
        SeedDefaultLifePreference(132, "Estudiar", "Study", modelBuilder);
        SeedDefaultLifePreference(133, "Exploración urbana(urbex)", "Urban exploration(urbex)", modelBuilder);
        SeedDefaultLifePreference(134, "Flamenco", "Flamenco", modelBuilder);
        SeedDefaultLifePreference(135, "Floorball(o unihockey)", "Floorball(or unihockey)", modelBuilder);
        SeedDefaultLifePreference(136, "Flores kanzashi", "Kanzashi flowers", modelBuilder);
        SeedDefaultLifePreference(137, "Flores secas(trabajar con)", "Dried flowers(work with)", modelBuilder);
        SeedDefaultLifePreference(138, "Flyboard", "Flyboard", modelBuilder);
        SeedDefaultLifePreference(139, "Fotografía", "Photography", modelBuilder);
        SeedDefaultLifePreference(140, "Frisbee(disco volador)", "Frisbee(flying disc)", modelBuilder);
        SeedDefaultLifePreference(141, "Frontón", "Fronton", modelBuilder);
        SeedDefaultLifePreference(142, "Futbol", "Soccer", modelBuilder);
        SeedDefaultLifePreference(143, "Futbol americano", "Football", modelBuilder);
        SeedDefaultLifePreference(144, "Futbol sala", "Indoor soccer", modelBuilder);
        SeedDefaultLifePreference(145, "Futbolín", "Foosball", modelBuilder);
        SeedDefaultLifePreference(146, "Genealogía e historia familiar", "Genealogy and family history", modelBuilder);
        SeedDefaultLifePreference(147, "Geocaching y búsqueda de tesoros", "Geocaching and treasure hunting", modelBuilder);
        SeedDefaultLifePreference(148, "Gimnasia de mantenimiento y fitness", "Gymnastics and fitness", modelBuilder);
        SeedDefaultLifePreference(149, "Globos(trabajo con globos)", "Balloons(balloon work)", modelBuilder);
        SeedDefaultLifePreference(150, "Go(juego)", "Go(game)", modelBuilder);
        SeedDefaultLifePreference(151, "Golf", "Golf", modelBuilder);
        SeedDefaultLifePreference(152, "Grabados artísticos", "Artistic engravings", modelBuilder);
        SeedDefaultLifePreference(153, "Graffiti y arte urbano", "Graffiti and street art", modelBuilder);
        SeedDefaultLifePreference(154, "Groundhopping(«ir de estadio en estadio»)", "Groundhopping('going from stadium to stadium')", modelBuilder);
        SeedDefaultLifePreference(155, "Hacer vino", "Making wine", modelBuilder);
        SeedDefaultLifePreference(156, "Halterofilia(levantamiento de pesas)", "Weightlifting", modelBuilder);
        SeedDefaultLifePreference(157, "Hapkido", "Hapkido", modelBuilder);
        SeedDefaultLifePreference(158, "Hidroponía(cultivo en liquido)", "Hydroponics(liquid farming)", modelBuilder);
        SeedDefaultLifePreference(159, "Hockey de mesa(air hockey)", "Table field hockey(air field hockey)", modelBuilder); SeedDefaultLifePreference(160, "Hockey sobre hielo", "Ice hockey", modelBuilder);
        SeedDefaultLifePreference(161, "Hockey sobre hierba", "Field Hockey", modelBuilder);
        SeedDefaultLifePreference(162, "Hockey sobre patines", "Roller Hockey", modelBuilder);
        SeedDefaultLifePreference(163, "Hockey subacuático", "Underwater field hockey", modelBuilder);
        SeedDefaultLifePreference(164, "Horticultura(agricultura)", "Horticulture(agriculture)", modelBuilder);
        SeedDefaultLifePreference(165, "Huerto casero", "Home gardening", modelBuilder);
        SeedDefaultLifePreference(166, "Hydrospeed", "Hydrospeed", modelBuilder);
        SeedDefaultLifePreference(167, "Ikebana(arte floral)", "Ikebana(floral art)", modelBuilder);
        SeedDefaultLifePreference(168, "Internet", "Internet", modelBuilder);
        SeedDefaultLifePreference(169, "Invertir en bolsa", "Investing in the stock market", modelBuilder);
        SeedDefaultLifePreference(170, "Jabón(hacer)", "Soap(making)", modelBuilder);
        SeedDefaultLifePreference(171, "Jardinería", "Gardening", modelBuilder);
        SeedDefaultLifePreference(172, "Jiu jitsu", "Jiu jitsu", modelBuilder);
        SeedDefaultLifePreference(173, "Joyas y bisutería(creación de)", "Jewelry and costume jewelry(making)", modelBuilder);
        SeedDefaultLifePreference(174, "Judo", "Judo", modelBuilder);
        SeedDefaultLifePreference(175, "Juegos con dispositivos electrónicos", "Games with electronic devices", modelBuilder);
        SeedDefaultLifePreference(176, "Juegos de cartas o naipes", "Card games", modelBuilder);
        SeedDefaultLifePreference(177, "Juegos de mesa", "Board games", modelBuilder);
        SeedDefaultLifePreference(178, "Juegos de mesa temáticos", "Themed board games", modelBuilder);
        SeedDefaultLifePreference(179, "Juegos de rol", "Role - playing games", modelBuilder);
        SeedDefaultLifePreference(180, "Jugger", "Jugger", modelBuilder);
        SeedDefaultLifePreference(181, "Karaoke", "Karaoke", modelBuilder);
        SeedDefaultLifePreference(182, "Karate", "Karate", modelBuilder);
        SeedDefaultLifePreference(183, "Kayak polo", "Kayak polo", modelBuilder);
        SeedDefaultLifePreference(184, "Kendo", "Kendo", modelBuilder);
        SeedDefaultLifePreference(185, "Kenpo", "Kenpo", modelBuilder);
        SeedDefaultLifePreference(186, "Kick boxing", "Kick boxing", modelBuilder);
        SeedDefaultLifePreference(187, "Kintsugi", "Kintsugi", modelBuilder);
        SeedDefaultLifePreference(188, "Kitesurf", "Kitesurfing", modelBuilder);
        SeedDefaultLifePreference(189, "Kokedamas(cultivo de plantas)", "Kokedamas(plant cultivation)", modelBuilder);
        SeedDefaultLifePreference(190, "Korfball", "Korfball", modelBuilder);
        SeedDefaultLifePreference(191, "Kung fu", "Kung fu", modelBuilder);
        SeedDefaultLifePreference(192, "Labores(punto, bordado, encaje, etc)", "Needlework(knitting, embroidery, lace, etc.)", modelBuilder);
        SeedDefaultLifePreference(193, "Lectura", "Reading", modelBuilder);
        SeedDefaultLifePreference(194, "Lego", "Lego", modelBuilder);
        SeedDefaultLifePreference(195, "Lotería", "Lottery", modelBuilder);
        SeedDefaultLifePreference(196, "Lucha(olímpica, grecorromana)", "Wrestling(Olympic, Greco - Roman)", modelBuilder); SeedDefaultLifePreference(197, "Madera(tallas en madera)", "Wood(wood carving)", modelBuilder);
        SeedDefaultLifePreference(198, "Magia(ilusionismo, prestidigitación)", "Magic(illusionism, conjuring)", modelBuilder);
        SeedDefaultLifePreference(199, "Mahjong(juego de fichas chino)", "Mahjong(Chinese tile game)", modelBuilder);
        SeedDefaultLifePreference(200, "Malabares", "Juggling", modelBuilder);
        SeedDefaultLifePreference(201, "Manualidades", "Handicrafts", modelBuilder);
        SeedDefaultLifePreference(202, "Marcha o paseo nórdico(nordic walking)", "Nordic walking(Nordic walking)", modelBuilder);
        SeedDefaultLifePreference(203, "Marionetas(teatro de)", "Puppetry(puppet theater)", modelBuilder);
        SeedDefaultLifePreference(204, "Meditación", "Meditation", modelBuilder);
        SeedDefaultLifePreference(205, "Mercadillos / flea markets", "Flea markets", modelBuilder);
        SeedDefaultLifePreference(206, "Mermeladas(preparar)", "Jams(preparing)", modelBuilder);
        SeedDefaultLifePreference(207, "Metales(búsqueda de)", "Metals(search for)", modelBuilder);
        SeedDefaultLifePreference(208, "Mindfulness o atención plena", "Mindfulness", modelBuilder);
        SeedDefaultLifePreference(209, "Minerales(búsqueda de)", "Minerals(search for)", modelBuilder);
        SeedDefaultLifePreference(210, "Moda(afición a la moda)", "Fashion(fashion hobby)", modelBuilder);
        SeedDefaultLifePreference(211, "Modelismo(aviones, coches, barcos, drones)", "Modeling(airplanes, cars, boats, drones)", modelBuilder);
        SeedDefaultLifePreference(212, "Modelismo con cerillas", "Match modeling", modelBuilder);
        SeedDefaultLifePreference(213, "Montañismo / alpinismo", "Mountaineering / alpinism", modelBuilder);
        SeedDefaultLifePreference(214, "Moto acuática", "Jet skiing", modelBuilder);
        SeedDefaultLifePreference(215, "Muñecas(hacer)", "Dolls(doll making)", modelBuilder);
        SeedDefaultLifePreference(216, "Mushing", "Mushing", modelBuilder);
        SeedDefaultLifePreference(217, "Música(afición a la)", "Music(hobby)", modelBuilder);
        SeedDefaultLifePreference(218, "Nadar(natación)", "Swimming", modelBuilder);
        SeedDefaultLifePreference(219, "Observación de trenes y aviones", "Train and airplane observation", modelBuilder);
        SeedDefaultLifePreference(220, "Orientación", "Orientation", modelBuilder);
        SeedDefaultLifePreference(221, "Origami(o papiroflexia)", "Origami", modelBuilder);
        SeedDefaultLifePreference(222, "Padbol", "Paddleball", modelBuilder);
        SeedDefaultLifePreference(223, "Paddle surf(surf a remo)", "Paddle surfing", modelBuilder);
        SeedDefaultLifePreference(224, "Padel", "Paddle boarding", modelBuilder);
        SeedDefaultLifePreference(225, "Paintball", "Paintball", modelBuilder);
        SeedDefaultLifePreference(226, "Palomas(cría de palomas)", "Pigeons(pigeon breeding)", modelBuilder);
        SeedDefaultLifePreference(227, "Papel mache", "Paper mache", modelBuilder);
        SeedDefaultLifePreference(228, "Paracaidismo", "Parachuting", modelBuilder);
        SeedDefaultLifePreference(229, "Paramotor", "Paramotoring", modelBuilder);
        SeedDefaultLifePreference(230, "Parapente", "Paragliding", modelBuilder);
        SeedDefaultLifePreference(231, "Parkour(freerunning)", "Parkour(freerunning)", modelBuilder);
        SeedDefaultLifePreference(232, "Pasatiempos(crucigramas, sudokus)", "Hobbies(crossword puzzles, sudoku)", modelBuilder);
        SeedDefaultLifePreference(233, "Pasear", "Walking", modelBuilder);
        SeedDefaultLifePreference(234, "Patchwork y colchas", "Patchwork and quilting", modelBuilder);
        SeedDefaultLifePreference(235, "Patinaje sobre hielo", "Ice skating", modelBuilder);
        SeedDefaultLifePreference(236, "Patinaje sobre ruedas(roller)", "Roller skating(roller skating)", modelBuilder);
        SeedDefaultLifePreference(237, "Perros(concursos / exhibiciones)", "Dogs(contests / exhibitions)", modelBuilder);
        SeedDefaultLifePreference(238, "Pesca", "Fishing", modelBuilder);
        SeedDefaultLifePreference(239, "Pesca submarina", "Underwater fishing", modelBuilder);
        SeedDefaultLifePreference(240, "Petanca", "Petanque", modelBuilder);
        SeedDefaultLifePreference(241, "Pilates", "Pilates", modelBuilder);
        SeedDefaultLifePreference(242, "Ping pong o tenis de mesa", "Ping pong or table tennis", modelBuilder);
        SeedDefaultLifePreference(243, "Pintura artística", "Artistic painting", modelBuilder);
        SeedDefaultLifePreference(244, "Pintura de figuras", "Figure painting", modelBuilder);
        SeedDefaultLifePreference(245, "Pintura sobre seda", "Silk painting", modelBuilder);
        SeedDefaultLifePreference(246, "Piragüismo(canoismo / canotaje)", "Canoeing(boating)", modelBuilder);
        SeedDefaultLifePreference(247, "Plantas de interior", "Indoor plants", modelBuilder);
        SeedDefaultLifePreference(248, "Plantas silvestres", "Wild plants", modelBuilder);
        SeedDefaultLifePreference(249, "Podcasts(afición / creación)", "Podcasts(hobby / creation)", modelBuilder);
        SeedDefaultLifePreference(250, "Polo", "Polo", modelBuilder);
        SeedDefaultLifePreference(251, "Porcelana fría", "Cold porcelain", modelBuilder);
        SeedDefaultLifePreference(252, "Powerlifting", "Powerlifting", modelBuilder);
        SeedDefaultLifePreference(253, "Producción musical", "Music production", modelBuilder);
        SeedDefaultLifePreference(254, "Programación informática", "Computer programming", modelBuilder);
        SeedDefaultLifePreference(255, "Puzzles", "Puzzles", modelBuilder);
        SeedDefaultLifePreference(256, "Quad y buggy", "Quad and buggy", modelBuilder);
        SeedDefaultLifePreference(257, "Radio(afición a la)", "Radio(hobby)", modelBuilder);
        SeedDefaultLifePreference(258, "Radioafición", "Amateur radio", modelBuilder);
        SeedDefaultLifePreference(259, "Rafting", "Rafting", modelBuilder);
        SeedDefaultLifePreference(260, "Raquetas de nieve", "Snowshoeing", modelBuilder);
        SeedDefaultLifePreference(261, "Reciclaje creativo", "Creative recycling", modelBuilder);
        SeedDefaultLifePreference(262, "Redes sociales", "Social networking", modelBuilder);
        SeedDefaultLifePreference(263, "Reiki(técnica espiritual sanadora)", "Reiki(spiritual healing technique)", modelBuilder);
        SeedDefaultLifePreference(264, "Remo deportivo", "Sports rowing", modelBuilder);
        SeedDefaultLifePreference(265, "Repostería / pastelería y cupcakes", "Baking / pastry and cupcakes", modelBuilder); SeedDefaultLifePreference(266, "Repujado de metal", "Metal working", modelBuilder);
        SeedDefaultLifePreference(267, "Restauración de coches clásicos", "Classic car restoration", modelBuilder);
        SeedDefaultLifePreference(268, "Restauración de muebles", "Furniture restoration", modelBuilder);
        SeedDefaultLifePreference(269, "Leer", "Read", modelBuilder);
        SeedDefaultLifePreference(270, "Robótica", "Robotics", modelBuilder);
        SeedDefaultLifePreference(271, "Roller derby", "Roller derby", modelBuilder);
        SeedDefaultLifePreference(272, "Rugby", "Rugby", modelBuilder);
        SeedDefaultLifePreference(273, "Scalextric", "Scalextric", modelBuilder);
        SeedDefaultLifePreference(274, "Scrapbook(album de recortes)", "Scrapbooking", modelBuilder);
        SeedDefaultLifePreference(275, "Senderismo", "Hiking", modelBuilder);
        SeedDefaultLifePreference(276, "Series de tv", "TV series", modelBuilder);
        SeedDefaultLifePreference(277, "Serigrafia", "Serigraphy", modelBuilder);
        SeedDefaultLifePreference(278, "Setas(búsqueda de)", "Mushrooms(mushroom hunting)", modelBuilder);
        SeedDefaultLifePreference(279, "Skateboarding", "Skateboarding", modelBuilder);
        SeedDefaultLifePreference(280, "Slime(jugar con slime)", "Slime(playing with slime)", modelBuilder);
        SeedDefaultLifePreference(281, "Snowbike", "Snowbike", modelBuilder);
        SeedDefaultLifePreference(282, "Snowboard", "Snowboard", modelBuilder);
        SeedDefaultLifePreference(283, "Softball", "Softball", modelBuilder);
        SeedDefaultLifePreference(284, "Speedriding", "Speedriding", modelBuilder);
        SeedDefaultLifePreference(285, "Spinning", "Spinning", modelBuilder);
        SeedDefaultLifePreference(286, "Squash", "Squash", modelBuilder);
        SeedDefaultLifePreference(287, "Surf", "Surf", modelBuilder);
        SeedDefaultLifePreference(288, "Taekwondo", "Taekwondo", modelBuilder);
        SeedDefaultLifePreference(289, "Taichi", "Taichi", modelBuilder);
        SeedDefaultLifePreference(290, "Tampones o sellos de caucho", "Rubber stamps or stamps", modelBuilder);
        SeedDefaultLifePreference(291, "Tarot", "Tarot", modelBuilder);
        SeedDefaultLifePreference(292, "Tatuajes", "Tattoos", modelBuilder);
        SeedDefaultLifePreference(293, "Taxidermia", "Taxidermy", modelBuilder);
        SeedDefaultLifePreference(294, "Tejer", "Knit", modelBuilder);
        SeedDefaultLifePreference(295, "Tejo", "Shuffleboard", modelBuilder);
        SeedDefaultLifePreference(296, "Tenis", "Tennis", modelBuilder);
        SeedDefaultLifePreference(297, "Tintado / teñido de tejidos", "Dyeing / dyeing of fabrics", modelBuilder);
        SeedDefaultLifePreference(298, "Tiro con arco", "Archery", modelBuilder);
        SeedDefaultLifePreference(299, "Tiro con tirachinas", "Slingshot shooting", modelBuilder);
        SeedDefaultLifePreference(300, "Tiro deportivo con arma de fuego", "Shooting sports with a firearm", modelBuilder); SeedDefaultLifePreference(301, "Tocar un instrumento musical", "Play a musical instrument", modelBuilder);
        SeedDefaultLifePreference(302, "Toros(afición a los)", "Bullfighting(bullfighting hobby)", modelBuilder);
        SeedDefaultLifePreference(303, "Tostar café", "Roast coffee", modelBuilder);
        SeedDefaultLifePreference(304, "Transferencia de imágenes", "Image transfer", modelBuilder);
        SeedDefaultLifePreference(305, "Trenes a escala", "Scale trains", modelBuilder);
        SeedDefaultLifePreference(306, "Triatlon", "Triathlon", modelBuilder);
        SeedDefaultLifePreference(307, "Uñas(decoración)", "Nails(decoration)", modelBuilder);
        SeedDefaultLifePreference(308, "Vehículos de control remoto(rc)", "Remote control vehicles(rc)", modelBuilder);
        SeedDefaultLifePreference(309, "Vela deportiva y recreativa", "Sailing sports and recreation", modelBuilder);
        SeedDefaultLifePreference(310, "Velas(creación de velas)", "Candles(making candles)", modelBuilder);
        SeedDefaultLifePreference(311, "Viajar", "Travel", modelBuilder);
        SeedDefaultLifePreference(312, "Viajar lentamente", "Travel slowly", modelBuilder);
        SeedDefaultLifePreference(313, "Viajes de aventura", "Adventure travel", modelBuilder);
        SeedDefaultLifePreference(314, "Viajes temáticos", "Themed trips", modelBuilder);
        SeedDefaultLifePreference(315, "Video(realización de videos)", "Video(making videos)", modelBuilder);
        SeedDefaultLifePreference(316, "Videojuegos", "Video games", modelBuilder);
        SeedDefaultLifePreference(317, "Visitar monumentos", "Visit monuments", modelBuilder);
        SeedDefaultLifePreference(318, "Visitar museos y exposiciones", "Visit museums and exhibitions", modelBuilder);
        SeedDefaultLifePreference(319, "Visitas urbanas", "Urban visits", modelBuilder);
        SeedDefaultLifePreference(320, "Voleibol", "Volleyball", modelBuilder);
        SeedDefaultLifePreference(321, "Voluntariado ambiental", "Environmental volunteering", modelBuilder);
        SeedDefaultLifePreference(322, "Voluntariado animal", "Animal volunteering", modelBuilder);
        SeedDefaultLifePreference(323, "Voluntariado cultural", "Cultural volunteering", modelBuilder);
        SeedDefaultLifePreference(324, "Voluntariado en deporte", "Sports volunteering", modelBuilder);
        SeedDefaultLifePreference(325, "Voluntariado social", "Social volunteering", modelBuilder);
        SeedDefaultLifePreference(326, "Vuelo a vela", "Gliding", modelBuilder);
        SeedDefaultLifePreference(327, "Wakeboard", "Wakeboarding", modelBuilder);
        SeedDefaultLifePreference(328, "Waterpolo", "Water polo", modelBuilder);
        SeedDefaultLifePreference(329, "Windsurf", "Windsurfing", modelBuilder);
        SeedDefaultLifePreference(330, "Yoga", "Yoga", modelBuilder);
        SeedDefaultLifePreference(331, "Youtuber", "Youtuber", modelBuilder);
        SeedDefaultLifePreference(332, "Zumba", "Zumba", modelBuilder);
        SeedDefaultLifePreference(333, "Otra", "Other", modelBuilder);
        #endregion

        /* Seed Default Language Type */
        SeedDefaultLanguageLevel(1, "Ninguno", "None", modelBuilder);
        SeedDefaultLanguageLevel(2, "Español", "Spanish", modelBuilder);
        SeedDefaultLanguageLevel(3, "Inglés", "English", modelBuilder);
        SeedDefaultLanguageLevel(4, "Francés", "French", modelBuilder);
        SeedDefaultLanguageLevel(5, "Alemán", "German", modelBuilder);
        SeedDefaultLanguageLevel(6, "Mandarín", "Chinese", modelBuilder);
        SeedDefaultLanguageLevel(7, "Portugués", "Portuguese", modelBuilder);
        SeedDefaultLanguageLevel(8, "Otro", "Other", modelBuilder);


        /* Seed Default Knowledge Level */
        SeedDefaultKnowledgeLevel(1, "Ninguno", "None", modelBuilder);
        SeedDefaultKnowledgeLevel(2, "Basico", "Basic", modelBuilder);
        SeedDefaultKnowledgeLevel(3, "Intermedio", "Intermediate", modelBuilder);
        SeedDefaultKnowledgeLevel(4, "Avanzado", "Advanced", modelBuilder);


        #region SeedDefaultTechnologyName
        /* Seed Default Technology Name*/
        SeedDefaultTechnologyName(1, "Ninguno", "None", modelBuilder);
        SeedDefaultTechnologyName(2, "HTML 5", "HTML 5", modelBuilder);
        SeedDefaultTechnologyName(3, "CSS3", "CSS3", modelBuilder);
        SeedDefaultTechnologyName(4, "Javascript", "Javascript", modelBuilder);
        SeedDefaultTechnologyName(5, "Angular", "Angular", modelBuilder);
        SeedDefaultTechnologyName(6, "React_Redux", "React Redux", modelBuilder);
        SeedDefaultTechnologyName(7, "VUE", "VUE", modelBuilder);
        SeedDefaultTechnologyName(8, "Sql_Server", "Sql Server", modelBuilder);
        SeedDefaultTechnologyName(9, "Mongo_Db", "Mongo Db", modelBuilder);
        SeedDefaultTechnologyName(10, "Oracle", "Oracle", modelBuilder);
        SeedDefaultTechnologyName(11, "Mysql", "Mysql", modelBuilder);
        SeedDefaultTechnologyName(12, "IBM_Db2", "IBM Db2", modelBuilder);
        SeedDefaultTechnologyName(13, "Postgresql", "Postgresql", modelBuilder);
        SeedDefaultTechnologyName(14, "Redis", "Redis", modelBuilder);
        SeedDefaultTechnologyName(15, "Firebase", "Firebase", modelBuilder);
        SeedDefaultTechnologyName(16, ".Net", ".Net", modelBuilder);
        SeedDefaultTechnologyName(17, "Node_Js", "Node Js", modelBuilder);
        SeedDefaultTechnologyName(18, "As400", "As400", modelBuilder);
        SeedDefaultTechnologyName(19, "Java", "Java", modelBuilder);
        SeedDefaultTechnologyName(20, "PHP", "PHP", modelBuilder);
        SeedDefaultTechnologyName(21, "Python", "Python", modelBuilder);
        SeedDefaultTechnologyName(22, "Ruby", "Ruby", modelBuilder);
        SeedDefaultTechnologyName(23, "Cobol", "Cobol", modelBuilder);
        SeedDefaultTechnologyName(24, "Kotlin", "Kotlin", modelBuilder);
        SeedDefaultTechnologyName(25, "Swift", "Swift", modelBuilder);
        SeedDefaultTechnologyName(26, "Java2", "Java2", modelBuilder);
        SeedDefaultTechnologyName(27, "Xamarin", "Xamarin", modelBuilder);
        SeedDefaultTechnologyName(28, "Ionic", "Ionic", modelBuilder);
        SeedDefaultTechnologyName(29, "React Native", "React Native", modelBuilder);
        SeedDefaultTechnologyName(30, "Microsoft Azure", "Microsoft Azure", modelBuilder);
        SeedDefaultTechnologyName(31, "Google Cloud Plattform", "Google Cloud Plattform", modelBuilder);
        SeedDefaultTechnologyName(32, "IBM Bluemix", "IBM Bluemix", modelBuilder);
        SeedDefaultTechnologyName(33, "Heroku", "Heroku", modelBuilder);
        SeedDefaultTechnologyName(34, "Firebase2", "Firebase2", modelBuilder);
        SeedDefaultTechnologyName(35, "Firebase3", "Firebase3", modelBuilder);
        SeedDefaultTechnologyName(36, "Selenium", "Selenium", modelBuilder);
        SeedDefaultTechnologyName(37, "Appium", "Appium", modelBuilder);
        SeedDefaultTechnologyName(38, "Nunit", "Nunit", modelBuilder);
        SeedDefaultTechnologyName(39, "Junit", "Junit", modelBuilder);
        SeedDefaultTechnologyName(40, "Jmeter", "Jmeter", modelBuilder);
        SeedDefaultTechnologyName(41, "Testing_Funcional", "Testing Funcional", modelBuilder);
        SeedDefaultTechnologyName(42, "Azure_Devops", "Azure Devops", modelBuilder);
        SeedDefaultTechnologyName(43, "Gitlab", "Gitlab", modelBuilder);
        SeedDefaultTechnologyName(44, "Jenkins", "Jenkins", modelBuilder);
        SeedDefaultTechnologyName(45, "Git", "Git", modelBuilder);
        SeedDefaultTechnologyName(46, "TFS", "TFS", modelBuilder);
        SeedDefaultTechnologyName(47, "SVN", "SVN", modelBuilder);
        SeedDefaultTechnologyName(48, "RPA", "RPA", modelBuilder);
        SeedDefaultTechnologyName(49, "Apache", "Apache", modelBuilder);
        SeedDefaultTechnologyName(50, "IIS", "IIS", modelBuilder);
        SeedDefaultTechnologyName(51, "Tomcat", "Tomcat", modelBuilder);
        SeedDefaultTechnologyName(52, "Blockchain", "Blockchain", modelBuilder);
        SeedDefaultTechnologyName(53, "Machine_Learning", "Machine Learning", modelBuilder);
        SeedDefaultTechnologyName(54, "Wso2", "Wso2", modelBuilder);
        SeedDefaultTechnologyName(55, "Otras", "Others", modelBuilder);
        #endregion

        #region SeedDefaultSoftSkill
        /* Seed Default Soft Skills */
        SeedDefaultSoftSkill(1, "Ninguno", "None", modelBuilder);
        SeedDefaultSoftSkill(2, "Adaptación al cambio", "Adaptation to change", modelBuilder);
        SeedDefaultSoftSkill(3, "Análisis numérico", "Numerical analysis", modelBuilder);
        SeedDefaultSoftSkill(4, "Aprendizaje rápido", "Fast learning", modelBuilder);
        SeedDefaultSoftSkill(5, "Autogestión", "Self-management", modelBuilder);
        SeedDefaultSoftSkill(6, "Compromiso", "Commitment", modelBuilder);
        SeedDefaultSoftSkill(7, "Creatividad", "Creativity", modelBuilder);
        SeedDefaultSoftSkill(8, "Empatía", "Empathy", modelBuilder);
        SeedDefaultSoftSkill(9, "Escucha asertiva", "Assertive listening", modelBuilder);
        SeedDefaultSoftSkill(10, "Gestión de conflictos", "Conflict management", modelBuilder);
        SeedDefaultSoftSkill(11, "Influencia", "Influence", modelBuilder);
        SeedDefaultSoftSkill(12, "Iniciativa", "Initiative", modelBuilder);
        SeedDefaultSoftSkill(13, "Liderazgo", "Leadership", modelBuilder);
        SeedDefaultSoftSkill(14, "Motivación", "Motivation", modelBuilder);
        SeedDefaultSoftSkill(15, "Negociación", "Negotiation", modelBuilder);
        SeedDefaultSoftSkill(16, "Orientación al detalle", "Detail orientation", modelBuilder);
        SeedDefaultSoftSkill(17, "Orientación al logro", "Achievement orientation", modelBuilder);
        SeedDefaultSoftSkill(18, "Pensamiento crítico", "Critical thinking", modelBuilder);
        SeedDefaultSoftSkill(19, "Persistencia", "Persistence", modelBuilder);
        SeedDefaultSoftSkill(20, "Planeación", "Planning", modelBuilder);
        SeedDefaultSoftSkill(21, "Proactividad", "Proactivity", modelBuilder);
        SeedDefaultSoftSkill(22, "Receptividad", "Receptivity", modelBuilder);
        SeedDefaultSoftSkill(23, "Resiliencia", "Resilience", modelBuilder);
        SeedDefaultSoftSkill(24, "Resolución de problemas", "Problem resolution", modelBuilder);
        SeedDefaultSoftSkill(25, "Responsabilidad", "Responsibility", modelBuilder);
        SeedDefaultSoftSkill(26, "Servicio al cliente", "Customer Service", modelBuilder);
        SeedDefaultSoftSkill(27, "Sociabilidad", "Sociability", modelBuilder);
        SeedDefaultSoftSkill(28, "Tolerancia a la frustración", "Tolerance to frustration", modelBuilder);
        SeedDefaultSoftSkill(29, "Toma de decisiones", "Decision making", modelBuilder);
        SeedDefaultSoftSkill(30, "Trabajo en equipo", "Teamwork", modelBuilder);
        SeedDefaultSoftSkill(31, "Otro", "Other", modelBuilder);
        #endregion

        #region SeedDefaultTimeZone
        /* Seed Default Time Zone*/
        SeedDefaultTimeZone(1, "Ninguno", "None", modelBuilder);
        SeedDefaultTimeZone(2, "(GMT+00:00) Africa/Abidjan", "(GMT+00:00) Africa/Abidjan", modelBuilder);
        SeedDefaultTimeZone(3, "(GMT+00:00) Africa/Accra", "(GMT+00:00) Africa/Accra", modelBuilder);
        SeedDefaultTimeZone(4, "(GMT+03:00) Africa/Addis_Ababa", "(GMT+03:00) Africa/Addis_Ababa", modelBuilder);
        SeedDefaultTimeZone(5, "(GMT+01:00) Africa/Algiers", "(GMT+01:00) Africa/Algiers", modelBuilder);
        SeedDefaultTimeZone(6, "(GMT+03:00) Africa/Asmara", "(GMT+03:00) Africa/Asmara", modelBuilder);
        SeedDefaultTimeZone(7, "(GMT+03:00) Africa/Asmera", "(GMT+03:00) Africa/Asmera", modelBuilder);
        SeedDefaultTimeZone(8, "(GMT+00:00) Africa/Bamako", "(GMT+00:00) Africa/Bamako", modelBuilder);
        SeedDefaultTimeZone(9, "(GMT+01:00) Africa/Bangui", "(GMT+01:00) Africa/Bangui", modelBuilder);
        SeedDefaultTimeZone(10, "(GMT+00:00) Africa/Banjul", "(GMT+00:00) Africa/Banjul", modelBuilder);
        SeedDefaultTimeZone(11, "(GMT+00:00) Africa/Bissau", "(GMT+00:00) Africa/Bissau", modelBuilder);
        SeedDefaultTimeZone(12, "(GMT+02:00) Africa/Blantyre", "(GMT+02:00) Africa/Blantyre", modelBuilder);
        SeedDefaultTimeZone(13, "(GMT+01:00) Africa/Brazzaville", "(GMT+01:00) Africa/Brazzaville", modelBuilder);
        SeedDefaultTimeZone(14, "(GMT+02:00) Africa/Bujumbura", "(GMT+02:00) Africa/Bujumbura", modelBuilder);
        SeedDefaultTimeZone(15, "(GMT+02:00) Africa/Cairo", "(GMT+02:00) Africa/Cairo", modelBuilder);
        SeedDefaultTimeZone(16, "(GMT+01:00) Africa/Casablanca", "(GMT+01:00) Africa/Casablanca", modelBuilder);
        SeedDefaultTimeZone(17, "(GMT+01:00) Africa/Ceuta", "(GMT+01:00) Africa/Ceuta", modelBuilder);
        SeedDefaultTimeZone(18, "(GMT+00:00) Africa/Conakry", "(GMT+00:00) Africa/Conakry", modelBuilder);
        SeedDefaultTimeZone(19, "(GMT+00:00) Africa/Dakar", "(GMT+00:00) Africa/Dakar", modelBuilder);
        SeedDefaultTimeZone(20, "(GMT+03:00) Africa/Dar_es_Salaam", "(GMT+03:00) Africa/Dar_es_Salaam", modelBuilder);
        SeedDefaultTimeZone(21, "(GMT+03:00) Africa/Djibouti", "(GMT+03:00) Africa/Djibouti", modelBuilder);
        SeedDefaultTimeZone(22, "(GMT+01:00) Africa/Douala", "(GMT+01:00) Africa/Douala", modelBuilder);
        SeedDefaultTimeZone(23, "(GMT+01:00) Africa/El_Aaiun", "(GMT+01:00) Africa/El_Aaiun", modelBuilder);
        SeedDefaultTimeZone(24, "(GMT+00:00) Africa/Freetown", "(GMT+00:00) Africa/Freetown", modelBuilder);
        SeedDefaultTimeZone(25, "(GMT+02:00) Africa/Gaborone", "(GMT+02:00) Africa/Gaborone", modelBuilder);
        SeedDefaultTimeZone(26, "(GMT+02:00) Africa/Harare", "(GMT+02:00) Africa/Harare", modelBuilder);
        SeedDefaultTimeZone(27, "(GMT+02:00) Africa/Johannesburg", "(GMT+02:00) Africa/Johannesburg", modelBuilder);
        SeedDefaultTimeZone(28, "(GMT+02:00) Africa/Juba", "(GMT+02:00) Africa/Juba", modelBuilder);
        SeedDefaultTimeZone(29, "(GMT+03:00) Africa/Kampala", "(GMT+03:00) Africa/Kampala", modelBuilder);
        SeedDefaultTimeZone(30, "(GMT+02:00) Africa/Khartoum", "(GMT+02:00) Africa/Khartoum", modelBuilder);
        SeedDefaultTimeZone(31, "(GMT+02:00) Africa/Kigali", "(GMT+02:00) Africa/Kigali", modelBuilder);
        SeedDefaultTimeZone(32, "(GMT+01:00) Africa/Kinshasa", "(GMT+01:00) Africa/Kinshasa", modelBuilder);
        SeedDefaultTimeZone(33, "(GMT+01:00) Africa/Lagos", "(GMT+01:00) Africa/Lagos", modelBuilder);
        SeedDefaultTimeZone(34, "(GMT+01:00) Africa/Libreville", "(GMT+01:00) Africa/Libreville", modelBuilder);
        SeedDefaultTimeZone(35, "(GMT+00:00) Africa/Lome", "(GMT+00:00) Africa/Lome", modelBuilder);
        SeedDefaultTimeZone(36, "(GMT+01:00) Africa/Luanda", "(GMT+01:00) Africa/Luanda", modelBuilder);
        SeedDefaultTimeZone(37, "(GMT+02:00) Africa/Lubumbashi", "(GMT+02:00) Africa/Lubumbashi", modelBuilder);
        SeedDefaultTimeZone(38, "(GMT+02:00) Africa/Lusaka", "(GMT+02:00) Africa/Lusaka", modelBuilder);
        SeedDefaultTimeZone(39, "(GMT+01:00) Africa/Malabo", "(GMT+01:00) Africa/Malabo", modelBuilder);
        SeedDefaultTimeZone(40, "(GMT+02:00) Africa/Maputo", "(GMT+02:00) Africa/Maputo", modelBuilder);
        SeedDefaultTimeZone(41, "(GMT+02:00) Africa/Maseru", "(GMT+02:00) Africa/Maseru", modelBuilder);
        SeedDefaultTimeZone(42, "(GMT+02:00) Africa/Mbabane", "(GMT+02:00) Africa/Mbabane", modelBuilder);
        SeedDefaultTimeZone(43, "(GMT+03:00) Africa/Mogadishu", "(GMT+03:00) Africa/Mogadishu", modelBuilder);
        SeedDefaultTimeZone(44, "(GMT+00:00) Africa/Monrovia", "(GMT+00:00) Africa/Monrovia", modelBuilder);
        SeedDefaultTimeZone(45, "(GMT+03:00) Africa/Nairobi", "(GMT+03:00) Africa/Nairobi", modelBuilder);
        SeedDefaultTimeZone(46, "(GMT+01:00) Africa/Ndjamena", "(GMT+01:00) Africa/Ndjamena", modelBuilder);
        SeedDefaultTimeZone(47, "(GMT+01:00) Africa/Niamey", "(GMT+01:00) Africa/Niamey", modelBuilder);
        SeedDefaultTimeZone(48, "(GMT+00:00) Africa/Nouakchott", "(GMT+00:00) Africa/Nouakchott", modelBuilder);
        SeedDefaultTimeZone(49, "(GMT+00:00) Africa/Ouagadougou", "(GMT+00:00) Africa/Ouagadougou", modelBuilder);
        SeedDefaultTimeZone(50, "(GMT+01:00) Africa/Porto-Novo", "(GMT+01:00) Africa/Porto-Novo", modelBuilder);
        SeedDefaultTimeZone(51, "(GMT+00:00) Africa/Sao_Tome", "(GMT+00:00) Africa/Sao_Tome", modelBuilder);
        SeedDefaultTimeZone(52, "(GMT+00:00) Africa/Timbuktu", "(GMT+00:00) Africa/Timbuktu", modelBuilder);
        SeedDefaultTimeZone(53, "(GMT+02:00) Africa/Tripoli", "(GMT+02:00) Africa/Tripoli", modelBuilder);
        SeedDefaultTimeZone(54, "(GMT+01:00) Africa/Tunis", "(GMT+01:00) Africa/Tunis", modelBuilder);
        SeedDefaultTimeZone(55, "(GMT+02:00) Africa/Windhoek", "(GMT+02:00) Africa/Windhoek", modelBuilder);
        SeedDefaultTimeZone(56, "(GMT-10:00) America/Adak", "(GMT-10:00) America/Adak", modelBuilder);
        SeedDefaultTimeZone(57, "(GMT-09:00) America/Anchorage", "(GMT-09:00) America/Anchorage", modelBuilder);
        SeedDefaultTimeZone(58, "(GMT-04:00) America/Anguilla", "(GMT-04:00) America/Anguilla", modelBuilder);
        SeedDefaultTimeZone(59, "(GMT-04:00) America/Antigua", "(GMT-04:00) America/Antigua", modelBuilder);
        SeedDefaultTimeZone(60, "(GMT-03:00) America/Araguaina", "(GMT-03:00) America/Araguaina", modelBuilder);
        SeedDefaultTimeZone(61, "(GMT-03:00) America/Argentina/Buenos_Aires", "(GMT-03:00) America/Argentina/Buenos_Aires", modelBuilder);
        SeedDefaultTimeZone(62, "(GMT-03:00) America/Argentina/Catamarca", "(GMT-03:00) America/Argentina/Catamarca", modelBuilder);
        SeedDefaultTimeZone(63, "(GMT-03:00) America/Argentina/ComodRivadavia", "(GMT-03:00) America/Argentina/ComodRivadavia", modelBuilder); SeedDefaultTimeZone(64, "(GMT-03:00) America/Argentina/Cordoba", "(GMT-03:00) America/Argentina/Cordoba", modelBuilder);
        SeedDefaultTimeZone(65, "(GMT-03:00) America/Argentina/Jujuy", "(GMT-03:00) America/Argentina/Jujuy", modelBuilder);
        SeedDefaultTimeZone(66, "(GMT-03:00) America/Argentina/La_Rioja", "(GMT-03:00) America/Argentina/La_Rioja", modelBuilder);
        SeedDefaultTimeZone(67, "(GMT-03:00) America/Argentina/Mendoza", "(GMT-03:00) America/Argentina/Mendoza", modelBuilder);
        SeedDefaultTimeZone(68, "(GMT-03:00) America/Argentina/Rio_Gallegos", "(GMT-03:00) America/Argentina/Rio_Gallegos", modelBuilder);
        SeedDefaultTimeZone(69, "(GMT-03:00) America/Argentina/Salta", "(GMT-03:00) America/Argentina/Salta", modelBuilder);
        SeedDefaultTimeZone(70, "(GMT-03:00) America/Argentina/San_Juan", "(GMT-03:00) America/Argentina/San_Juan", modelBuilder);
        SeedDefaultTimeZone(71, "(GMT-03:00) America/Argentina/San_Luis", "(GMT-03:00) America/Argentina/San_Luis", modelBuilder);
        SeedDefaultTimeZone(72, "(GMT-03:00) America/Argentina/Tucuman", "(GMT-03:00) America/Argentina/Tucuman", modelBuilder);
        SeedDefaultTimeZone(73, "(GMT-03:00) America/Argentina/Ushuaia", "(GMT-03:00) America/Argentina/Ushuaia", modelBuilder);
        SeedDefaultTimeZone(74, "(GMT-04:00) America/Aruba", "(GMT-04:00) America/Aruba", modelBuilder);
        SeedDefaultTimeZone(75, "(GMT-03:00) America/Asuncion", "(GMT-03:00) America/Asuncion", modelBuilder);
        SeedDefaultTimeZone(76, "(GMT-05:00) America/Atikokan", "(GMT-05:00) America/Atikokan", modelBuilder);
        SeedDefaultTimeZone(77, "(GMT-10:00) America/Atka", "(GMT-10:00) America/Atka", modelBuilder);
        SeedDefaultTimeZone(78, "(GMT-03:00) America/Bahia", "(GMT-03:00) America/Bahia", modelBuilder);
        SeedDefaultTimeZone(79, "(GMT-06:00) America/Bahia_Banderas", "(GMT-06:00) America/Bahia_Banderas", modelBuilder);
        SeedDefaultTimeZone(80, "(GMT-04:00) America/Barbados", "(GMT-04:00) America/Barbados", modelBuilder);
        SeedDefaultTimeZone(81, "(GMT-03:00) America/Belem", "(GMT-03:00) America/Belem", modelBuilder);
        SeedDefaultTimeZone(82, "(GMT-06:00) America/Belize", "(GMT-06:00) America/Belize", modelBuilder);
        SeedDefaultTimeZone(83, "(GMT-04:00) America/Blanc-Sablon", "(GMT-04:00) America/Blanc-Sablon", modelBuilder);
        SeedDefaultTimeZone(84, "(GMT-04:00) America/Boa_Vista", "(GMT-04:00) America/Boa_Vista", modelBuilder);
        SeedDefaultTimeZone(85, "(GMT-05:00) America/Bogota", "(GMT-05:00) America/Bogota", modelBuilder);
        SeedDefaultTimeZone(86, "(GMT-07:00) America/Boise", "(GMT-07:00) America/Boise", modelBuilder);
        SeedDefaultTimeZone(87, "(GMT-03:00) America/Buenos_Aires", "(GMT-03:00) America/Buenos_Aires", modelBuilder);
        SeedDefaultTimeZone(88, "(GMT-07:00) America/Cambridge_Bay", "(GMT-07:00) America/Cambridge_Bay", modelBuilder);
        SeedDefaultTimeZone(89, "(GMT-04:00) America/Campo_Grande", "(GMT-04:00) America/Campo_Grande", modelBuilder);
        SeedDefaultTimeZone(90, "(GMT-05:00) America/Cancun", "(GMT-05:00) America/Cancun", modelBuilder);
        SeedDefaultTimeZone(91, "(GMT-04:00) America/Caracas", "(GMT-04:00) America/Caracas", modelBuilder);
        SeedDefaultTimeZone(92, "(GMT-03:00) America/Catamarca", "(GMT-03:00) America/Catamarca", modelBuilder);
        SeedDefaultTimeZone(93, "(GMT-03:00) America/Cayenne", "(GMT-03:00) America/Cayenne", modelBuilder);
        SeedDefaultTimeZone(94, "(GMT-05:00) America/Cayman", "(GMT-05:00) America/Cayman", modelBuilder);
        SeedDefaultTimeZone(95, "(GMT-06:00) America/Chicago", "(GMT-06:00) America/Chicago", modelBuilder);
        SeedDefaultTimeZone(96, "(GMT-06:00) America/Chihuahua", "(GMT-06:00) America/Chihuahua", modelBuilder);
        SeedDefaultTimeZone(97, "(GMT-05:00) America/Coral_Harbour", "(GMT-05:00) America/Coral_Harbour", modelBuilder);
        SeedDefaultTimeZone(98, "(GMT-03:00) America/Cordoba", "(GMT-03:00) America/Cordoba", modelBuilder);
        SeedDefaultTimeZone(99, "(GMT-06:00) America/Costa_Rica", "(GMT-06:00) America/Costa_Rica", modelBuilder);
        SeedDefaultTimeZone(100, "(GMT-07:00) America/Creston", "(GMT-07:00) America/Creston", modelBuilder);
        SeedDefaultTimeZone(101, "(GMT-04:00) America/Cuiaba", "(GMT-04:00) America/Cuiaba", modelBuilder);
        SeedDefaultTimeZone(102, "(GMT-04:00) America/Curacao", "(GMT-04:00) America/Curacao", modelBuilder);
        SeedDefaultTimeZone(103, "(GMT+00:00) America/Danmarkshavn", "(GMT+00:00) America/Danmarkshavn", modelBuilder);
        SeedDefaultTimeZone(104, "(GMT-07:00) America/Dawson", "(GMT-07:00) America/Dawson", modelBuilder);
        SeedDefaultTimeZone(105, "(GMT-07:00) America/Dawson_Creek", "(GMT-07:00) America/Dawson_Creek", modelBuilder);
        SeedDefaultTimeZone(106, "(GMT-07:00) America/Denver", "(GMT-07:00) America/Denver", modelBuilder);
        SeedDefaultTimeZone(107, "(GMT-05:00) America/Detroit", "(GMT-05:00) America/Detroit", modelBuilder);
        SeedDefaultTimeZone(108, "(GMT-04:00) America/Dominica", "(GMT-04:00) America/Dominica", modelBuilder);
        SeedDefaultTimeZone(109, "(GMT-07:00) America/Edmonton", "(GMT-07:00) America/Edmonton", modelBuilder);
        SeedDefaultTimeZone(110, "(GMT-05:00) America/Eirunepe", "(GMT-05:00) America/Eirunepe", modelBuilder);
        SeedDefaultTimeZone(111, "(GMT-06:00) America/El_Salvador", "(GMT-06:00) America/El_Salvador", modelBuilder);
        SeedDefaultTimeZone(112, "(GMT-08:00) America/Ensenada", "(GMT-08:00) America/Ensenada", modelBuilder);
        SeedDefaultTimeZone(113, "(GMT-07:00) America/Fort_Nelson", "(GMT-07:00) America/Fort_Nelson", modelBuilder);
        SeedDefaultTimeZone(114, "(GMT-05:00) America/Fort_Wayne", "(GMT-05:00) America/Fort_Wayne", modelBuilder);
        SeedDefaultTimeZone(115, "(GMT-03:00) America/Fortaleza", "(GMT-03:00) America/Fortaleza", modelBuilder);
        SeedDefaultTimeZone(116, "(GMT-04:00) America/Glace_Bay", "(GMT-04:00) America/Glace_Bay", modelBuilder);
        SeedDefaultTimeZone(117, "(GMT-03:00) America/Godthab", "(GMT-03:00) America/Godthab", modelBuilder);
        SeedDefaultTimeZone(118, "(GMT-04:00) America/Goose_Bay", "(GMT-04:00) America/Goose_Bay", modelBuilder);
        SeedDefaultTimeZone(119, "(GMT-05:00) America/Grand_Turk", "(GMT-05:00) America/Grand_Turk", modelBuilder);
        SeedDefaultTimeZone(120, "(GMT-04:00) America/Grenada", "(GMT-04:00) America/Grenada", modelBuilder);
        SeedDefaultTimeZone(121, "(GMT-04:00) America/Guadeloupe", "(GMT-04:00) America/Guadeloupe", modelBuilder);
        SeedDefaultTimeZone(122, "(GMT-06:00) America/Guatemala", "(GMT-06:00) America/Guatemala", modelBuilder);
        SeedDefaultTimeZone(123, "(GMT-05:00) America/Guayaquil", "(GMT-05:00) America/Guayaquil", modelBuilder);
        SeedDefaultTimeZone(124, "(GMT-04:00) America/Guyana", "(GMT-04:00) America/Guyana", modelBuilder);
        SeedDefaultTimeZone(125, "(GMT-04:00) America/Halifax", "(GMT-04:00) America/Halifax", modelBuilder);
        SeedDefaultTimeZone(126, "(GMT-05:00) America/Havana", "(GMT-05:00) America/Havana", modelBuilder);
        SeedDefaultTimeZone(127, "(GMT-07:00) America/Hermosillo", "(GMT-07:00) America/Hermosillo", modelBuilder);
        SeedDefaultTimeZone(128, "(GMT-05:00) America/Indiana/Indianapolis", "(GMT-05:00) America/Indiana/Indianapolis", modelBuilder);
        SeedDefaultTimeZone(129, "(GMT-06:00) America/Indiana/Knox", "(GMT-06:00) America/Indiana/Knox", modelBuilder);
        SeedDefaultTimeZone(130, "(GMT-05:00) America/Indiana/Marengo", "(GMT-05:00) America/Indiana/Marengo", modelBuilder);
        SeedDefaultTimeZone(131, "(GMT-05:00) America/Indiana/Petersburg", "(GMT-05:00) America/Indiana/Petersburg", modelBuilder);
        SeedDefaultTimeZone(132, "(GMT-06:00) America/Indiana/Tell_City", "(GMT-06:00) America/Indiana/Tell_City", modelBuilder);
        SeedDefaultTimeZone(133, "(GMT-05:00) America/Indiana/Vevay", "(GMT-05:00) America/Indiana/Vevay", modelBuilder);
        SeedDefaultTimeZone(134, "(GMT-05:00) America/Indiana/Vincennes", "(GMT-05:00) America/Indiana/Vincennes", modelBuilder);
        SeedDefaultTimeZone(135, "(GMT-05:00) America/Indiana/Winamac", "(GMT-05:00) America/Indiana/Winamac", modelBuilder);
        SeedDefaultTimeZone(136, "(GMT-05:00) America/Indianapolis", "(GMT-05:00) America/Indianapolis", modelBuilder);
        SeedDefaultTimeZone(137, "(GMT-07:00) America/Inuvik", "(GMT-07:00) America/Inuvik", modelBuilder);
        SeedDefaultTimeZone(138, "(GMT-05:00) America/Iqaluit", "(GMT-05:00) America/Iqaluit", modelBuilder);
        SeedDefaultTimeZone(139, "(GMT-05:00) America/Jamaica", "(GMT-05:00) America/Jamaica", modelBuilder);
        SeedDefaultTimeZone(140, "(GMT-03:00) America/Jujuy", "(GMT-03:00) America/Jujuy", modelBuilder);
        SeedDefaultTimeZone(141, "(GMT-09:00) America/Juneau", "(GMT-09:00) America/Juneau", modelBuilder);
        SeedDefaultTimeZone(142, "(GMT-05:00) America/Kentucky/Louisville", "(GMT-05:00) America/Kentucky/Louisville", modelBuilder);
        SeedDefaultTimeZone(143, "(GMT-05:00) America/Kentucky/Monticello", "(GMT-05:00) America/Kentucky/Monticello", modelBuilder);
        SeedDefaultTimeZone(144, "(GMT-06:00) America/Knox_IN", "(GMT-06:00) America/Knox_IN", modelBuilder);
        SeedDefaultTimeZone(145, "(GMT-04:00) America/Kralendijk", "(GMT-04:00) America/Kralendijk", modelBuilder);
        SeedDefaultTimeZone(146, "(GMT-04:00) America/La_Paz", "(GMT-04:00) America/La_Paz", modelBuilder);
        SeedDefaultTimeZone(147, "(GMT-05:00) America/Lima", "(GMT-05:00) America/Lima", modelBuilder);
        SeedDefaultTimeZone(148, "(GMT-08:00) America/Los_Angeles", "(GMT-08:00) America/Los_Angeles", modelBuilder);
        SeedDefaultTimeZone(149, "(GMT-05:00) America/Louisville", "(GMT-05:00) America/Louisville", modelBuilder);
        SeedDefaultTimeZone(150, "(GMT-04:00) America/Lower_Princes", "(GMT-04:00) America/Lower_Princes", modelBuilder);
        SeedDefaultTimeZone(151, "(GMT-03:00) America/Maceio", "(GMT-03:00) America/Maceio", modelBuilder);
        SeedDefaultTimeZone(152, "(GMT-06:00) America/Managua", "(GMT-06:00) America/Managua", modelBuilder);
        SeedDefaultTimeZone(153, "(GMT-04:00) America/Manaus", "(GMT-04:00) America/Manaus", modelBuilder);
        SeedDefaultTimeZone(154, "(GMT-04:00) America/Marigot", "(GMT-04:00) America/Marigot", modelBuilder);
        SeedDefaultTimeZone(155, "(GMT-04:00) America/Martinique", "(GMT-04:00) America/Martinique", modelBuilder);
        SeedDefaultTimeZone(156, "(GMT-06:00) America/Matamoros", "(GMT-06:00) America/Matamoros", modelBuilder);
        SeedDefaultTimeZone(157, "(GMT-07:00) America/Mazatlan", "(GMT-07:00) America/Mazatlan", modelBuilder);
        SeedDefaultTimeZone(158, "(GMT-03:00) America/Mendoza", "(GMT-03:00) America/Mendoza", modelBuilder);
        SeedDefaultTimeZone(159, "(GMT-06:00) America/Menominee", "(GMT-06:00) America/Menominee", modelBuilder);
        SeedDefaultTimeZone(160, "(GMT-06:00) America/Merida", "(GMT-06:00) America/Merida", modelBuilder);
        SeedDefaultTimeZone(161, "(GMT-09:00) America/Metlakatla", "(GMT-09:00) America/Metlakatla", modelBuilder);
        SeedDefaultTimeZone(162, "(GMT-06:00) America/Mexico_City", "(GMT-06:00) America/Mexico_City", modelBuilder);
        SeedDefaultTimeZone(163, "(GMT-03:00) America/Miquelon", "(GMT-03:00) America/Miquelon", modelBuilder);
        SeedDefaultTimeZone(164, "(GMT-04:00) America/Moncton", "(GMT-04:00) America/Moncton", modelBuilder);
        SeedDefaultTimeZone(165, "(GMT-06:00) America/Monterrey", "(GMT-06:00) America/Monterrey", modelBuilder);
        SeedDefaultTimeZone(166, "(GMT-03:00) America/Montevideo", "(GMT-03:00) America/Montevideo", modelBuilder);
        SeedDefaultTimeZone(167, "(GMT-05:00) America/Montreal", "(GMT-05:00) America/Montreal", modelBuilder);
        SeedDefaultTimeZone(168, "(GMT-04:00) America/Montserrat", "(GMT-04:00) America/Montserrat", modelBuilder);
        SeedDefaultTimeZone(169, "(GMT-05:00) America/Nassau", "(GMT-05:00) America/Nassau", modelBuilder);
        SeedDefaultTimeZone(170, "(GMT-05:00) America/New_York", "(GMT-05:00) America/New_York", modelBuilder);
        SeedDefaultTimeZone(171, "(GMT-05:00) America/Nipigon", "(GMT-05:00) America/Nipigon", modelBuilder);
        SeedDefaultTimeZone(172, "(GMT-09:00) America/Nome", "(GMT-09:00) America/Nome", modelBuilder);
        SeedDefaultTimeZone(173, "(GMT-02:00) America/Noronha", "(GMT-02:00) America/Noronha", modelBuilder);
        SeedDefaultTimeZone(174, "(GMT-06:00) America/North_Dakota/Beulah", "(GMT-06:00) America/North_Dakota/Beulah", modelBuilder);
        SeedDefaultTimeZone(175, "(GMT-06:00) America/North_Dakota/Center", "(GMT-06:00) America/North_Dakota/Center", modelBuilder);
        SeedDefaultTimeZone(176, "(GMT-06:00) America/North_Dakota/New_Salem", "(GMT-06:00) America/North_Dakota/New_Salem", modelBuilder);
        SeedDefaultTimeZone(177, "(GMT-03:00) America/Nuuk", "(GMT-03:00) America/Nuuk", modelBuilder);
        SeedDefaultTimeZone(178, "(GMT-06:00) America/Ojinaga", "(GMT-06:00) America/Ojinaga", modelBuilder);
        SeedDefaultTimeZone(179, "(GMT-05:00) America/Panama", "(GMT-05:00) America/Panama", modelBuilder);
        SeedDefaultTimeZone(180, "(GMT-05:00) America/Pangnirtung", "(GMT-05:00) America/Pangnirtung", modelBuilder);
        SeedDefaultTimeZone(181, "(GMT-03:00) America/Paramaribo", "(GMT-03:00) America/Paramaribo", modelBuilder);
        SeedDefaultTimeZone(182, "(GMT-07:00) America/Phoenix", "(GMT-07:00) America/Phoenix", modelBuilder);
        SeedDefaultTimeZone(183, "(GMT-05:00) America/Port-au-Prince", "(GMT-05:00) America/Port-au-Prince", modelBuilder);
        SeedDefaultTimeZone(184, "(GMT-04:00) America/Port_of_Spain", "(GMT-04:00) America/Port_of_Spain", modelBuilder);
        SeedDefaultTimeZone(185, "(GMT-05:00) America/Porto_Acre", "(GMT-05:00) America/Porto_Acre", modelBuilder);
        SeedDefaultTimeZone(186, "(GMT-04:00) America/Porto_Velho", "(GMT-04:00) America/Porto_Velho", modelBuilder);
        SeedDefaultTimeZone(187, "(GMT-04:00) America/Puerto_Rico", "(GMT-04:00) America/Puerto_Rico", modelBuilder);
        SeedDefaultTimeZone(188, "(GMT-03:00) America/Punta_Arenas", "(GMT-03:00) America/Punta_Arenas", modelBuilder);
        SeedDefaultTimeZone(189, "(GMT-06:00) America/Rainy_River", "(GMT-06:00) America/Rainy_River", modelBuilder);
        SeedDefaultTimeZone(190, "(GMT-06:00) America/Rankin_Inlet", "(GMT-06:00) America/Rankin_Inlet", modelBuilder);
        SeedDefaultTimeZone(191, "(GMT-03:00) America/Recife", "(GMT-03:00) America/Recife", modelBuilder);
        SeedDefaultTimeZone(192, "(GMT-06:00) America/Regina", "(GMT-06:00) America/Regina", modelBuilder);
        SeedDefaultTimeZone(193, "(GMT-06:00) America/Resolute", "(GMT-06:00) America/Resolute", modelBuilder);
        SeedDefaultTimeZone(194, "(GMT-05:00) America/Rio_Branco", "(GMT-05:00) America/Rio_Branco", modelBuilder);
        SeedDefaultTimeZone(195, "(GMT-03:00) America/Rosario", "(GMT-03:00) America/Rosario", modelBuilder);
        SeedDefaultTimeZone(196, "(GMT-08:00) America/Santa_Isabel", "(GMT-08:00) America/Santa_Isabel", modelBuilder);
        SeedDefaultTimeZone(197, "(GMT-03:00) America/Santarem", "(GMT-03:00) America/Santarem", modelBuilder);
        SeedDefaultTimeZone(198, "(GMT-03:00) America/Santiago", "(GMT-03:00) America/Santiago", modelBuilder);
        SeedDefaultTimeZone(199, "(GMT-04:00) America/Santo_Domingo", "(GMT-04:00) America/Santo_Domingo", modelBuilder);
        SeedDefaultTimeZone(200, "(GMT-03:00) America/Sao_Paulo", "(GMT-03:00) America/Sao_Paulo", modelBuilder);
        SeedDefaultTimeZone(201, "(GMT-01:00) America/Scoresbysund", "(GMT-01:00) America/Scoresbysund", modelBuilder);
        SeedDefaultTimeZone(202, "(GMT-07:00) America/Shiprock", "(GMT-07:00) America/Shiprock", modelBuilder);
        SeedDefaultTimeZone(203, "(GMT-09:00) America/Sitka", "(GMT-09:00) America/Sitka", modelBuilder);
        SeedDefaultTimeZone(204, "(GMT-04:00) America/St_Barthelemy", "(GMT-04:00) America/St_Barthelemy", modelBuilder);
        SeedDefaultTimeZone(205, "(GMT-03:30) America/St_Johns", "(GMT-03:30) America/St_Johns", modelBuilder);
        SeedDefaultTimeZone(206, "(GMT-04:00) America/St_Kitts", "(GMT-04:00) America/St_Kitts", modelBuilder);
        SeedDefaultTimeZone(207, "(GMT-04:00) America/St_Lucia", "(GMT-04:00) America/St_Lucia", modelBuilder);
        SeedDefaultTimeZone(208, "(GMT-04:00) America/St_Thomas", "(GMT-04:00) America/St_Thomas", modelBuilder);
        SeedDefaultTimeZone(209, "(GMT-04:00) America/St_Vincent", "(GMT-04:00) America/St_Vincent", modelBuilder);
        SeedDefaultTimeZone(210, "(GMT-06:00) America/Swift_Current", "(GMT-06:00) America/Swift_Current", modelBuilder);
        SeedDefaultTimeZone(211, "(GMT-06:00) America/Tegucigalpa", "(GMT-06:00) America/Tegucigalpa", modelBuilder);
        SeedDefaultTimeZone(212, "(GMT-04:00) America/Thule", "(GMT-04:00) America/Thule", modelBuilder);
        SeedDefaultTimeZone(213, "(GMT-05:00) America/Thunder_Bay", "(GMT-05:00) America/Thunder_Bay", modelBuilder);
        SeedDefaultTimeZone(214, "(GMT-08:00) America/Tijuana", "(GMT-08:00) America/Tijuana", modelBuilder);
        SeedDefaultTimeZone(215, "(GMT-05:00) America/Toronto", "(GMT-05:00) America/Toronto", modelBuilder);
        SeedDefaultTimeZone(216, "(GMT-04:00) America/Tortola", "(GMT-04:00) America/Tortola", modelBuilder);
        SeedDefaultTimeZone(217, "(GMT-08:00) America/Vancouver", "(GMT-08:00) America/Vancouver", modelBuilder);
        SeedDefaultTimeZone(218, "(GMT-04:00) America/Virgin", "(GMT-04:00) America/Virgin", modelBuilder);
        SeedDefaultTimeZone(219, "(GMT-07:00) America/Whitehorse", "(GMT-07:00) America/Whitehorse", modelBuilder);
        SeedDefaultTimeZone(220, "(GMT-06:00) America/Winnipeg", "(GMT-06:00) America/Winnipeg", modelBuilder);
        SeedDefaultTimeZone(221, "(GMT-09:00) America/Yakutat", "(GMT-09:00) America/Yakutat", modelBuilder);
        SeedDefaultTimeZone(222, "(GMT-07:00) America/Yellowknife", "(GMT-07:00) America/Yellowknife", modelBuilder);
        SeedDefaultTimeZone(223, "(GMT+11:00) Antarctica/Casey", "(GMT+11:00) Antarctica/Casey", modelBuilder);
        SeedDefaultTimeZone(224, "(GMT+07:00) Antarctica/Davis", "(GMT+07:00) Antarctica/Davis", modelBuilder);
        SeedDefaultTimeZone(225, "(GMT+10:00) Antarctica/DumontDUrville", "(GMT+10:00) Antarctica/DumontDUrville", modelBuilder);
        SeedDefaultTimeZone(226, "(GMT+11:00) Antarctica/Macquarie", "(GMT+11:00) Antarctica/Macquarie", modelBuilder);
        SeedDefaultTimeZone(227, "(GMT+05:00) Antarctica/Mawson", "(GMT+05:00) Antarctica/Mawson", modelBuilder);
        SeedDefaultTimeZone(228, "(GMT+13:00) Antarctica/McMurdo", "(GMT+13:00) Antarctica/McMurdo", modelBuilder);
        SeedDefaultTimeZone(229, "(GMT-03:00) Antarctica/Palmer", "(GMT-03:00) Antarctica/Palmer", modelBuilder);
        SeedDefaultTimeZone(230, "(GMT-03:00) Antarctica/Rothera", "(GMT-03:00) Antarctica/Rothera", modelBuilder);
        SeedDefaultTimeZone(231, "(GMT+13:00) Antarctica/South_Pole", "(GMT+13:00) Antarctica/South_Pole", modelBuilder);
        SeedDefaultTimeZone(232, "(GMT+03:00) Antarctica/Syowa", "(GMT+03:00) Antarctica/Syowa", modelBuilder);
        SeedDefaultTimeZone(233, "(GMT+00:00) Antarctica/Troll", "(GMT+00:00) Antarctica/Troll", modelBuilder);
        SeedDefaultTimeZone(234, "(GMT+06:00) Antarctica/Vostok", "(GMT+06:00) Antarctica/Vostok", modelBuilder);
        SeedDefaultTimeZone(235, "(GMT+01:00) Arctic/Longyearbyen", "(GMT+01:00) Arctic/Longyearbyen", modelBuilder);
        SeedDefaultTimeZone(236, "(GMT+03:00) Asia/Aden", "(GMT+03:00) Asia/Aden", modelBuilder);
        SeedDefaultTimeZone(237, "(GMT+06:00) Asia/Almaty", "(GMT+06:00) Asia/Almaty", modelBuilder);
        SeedDefaultTimeZone(238, "(GMT+03:00) Asia/Amman", "(GMT+03:00) Asia/Amman", modelBuilder);
        SeedDefaultTimeZone(239, "(GMT+12:00) Asia/Anadyr", "(GMT+12:00) Asia/Anadyr", modelBuilder);
        SeedDefaultTimeZone(240, "(GMT+05:00) Asia/Aqtau", "(GMT+05:00) Asia/Aqtau", modelBuilder);
        SeedDefaultTimeZone(241, "(GMT+05:00) Asia/Aqtobe", "(GMT+05:00) Asia/Aqtobe", modelBuilder);
        SeedDefaultTimeZone(242, "(GMT+05:00) Asia/Ashgabat", "(GMT+05:00) Asia/Ashgabat", modelBuilder);
        SeedDefaultTimeZone(243, "(GMT+05:00) Asia/Ashkhabad", "(GMT+05:00) Asia/Ashkhabad", modelBuilder);
        SeedDefaultTimeZone(244, "(GMT+05:00) Asia/Atyrau", "(GMT+05:00) Asia/Atyrau", modelBuilder);
        SeedDefaultTimeZone(245, "(GMT+03:00) Asia/Baghdad", "(GMT+03:00) Asia/Baghdad", modelBuilder);
        SeedDefaultTimeZone(246, "(GMT+03:00) Asia/Bahrain", "(GMT+03:00) Asia/Bahrain", modelBuilder);
        SeedDefaultTimeZone(247, "(GMT+04:00) Asia/Baku", "(GMT+04:00) Asia/Baku", modelBuilder);
        SeedDefaultTimeZone(248, "(GMT+07:00) Asia/Bangkok", "(GMT+07:00) Asia/Bangkok", modelBuilder);
        SeedDefaultTimeZone(249, "(GMT+07:00) Asia/Barnaul", "(GMT+07:00) Asia/Barnaul", modelBuilder);
        SeedDefaultTimeZone(250, "(GMT+02:00) Asia/Beirut", "(GMT+02:00) Asia/Beirut", modelBuilder);
        SeedDefaultTimeZone(251, "(GMT+06:00) Asia/Bishkek", "(GMT+06:00) Asia/Bishkek", modelBuilder);
        SeedDefaultTimeZone(252, "(GMT+08:00) Asia/Brunei", "(GMT+08:00) Asia/Brunei", modelBuilder);
        SeedDefaultTimeZone(253, "(GMT+05:30) Asia/Calcutta", "(GMT+05:30) Asia/Calcutta", modelBuilder);
        SeedDefaultTimeZone(254, "(GMT+09:00) Asia/Chita", "(GMT+09:00) Asia/Chita", modelBuilder);
        SeedDefaultTimeZone(255, "(GMT+08:00) Asia/Choibalsan", "(GMT+08:00) Asia/Choibalsan", modelBuilder);
        SeedDefaultTimeZone(256, "(GMT+08:00) Asia/Chongqing", "(GMT+08:00) Asia/Chongqing", modelBuilder);
        SeedDefaultTimeZone(257, "(GMT+08:00) Asia/Chungking", "(GMT+08:00) Asia/Chungking", modelBuilder);
        SeedDefaultTimeZone(258, "(GMT+05:30) Asia/Colombo", "(GMT+05:30) Asia/Colombo", modelBuilder);
        SeedDefaultTimeZone(259, "(GMT+06:00) Asia/Dacca", "(GMT+06:00) Asia/Dacca", modelBuilder);
        SeedDefaultTimeZone(260, "(GMT+03:00) Asia/Damascus", "(GMT+03:00) Asia/Damascus", modelBuilder);
        SeedDefaultTimeZone(261, "(GMT+06:00) Asia/Dhaka", "(GMT+06:00) Asia/Dhaka", modelBuilder);
        SeedDefaultTimeZone(262, "(GMT+09:00) Asia/Dili", "(GMT+09:00) Asia/Dili", modelBuilder);
        SeedDefaultTimeZone(263, "(GMT+04:00) Asia/Dubai", "(GMT+04:00) Asia/Dubai", modelBuilder);
        SeedDefaultTimeZone(264, "(GMT+05:00) Asia/Dushanbe", "(GMT+05:00) Asia/Dushanbe", modelBuilder);
        SeedDefaultTimeZone(265, "(GMT+02:00) Asia/Famagusta", "(GMT+02:00) Asia/Famagusta", modelBuilder);
        SeedDefaultTimeZone(266, "(GMT+02:00) Asia/Gaza", "(GMT+02:00) Asia/Gaza", modelBuilder);
        SeedDefaultTimeZone(267, "(GMT+08:00) Asia/Harbin", "(GMT+08:00) Asia/Harbin", modelBuilder);
        SeedDefaultTimeZone(268, "(GMT+02:00) Asia/Hebron", "(GMT+02:00) Asia/Hebron", modelBuilder);
        SeedDefaultTimeZone(269, "(GMT+07:00) Asia/Ho_Chi_Minh", "(GMT+07:00) Asia/Ho_Chi_Minh", modelBuilder);
        SeedDefaultTimeZone(270, "(GMT+08:00) Asia/Hong_Kong", "(GMT+08:00) Asia/Hong_Kong", modelBuilder);
        SeedDefaultTimeZone(271, "(GMT+07:00) Asia/Hovd", "(GMT+07:00) Asia/Hovd", modelBuilder);
        SeedDefaultTimeZone(272, "(GMT+08:00) Asia/Irkutsk", "(GMT+08:00) Asia/Irkutsk", modelBuilder);
        SeedDefaultTimeZone(273, "(GMT+03:00) Asia/Istanbul", "(GMT+03:00) Asia/Istanbul", modelBuilder);
        SeedDefaultTimeZone(274, "(GMT+07:00) Asia/Jakarta", "(GMT+07:00) Asia/Jakarta", modelBuilder);
        SeedDefaultTimeZone(275, "(GMT+09:00) Asia/Jayapura", "(GMT+09:00) Asia/Jayapura", modelBuilder);
        SeedDefaultTimeZone(276, "(GMT+02:00) Asia/Jerusalem", "(GMT+02:00) Asia/Jerusalem", modelBuilder);
        SeedDefaultTimeZone(277, "(GMT+04:30) Asia/Kabul", "(GMT+04:30) Asia/Kabul", modelBuilder);
        SeedDefaultTimeZone(278, "(GMT+12:00) Asia/Kamchatka", "(GMT+12:00) Asia/Kamchatka", modelBuilder);
        SeedDefaultTimeZone(279, "(GMT+05:00) Asia/Karachi", "(GMT+05:00) Asia/Karachi", modelBuilder);
        SeedDefaultTimeZone(280, "(GMT+06:00) Asia/Kashgar", "(GMT+06:00) Asia/Kashgar", modelBuilder);
        SeedDefaultTimeZone(281, "(GMT+05:45) Asia/Kathmandu", "(GMT+05:45) Asia/Kathmandu", modelBuilder);
        SeedDefaultTimeZone(282, "(GMT+05:45) Asia/Katmandu", "(GMT+05:45) Asia/Katmandu", modelBuilder);
        SeedDefaultTimeZone(283, "(GMT+09:00) Asia/Khandyga", "(GMT+09:00) Asia/Khandyga", modelBuilder);
        SeedDefaultTimeZone(284, "(GMT+05:30) Asia/Kolkata", "(GMT+05:30) Asia/Kolkata", modelBuilder);
        SeedDefaultTimeZone(285, "(GMT+07:00) Asia/Krasnoyarsk", "(GMT+07:00) Asia/Krasnoyarsk", modelBuilder);
        SeedDefaultTimeZone(286, "(GMT+08:00) Asia/Kuala_Lumpur", "(GMT+08:00) Asia/Kuala_Lumpur", modelBuilder);
        SeedDefaultTimeZone(287, "(GMT+08:00) Asia/Kuching", "(GMT+08:00) Asia/Kuching", modelBuilder);
        SeedDefaultTimeZone(288, "(GMT+03:00) Asia/Kuwait", "(GMT+03:00) Asia/Kuwait", modelBuilder);
        SeedDefaultTimeZone(289, "(GMT+08:00) Asia/Macao", "(GMT+08:00) Asia/Macao", modelBuilder);
        SeedDefaultTimeZone(290, "(GMT+08:00) Asia/Macau", "(GMT+08:00) Asia/Macau", modelBuilder);
        SeedDefaultTimeZone(291, "(GMT+11:00) Asia/Magadan", "(GMT+11:00) Asia/Magadan", modelBuilder);
        SeedDefaultTimeZone(292, "(GMT+08:00) Asia/Makassar", "(GMT+08:00) Asia/Makassar", modelBuilder);
        SeedDefaultTimeZone(293, "(GMT+08:00) Asia/Manila", "(GMT+08:00) Asia/Manila", modelBuilder);
        SeedDefaultTimeZone(294, "(GMT+04:00) Asia/Muscat", "(GMT+04:00) Asia/Muscat", modelBuilder);
        SeedDefaultTimeZone(295, "(GMT+02:00) Asia/Nicosia", "(GMT+02:00) Asia/Nicosia", modelBuilder);
        SeedDefaultTimeZone(296, "(GMT+07:00) Asia/Novokuznetsk", "(GMT+07:00) Asia/Novokuznetsk", modelBuilder);
        SeedDefaultTimeZone(297, "(GMT+07:00) Asia/Novosibirsk", "(GMT+07:00) Asia/Novosibirsk", modelBuilder);
        SeedDefaultTimeZone(298, "(GMT+06:00) Asia/Omsk", "(GMT+06:00) Asia/Omsk", modelBuilder);
        SeedDefaultTimeZone(299, "(GMT+05:00) Asia/Oral", "(GMT+05:00) Asia/Oral", modelBuilder);
        SeedDefaultTimeZone(300, "(GMT+07:00) Asia/Phnom_Penh", "(GMT+07:00) Asia/Phnom_Penh", modelBuilder);
        SeedDefaultTimeZone(301, "(GMT+07:00) Asia/Pontianak", "(GMT+07:00) Asia/Pontianak", modelBuilder);
        SeedDefaultTimeZone(302, "(GMT+09:00) Asia/Pyongyang", "(GMT+09:00) Asia/Pyongyang", modelBuilder);
        SeedDefaultTimeZone(303, "(GMT+03:00) Asia/Qatar", "(GMT+03:00) Asia/Qatar", modelBuilder);
        SeedDefaultTimeZone(304, "(GMT+06:00) Asia/Qostanay", "(GMT+06:00) Asia/Qostanay", modelBuilder);
        SeedDefaultTimeZone(305, "(GMT+05:00) Asia/Qyzylorda", "(GMT+05:00) Asia/Qyzylorda", modelBuilder);
        SeedDefaultTimeZone(306, "(GMT+06:30) Asia/Rangoon", "(GMT+06:30) Asia/Rangoon", modelBuilder);
        SeedDefaultTimeZone(307, "(GMT+03:00) Asia/Riyadh", "(GMT+03:00) Asia/Riyadh", modelBuilder);
        SeedDefaultTimeZone(308, "(GMT+07:00) Asia/Saigon", "(GMT+07:00) Asia/Saigon", modelBuilder);
        SeedDefaultTimeZone(309, "(GMT+11:00) Asia/Sakhalin", "(GMT+11:00) Asia/Sakhalin", modelBuilder);
        SeedDefaultTimeZone(310, "(GMT+05:00) Asia/Samarkand", "(GMT+05:00) Asia/Samarkand", modelBuilder);
        SeedDefaultTimeZone(311, "(GMT+09:00) Asia/Seoul", "(GMT+09:00) Asia/Seoul", modelBuilder);
        SeedDefaultTimeZone(312, "(GMT+08:00) Asia/Shanghai", "(GMT+08:00) Asia/Shanghai", modelBuilder);
        SeedDefaultTimeZone(313, "(GMT+08:00) Asia/Singapore", "(GMT+08:00) Asia/Singapore", modelBuilder);
        SeedDefaultTimeZone(314, "(GMT+11:00) Asia/Srednekolymsk", "(GMT+11:00) Asia/Srednekolymsk", modelBuilder);
        SeedDefaultTimeZone(315, "(GMT+08:00) Asia/Taipei", "(GMT+08:00) Asia/Taipei", modelBuilder);
        SeedDefaultTimeZone(316, "(GMT+05:00) Asia/Tashkent", "(GMT+05:00) Asia/Tashkent", modelBuilder);
        SeedDefaultTimeZone(317, "(GMT+04:00) Asia/Tbilisi", "(GMT+04:00) Asia/Tbilisi", modelBuilder);
        SeedDefaultTimeZone(318, "(GMT+03:30) Asia/Tehran", "(GMT+03:30) Asia/Tehran", modelBuilder);
        SeedDefaultTimeZone(319, "(GMT+02:00) Asia/Tel_Aviv", "(GMT+02:00) Asia/Tel_Aviv", modelBuilder);
        SeedDefaultTimeZone(320, "(GMT+06:00) Asia/Thimbu", "(GMT+06:00) Asia/Thimbu", modelBuilder);
        SeedDefaultTimeZone(321, "(GMT+06:00) Asia/Thimphu", "(GMT+06:00) Asia/Thimphu", modelBuilder);
        SeedDefaultTimeZone(322, "(GMT+09:00) Asia/Tokyo", "(GMT+09:00) Asia/Tokyo", modelBuilder);
        SeedDefaultTimeZone(323, "(GMT+07:00) Asia/Tomsk", "(GMT+07:00) Asia/Tomsk", modelBuilder);
        SeedDefaultTimeZone(324, "(GMT+08:00) Asia/Ujung_Pandang", "(GMT+08:00) Asia/Ujung_Pandang", modelBuilder);
        SeedDefaultTimeZone(325, "(GMT+08:00) Asia/Ulaanbaatar", "(GMT+08:00) Asia/Ulaanbaatar", modelBuilder);
        SeedDefaultTimeZone(326, "(GMT+08:00) Asia/Ulan_Bator", "(GMT+08:00) Asia/Ulan_Bator", modelBuilder);
        SeedDefaultTimeZone(327, "(GMT+06:00) Asia/Urumqi", "(GMT+06:00) Asia/Urumqi", modelBuilder);
        SeedDefaultTimeZone(328, "(GMT+10:00) Asia/Ust-Nera", "(GMT+10:00) Asia/Ust-Nera", modelBuilder);
        SeedDefaultTimeZone(329, "(GMT+07:00) Asia/Vientiane", "(GMT+07:00) Asia/Vientiane", modelBuilder);
        SeedDefaultTimeZone(330, "(GMT+10:00) Asia/Vladivostok", "(GMT+10:00) Asia/Vladivostok", modelBuilder);
        SeedDefaultTimeZone(331, "(GMT+09:00) Asia/Yakutsk", "(GMT+09:00) Asia/Yakutsk", modelBuilder);
        SeedDefaultTimeZone(332, "(GMT+06:30) Asia/Yangon", "(GMT+06:30) Asia/Yangon", modelBuilder);
        SeedDefaultTimeZone(333, "(GMT+05:00) Asia/Yekaterinburg", "(GMT+05:00) Asia/Yekaterinburg", modelBuilder);
        SeedDefaultTimeZone(334, "(GMT+04:00) Asia/Yerevan", "(GMT+04:00) Asia/Yerevan", modelBuilder);
        SeedDefaultTimeZone(335, "(GMT-01:00) Atlantic/Azores", "(GMT-01:00) Atlantic/Azores", modelBuilder);
        SeedDefaultTimeZone(336, "(GMT-04:00) Atlantic/Bermuda", "(GMT-04:00) Atlantic/Bermuda", modelBuilder);
        SeedDefaultTimeZone(337, "(GMT+00:00) Atlantic/Canary", "(GMT+00:00) Atlantic/Canary", modelBuilder);
        SeedDefaultTimeZone(338, "(GMT-01:00) Atlantic/Cape_Verde", "(GMT-01:00) Atlantic/Cape_Verde", modelBuilder);
        SeedDefaultTimeZone(339, "(GMT+00:00) Atlantic/Faeroe", "(GMT+00:00) Atlantic/Faeroe", modelBuilder);
        SeedDefaultTimeZone(340, "(GMT+00:00) Atlantic/Faroe", "(GMT+00:00) Atlantic/Faroe", modelBuilder);
        SeedDefaultTimeZone(341, "(GMT+01:00) Atlantic/Jan_Mayen", "(GMT+01:00) Atlantic/Jan_Mayen", modelBuilder);
        SeedDefaultTimeZone(342, "(GMT+00:00) Atlantic/Madeira", "(GMT+00:00) Atlantic/Madeira", modelBuilder);
        SeedDefaultTimeZone(343, "(GMT+00:00) Atlantic/Reykjavik", "(GMT+00:00) Atlantic/Reykjavik", modelBuilder);
        SeedDefaultTimeZone(344, "(GMT-02:00) Atlantic/South_Georgia", "(GMT-02:00) Atlantic/South_Georgia", modelBuilder);
        SeedDefaultTimeZone(345, "(GMT+00:00) Atlantic/St_Helena", "(GMT+00:00) Atlantic/St_Helena", modelBuilder);
        SeedDefaultTimeZone(346, "(GMT-03:00) Atlantic/Stanley", "(GMT-03:00) Atlantic/Stanley", modelBuilder);
        SeedDefaultTimeZone(347, "(GMT+11:00) Australia/ACT", "(GMT+11:00) Australia/ACT", modelBuilder);
        SeedDefaultTimeZone(348, "(GMT+10:30) Australia/Adelaide", "(GMT+10:30) Australia/Adelaide", modelBuilder);
        SeedDefaultTimeZone(349, "(GMT+10:00) Australia/Brisbane", "(GMT+10:00) Australia/Brisbane", modelBuilder);
        SeedDefaultTimeZone(350, "(GMT+10:30) Australia/Broken_Hill", "(GMT+10:30) Australia/Broken_Hill", modelBuilder);
        SeedDefaultTimeZone(351, "(GMT+11:00) Australia/Canberra", "(GMT+11:00) Australia/Canberra", modelBuilder);
        SeedDefaultTimeZone(352, "(GMT+11:00) Australia/Currie", "(GMT+11:00) Australia/Currie", modelBuilder);
        SeedDefaultTimeZone(353, "(GMT+09:30) Australia/Darwin", "(GMT+09:30) Australia/Darwin", modelBuilder);
        SeedDefaultTimeZone(354, "(GMT+08:45) Australia/Eucla", "(GMT+08:45) Australia/Eucla", modelBuilder);
        SeedDefaultTimeZone(355, "(GMT+11:00) Australia/Hobart", "(GMT+11:00) Australia/Hobart", modelBuilder);
        SeedDefaultTimeZone(356, "(GMT+11:00) Australia/LHI", "(GMT+11:00) Australia/LHI", modelBuilder);
        SeedDefaultTimeZone(357, "(GMT+10:00) Australia/Lindeman", "(GMT+10:00) Australia/Lindeman", modelBuilder);
        SeedDefaultTimeZone(358, "(GMT+11:00) Australia/Lord_Howe", "(GMT+11:00) Australia/Lord_Howe", modelBuilder);
        SeedDefaultTimeZone(359, "(GMT+11:00) Australia/Melbourne", "(GMT+11:00) Australia/Melbourne", modelBuilder);
        SeedDefaultTimeZone(360, "(GMT+11:00) Australia/NSW", "(GMT+11:00) Australia/NSW", modelBuilder);
        SeedDefaultTimeZone(361, "(GMT+09:30) Australia/North", "(GMT+09:30) Australia/North", modelBuilder);
        SeedDefaultTimeZone(362, "(GMT+08:00) Australia/Perth", "(GMT+08:00) Australia/Perth", modelBuilder);
        SeedDefaultTimeZone(363, "(GMT+10:00) Australia/Queensland", "(GMT+10:00) Australia/Queensland", modelBuilder);
        SeedDefaultTimeZone(364, "(GMT+10:30) Australia/South", "(GMT+10:30) Australia/South", modelBuilder);
        SeedDefaultTimeZone(365, "(GMT+11:00) Australia/Sydney", "(GMT+11:00) Australia/Sydney", modelBuilder);
        SeedDefaultTimeZone(366, "(GMT+11:00) Australia/Tasmania", "(GMT+11:00) Australia/Tasmania", modelBuilder);
        SeedDefaultTimeZone(367, "(GMT+11:00) Australia/Victoria", "(GMT+11:00) Australia/Victoria", modelBuilder);
        SeedDefaultTimeZone(368, "(GMT+08:00) Australia/West", "(GMT+08:00) Australia/West", modelBuilder);
        SeedDefaultTimeZone(369, "(GMT+10:30) Australia/Yancowinna", "(GMT+10:30) Australia/Yancowinna", modelBuilder);
        SeedDefaultTimeZone(370, "(GMT-05:00) Brazil/Acre", "(GMT-05:00) Brazil/Acre", modelBuilder);
        SeedDefaultTimeZone(371, "(GMT-02:00) Brazil/DeNoronha", "(GMT-02:00) Brazil/DeNoronha", modelBuilder);
        SeedDefaultTimeZone(372, "(GMT-03:00) Brazil/East", "(GMT-03:00) Brazil/East", modelBuilder);
        SeedDefaultTimeZone(373, "(GMT-04:00) Brazil/West", "(GMT-04:00) Brazil/West", modelBuilder);
        SeedDefaultTimeZone(374, "(GMT+01:00) CET", "(GMT+01:00) CET", modelBuilder);
        SeedDefaultTimeZone(375, "(GMT-06:00) CST6CDT", "(GMT-06:00) CST6CDT", modelBuilder);
        SeedDefaultTimeZone(376, "(GMT-04:00) Canada/Atlantic", "(GMT-04:00) Canada/Atlantic", modelBuilder);
        SeedDefaultTimeZone(377, "(GMT-06:00) Canada/Central", "(GMT-06:00) Canada/Central", modelBuilder);
        SeedDefaultTimeZone(378, "(GMT-05:00) Canada/Eastern", "(GMT-05:00) Canada/Eastern", modelBuilder);
        SeedDefaultTimeZone(379, "(GMT-07:00) Canada/Mountain", "(GMT-07:00) Canada/Mountain", modelBuilder);
        SeedDefaultTimeZone(380, "(GMT-03:30) Canada/Newfoundland", "(GMT-03:30) Canada/Newfoundland", modelBuilder);
        SeedDefaultTimeZone(381, "(GMT-08:00) Canada/Pacific", "(GMT-08:00) Canada/Pacific", modelBuilder);
        SeedDefaultTimeZone(382, "(GMT-06:00) Canada/Saskatchewan", "(GMT-06:00) Canada/Saskatchewan", modelBuilder);
        SeedDefaultTimeZone(383, "(GMT-07:00) Canada/Yukon", "(GMT-07:00) Canada/Yukon", modelBuilder);
        SeedDefaultTimeZone(384, "(GMT-03:00) Chile/Continental", "(GMT-03:00) Chile/Continental", modelBuilder);
        SeedDefaultTimeZone(385, "(GMT-05:00) Chile/EasterIsland", "(GMT-05:00) Chile/EasterIsland", modelBuilder);
        SeedDefaultTimeZone(386, "(GMT-05:00) Cuba", "(GMT-05:00) Cuba", modelBuilder);
        SeedDefaultTimeZone(387, "(GMT+02:00) EET", "(GMT+02:00) EET", modelBuilder);
        SeedDefaultTimeZone(388, "(GMT-05:00) EST", "(GMT-05:00) EST", modelBuilder);
        SeedDefaultTimeZone(389, "(GMT-05:00) EST5EDT", "(GMT-05:00) EST5EDT", modelBuilder);
        SeedDefaultTimeZone(390, "(GMT+02:00) Egypt", "(GMT+02:00) Egypt", modelBuilder);
        SeedDefaultTimeZone(391, "(GMT+00:00) Eire", "(GMT+00:00) Eire", modelBuilder);
        SeedDefaultTimeZone(392, "(GMT+00:00) Etc/GMT", "(GMT+00:00) Etc/GMT", modelBuilder);
        SeedDefaultTimeZone(393, "(GMT+00:00) Etc/GMT+0", "(GMT+00:00) Etc/GMT+0", modelBuilder);
        SeedDefaultTimeZone(394, "(GMT-01:00) Etc/GMT+1", "(GMT-01:00) Etc/GMT+1", modelBuilder);
        SeedDefaultTimeZone(395, "(GMT-10:00) Etc/GMT+10", "(GMT-10:00) Etc/GMT+10", modelBuilder);
        SeedDefaultTimeZone(396, "(GMT-11:00) Etc/GMT+11", "(GMT-11:00) Etc/GMT+11", modelBuilder);
        SeedDefaultTimeZone(397, "(GMT-12:00) Etc/GMT+12", "(GMT-12:00) Etc/GMT+12", modelBuilder);
        SeedDefaultTimeZone(398, "(GMT-02:00) Etc/GMT+2", "(GMT-02:00) Etc/GMT+2", modelBuilder);
        SeedDefaultTimeZone(399, "(GMT-03:00) Etc/GMT+3", "(GMT-03:00) Etc/GMT+3", modelBuilder);
        SeedDefaultTimeZone(400, "(GMT-04:00) Etc/GMT+4", "(GMT-04:00) Etc/GMT+4", modelBuilder);
        SeedDefaultTimeZone(401, "(GMT-05:00) Etc/GMT+5", "(GMT-05:00) Etc/GMT+5", modelBuilder);
        SeedDefaultTimeZone(402, "(GMT-06:00) Etc/GMT+6", "(GMT-06:00) Etc/GMT+6", modelBuilder);
        SeedDefaultTimeZone(403, "(GMT-07:00) Etc/GMT+7", "(GMT-07:00) Etc/GMT+7", modelBuilder);
        SeedDefaultTimeZone(404, "(GMT-08:00) Etc/GMT+8", "(GMT-08:00) Etc/GMT+8", modelBuilder);
        SeedDefaultTimeZone(405, "(GMT-09:00) Etc/GMT+9", "(GMT-09:00) Etc/GMT+9", modelBuilder);
        SeedDefaultTimeZone(406, "(GMT+00:00) Etc/GMT-0", "(GMT+00:00) Etc/GMT-0", modelBuilder);
        SeedDefaultTimeZone(407, "(GMT+01:00) Etc/GMT-1", "(GMT+01:00) Etc/GMT-1", modelBuilder);
        SeedDefaultTimeZone(408, "(GMT+10:00) Etc/GMT-10", "(GMT+10:00) Etc/GMT-10", modelBuilder);
        SeedDefaultTimeZone(409, "(GMT+11:00) Etc/GMT-11", "(GMT+11:00) Etc/GMT-11", modelBuilder);
        SeedDefaultTimeZone(410, "(GMT+12:00) Etc/GMT-12", "(GMT+12:00) Etc/GMT-12", modelBuilder);
        SeedDefaultTimeZone(411, "(GMT+13:00) Etc/GMT-13", "(GMT+13:00) Etc/GMT-13", modelBuilder);
        SeedDefaultTimeZone(412, "(GMT+14:00) Etc/GMT-14", "(GMT+14:00) Etc/GMT-14", modelBuilder);
        SeedDefaultTimeZone(413, "(GMT+02:00) Etc/GMT-2", "(GMT+02:00) Etc/GMT-2", modelBuilder);
        SeedDefaultTimeZone(414, "(GMT+03:00) Etc/GMT-3", "(GMT+03:00) Etc/GMT-3", modelBuilder);
        SeedDefaultTimeZone(415, "(GMT+04:00) Etc/GMT-4", "(GMT+04:00) Etc/GMT-4", modelBuilder);
        SeedDefaultTimeZone(416, "(GMT+05:00) Etc/GMT-5", "(GMT+05:00) Etc/GMT-5", modelBuilder);
        SeedDefaultTimeZone(417, "(GMT+06:00) Etc/GMT-6", "(GMT+06:00) Etc/GMT-6", modelBuilder);
        SeedDefaultTimeZone(418, "(GMT+07:00) Etc/GMT-7", "(GMT+07:00) Etc/GMT-7", modelBuilder);
        SeedDefaultTimeZone(419, "(GMT+08:00) Etc/GMT-8", "(GMT+08:00) Etc/GMT-8", modelBuilder);
        SeedDefaultTimeZone(420, "(GMT+09:00) Etc/GMT-9", "(GMT+09:00) Etc/GMT-9", modelBuilder);
        SeedDefaultTimeZone(421, "(GMT+00:00) Etc/GMT0", "(GMT+00:00) Etc/GMT0", modelBuilder);
        SeedDefaultTimeZone(422, "(GMT+00:00) Etc/Greenwich", "(GMT+00:00) Etc/Greenwich", modelBuilder);
        SeedDefaultTimeZone(423, "(GMT+00:00) Etc/UCT", "(GMT+00:00) Etc/UCT", modelBuilder);
        SeedDefaultTimeZone(424, "(GMT+00:00) Etc/UTC", "(GMT+00:00) Etc/UTC", modelBuilder);
        SeedDefaultTimeZone(425, "(GMT+00:00) Etc/Universal", "(GMT+00:00) Etc/Universal", modelBuilder);
        SeedDefaultTimeZone(426, "(GMT+00:00) Etc/Zulu", "(GMT+00:00) Etc/Zulu", modelBuilder);
        SeedDefaultTimeZone(427, "(GMT+01:00) Europe/Amsterdam", "(GMT+01:00) Europe/Amsterdam", modelBuilder);
        SeedDefaultTimeZone(428, "(GMT+01:00) Europe/Andorra", "(GMT+01:00) Europe/Andorra", modelBuilder);
        SeedDefaultTimeZone(429, "(GMT+04:00) Europe/Astrakhan", "(GMT+04:00) Europe/Astrakhan", modelBuilder);
        SeedDefaultTimeZone(430, "(GMT+02:00) Europe/Athens", "(GMT+02:00) Europe/Athens", modelBuilder);
        SeedDefaultTimeZone(431, "(GMT+00:00) Europe/Belfast", "(GMT+00:00) Europe/Belfast", modelBuilder);
        SeedDefaultTimeZone(432, "(GMT+01:00) Europe/Belgrade", "(GMT+01:00) Europe/Belgrade", modelBuilder);
        SeedDefaultTimeZone(433, "(GMT+01:00) Europe/Berlin", "(GMT+01:00) Europe/Berlin", modelBuilder);
        SeedDefaultTimeZone(434, "(GMT+01:00) Europe/Bratislava", "(GMT+01:00) Europe/Bratislava", modelBuilder);
        SeedDefaultTimeZone(435, "(GMT+01:00) Europe/Brussels", "(GMT+01:00) Europe/Brussels", modelBuilder);
        SeedDefaultTimeZone(436, "(GMT+02:00) Europe/Bucharest", "(GMT+02:00) Europe/Bucharest", modelBuilder);
        SeedDefaultTimeZone(437, "(GMT+01:00) Europe/Budapest", "(GMT+01:00) Europe/Budapest", modelBuilder);
        SeedDefaultTimeZone(438, "(GMT+01:00) Europe/Busingen", "(GMT+01:00) Europe/Busingen", modelBuilder);
        SeedDefaultTimeZone(439, "(GMT+02:00) Europe/Chisinau", "(GMT+02:00) Europe/Chisinau", modelBuilder);
        SeedDefaultTimeZone(440, "(GMT+01:00) Europe/Copenhagen", "(GMT+01:00) Europe/Copenhagen", modelBuilder);
        SeedDefaultTimeZone(441, "(GMT+00:00) Europe/Dublin", "(GMT+00:00) Europe/Dublin", modelBuilder);
        SeedDefaultTimeZone(442, "(GMT+01:00) Europe/Gibraltar", "(GMT+01:00) Europe/Gibraltar", modelBuilder);
        SeedDefaultTimeZone(443, "(GMT+00:00) Europe/Guernsey", "(GMT+00:00) Europe/Guernsey", modelBuilder);
        SeedDefaultTimeZone(444, "(GMT+02:00) Europe/Helsinki", "(GMT+02:00) Europe/Helsinki", modelBuilder);
        SeedDefaultTimeZone(445, "(GMT+00:00) Europe/Isle_of_Man", "(GMT+00:00) Europe/Isle_of_Man", modelBuilder);
        SeedDefaultTimeZone(446, "(GMT+03:00) Europe/Istanbul", "(GMT+03:00) Europe/Istanbul", modelBuilder);
        SeedDefaultTimeZone(447, "(GMT+00:00) Europe/Jersey", "(GMT+00:00) Europe/Jersey", modelBuilder);
        SeedDefaultTimeZone(448, "(GMT+02:00) Europe/Kaliningrad", "(GMT+02:00) Europe/Kaliningrad", modelBuilder);
        SeedDefaultTimeZone(449, "(GMT+02:00) Europe/Kiev", "(GMT+02:00) Europe/Kiev", modelBuilder);
        SeedDefaultTimeZone(450, "(GMT+03:00) Europe/Kirov", "(GMT+03:00) Europe/Kirov", modelBuilder);
        SeedDefaultTimeZone(451, "(GMT+02:00) Europe/Kyiv", "(GMT+02:00) Europe/Kyiv", modelBuilder);
        SeedDefaultTimeZone(452, "(GMT+00:00) Europe/Lisbon", "(GMT+00:00) Europe/Lisbon", modelBuilder);
        SeedDefaultTimeZone(453, "(GMT+01:00) Europe/Ljubljana", "(GMT+01:00) Europe/Ljubljana", modelBuilder);
        SeedDefaultTimeZone(454, "(GMT+00:00) Europe/London", "(GMT+00:00) Europe/London", modelBuilder);
        SeedDefaultTimeZone(455, "(GMT+01:00) Europe/Luxembourg", "(GMT+01:00) Europe/Luxembourg", modelBuilder);
        SeedDefaultTimeZone(456, "(GMT+01:00) Europe/Madrid", "(GMT+01:00) Europe/Madrid", modelBuilder);
        SeedDefaultTimeZone(457, "(GMT+01:00) Europe/Malta", "(GMT+01:00) Europe/Malta", modelBuilder);
        SeedDefaultTimeZone(458, "(GMT+02:00) Europe/Mariehamn", "(GMT+02:00) Europe/Mariehamn", modelBuilder);
        SeedDefaultTimeZone(459, "(GMT+03:00) Europe/Minsk", "(GMT+03:00) Europe/Minsk", modelBuilder);
        SeedDefaultTimeZone(460, "(GMT+01:00) Europe/Monaco", "(GMT+01:00) Europe/Monaco", modelBuilder);
        SeedDefaultTimeZone(461, "(GMT+03:00) Europe/Moscow", "(GMT+03:00) Europe/Moscow", modelBuilder);
        SeedDefaultTimeZone(462, "(GMT+02:00) Europe/Nicosia", "(GMT+02:00) Europe/Nicosia", modelBuilder);
        SeedDefaultTimeZone(463, "(GMT+01:00) Europe/Oslo", "(GMT+01:00) Europe/Oslo", modelBuilder);
        SeedDefaultTimeZone(464, "(GMT+01:00) Europe/Paris", "(GMT+01:00) Europe/Paris", modelBuilder);
        SeedDefaultTimeZone(465, "(GMT+01:00) Europe/Podgorica", "(GMT+01:00) Europe/Podgorica", modelBuilder);
        SeedDefaultTimeZone(466, "(GMT+01:00) Europe/Prague", "(GMT+01:00) Europe/Prague", modelBuilder);
        SeedDefaultTimeZone(467, "(GMT+02:00) Europe/Riga", "(GMT+02:00) Europe/Riga", modelBuilder);
        SeedDefaultTimeZone(468, "(GMT+01:00) Europe/Rome", "(GMT+01:00) Europe/Rome", modelBuilder);
        SeedDefaultTimeZone(469, "(GMT+04:00) Europe/Samara", "(GMT+04:00) Europe/Samara", modelBuilder);
        SeedDefaultTimeZone(470, "(GMT+01:00) Europe/San_Marino", "(GMT+01:00) Europe/San_Marino", modelBuilder);
        SeedDefaultTimeZone(471, "(GMT+01:00) Europe/Sarajevo", "(GMT+01:00) Europe/Sarajevo", modelBuilder);
        SeedDefaultTimeZone(472, "(GMT+04:00) Europe/Saratov", "(GMT+04:00) Europe/Saratov", modelBuilder);
        SeedDefaultTimeZone(473, "(GMT+03:00) Europe/Simferopol", "(GMT+03:00) Europe/Simferopol", modelBuilder);
        SeedDefaultTimeZone(474, "(GMT+01:00) Europe/Skopje", "(GMT+01:00) Europe/Skopje", modelBuilder);
        SeedDefaultTimeZone(475, "(GMT+02:00) Europe/Sofia", "(GMT+02:00) Europe/Sofia", modelBuilder);
        SeedDefaultTimeZone(476, "(GMT+01:00) Europe/Stockholm", "(GMT+01:00) Europe/Stockholm", modelBuilder);
        SeedDefaultTimeZone(477, "(GMT+02:00) Europe/Tallinn", "(GMT+02:00) Europe/Tallinn", modelBuilder);
        SeedDefaultTimeZone(478, "(GMT+01:00) Europe/Tirane", "(GMT+01:00) Europe/Tirane", modelBuilder);
        SeedDefaultTimeZone(479, "(GMT+02:00) Europe/Tiraspol", "(GMT+02:00) Europe/Tiraspol", modelBuilder);
        SeedDefaultTimeZone(480, "(GMT+04:00) Europe/Ulyanovsk", "(GMT+04:00) Europe/Ulyanovsk", modelBuilder);
        SeedDefaultTimeZone(481, "(GMT+02:00) Europe/Uzhgorod", "(GMT+02:00) Europe/Uzhgorod", modelBuilder);
        SeedDefaultTimeZone(482, "(GMT+01:00) Europe/Vaduz", "(GMT+01:00) Europe/Vaduz", modelBuilder);
        SeedDefaultTimeZone(483, "(GMT+01:00) Europe/Vatican", "(GMT+01:00) Europe/Vatican", modelBuilder);
        SeedDefaultTimeZone(484, "(GMT+01:00) Europe/Vienna", "(GMT+01:00) Europe/Vienna", modelBuilder);
        SeedDefaultTimeZone(485, "(GMT+02:00) Europe/Vilnius", "(GMT+02:00) Europe/Vilnius", modelBuilder);
        SeedDefaultTimeZone(486, "(GMT+03:00) Europe/Volgograd", "(GMT+03:00) Europe/Volgograd", modelBuilder);
        SeedDefaultTimeZone(487, "(GMT+01:00) Europe/Warsaw", "(GMT+01:00) Europe/Warsaw", modelBuilder);
        SeedDefaultTimeZone(488, "(GMT+01:00) Europe/Zagreb", "(GMT+01:00) Europe/Zagreb", modelBuilder);
        SeedDefaultTimeZone(489, "(GMT+02:00) Europe/Zaporozhye", "(GMT+02:00) Europe/Zaporozhye", modelBuilder);
        SeedDefaultTimeZone(490, "(GMT+01:00) Europe/Zurich", "(GMT+01:00) Europe/Zurich", modelBuilder);
        SeedDefaultTimeZone(491, "(GMT+00:00) GB", "(GMT+00:00) GB", modelBuilder);
        SeedDefaultTimeZone(492, "(GMT+00:00) GB-Eire", "(GMT+00:00) GB-Eire", modelBuilder);
        SeedDefaultTimeZone(493, "(GMT+00:00) GMT", "(GMT+00:00) GMT", modelBuilder);
        SeedDefaultTimeZone(494, "(GMT+00:00) GMT+0", "(GMT+00:00) GMT+0", modelBuilder);
        SeedDefaultTimeZone(495, "(GMT+00:00) GMT-0", "(GMT+00:00) GMT-0", modelBuilder);
        SeedDefaultTimeZone(496, "(GMT+00:00) GMT0", "(GMT+00:00) GMT0", modelBuilder);
        SeedDefaultTimeZone(497, "(GMT+00:00) Greenwich", "(GMT+00:00) Greenwich", modelBuilder);
        SeedDefaultTimeZone(498, "(GMT-10:00) HST", "(GMT-10:00) HST", modelBuilder);
        SeedDefaultTimeZone(499, "(GMT+08:00) Hongkong", "(GMT+08:00) Hongkong", modelBuilder);
        SeedDefaultTimeZone(500, "(GMT+00:00) Iceland", "(GMT+00:00) Iceland", modelBuilder);
        SeedDefaultTimeZone(501, "(GMT+03:00) Indian/Antananarivo", "(GMT+03:00) Indian/Antananarivo", modelBuilder);
        SeedDefaultTimeZone(502, "(GMT+06:00) Indian/Chagos", "(GMT+06:00) Indian/Chagos", modelBuilder);
        SeedDefaultTimeZone(503, "(GMT+07:00) Indian/Christmas", "(GMT+07:00) Indian/Christmas", modelBuilder);
        SeedDefaultTimeZone(504, "(GMT+06:30) Indian/Cocos", "(GMT+06:30) Indian/Cocos", modelBuilder);
        SeedDefaultTimeZone(505, "(GMT+03:00) Indian/Comoro", "(GMT+03:00) Indian/Comoro", modelBuilder);
        SeedDefaultTimeZone(506, "(GMT+05:00) Indian/Kerguelen", "(GMT+05:00) Indian/Kerguelen", modelBuilder);
        SeedDefaultTimeZone(507, "(GMT+04:00) Indian/Mahe", "(GMT+04:00) Indian/Mahe", modelBuilder);
        SeedDefaultTimeZone(508, "(GMT+05:00) Indian/Maldives", "(GMT+05:00) Indian/Maldives", modelBuilder);
        SeedDefaultTimeZone(509, "(GMT+04:00) Indian/Mauritius", "(GMT+04:00) Indian/Mauritius", modelBuilder);
        SeedDefaultTimeZone(510, "(GMT+03:00) Indian/Mayotte", "(GMT+03:00) Indian/Mayotte", modelBuilder);
        SeedDefaultTimeZone(511, "(GMT+04:00) Indian/Reunion", "(GMT+04:00) Indian/Reunion", modelBuilder);
        SeedDefaultTimeZone(512, "(GMT+03:30) Iran", "(GMT+03:30) Iran", modelBuilder);
        SeedDefaultTimeZone(513, "(GMT+02:00) Israel", "(GMT+02:00) Israel", modelBuilder);
        SeedDefaultTimeZone(514, "(GMT-05:00) Jamaica", "(GMT-05:00) Jamaica", modelBuilder);
        SeedDefaultTimeZone(515, "(GMT+09:00) Japan", "(GMT+09:00) Japan", modelBuilder);
        SeedDefaultTimeZone(516, "(GMT+12:00) Kwajalein", "(GMT+12:00) Kwajalein", modelBuilder);
        SeedDefaultTimeZone(517, "(GMT+02:00) Libya", "(GMT+02:00) Libya", modelBuilder);
        SeedDefaultTimeZone(518, "(GMT+01:00) MET", "(GMT+01:00) MET", modelBuilder);
        SeedDefaultTimeZone(519, "(GMT-07:00) MST", "(GMT-07:00) MST", modelBuilder);
        SeedDefaultTimeZone(520, "(GMT-07:00) MST7MDT", "(GMT-07:00) MST7MDT", modelBuilder);
        SeedDefaultTimeZone(521, "(GMT-08:00) Mexico/BajaNorte", "(GMT-08:00) Mexico/BajaNorte", modelBuilder);
        SeedDefaultTimeZone(522, "(GMT-07:00) Mexico/BajaSur", "(GMT-07:00) Mexico/BajaSur", modelBuilder);
        SeedDefaultTimeZone(523, "(GMT-06:00) Mexico/General", "(GMT-06:00) Mexico/General", modelBuilder);
        SeedDefaultTimeZone(524, "(GMT+13:00) NZ", "(GMT+13:00) NZ", modelBuilder);
        SeedDefaultTimeZone(525, "(GMT+13:45) NZ-CHAT", "(GMT+13:45) NZ-CHAT", modelBuilder);
        SeedDefaultTimeZone(526, "(GMT-07:00) Navajo", "(GMT-07:00) Navajo", modelBuilder);
        SeedDefaultTimeZone(527, "(GMT+08:00) PRC", "(GMT+08:00) PRC", modelBuilder);
        SeedDefaultTimeZone(528, "(GMT-08:00) PST8PDT", "(GMT-08:00) PST8PDT", modelBuilder);
        SeedDefaultTimeZone(529, "(GMT+13:00) Pacific/Apia", "(GMT+13:00) Pacific/Apia", modelBuilder);
        SeedDefaultTimeZone(530, "(GMT+13:00) Pacific/Auckland", "(GMT+13:00) Pacific/Auckland", modelBuilder);
        SeedDefaultTimeZone(531, "(GMT+11:00) Pacific/Bougainville", "(GMT+11:00) Pacific/Bougainville", modelBuilder);
        SeedDefaultTimeZone(532, "(GMT+13:45) Pacific/Chatham", "(GMT+13:45) Pacific/Chatham", modelBuilder);
        SeedDefaultTimeZone(533, "(GMT+10:00) Pacific/Chuuk", "(GMT+10:00) Pacific/Chuuk", modelBuilder);
        SeedDefaultTimeZone(534, "(GMT-05:00) Pacific/Easter", "(GMT-05:00) Pacific/Easter", modelBuilder);
        SeedDefaultTimeZone(535, "(GMT+11:00) Pacific/Efate", "(GMT+11:00) Pacific/Efate", modelBuilder);
        SeedDefaultTimeZone(536, "(GMT+13:00) Pacific/Enderbury", "(GMT+13:00) Pacific/Enderbury", modelBuilder);
        SeedDefaultTimeZone(537, "(GMT+13:00) Pacific/Fakaofo", "(GMT+13:00) Pacific/Fakaofo", modelBuilder);
        SeedDefaultTimeZone(538, "(GMT+12:00) Pacific/Fiji", "(GMT+12:00) Pacific/Fiji", modelBuilder);
        SeedDefaultTimeZone(539, "(GMT+12:00) Pacific/Funafuti", "(GMT+12:00) Pacific/Funafuti", modelBuilder);
        SeedDefaultTimeZone(540, "(GMT-06:00) Pacific/Galapagos", "(GMT-06:00) Pacific/Galapagos", modelBuilder);
        SeedDefaultTimeZone(541, "(GMT-09:00) Pacific/Gambier", "(GMT-09:00) Pacific/Gambier", modelBuilder);
        SeedDefaultTimeZone(542, "(GMT+11:00) Pacific/Guadalcanal", "(GMT+11:00) Pacific/Guadalcanal", modelBuilder);
        SeedDefaultTimeZone(543, "(GMT+10:00) Pacific/Guam", "(GMT+10:00) Pacific/Guam", modelBuilder);
        SeedDefaultTimeZone(544, "(GMT-10:00) Pacific/Honolulu", "(GMT-10:00) Pacific/Honolulu", modelBuilder);
        SeedDefaultTimeZone(545, "(GMT-10:00) Pacific/Johnston", "(GMT-10:00) Pacific/Johnston", modelBuilder);
        SeedDefaultTimeZone(546, "(GMT+13:00) Pacific/Kanton", "(GMT+13:00) Pacific/Kanton", modelBuilder);
        SeedDefaultTimeZone(547, "(GMT+14:00) Pacific/Kiritimati", "(GMT+14:00) Pacific/Kiritimati", modelBuilder);
        SeedDefaultTimeZone(548, "(GMT+11:00) Pacific/Kosrae", "(GMT+11:00) Pacific/Kosrae", modelBuilder);
        SeedDefaultTimeZone(549, "(GMT+12:00) Pacific/Kwajalein", "(GMT+12:00) Pacific/Kwajalein", modelBuilder);
        SeedDefaultTimeZone(550, "(GMT+12:00) Pacific/Majuro", "(GMT+12:00) Pacific/Majuro", modelBuilder);
        SeedDefaultTimeZone(551, "(GMT-09:30) Pacific/Marquesas", "(GMT-09:30) Pacific/Marquesas", modelBuilder);
        SeedDefaultTimeZone(552, "(GMT-11:00) Pacific/Midway", "(GMT-11:00) Pacific/Midway", modelBuilder);
        SeedDefaultTimeZone(553, "(GMT+12:00) Pacific/Nauru", "(GMT+12:00) Pacific/Nauru", modelBuilder);
        SeedDefaultTimeZone(554, "(GMT-11:00) Pacific/Niue", "(GMT-11:00) Pacific/Niue", modelBuilder);
        SeedDefaultTimeZone(555, "(GMT+12:00) Pacific/Norfolk", "(GMT+12:00) Pacific/Norfolk", modelBuilder);
        SeedDefaultTimeZone(556, "(GMT+11:00) Pacific/Noumea", "(GMT+11:00) Pacific/Noumea", modelBuilder);
        SeedDefaultTimeZone(557, "(GMT-11:00) Pacific/Pago_Pago", "(GMT-11:00) Pacific/Pago_Pago", modelBuilder);
        SeedDefaultTimeZone(558, "(GMT+09:00) Pacific/Palau", "(GMT+09:00) Pacific/Palau", modelBuilder);
        SeedDefaultTimeZone(559, "(GMT-08:00) Pacific/Pitcairn", "(GMT-08:00) Pacific/Pitcairn", modelBuilder);
        SeedDefaultTimeZone(560, "(GMT+11:00) Pacific/Pohnpei", "(GMT+11:00) Pacific/Pohnpei", modelBuilder);
        SeedDefaultTimeZone(561, "(GMT+11:00) Pacific/Ponape", "(GMT+11:00) Pacific/Ponape", modelBuilder);
        SeedDefaultTimeZone(562, "(GMT+10:00) Pacific/Port_Moresby", "(GMT+10:00) Pacific/Port_Moresby", modelBuilder);
        SeedDefaultTimeZone(563, "(GMT-10:00) Pacific/Rarotonga", "(GMT-10:00) Pacific/Rarotonga", modelBuilder);
        SeedDefaultTimeZone(564, "(GMT+10:00) Pacific/Saipan", "(GMT+10:00) Pacific/Saipan", modelBuilder);
        SeedDefaultTimeZone(565, "(GMT-11:00) Pacific/Samoa", "(GMT-11:00) Pacific/Samoa", modelBuilder);
        SeedDefaultTimeZone(566, "(GMT-10:00) Pacific/Tahiti", "(GMT-10:00) Pacific/Tahiti", modelBuilder);
        SeedDefaultTimeZone(567, "(GMT+12:00) Pacific/Tarawa", "(GMT+12:00) Pacific/Tarawa", modelBuilder);
        SeedDefaultTimeZone(568, "(GMT+13:00) Pacific/Tongatapu", "(GMT+13:00) Pacific/Tongatapu", modelBuilder);
        SeedDefaultTimeZone(569, "(GMT+10:00) Pacific/Truk", "(GMT+10:00) Pacific/Truk", modelBuilder);
        SeedDefaultTimeZone(570, "(GMT+12:00) Pacific/Wake", "(GMT+12:00) Pacific/Wake", modelBuilder);
        SeedDefaultTimeZone(571, "(GMT+12:00) Pacific/Wallis", "(GMT+12:00) Pacific/Wallis", modelBuilder);
        SeedDefaultTimeZone(572, "(GMT+10:00) Pacific/Yap", "(GMT+10:00) Pacific/Yap", modelBuilder);
        SeedDefaultTimeZone(573, "(GMT+01:00) Poland", "(GMT+01:00) Poland", modelBuilder);
        SeedDefaultTimeZone(574, "(GMT+00:00) Portugal", "(GMT+00:00) Portugal", modelBuilder);
        SeedDefaultTimeZone(575, "(GMT+08:00) ROC", "(GMT+08:00) ROC", modelBuilder);
        SeedDefaultTimeZone(576, "(GMT+09:00) ROK", "(GMT+09:00) ROK", modelBuilder);
        SeedDefaultTimeZone(577, "(GMT+08:00) Singapore", "(GMT+08:00) Singapore", modelBuilder);
        SeedDefaultTimeZone(578, "(GMT+03:00) Turkey", "(GMT+03:00) Turkey", modelBuilder);
        SeedDefaultTimeZone(579, "(GMT+00:00) UCT", "(GMT+00:00) UCT", modelBuilder);
        SeedDefaultTimeZone(580, "(GMT-09:00) US/Alaska", "(GMT-09:00) US/Alaska", modelBuilder);
        SeedDefaultTimeZone(581, "(GMT-10:00) US/Aleutian", "(GMT-10:00) US/Aleutian", modelBuilder);
        SeedDefaultTimeZone(582, "(GMT-07:00) US/Arizona", "(GMT-07:00) US/Arizona", modelBuilder);
        SeedDefaultTimeZone(583, "(GMT-06:00) US/Central", "(GMT-06:00) US/Central", modelBuilder);
        SeedDefaultTimeZone(584, "(GMT-05:00) US/East-Indiana", "(GMT-05:00) US/East-Indiana", modelBuilder);
        SeedDefaultTimeZone(585, "(GMT-05:00) US/Eastern", "(GMT-05:00) US/Eastern", modelBuilder);
        SeedDefaultTimeZone(586, "(GMT-10:00) US/Hawaii", "(GMT-10:00) US/Hawaii", modelBuilder);
        SeedDefaultTimeZone(587, "(GMT-06:00) US/Indiana-Starke", "(GMT-06:00) US/Indiana-Starke", modelBuilder);
        SeedDefaultTimeZone(588, "(GMT-05:00) US/Michigan", "(GMT-05:00) US/Michigan", modelBuilder);
        SeedDefaultTimeZone(589, "(GMT-07:00) US/Mountain", "(GMT-07:00) US/Mountain", modelBuilder);
        SeedDefaultTimeZone(590, "(GMT-08:00) US/Pacific", "(GMT-08:00) US/Pacific", modelBuilder);
        SeedDefaultTimeZone(591, "(GMT-11:00) US/Samoa", "(GMT-11:00) US/Samoa", modelBuilder);
        SeedDefaultTimeZone(592, "(GMT+00:00) UTC", "(GMT+00:00) UTC", modelBuilder);
        SeedDefaultTimeZone(593, "(GMT+00:00) Universal", "(GMT+00:00) Universal", modelBuilder);
        SeedDefaultTimeZone(594, "(GMT+03:00) W-SU", "(GMT+03:00) W-SU", modelBuilder);
        SeedDefaultTimeZone(595, "(GMT+00:00) WET", "(GMT+00:00) WET", modelBuilder);
        SeedDefaultTimeZone(596, "(GMT+00:00) Zulu", "(GMT+00:00) Zulu", modelBuilder);
        #endregion

        /* Seed Default Language Level */
        SeedDefaultLanguageType(1, "Ninguno", "None", modelBuilder);
        SeedDefaultLanguageType(2, "A1", "A1", modelBuilder);
        SeedDefaultLanguageType(3, "A2", "A2", modelBuilder);
        SeedDefaultLanguageType(4, "B1", "B1", modelBuilder);
        SeedDefaultLanguageType(5, "B2", "B2", modelBuilder);
        SeedDefaultLanguageType(6, "C1", "C1", modelBuilder);
        SeedDefaultLanguageType(7, "C2", "C2", modelBuilder);
        SeedDefaultLanguageType(8, "Nativo", "Native", modelBuilder);

        /* Seed Default Pensions */
        SeedDefaultPension(1, "Ninguno", "None", modelBuilder);
        SeedDefaultPension(2, "Colfondos", "Colfondos", modelBuilder);
        SeedDefaultPension(3, "Colpensiones", "Colpensiones", modelBuilder);
        SeedDefaultPension(4, "Old Mutual", "Old Mutual", modelBuilder);
        SeedDefaultPension(5, "Porvenir", "Porvenir", modelBuilder);
        SeedDefaultPension(6, "Protección", "Protección", modelBuilder);

        /* Seed DefaultPositions */
        SeedDefaultPosition(1, "Colaborador", "Employee", "Responsable de diversas acciones dentro de la empresa.", "Responsible for various actions within the company.", modelBuilder);

        /* Seed Default Professional Advices */
        SeedDefaultProfessionalAdvice(1, "Ninguno", "None", "", "", modelBuilder);
        SeedDefaultProfessionalAdvice(2, "Consejo profesional nacional de ingeniería", "National Engineering Professional Council", "COPNIA", "NEPC", modelBuilder);
        SeedDefaultProfessionalAdvice(3, "Colegio de psicólogos", "College of psychologists", "COLPSIC", "COLPSIC", modelBuilder);
        SeedDefaultProfessionalAdvice(4, "Consejo técnico de contaduría publica", "Public accounting technical advice", "CTCP", "PATA", modelBuilder);
        SeedDefaultProfessionalAdvice(5, "Junta central de contadores", "Central Board of Accountants", "JCC", "CBA", modelBuilder);
        SeedDefaultProfessionalAdvice(6, "Colegio profesional de administración de empresas", "Professional College of Business Administration", "CEPAE", "PCBA", modelBuilder);

        /* Seed Default SeveranceBenefits */
        SeedDefaultSeveranceBenefit(1, "Ninguno", "None", modelBuilder);
        SeedDefaultSeveranceBenefit(2, "Colfondos", "Colfondos", modelBuilder);
        SeedDefaultSeveranceBenefit(3, "Colpensiones", "Colpensiones", modelBuilder);
        SeedDefaultSeveranceBenefit(4, "FNA", "FNA", modelBuilder);
        SeedDefaultSeveranceBenefit(5, "IMSS", "IMSS", modelBuilder);
        SeedDefaultSeveranceBenefit(6, "Old Mutual/Skandia", "Old Mutual/Skandia", modelBuilder);
        SeedDefaultSeveranceBenefit(7, "Porvenir", "Porvenir", modelBuilder);
        SeedDefaultSeveranceBenefit(8, "Protección", "Protección", modelBuilder);

        /* Seed Document Types */
        SeedDocumentType(1, "T.I", "T.I", modelBuilder);
        SeedDocumentType(2, "C.C", "C.C", modelBuilder);
        SeedDocumentType(3, "C.E", "C.E", modelBuilder);
        SeedDocumentType(4, "Pasaporte", "Passport", modelBuilder);
        SeedDocumentType(5, "PEP", "PEP", modelBuilder);
        SeedDocumentType(6, "Visa", "Visa", modelBuilder);
        SeedDocumentType(7, "INE", "INE", modelBuilder);
        SeedDocumentType(8, "Otro", "Other", modelBuilder);

        /* Seed Economic Levels */
        SeedEconomicLevel(1, "Ninguno", "None", modelBuilder);
        SeedEconomicLevel(2, "1", "1", modelBuilder);
        SeedEconomicLevel(3, "2", "2", modelBuilder);
        SeedEconomicLevel(4, "3", "3", modelBuilder);
        SeedEconomicLevel(5, "4", "4", modelBuilder);
        SeedEconomicLevel(6, "5", "5", modelBuilder);
        SeedEconomicLevel(7, "6", "6", modelBuilder);

        /* Seed Marital Status */
        SeedMaritalStatus(1, "Ninguno", "None", modelBuilder);
        SeedMaritalStatus(2, "Solter@", "Single", modelBuilder);
        SeedMaritalStatus(3, "Casad@", "Married", modelBuilder);
        SeedMaritalStatus(4, "Unión libre", "Civil union", modelBuilder);
        SeedMaritalStatus(5, "Separad@", "Divorced", modelBuilder);
        SeedMaritalStatus(6, "Viud@", "Widow/Widower", modelBuilder);

        /* Seed PriorityNovelties */
        SeedPriorityNovelties(1, "Alta", "High", modelBuilder);
        SeedPriorityNovelties(2, "Media", "Medium", modelBuilder);
        SeedPriorityNovelties(3, "Baja", "Low", modelBuilder);
        SeedPriorityNovelties(4, "No aplica", "Not applicable", modelBuilder);

        /* Seed NotificationTypes */
        SeedNotificationType(1, "La solicitud al beneficio @1 ha sido @2. Para más información, consulte con el equipo encargado.", "The application to the @1 benefit has been @2. For more information, please contact the team in charge.", modelBuilder);
        SeedNotificationType(2, "¡Feliz cumpleaños! @1. Disfruta de tu día al máximo y que todos tus sueños se hagan realidad.", "Happy birthday! @1. Enjoy your day to the fullest and may all your dreams come true.", modelBuilder);
        SeedNotificationType(3, "La  solicitud al beneficio @1 @2 ha sido <em>Eliminada</em>. Para más información consulte con el equipo encargado.", "The request to the @1 @2 has been <em>Deleted</em>. For more information, please contact the team in charge.", modelBuilder);
        SeedNotificationType(4, "El beneficio @1 @2 ha sido <em>Eliminada</em>. Para más información consulte con el equipo encargado.", "The benefit @1 @2 has been <em>Deleted</em>. For more information, please contact the team in charge.", modelBuilder);
        SeedNotificationType(5, "@1 ha respondido a la nota enviada.", "@1 has replied to the note sent.", modelBuilder);

        /* Seed Permissions */
        // Processes -> 1
        SeedPermission(1, 1, "Visualizar la sección de bienestar", "View the wellness section",
            "Permite visualizar la sección de bienestar con las opciones que estén disponibles", "Allows you to view the wellness section with the options that are available",
            "ViewWellness", modelBuilder);
        // Employees -> 2
        SeedPermission(2, 2, "Visualizar la sección de perfil sociodemográfico", "View the sociodemographic profile section",
            "Permite visualizar sección del perfil sociodemográfico con las opciones que estén disponibles", "Allows you to view a section of the sociodemographic profile with the options that are available",
            "ViewSocioDemo", modelBuilder);
        // OHSMS -> 3
        SeedPermission(3, 3, "Visualizar la sección de exámenes ocupacionales", "View the occupational exams section",
            "Permite visualizar sección de exámenes ocupacionales con las opciones que estén disponibles", "Allows you to view the occupational exam section with the options that are available",
            "ViewOccupational", modelBuilder);
        SeedPermission(4, 3, "Visualizar la sección de COPASST", "View the Joint Health and Safety Committee (JHSC) section",
            "Permite visualizar sección de COPASST con las opciones que estén disponibles", "Allows you to view JHSC section with the options that are available",
            "ViewJHSC", modelBuilder);
        // Setting -> 4
        SeedPermission(5, 4, "Visualizar sección de configuración", "View setting section",
            "Permite visualizar sección de configuración asociada con mi cuenta", "Allows to view the setting section associated with my account",
            "ViewAccountSettings", modelBuilder);

        /* Seed PermissionGroups */
        SeedPermissionGroup(1, "Procesos", "Processes", modelBuilder);
        SeedPermissionGroup(2, "Colaboradores", "Employees", modelBuilder);
        SeedPermissionGroup(3, "SG-SST", "OHSMS", modelBuilder);
        SeedPermissionGroup(4, "Configuración", "Setting", modelBuilder);

        /* Seed Evaluation Criteria Types */
        SeedEvaluationCriteriaType(1, "Criterio objetivo", "Objective criteria", 70, false, false, modelBuilder);
        SeedEvaluationCriteriaType(2, "Criterio subjetivo", "Subjective criteria", 30, false, false, modelBuilder);

        /* Seed Default Evaluation Criterias */
        SeedDefaultEvaluationCriteria(1, 1, "Cumplimiento de metas y objetivos", "Achievement of goals and objectives", "Evaluar si el empleado ha cumplido con las metas y objetivos establecidos para su cargo. Esto incluye la cantidad de trabajo realizado, la calidad y la eficiencia.", "Evaluate whether the employee has met the goals and objectives established for his or her position. This includes quantity of work performed, quality and efficiency.", 25, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(2, 1, "Competencias técnicas", "Technical competence", "Evaluar el dominio de las habilidades técnicas requeridas para el cargo, como el manejo de herramientas.", "Evaluate the mastery of the technical skills required for the position, such as tool handling.", 25, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(3, 1, "Puntualidad y cumplimiento de horario ", "Punctuality and compliance with schedule", "Cumplimiento del horario de trabajo, respeto por los tiempos establecidos y asistencia regular a sus compromisos.", "Adherence to the work schedule, respect for established times and regular attendance to commitments.", 25, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(4, 1, "Entrega de proyectos y tareas", "Delivery of projects and tasks", "Evaluar si el empleado entrega sus proyectos y tareas dentro de los plazos establecidos y con el nivel de calidad esperado", "Evaluate whether the employee delivers projects and tasks within the established deadlines and with the expected level of quality.", 25, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(5, 2, "Trabajo en equipo y colaboración", "Teamwork and collaboration", "Evaluar cómo el empleado se integra al equipo, colabora con sus compañeros y contribuye al buen ambiente laboral.", "Evaluate how the employee integrates into the team, collaborates with colleagues and contributes to a good work environment.", 40, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(6, 2, "Adaptabilidad y flexibilidad", "Adaptability and flexibility", "Capacidad del empleado para adaptarse a cambios, nuevas herramientas o metodologías de trabajo, y responder adecuadamente a situaciones imprevistas.", "Employee's ability to adapt to changes, new tools or work methodologies, and to respond appropriately to unforeseen situations.", 30, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(7, 2, "Actitud y proactividad", "Attitude and proactivity", "Evaluar la actitud general del empleado hacia su trabajo y si toma la iniciativa para resolver problemas o mejorar procesos sin esperar indicaciones.", "Evaluate the employee's general attitude towards his or her work and whether he or she takes the initiative to solve problems or improve processes without waiting for directions.", 30, false, false, modelBuilder);
        SeedDefaultEvaluationCriteria(8, 2, "Criterio por defecto", "Defualt criteria", "Criterio por defecto.", "Defualt criteria.", 30, false, false, modelBuilder);

        /* Seed Default Evaluation Criteria Scores */
        SeedDefaultEvaluationCriteriaScore(1, 1, "No cumple con las metas establecidas.", "Does not meet established goals.", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(2, 1, "Cumple algunas metas, pero necesita mejorar.", "Meets some goals, but needs improvement.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(3, 1, "Cumple con la mayoría de las metas.", "Meets most goals.", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(4, 1, "Excede las expectativas, superando las metas.", "Exceeds expectations, exceeding goals.", 76, 100, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(5, 2, "Habilidades técnicas insuficientes.", "Insufficient technical skills.", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(6, 2, "Buen dominio, pero requiere mejorar.", "Good domain, but requires improvement.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(7, 2, "Buen nivel técnico, adecuado para su posición.", "Good technical level, adequate for position.", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(8, 2, "Experto en su área técnica, superando las expectativas.", "Expert in technical area, exceeding expectations.", 76, 100, true, false, false, modelBuilder);
       
        SeedDefaultEvaluationCriteriaScore(9, 4, "Frecuentemente incumple con los plazos.", "Frequently fails to meet deadlines.", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(10, 4, "Cumple con los plazos la mayor parte del tiempo.", "Meets deadlines most of the time.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(11, 4, "Cumple siempre con los plazos establecidos.", "Always meets deadlines.", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(12, 4, "Entrega antes de los plazos y con alta calidad", "Delivery before deadlines and with high quality", 76, 100, true, false, false, modelBuilder);
        
        SeedDefaultEvaluationCriteriaScore(13, 3, "Alta tasa de ausencias y/o impuntualidad.", "High rate of absenteeism and/or lateness.", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(14, 3, "Algunas ausencias y retrasos ocasionales.", "Some occasional absences and delays.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(15, 3, "Cumple regularmente con su horario.", "Regularly meets schedule.", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(16, 3, "Excelente asistencia y puntualidad.", "Excellent attendance and punctuality.", 76, 100, true, false, false, modelBuilder);
       
        SeedDefaultEvaluationCriteriaScore(17, 5, "Dificultades frecuentes para trabajar en equipo.", "Frequent difficulties working as part of a team.", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(18, 5, "Colabora ocasionalmente, pero podría mejorar.", "Collaborates occasionally, but could improve.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(19, 5, "Colabora efectivamente con su equipo.", "Collaborates effectively with his team.", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(20, 5, "Excelente trabajo en equipo, fomenta la colaboración.", "Excellent teamwork, encourages collaboration.", 76, 100, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(21, 6, "Dificultades para adaptarse a cambios", "Difficulties adapting to changes", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(22, 6, "Se adapta, pero requiere orientación constante.", "Adapts, but requires constant guidance.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(23, 6, "Se adapta fácilmente a nuevos cambios.", "Adapts easily to new changes", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(24, 6, "Sobresale en situaciones cambiantes, demostrando gran flexibilidad.", "Excels in changing situations, demonstrating great flexibility.", 76, 100, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(25, 7, "Actitud negativa o falta de proactividad.", "Negative attitude or lack of proactivity.", 0, 25, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(26, 7, "Actitud positiva, pero proactividad limitada.", "Positive attitude, but limited proactivity.", 26, 50, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(27, 7, "Buena actitud y proactividad constante.", "Good attitude and constant proactivity.", 51, 75, true, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(28, 7, "Sobresaliente, siempre toma la iniciativa y busca mejoras.", "Outstanding, always takes initiative and seeks improvement.", 76, 100, true, false, false, modelBuilder);

        SeedDefaultEvaluationCriteriaScore(29, 8, "No cumple ", "Does not meet", 0, 25, false, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(30, 8, "Cumple algunas veces", "Meets sometimes", 26, 50, false, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(31, 8, "Cumple la mayoría de las veces", "Meets most of the time", 51, 75, false, false, false, modelBuilder);
        SeedDefaultEvaluationCriteriaScore(32, 8, "Cumple todas las veces", "Meets all the time", 76, 100, false, false, false, modelBuilder);

        /* Seed Survey Mandatory Types */
        SeedSurveyQuestionMandatoryType(1, "Requerido", "Required", false, false, modelBuilder);
        SeedSurveyQuestionMandatoryType(2, "Opcional", "Optional", false, false, modelBuilder);

        /* Seed Survey Question Types */
        SeedSurveyQuestionType(1, "Texto corto", "Short text", false, false, modelBuilder);

        /* Seed Form Answer Group States */
        SeedFormAnswerGroupState(1, "Ninguno", "None", false, false, modelBuilder);
        SeedFormAnswerGroupState(2, "Aprobado", "Approved", false, false, modelBuilder);
        SeedFormAnswerGroupState(3, "Negado", "Denied", false, false, modelBuilder);
        SeedFormAnswerGroupState(4, "Pendiente", "Pending", false, false, modelBuilder);
    }

    public static void SeedAssignationType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignationType>().HasData(
        new AssignationType
        (
            new AssignationTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedCollaboratorStates(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollaboratorState>().HasData(
        new CollaboratorState
        (
            new CollaboratorStateId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultBrigadeAdjustment(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultBrigadeAdjustment>().HasData(
        new DefaultBrigadeAdjustment
        (
            new DefaultBrigadeAdjustmentId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultEducationStage(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEducationStage>().HasData(
        new DefaultEducationStage
        (
            new DefaultEducationStageId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultEventReplay(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEventReplay>().HasData(
        new DefaultEventReplay
        (
            new DefaultEventReplayId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultTimeZone(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultTimeZone>().HasData(
        new DefaultTimeZone
        (
            new DefaultTimeZoneId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultContractType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultContractType>().HasData(
        new DefaultContractType
        (
            new DefaultContractTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDocumentManagementFileType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentManagementFileType>().HasData(
        new DocumentManagementFileType
        (
            new DocumentManagementFileTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultMonth(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultMonth>().HasData(
        new DefaultMonth
        (
            new DefaultMonthId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultDaysOfWeek(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultDaysOfWeek>().HasData(
        new DefaultDaysOfWeek
        (
            new DefaultDaysOfWeekId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultCurrencyType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultCurrencyType>().HasData(
        new DefaultCurrencyType
        (
            new DefaultCurrencyTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultEmergencyPlanType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEmergencyPlanType>().HasData(
        new DefaultEmergencyPlanType
        (
            new DefaultEmergencyPlanTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultCollaboratorContract(int id, string arl, string bonus, string contractType, decimal salary, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultCollaboratorContract>().HasData(
        new DefaultCollaboratorContract
        (
            new DefaultCollaboratorContractId(id),
            arl,
            bonus,
            contractType,
            salary
        ));
    }

    public static void SeedDefaultAssignation(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultAssignation>().HasData(
        new DefaultAssignation
        (
            new DefaultAssignationId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultEventType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEventType>().HasData(
        new DefaultEventType
        (
            new DefaultEventTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultArea(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultArea>().HasData(
        new DefaultArea
        (
            new DefaultAreaId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultStudyType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultStudyType>().HasData(
        new DefaultStudyType
        (
            new DefaultStudyTypeId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultStudyArea(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultStudyArea>().HasData(
        new DefaultStudyArea
        (
            new DefaultStudyAreaId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultKnowledgeLevel(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultKnowledgeLevel>().HasData(
        new DefaultKnowledgeLevel
        (
            new DefaultKnowledgeLevelId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultBank(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultBank>().HasData(
        new DefaultBank
        (
            new DefaultBankId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultSoftSkill(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultSoftSkill>().HasData(
        new DefaultSoftSkill
        (
            new DefaultSoftSkillId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultFileType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultFileType>().HasData(
        new DefaultFileType
        (
            new DefaultFileTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultProfession(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultProfession>().HasData(
        new DefaultProfession
        (
            new DefaultProfessionId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultLanguageType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultLanguageLevel>().HasData(
        new DefaultLanguageLevel
        (
            new DefaultLanguageLevelId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultLifePreference(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultLifePreference>().HasData(
        new DefaultLifePreference
        (
            new DefaultLifePreferenceId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultTechnologyName(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultTechnologyName>().HasData(
        new DefaultTechnologyName
        (
            new DefaultTechnologyNameId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultLanguageLevel(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultLanguageType>().HasData(
        new DefaultLanguageType
        (
            new DefaultLanguageTypeId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultTag(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultTag>().HasData(
        new DefaultTag
        (
            new DefaultTagId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultTypeAccount(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultTypeAccount>().HasData(
        new DefaultTypeAccount
        (
            new DefaultTypeAccountId(id),
            name,
            nameEnglish
        ));
    }
    
    public static void SeedDefaultQuestionType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultQuestionType>().HasData(
        new DefaultQuestionType
        (
            new DefaultQuestionTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultRepeatEveryEvent(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultRepeatEveryEvent>().HasData(
        new DefaultRepeatEveryEvent
        (
            new DefaultRepeatEveryEventId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultEducationalLevel(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEducationalLevel>().HasData(
        new DefaultEducationalLevel
        (
            new DefaultEducationalLevelId(id),
            name,
            nameEnglish
        ));
    }
    public static void SeedDefaultRiskType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultRiskType>().HasData(
        new DefaultRiskType
        (
            new DefaultRiskTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultFamilyComposition(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultFamilyComposition>().HasData(
        new DefaultFamilyComposition
        (
            new DefaultFamilyCompositionId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultPension(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultPension>().HasData(
        new DefaultPension
        (
            new DefaultPensionId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultPosition(int id, string name, string nameEnglish, string description, string descriptionEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultPosition>().HasData(
        new DefaultPosition
        (
            new DefaultPositionId(id),

            name,
            nameEnglish,

            description,
            descriptionEnglish
        ));
    }

    public static void SeedDefaultProfessionalAdvice(int id, string name, string nameEnglish, string nameAcronyms, string nameAcronymsEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultProfessionalAdvice>().HasData(
        new DefaultProfessionalAdvice
        (
            new DefaultProfessionalAdviceId(id),
            name,
            nameEnglish,
            nameAcronyms,
            nameAcronymsEnglish
        ));
    }

    public static void SeedDefaultRole(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultRole>().HasData(
        new DefaultRole
        (
            new DefaultRoleId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDefaultSeveranceBenefit(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultSeveranceBenefit>().HasData(
        new DefaultSeveranceBenefit
        (
            new DefaultSeveranceBenefitId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedDocumentType(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentType>().HasData(
        new DocumentType
        (
            new DocumentTypeId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedEconomicLevel(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EconomicLevel>().HasData(
        new EconomicLevel
        (
            new EconomicLevelId(id),
            name,
            nameEnglish
        ));
    }

    public static void SeedMaritalStatus(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaritalStatus>().HasData(
            new MaritalStatus
            (
                new MaritalStatusId(id),
                name,
                nameEnglish
            ));
    }

    public static void SeedPriorityNovelties(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriorityNovelty>().HasData(
            new PriorityNovelty
            (
                new PriorityNoveltyId(id),

                name,
                nameEnglish
            ));
    }

    public static void SeedNotificationType(int id, string message, string messageEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationType>().HasData(
            new NotificationType
            (
                new NotificationTypeId(id),

                message,
                messageEnglish
            ));
    }

    public static void SeedPermission(int id, int permissionGroupId, string name, string nameEnglish, string description, string descriptionEnglish, string validationString, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>().HasData(
        new Permission
        (
            new PermissionId(id),

            new PermissionGroupId(permissionGroupId),

            name,
            nameEnglish,

            description,
            descriptionEnglish,

            validationString
        ));
    }

    public static void SeedPermissionGroup(int id, string name, string nameEnglish, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionGroup>().HasData(
        new PermissionGroup
        (
            new PermissionGroupId(id),

            name,
            nameEnglish
        ));
    }

    /* Seed Evaluation Criteria Types */
    public static void SeedEvaluationCriteriaType(int id, string name, string nameEnglish, int value, bool isEditable, bool isDeleteable, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EvaluationCriteriaType>().HasData(
        new EvaluationCriteriaType
        (
            new EvaluationCriteriaTypeId(id),

            name,
            nameEnglish,

            value,

            isEditable,
            isDeleteable
        ));
    }

    /* Seed Default Evaluation Criterias */
    public static void SeedDefaultEvaluationCriteria(int id, int evaluationCriteriaTypeId, string name, string nameEnglish, string description, string descriptionEnglish, int percentage, bool isEditable, bool isDeleteable,
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEvaluationCriteria>().HasData(
        new DefaultEvaluationCriteria
        (
            new DefaultEvaluationCriteriaId(id),

            evaluationCriteriaTypeId,

            name,
            nameEnglish,

            description,
            descriptionEnglish,

            percentage,

            isEditable,
            isDeleteable
        ));
    }

    /* Seed Default Evaluation Criteria Scores */
    public static void SeedDefaultEvaluationCriteriaScore(int id, int defaultEvaluationCriteriaId, string description, string descriptionEnglish, int lowerScore, int upperScore, 
        bool isForDefaultCriterias, bool isEditable, bool isDeleteable, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultEvaluationCriteriaScore>().HasData(
        new DefaultEvaluationCriteriaScore
        (
            new DefaultEvaluationCriteriaScoreId(id),

            defaultEvaluationCriteriaId,

            description,
            descriptionEnglish,

            lowerScore,
            upperScore,

            isForDefaultCriterias,

            isEditable,
            isDeleteable
        ));
    }

    /* Seed Survey Mandatory Types */
    public static void SeedSurveyQuestionMandatoryType(int id, string name, string nameEnglish, bool isEditable, bool isDeleteable, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SurveyQuestionMandatoryType>().HasData(
        new SurveyQuestionMandatoryType
        (
            new SurveyQuestionMandatoryTypeId(id),

            name,
            nameEnglish,

            isEditable,
            isDeleteable
        ));
    }

    /* Seed Survey Mandatory Types */
    public static void SeedSurveyQuestionType(int id, string name, string nameEnglish, bool isEditable, bool isDeleteable, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SurveyQuestionType>().HasData(
        new SurveyQuestionType
        (
            new SurveyQuestionTypeId(id),

            name,
            nameEnglish,

            isEditable,
            isDeleteable
        ));
    }

    /* Seed Form Answer Group States */
    public static void SeedFormAnswerGroupState(int id, string name, string nameEnglish, bool isEditable, bool isDeleteable, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FormAnswerGroupState>().HasData(
        new FormAnswerGroupState
        (
            new FormAnswerGroupStateId(id),

            name,
            nameEnglish,

            isEditable,
            isDeleteable
        ));
    }
}
