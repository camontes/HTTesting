using ErrorOr;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Common;
using MediatR;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.GetByCompanyId;

public record GetBaseEvidenceCoexistenceCommitteeVoteByCompanyIdQuery(string Year) : IRequest<ErrorOr<List<EvidenceCoexistenceCommitteeVoteFileResponse>>>;