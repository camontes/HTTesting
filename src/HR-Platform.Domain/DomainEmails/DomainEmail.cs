using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.DomainEmails;

public sealed class DomainEmail : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DomainEmail()
    {
    }

    public DomainEmail(DomainEmailId id, CompanyId companyId, MailDomain domain, TimeDate creationDate, TimeDate editionDate, bool isMainDomainEmail)
    {
        Id = id;

        CompanyId = companyId;

        Domain = domain;

        CreationDate = creationDate;
        EditionDate = editionDate;

        IsMainDomainEmail = isMainDomainEmail;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DomainEmailId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public MailDomain Domain { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public bool IsMainDomainEmail { get; set; }
}

