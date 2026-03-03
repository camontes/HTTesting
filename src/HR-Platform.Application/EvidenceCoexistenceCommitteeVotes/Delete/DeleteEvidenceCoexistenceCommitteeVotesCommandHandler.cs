using ErrorOr;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Delete;

internal sealed class DeleteEvidenceCoexistenceCommitteeVoteCommandHandler(
    IEvidenceCoexistenceCommitteeVoteRepository evidenceCoexistenceCommitteeVoteRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteEvidenceCoexistenceCommitteeVotesCommand, ErrorOr<bool>>
{
    private readonly IEvidenceCoexistenceCommitteeVoteRepository _evidenceCoexistenceCommitteeVoteRepository = evidenceCoexistenceCommitteeVoteRepository ?? throw new ArgumentNullException(nameof(evidenceCoexistenceCommitteeVoteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteEvidenceCoexistenceCommitteeVotesCommand query, CancellationToken cancellationToken)
    {
        if (await _evidenceCoexistenceCommitteeVoteRepository.GetByIdAsync(new EvidenceCoexistenceCommitteeVoteId(query.EvidenceCoexistenceCommitteeVoteId)) is not EvidenceCoexistenceCommitteeVote evidenceCoexistenceCommitteeVote)
            return Error.NotFound("EvidenceCoexistenceCommitteeVote.NotFound", "The Evidence Coexistence Committee Vote with the provide Id was not found.");

        try
        {
            _evidenceCoexistenceCommitteeVoteRepository.Delete(evidenceCoexistenceCommitteeVote);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}