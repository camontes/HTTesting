using ErrorOr;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DomainEmails.Delete;

internal sealed class DeleteDomainEmailCommandHandler : IRequestHandler<DeleteDomainEmailCommand, ErrorOr<bool>>
{
    private readonly IDomainEmailRepository _domainEmailRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDomainEmailCommandHandler
    (
        IDomainEmailRepository domainEmailRepository,
        IUnitOfWork unitOfWork
    )
    {
        _domainEmailRepository = domainEmailRepository ?? throw new ArgumentNullException(nameof(domainEmailRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(DeleteDomainEmailCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        DomainEmail? domainEmailResult = await _domainEmailRepository.GetByIdAsync(new DomainEmailId(query.Id));

        if (domainEmailResult is null)
            return Error.Validation("DomainEmailId", "The DomainEmail with the provide Id was not found.");

        if (domainEmailResult.IsMainDomainEmail)
            return Error.Validation("DomainEmailId", "The main domain cannot be deleted");

        try
        {
            _domainEmailRepository.Delete(domainEmailResult);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}