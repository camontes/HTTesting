using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.TalentPools;

public sealed class TalentPool : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private TalentPool()
    {
    }

    public TalentPool(TalentPoolId id, CompanyId companyId, string tittle, string description, bool isArchived, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Tittle = tittle;
        Description = description;
        IsArchived = isArchived;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public TalentPoolId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Tittle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsArchived { get; set; }
    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<CollaboratorTalentPool> CollaboratorTalentPools { get; set; }
}

