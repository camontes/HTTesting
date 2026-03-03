using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorBenefitClaims;

public sealed class CollaboratorBenefitClaim : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorBenefitClaim()
    {
    }

    public CollaboratorBenefitClaim(CollaboratorBenefitClaimId id, CompanyId companyId,  BenefitId benefitId, CollaboratorId collaboratorId, string referenceNumber, bool isAccepted, bool isAnySelected, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        BenefitId = benefitId;
        CollaboratorId = collaboratorId;

        ReferenceNumber = referenceNumber;

        IsAccepted = isAccepted;
        IsAnySelected = isAnySelected;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorBenefitClaimId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;

    public BenefitId BenefitId { get; set; }
    public Benefit Benefit { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public bool IsAccepted { get; set; }
    public bool IsAnySelected { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

