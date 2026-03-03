using HR_Platform.Application.Data;
using HR_Platform.Domain.ActiveBreaks;
using HR_Platform.Domain.Acts;
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
using HR_Platform.Domain.CollaboratorStates;
using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.ContractTypes;
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
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Genders;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
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
using HR_Platform.Domain.Permissions;
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
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.UnitMeasures;
using HR_Platform.Domain.WorkplaceEvidences;
using HR_Platform.Domain.WorkplaceInformations;
using HR_Platform.Domain.WorkplaceRecommendations;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.ImprovementPlanResponseFiles;
using HR_Platform.Domain.Surveys;
using HR_Platform.Domain.SurveyQuestionTypes;
using HR_Platform.Domain.SurveyQuestionMandatoryTypes;

using HR_Platform.Infrastructure.Persistence.Seed;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.SurveyQuestions;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormAnswerGroupStates;
using HR_Platform.Domain.FormAnswerGroupFiles;

namespace HR_Platform.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options, IPublisher publisher) : DbContext(options), IApplicationDBContext, IUnitOfWork
{
    private readonly IPublisher _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));

    public DbSet<ActiveBreak> ActiveBreaks { get; set; }
    public DbSet<Act> Acts { get; set; }
    public DbSet<AssignationType> AssignationTypes { get; set; }
    public DbSet<Assignation> Assignations { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<BenefitClaimAnswer> BenefitClaimAnswers { get; set; }
    public DbSet<BirthdayTemplateSetting> BirthdayTemplateSettings { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<BloodType> BloodTypes { get; set; }
    public DbSet<BrigadeAdjustment> BrigadeAdjustments { get; set; }
    public DbSet<BrigadeDocumentation> BrigadeDocumentations { get; set; }
    public DbSet<BrigadeInventory> BrigadeInventories { get; set; }
    public DbSet<BrigadeInventoryFile> BrigadeInventoryFiles { get; set; }
    public DbSet<BrigadeMember> BrigadeMembers { get; set; }
    public DbSet<CoexistenceCommitteeMinute> CoexistenceCommitteeMinutes { get; set; }
    public DbSet<Children> Children { get; set; }
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<CollaboratorState> CollaboratorStates { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ContractType> ContractTypes { get; set; }
    public DbSet<CollaboratorBenefitClaim> CollaboratorBenefitClaims { get; set; }
    public DbSet<CollaboratorBrigade> CollaboratorBrigades { get; set; }
    public DbSet<CollaboratorBrigadeInventory> CollaboratorBrigadeInventory { get; set; }
    public DbSet<CollaboratorBrigadeInventoryFile> CollaboratorBrigadeInventoryFile { get; set; }
    public DbSet<CollaboratorContract> CollaboratorContracts { get; set; }
    public DbSet<CollaboratorCriteria> CollaboratorCriteria { get; set; }
    public DbSet<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswer { get; set; }
    public DbSet<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswers { get; set; }
    public DbSet<CollaboratorEducation> CollaboratorEducations { get; set; }
    public DbSet<CollaboratorEvent> CollaboratorEvents { get; set; }
    public DbSet<CollaboratorGeneralInduction> CollaboratorGeneralInductions { get; set; }
    public DbSet<CollaboratorInduction> CollaboratorInductions { get; set; }
    public DbSet<CollaboratorLanguage> CollaboratorLanguages { get; set; }
    public DbSet<CollaboratorLifePreference> CollaboratorLifePreferences { get; set; }
    public DbSet<CollaboratorSoftSkill> CollaboratorSoftSkills { get; set; }
    public DbSet<CollaboratorTag> CollaboratorTag { get; set; }
    public DbSet<CollaboratorTechnologyTool> CollaboratorTechnologyTools { get; set; }
    public DbSet<CollaboratorTalentPool> CollaboratorTalentPool { get; set; }
    public DbSet<DefaultAssignation> DefaultAssignations { get; set; }
    public DbSet<DefaultBank> DefaultBanks { get; set; }
    public DbSet<DefaultBrigadeAdjustment> DefaultBrigadeAdjustments { get; set; }
    public DbSet<DefaultCollaboratorContract> DefaultCollaboratorContracts { get; set; }
    public DbSet<DefaultContractType> DefaultContractTypes { get; set; }
    public DbSet<DefaultCurrencyType> DefaultCurrencyTypes { get; set; }
    public DbSet<DefaultDaysOfWeek> DefaultDaysOfWeeks { get; set; }
    public DbSet<DefaultEducationalLevel> DefaultEducationalLevels { get; set; }
    public DbSet<DefaultEmergencyPlanType> DefaultEmergencyPlanTypes { get; set; }
    public DbSet<DefaultEducationStage> DefaultEducationStages { get; set; }
    public DbSet<DefaultEvaluationCriteria> DefaultEvaluationCriterias { get; set; }
    public DbSet<DefaultEvaluationCriteriaScore> DefaultEvaluationCriteriaScores { get; set; }
    public DbSet<DefaultEventReplay> DefaultEventReplays { get; set; }
    public DbSet<DefaultEventType> DefaultEventTypes { get; set; }
    public DbSet<DefaultFileType> DefaultFileTypes { get; set; }
    public DbSet<DefaultFamilyComposition> DefaultFamilyCompositions { get; set; }
    public DbSet<DefaultKnowledgeLevel> DefaultKnowledgeLevels { get; set; }
    public DbSet<DefaultLanguageLevel> DefaultLanguageLevels { get; set; }
    public DbSet<DefaultLanguageType> DefaultLanguageTypes { get; set; }
    public DbSet<DefaultLifePreference> DefaultLifePreferences { get; set; }
    public DbSet<DefaultMonth> DefaultMonths { get; set; }
    public DbSet<DefaultPension> DefaultPensions { get; set; }
    public DbSet<DefaultPosition> DefaultPositions { get; set; }
    public DbSet<DefaultProfessionalAdvice> DefaultProfessionalAdvices { get; set; }
    public DbSet<DefaultProfession> DefaultProfessions { get; set; }
    public DbSet<DefaultQuestionType> DefaultQuestionTypes { get; set; }
    public DbSet<DefaultRole> DefaultRoles { get; set; }
    public DbSet<DefaultRiskType> DefaultRiskTypes { get; set; }
    public DbSet<DefaultRepeatEveryEvent> DefaultRepeatEveryEvents { get; set; }
    public DbSet<DefaultSeveranceBenefit> DefaultSeveranceBenefits { get; set; }
    public DbSet<DefaultSoftSkill> DefaultSoftSkills { get; set; }
    public DbSet<DefaultStudyArea> DefaultStudyAreas { get; set; }
    public DbSet<DefaultStudyType> DefaultStudyTypes { get; set; }
    public DbSet<DefaultTypeAccount> DefaultTypeAccounts { get; set; }
    public DbSet<DefaultTimeZone> DefaultTimeZones { get; set; }
    public DbSet<DefaultTag> DefaultTags { get; set; }
    public DbSet<DefaultTechnologyName> DefaultTechnologyNames { get; set; }
    public DbSet<DocumentManagement> DocumentManagement { get; set; }
    public DbSet<DocumentManagementFileType> DocumentManagementFileTypes { get; set; }
    public DbSet<DreamMapAnswer> DreamMapAnswers { get; set; }
    public DbSet<DreamMapQuestion> DreamMapQuestions { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<DomainEmail> DomainEmails { get; set; }
    public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    public DbSet<EconomicLevel> EconomicLevels { get; set; }
    public DbSet<EducationalLevel> EducationalLevels { get; set; }
    public DbSet<EmergencyPlan> EmergencyPlans { get; set; }
    public DbSet<EmergencyPlanType> EmergencyPlanTypes { get; set; }
    public DbSet<EvaluationCriteria> EvaluationCriterias { get; set; }
    public DbSet<EvaluationCriteriaScore> EvaluationCriteriaScores { get; set; }
    public DbSet<EvidenceCoexistenceCommitteeVote> EvidenceCoexistenceCommitteeVotes { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<EventRecurrence> EventRecurrence { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<FamilyComposition> FamilyCompositions { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<FormAnswer> FormAnswers { get; set; }
    public DbSet<FormAnswerGroup> FormAnswerGroups { get; set; }
    public DbSet<FormAnswerGroupFile> FormAnswerGroupFiles { get; set; }
    public DbSet<FormAnswerGroupState> FormAnswerGroupStates { get; set; }
    public DbSet<FormQuestionsType> FormQuestionsTypes { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<HealthEntity> HealthEntities { get; set; }
    public DbSet<ImprovementPlan> ImprovementPlans { get; set; }
    public DbSet<ImprovementPlanResponse> ImprovementPlanResponses { get; set; }
    public DbSet<ImprovementPlanResponseFile> ImprovementPlanResponseFiles { get; set; }
    public DbSet<ImprovementPlanTask> ImprovementPlanTasks { get; set; }
    public DbSet<ImprovementPlanTaskFile> ImprovementPlanTaskFiles { get; set; }
    public DbSet<InductionFile> InductionFiles { get; set; }
    public DbSet<Induction> Induction { get; set; }
    public DbSet<MaritalStatus> MaritalStatuses { get; set; }
    public DbSet<MasterUser> MasterUsers { get; set; }
    public DbSet<Minute> Minutes { get; set; }
    public DbSet<NewCommunication> NewCommunications { get; set; }
    public DbSet<Note> Notes{ get; set; }
    public DbSet<NoteFile> NoteFiles{ get; set; }
    public DbSet<NoteViewer> NoteViewers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationNote> NotificationNotes { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }
    public DbSet<OccupationalTest> OccupationalTests { get; set; }
    public DbSet<OccupationalRecommendation> OccupationalRecommendations { get; set; }
    public DbSet<OrganizationChart> OrganizationCharts { get; set; }
    public DbSet<Pension> Pensions { get; set; }
    public DbSet<ProfessionalAdvice> ProfessionalAdvices { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<PriorityNovelty> PriorityNovelties { get; set; }
    public DbSet<QuestionType> QuestionTypes { get; set; }
    public DbSet<Regulation> Regulations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RiskTypeMain> RiskTypeMains { get; set; }
    public DbSet<Risk> Risks { get; set; }
    public DbSet<RolePermission> RolesPermission { get; set; }
    public DbSet<SeveranceBenefit> SeveranceBenefits { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<SurveyQuestionMandatoryType> SurveyQuestionMandatoryTypes { get; set; }
    public DbSet<SurveyQuestionType> SurveyQuestionTypes { get; set; }
    public DbSet<TypeAccount> TypeAccounts { get; set; }
    public DbSet<TalentPool> TalentPool { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<UnitMeasure> UnitMeasures { get; set; }
    public DbSet<WorkplaceEvidence> WorkplaceEvidences { get; set; }
    public DbSet<WorkplaceInformation> WorkplaceInformations { get; set; }
    public DbSet<WorkplaceRecommendation> WorkplaceRecommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder
        .HasDbFunction(() => DbFunctions.DbFunctions.RemoveAccents(default))
        .HasName("remove_accents")
        .HasSchema("public"); // Especifica el esquema correcto
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            IEnumerable<DomainEvent> domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Count != 0)
            .SelectMany(e => e.GetDomainEvents());

            int result = await base.SaveChangesAsync(cancellationToken);

            foreach (DomainEvent domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
