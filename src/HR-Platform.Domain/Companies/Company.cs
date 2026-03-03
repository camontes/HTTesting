using HR_Platform.Domain.ActiveBreaks;
using HR_Platform.Domain.Acts;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.BirthdayTemplateSettings;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.DreamMapQuestions;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.Events;
using HR_Platform.Domain.EventTypes;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Minutes;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.OrganizationCharts;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.QuestionTypes;
using HR_Platform.Domain.Regulations;
using HR_Platform.Domain.RiskTypeMains;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.Surveys;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Companies;

public sealed class Company : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Company()
    {
    }
    
    public Company(CompanyId id, Email email, Email requestsEmail, string name, string menuName, Address address, PhoneNumber phoneNumber, 
        string logo, string logoName, string url, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        Email = email;
        RequestsEmail = requestsEmail;

        Name = name;
        MenuName = menuName;

        Address = address;

        PhoneNumber = phoneNumber;

        Logo = logo;
        LogoName = logoName;

        URL = url;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

    #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CompanyId Id { get; set; }

    public Email Email { get; set; }
    public Email RequestsEmail { get; set; }

    public string Name { get; set; } = string.Empty;
    public string MenuName { get; set; }

    public Address Address { get; set; }

    public PhoneNumber PhoneNumber { get; set; }

    public string Logo { get; set; }
    public string LogoName { get; set; }

    public string URL { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<ActiveBreak> ActiveBreaks { get; set; }
    public List<Act> Acts { get; set; }
    public List<Area> Areas { get; set; }
    public List<Assignation> Assignations { get; set; }
    public List<Bank> Banks { get; set; }
    public List<Benefit> Benefits { get; set; }
    public List<BenefitClaimAnswer> BenefitClaimAnswers { get; set; }
    public List<BirthdayTemplateSetting> BirthdayTemplateSettings { get; set; }
    public List<BrigadeAdjustment> BrigadeAdjustments { get; set; }
    public List<BrigadeDocumentation> BrigadeDocumentations { get; set; }
    public List<BrigadeInventory> BrigadeInventories { get; set; }
    public List<CollaboratorBrigadeInventory> CollaboratorBrigadeInventories { get; set; }
    public List<CollaboratorBenefitClaim> CollaboratorBenefitClaims { get; set; }
    public List<CoexistenceCommitteeMinute> CoexistenceCommitteeMinutes { get; set; }
    public List<Collaborator> Collaborators { get; set; }
    public List<CollaboratorContract> CollaboratorContracts { get; set; }
    public List<ContractType> ContractTypes { get; set; }
    public List<DomainEmail> DomainEmails { get; set; }
    public List<DreamMapQuestion> DreamMapQuestions { get; set; }
    public List<EducationalLevel> EducationalLevels { get; set; }
    public List<EmergencyPlanType> EmergencyPlanTypes { get; set; }
    public List<EvidenceCoexistenceCommitteeVote> EvidenceCoexistenceCommitteeVotes { get; set; }
    public List<EventType> EventTypes { get; set; }
    public List<Event> Events { get; set; }
    public List<Form> Forms { get; set; }
    public List<HealthEntity> HealthEntities { get; set; }
    public List<Induction> Inductions { get; set; }
    public List<Minute> Minutes { get; set; }
    public List<NewCommunication> NewCommunications { get; set; }
    public List<Pension> Pensions { get; set; }
    public List<ProfessionalAdvice> ProfessionalAdvices { get; set; }
    public List<Position> Positions { get; set; }
    public List<QuestionType> QuestionTypes { get; set; }
    public List<OrganizationChart> OrganizationCharts { get; set; }
    public List<Role> Roles { get; set; }
    public List<RiskTypeMain> RiskTypeMains { get; set; }
    public List<Regulation> Regulations { get; set; }
    public List<SeveranceBenefit> SeveranceBenefits { get; set; }
    public List<Survey> Surveys { get; set; }
    public List<Tag> Tags { get; set; }
    public List<TalentPool> TalentPools { get; set; }
    public List<TypeAccount> TypeAccounts { get; set; }
}

