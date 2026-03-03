using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Create;

public record CreateBaseEvidenceCoexistenceCommitteeVoteCommand
(
    List<IFormFile> EvidenceCoexistenceCommitteeVoteFiles
) : IRequest<ErrorOr<bool>>;


