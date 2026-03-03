using HR_Platform.Application.Data;
using HR_Platform.Application.Services;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.ActiveBreaks;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.BankAccounts;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.BirthdayTemplateSettings;
using HR_Platform.Domain.BloodTypes;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.ChildrenNamespace;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.CollaboratorLanguages;
using HR_Platform.Domain.CollaboratorLifePreferences;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.DefaultAssignations;
using HR_Platform.Domain.DefaultCollaboratorContracts;
using HR_Platform.Domain.DefaultContractTypes;
using HR_Platform.Domain.DefaultCurrencyTypes;
using HR_Platform.Domain.DefaultDaysOfWeeks;
using HR_Platform.Domain.DefaultEducationStages;
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
using HR_Platform.Domain.DefaultRepeatEveryEvents;
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
using HR_Platform.Domain.DocumentManagements;
using HR_Platform.Domain.DocumentTypes;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.DreamMapQuestions;
using HR_Platform.Domain.EconomicLevels;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.EmergencyContacts;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.EventRecurrences;
using HR_Platform.Domain.Events;
using HR_Platform.Domain.EventTypes;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using HR_Platform.Domain.FamilyCompositions;
using HR_Platform.Domain.FormAnswerGroupFiles;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormAnswerGroupStates;
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Genders;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.ImprovementPlanResponseFiles;
using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.MaritalStatuses;
using HR_Platform.Domain.MasterUsers;
using HR_Platform.Domain.Minutes;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.Notes;
using HR_Platform.Domain.NoteViewers;
using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.OccupationalRecommendations;
using HR_Platform.Domain.OccupationalTests;
using HR_Platform.Domain.OrganizationCharts;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.PermissionGroups;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.PriorityNovelties;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.QuestionTypes;
using HR_Platform.Domain.Regulations;
using HR_Platform.Domain.Risks;
using HR_Platform.Domain.RiskTypeMains;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.RolesPermissions;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.SurveyQuestionMandatoryTypes;
using HR_Platform.Domain.SurveyQuestions;
using HR_Platform.Domain.SurveyQuestionTypes;
using HR_Platform.Domain.Surveys;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.UnitMeasures;
using HR_Platform.Domain.WorkplaceEvidences;
using HR_Platform.Domain.WorkplaceInformations;
using HR_Platform.Domain.WorkplaceRecommendations;
using HR_Platform.Infrastructure.ExternalServices;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using HR_Platform.Infrastructure.MailerServices;
using HR_Platform.Infrastructure.MailerServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using HR_Platform.Infrastructure.PermissionsValidatorServices;
using HR_Platform.Infrastructure.Persistence;
using HR_Platform.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;

namespace HR_Platform.Infrastructure
;

public static class DependencyInjection
{
    [Obsolete("WebClient() is Obsolete")]
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddAuth(configuration);

