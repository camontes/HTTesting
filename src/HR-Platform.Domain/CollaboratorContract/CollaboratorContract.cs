using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.DefaultCurrencyTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Contracts;

public sealed class CollaboratorContract : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private CollaboratorContract()
    {
    }

    public CollaboratorContract(CollaboratorContractId id, CompanyId companyId, string salary, ContractTypeId contractTypeId, DefaultCurrencyTypeId defaultCurrencyTypeId, string arl, string bonus, string emailWhoChangedByTH, string nameWhoChangedByTH, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Arl = arl;
        Bonus = bonus;
        Salary = salary;

        ContractTypeId = contractTypeId;
        DefaultCurrencyTypeId = defaultCurrencyTypeId;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorContractId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Arl { get; set; } = string.Empty;
    public string Bonus { get; set; } = string.Empty;

    public ContractTypeId ContractTypeId { get; set; }
    public ContractType ContractTypes { get; set; }

    public DefaultCurrencyTypeId DefaultCurrencyTypeId { get; set; }
    public DefaultCurrencyType DefaultCurrencyTypes { get; set; }

    public string Salary { get; set; } = string.Empty;

    public string EmailWhoChangedByTH { get; set; }
    public string NameWhoChangedByTH { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<Collaborator> Collaborators { get; set; }
}

