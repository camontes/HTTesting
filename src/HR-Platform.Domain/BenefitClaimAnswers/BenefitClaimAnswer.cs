using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.BenefitClaimAnswers;

public sealed class BenefitClaimAnswer : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private BenefitClaimAnswer()
    {
    }

    public BenefitClaimAnswer(BenefitClaimAnswerId id, CompanyId companyId, CollaboratorId collaboratorId, string benefitName,
        string details, string referenceNumber, bool isBenefitAccepeted, bool isAvailableForAll, int minimumMonthsConstraint,
        bool isAnotherContraint, string anotherContraint, string nameWhoManagedClaim, string emailWhoManagedClaim,
        TimeDate managementDate, bool hasDeleted, string nameWhoDeletedBenefitClaim, string emailWhoDeletedBenefitClaim, TimeDate deletedDate, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        CollaboratorId = collaboratorId;

        BenefitName = benefitName;
        Details = details;
        ReferenceNumber = referenceNumber;
        IsBenefitAccepeted = isBenefitAccepeted;

        IsAvailableForAll = isAvailableForAll;
        MinimumMonthsConstraint = minimumMonthsConstraint;
        IsAnotherContraint = isAnotherContraint;
        AnotherContraint = anotherContraint;
        NameWhoManagedClaim = nameWhoManagedClaim;
        EmailWhoManagedClaim = emailWhoManagedClaim;

        ManagementDate = managementDate;

        HasDeleted = hasDeleted;
        NameWhoDeletedBenefitClaim = nameWhoDeletedBenefitClaim;
        EmailWhoDeletedBenefitClaim = emailWhoDeletedBenefitClaim;
        DeletedDate = deletedDate;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BenefitClaimAnswerId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public string BenefitName {  get; set; } = string.Empty;  
    public string Details {  get; set; } = string.Empty;  
    public string ReferenceNumber {  get; set; } = string.Empty;  
    public bool IsBenefitAccepeted { get; set; }

    //New Fields
    public bool IsAvailableForAll { get; set; }
    public int MinimumMonthsConstraint { get; set; }
    public bool IsAnotherContraint { get; set; }
    public string AnotherContraint { get; set; } = string.Empty;
    public string NameWhoManagedClaim { get; set; } = string.Empty;
    public string EmailWhoManagedClaim { get; set; } = string.Empty;
    
    //new Fields 2
    public bool HasDeleted { get; set; }
    public string NameWhoDeletedBenefitClaim { get; set; } = string.Empty;
    public string EmailWhoDeletedBenefitClaim { get; set; } = string.Empty;
    public TimeDate DeletedDate { get; set; }


    public TimeDate ManagementDate {  get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; } 
}

