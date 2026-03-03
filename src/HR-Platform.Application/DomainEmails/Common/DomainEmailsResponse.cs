namespace HR_Platform.Application.DomainEmails.Common;

public record DomainEmailsResponse(
    Guid Id,

    Guid CompanyId,

    string Domain,

    bool IsMainDomainEmail
);
