using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Create;

internal sealed class CreateEvidenceCoexistenceCommitteeVotesCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IEvidenceCoexistenceCommitteeVoteRepository EvidenceCoexistenceCommitteeVoteRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateEvidenceCoexistenceCommitteeVotesCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IEvidenceCoexistenceCommitteeVoteRepository _evidenceCoexistenceCommitteeVoteRepository = EvidenceCoexistenceCommitteeVoteRepository ?? throw new ArgumentNullException(nameof(EvidenceCoexistenceCommitteeVoteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateEvidenceCoexistenceCommitteeVotesCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("EvidenceCoexistenceCommitteeVotes.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<EvidenceCoexistenceCommitteeVote> evidenceCoexistenceCommitteeVotes = [];

        foreach (CreateEvidenceCoexistenceCommitteeVotesObjectCommand item in command.EvidenceCoexistenceCommitteeVotesList)
        {
            EvidenceCoexistenceCommitteeVote result = new
            (
                new EvidenceCoexistenceCommitteeVoteId(Guid.NewGuid()),
                new CompanyId(command.CompanyId),
                item.FileName,
                item.UrlFile,
                command.EmailChangeBy,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                true,
                true,
                creationDate,
                creationDate
            );
            evidenceCoexistenceCommitteeVotes.Add(result);
        }

        if (evidenceCoexistenceCommitteeVotes.Count > 0)
        {
            _evidenceCoexistenceCommitteeVoteRepository.AddRange(evidenceCoexistenceCommitteeVotes);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}