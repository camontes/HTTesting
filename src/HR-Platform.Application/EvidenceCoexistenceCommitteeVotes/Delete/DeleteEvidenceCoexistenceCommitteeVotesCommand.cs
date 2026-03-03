using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Delete;

public record DeleteEvidenceCoexistenceCommitteeVotesCommand
(
    Guid EvidenceCoexistenceCommitteeVoteId
) : IRequest<ErrorOr<bool>>;

