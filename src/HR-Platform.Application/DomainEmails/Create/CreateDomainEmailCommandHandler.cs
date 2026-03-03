using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DomainEmails.Create;

internal sealed class CreateDomainEmailCommandHandler : IRequestHandler<CreateDomainEmailCommand, ErrorOr<Guid>>
{
    private readonly IDomainEmailRepository _domainEmailRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDomainEmailCommandHandler(IDomainEmailRepository domainEmailRepository, IUnitOfWork unitOfWork)
    {
        _domainEmailRepository = domainEmailRepository ?? throw new ArgumentNullException(nameof(domainEmailRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateDomainEmailCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (MailDomain.Create(command.DomainEmail) is not MailDomain domain)
            return Error.Validation("Collaborators.DomainEmail", "DomainEmail has not valid format");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Collaborators.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        DomainEmail domainEmail = new(new DomainEmailId(Guid.NewGuid()),
           new CompanyId(Guid.Parse(command.CompanyId)),
           domain,
           creationDate,
           editionDate,
           command.IsMainDomainEmail
        );

        _domainEmailRepository.Add(domainEmail);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return domainEmail.Id.Value;
    }
}