        return services;
    }

    [Obsolete("WebClient() is Obsolete")]
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        SecretsManagerAmazonService secretsManagerAmazonService = new();

        string Region = configuration["Region"]!;
        string CognitoSecret = configuration["CognitoSecret"]!;

        string connectionString = string.Empty;

        CognitoSecretDTO cognitoSecretObject = JsonConvert.DeserializeObject<CognitoSecretDTO>(secretsManagerAmazonService.GetSecret(CognitoSecret, Region))!;

        services
             .AddAuthentication()
             .AddJwtBearer("collaborators", options =>
             {
                 options.SaveToken = true;
                 options.Audience = cognitoSecretObject.AppClientId;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                     {
                         // Get JsonWebKeySet from AWS
                         string? json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                         // Serialize the result
                         return JsonConvert.DeserializeObject<JsonWebKeySet>(json)?.Keys;
                     },
                     ValidateIssuer = true,
                     ValidIssuer = $"https://cognito-idp.{Region}.amazonaws.com/{cognitoSecretObject.PoolId}",
                     ValidateLifetime = true,
                     LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                     ValidateAudience = false,
                     ValidAudience = cognitoSecretObject.AppClientId,
                 };
             });

        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("collaborators")
                .Build());

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        SecretsManagerAmazonService secretsManagerAmazonService = new();

        string Region = configuration["Region"]!;
        string ConnectionStringSecret = configuration["ConnectionStringSecret"]!;

        string connectionString = string.Empty;

        ConnectionStringDTO connectionStringObject = JsonConvert.DeserializeObject<ConnectionStringDTO>(secretsManagerAmazonService.GetSecret(ConnectionStringSecret, Region))!;

        connectionString = $"Server={connectionStringObject.Server};Database={connectionStringObject.Database};" +
            $"Port={connectionStringObject.Port};User Id={connectionStringObject.UserId};Password={connectionStringObject.Password};";

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IApplicationDBContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IActiveBreakRepository, ActiveBreakRepository>();
        //services.AddScoped<IActRepository, ActRepository>();
        services.AddScoped<IAssignationTypeRepository, AssignationTypeRepository>();
        services.AddScoped<IAreaRepository, AreaRepository>();
        services.AddScoped<IAssignationRepository, AssignationRepository>();
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IBenefitRepository, BenefitRepository>();
        services.AddScoped<IBenefitClaimAnswerRepository, BenefitClaimAnswerRepository>();
        services.AddScoped<IBirthdayTemplateSettingRepository, BirthdayTemplateSettingRepository>();
        services.AddScoped<IBloodTypeRepository, BloodTypeRepository>();
        services.AddScoped<IBrigadeAdjustmentRepository, BrigadeAdjustmentRepository>();
        services.AddScoped<IBrigadeDocumentationRepository, BrigadeDocumentationRepository>();
        services.AddScoped<IBrigadeInventoryRepository, BrigadeInventoryRepository>();
        services.AddScoped<IBrigadeInventoryFileRepository, BrigadeInventoryFileRepository>();
        services.AddScoped<IBrigadeMemberRepository, BrigadeMemberRepository>();
        services.AddScoped<IContractTypeRepository, ContractTypeRepository>();
        services.AddScoped<IChildrenRepository, ChildrenRepository>();
        services.AddScoped<ICoexistenceCommitteeMinuteRepository, CoexistenceCommitteeMinuteRepository>();
        services.AddScoped<ICollaboratorBenefitClaimRepository, CollaboratorBenefitClaimRepository>();
        services.AddScoped<ICollaboratorBrigadeInventoryFileRepository, CollaboratorBrigadeInventoryFileRepository>();
        services.AddScoped<ICollaboratorBrigadeInventoryRepository, CollaboratorBrigadeInventoryRepository>();
        services.AddScoped<ICollaboratorBrigadeRepository, CollaboratorBrigadeRepository>();
        services.AddScoped<ICollaboratorCriteriaRepository, CollaboratorCriteriaRepository>();
        services.AddScoped<ICollaboratorCriteriaAnswerRepository, CollaboratorCriteriaAnswerRepository>();
        services.AddScoped<ICollaboratorDreamMapAnswerRepository, CollaboratorDreamMapAnswerRepository>();
        services.AddScoped<ICollaboratorEducationRepository, CollaboratorEducationRepository>();
        services.AddScoped<ICollaboratorEventRepository, CollaboratorEventRepository>();
        services.AddScoped<ICollaboratorGeneralInductionRepository, CollaboratorGeneralInductionRepository>();
        services.AddScoped<ICollaboratorLifePreferenceRepository, CollaboratorLifePreferenceRepository>();
        services.AddScoped<ICollaboratorLanguageRepository, CollaboratorLanguageRepository>();
        services.AddScoped<ICollaboratorInductionRepository, CollaboratorInductionRepository>();
        services.AddScoped<ICollaboratorSoftSkillRepository, CollaboratorSoftSkillRepository>();
        services.AddScoped<ICollaboratorTagRepository, CollaboratorTagRepository>();
        services.AddScoped<ICollaboratorTalentPoolRepository, CollaboratorTalentPoolRepository>();
        services.AddScoped<ICollaboratorTechnologyToolRepository, CollaboratorTechnologyToolRepository>();
        services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
        //services.AddScoped<ICollaboratorStateRepository, CollaboratorStateRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICollaboratorContractRepository, CollaboratorContractRepository>();
        services.AddScoped<IDefaultAssignationRepository, DefaultAssignationRepository>();
        services.AddScoped<IDefaultCollaboratorContractRepository, DefaultCollaboratorContractRepository>();
        services.AddScoped<IDefaultContractTypeRepository, DefaultContractTypeRepository>();
        services.AddScoped<IDefaultCurrencyTypeRepository, DefaultCurrencyTypeRepository>();
        services.AddScoped<IDefaultCollaboratorContractRepository, DefaultCollaboratorContractRepository>();
        services.AddScoped<IDefaultDaysOfWeekRepository, DefaultDaysOfWeekRepository>();
        services.AddScoped<IDefaultEducationStageRepository, DefaultEducationStageRepository>();
        services.AddScoped<IDefaultEvaluationCriteriaRepository, DefaultEvaluationCriteriaRepository>();
        services.AddScoped<IDefaultEvaluationCriteriaScoreRepository, DefaultEvaluationCriteriaScoreRepository>();
        services.AddScoped<IDefaultEventReplayRepository, DefaultEventReplayRepository>();
        services.AddScoped<IDefaultEventTypeRepository, DefaultEventTypeRepository>();
        services.AddScoped<IDefaultFamilyCompositionRepository, DefaultFamilyCompositionRepository>();
        services.AddScoped<IDefaultFileTypeRepository, DefaultFileTypeRepository>();
        services.AddScoped<IDefaultLifePreferenceRepository, DefaultLifePreferenceRepository>();
        services.AddScoped<IDefaultLanguageTypeRepository, DefaultLanguageTypeRepository>();
        services.AddScoped<IDefaultLanguageLevelRepository, DefaultLanguageLevelRepository>();
        services.AddScoped<IDefaultKnowledgeLevelRepository, DefaultKnowledgeLevelRepository>();
        services.AddScoped<IDefaultMonthRepository, DefaultMonthRepository>();
        services.AddScoped<IDefaultPensionRepository, DefaultPensionRepository>();
        services.AddScoped<IDefaultPositionRepository, DefaultPositionRepository>();
        services.AddScoped<IDefaultProfessionalAdviceRepository, DefaultProfessionalAdviceRepository>();
        services.AddScoped<IDefaultProfessionRepository, DefaultProfessionRepository>();
        services.AddScoped<IDefaultRoleRepository, DefaultRoleRepository>();
        services.AddScoped<IDefaultSeveranceBenefitRepository, DefaultSeveranceBenefitRepository>();
        services.AddScoped<IDefaultStudyAreaRepository, DefaultStudyAreaRepository>();
        services.AddScoped<IDefaultStudyTypeRepository, DefaultStudyTypeRepository>();
        services.AddScoped<IDefaultSoftSkillRepository, DefaultSoftSkillRepository>();
        services.AddScoped<IDefaultTechnologyNameRepository, DefaultTechnologyNameRepository>();
        services.AddScoped<IDefaultTypeAccountRepository, DefaultTypeAccountRepository>();
        services.AddScoped<IDefaultTagRepository, DefaultTagRepository>();
        services.AddScoped<IDefaultTimeZoneRepository, DefaultTimeZoneRepository>();
        services.AddScoped<IDefaultRepeatEveryEventRepository, DefaultRepeatEveryEventRepository>();
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IDocumentManagementRepository, DocumentManagementRepository>();
        services.AddScoped<IDocumentManagementFileTypeRepository, DocumentManagementFileTypeRepository>();
        services.AddScoped<IDomainEmailRepository, DomainEmailRepository>();
        services.AddScoped<IDreamMapQuestionRepository, DreamMapQuestionRepository>();
        services.AddScoped<IDreamMapAnswerRepository, DreamMapAnswerRepository>();
        services.AddScoped<IEconomicLevelRepository, EconomicLevelRepository>();
        services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
        services.AddScoped<IEmergencyPlanRepository, EmergencyPlanRepository>();
        services.AddScoped<IEmergencyPlanTypeRepository, EmergencyPlanTypeRepository>();
        services.AddScoped<IEducationalLevelRepository, EducationalLevelRepository>();
        services.AddScoped<IEvaluationCriteriaRepository, EvaluationCriteriaRepository>();
        services.AddScoped<IEvaluationCriteriaScoreRepository, EvaluationCriteriaScoreRepository>();
        services.AddScoped<IEvidenceCoexistenceCommitteeVoteRepository, EvidenceCoexistenceCommitteeVoteRepository>();
        services.AddScoped<IEventRecurrenceRepository, EventRecurrenceRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventTypeRepository, EventTypeRepository>();
        services.AddScoped<IFamilyCompositionRepository, FamilyCompositionRepository>();
        services.AddScoped<IFormAnswerRepository, FormAnswerRepository>();
        services.AddScoped<IFormAnswerGroupRepository, FormAnswerGroupRepository>();
        services.AddScoped<IFormAnswerGroupFileRepository, FormAnswerGroupFileRepository>();
        services.AddScoped<IFormAnswerGroupStateRepository, FormAnswerGroupStateRepository>();
        services.AddScoped<IFormQuestionsTypeRepository, FormQuestionsTypeRepository>();
        services.AddScoped<IFormRepository, FormRepository>();
        services.AddScoped<IGenderRepository, GenderRepository>();
        services.AddScoped<IHealthEntityRepository, HealthEntityRepository>();
        services.AddScoped<IImprovementPlanRepository, ImprovementPlanRepository>();
        services.AddScoped<IImprovementPlanResponseRepository, ImprovementPlanResponseRepository>();
        services.AddScoped<IImprovementPlanResponseFileRepository, ImprovementPlanResponseFileRepository>();
        services.AddScoped<IImprovementPlanTaskRepository, ImprovementPlanTaskRepository>();
        services.AddScoped<IImprovementPlanTaskFileRepository, ImprovementPlanTaskFileRepository>();
        services.AddScoped<IInductionRepository, InductionRepository>();
        services.AddScoped<IInductionFileRepository, InductionFileRepository>();
        services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
        services.AddScoped<IMinuteRepository, MinuteRepository>();
        services.AddScoped<IMasterUserRepository, MasterUserRepository>();
        services.AddScoped<INewCommunicationRepository, NewCommunicationRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationNoteRepository, NotificationNoteRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<INoteFileRepository, NoteFileRepository>();
        services.AddScoped<INoteViewerRepository, NoteViewerRepository>();
        services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
        services.AddScoped<IOccupationalRecommendationRepository, OccupationalRecommendationRepository>();
        services.AddScoped<IOccupationalTestRepository, OccupationalTestRepository>();
        services.AddScoped<IOrganizationChartRepository, OrganizationChartRepository>();
        services.AddScoped<IProfessionalAdviceRepository, ProfessionalAdviceRepository>();
        services.AddScoped<IPensionRepository, PensionRepository>();
        //services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IPriorityNoveltyRepository, PriorityNoveltyRepository>();
        services.AddScoped<IQuestionTypeRepository, QuestionTypeRepository>();
        services.AddScoped<IRegulationRepository, RegulationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRiskRepository, RiskRepository>();
        services.AddScoped<IRiskTypeMainRepository, RiskTypeMainRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<ISeveranceBenefitRepository, SeveranceBenefitRepository>();
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
        services.AddScoped<ISurveyQuestionMandatoryTypeRepository, SurveyQuestionMandatoryTypeRepository>();
        services.AddScoped<ISurveyQuestionTypeRepository, SurveyQuestionTypeRepository>();
        services.AddScoped<ITypeAccountRepository, TypeAccountRepository>();
        services.AddScoped<ITalentPoolRepository, TalentPoolRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUnitMeasureRepository, UnitMeasureRepository>();
        services.AddScoped<IWorkplaceEvidenceRepository, WorkplaceEvidenceRepository>();
        services.AddScoped<IWorkplaceInformationRepository, WorkplaceInformationRepository>();
        services.AddScoped<IWorkplaceRecommendationRepository, WorkplaceRecommendationRepository>();

        services.AddTransient<IAmazonS3Service, AmazonS3Service>();
        services.AddTransient<IAmazonCognitoService, AmazonCognitoService>();
        services.AddTransient<IPermissionsValidatorService, PermissionsValidatorService>();
        services.AddTransient<ISecretsManagerAmazonService, SecretsManagerAmazonService>();

        services.AddTransient<IMailerService, MailerService>();

        services.AddTransient<IEncryptService, EncryptService>();
        services.AddTransient<IRandomService, RandomService>();
        services.AddTransient<IStringService, StringService>();
        services.AddTransient<ITimeFormatService, TimeFormatService>();


        services.AddHttpClient();

        /* For Timestamp use */
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}

