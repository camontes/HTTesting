using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateEvidenceCoexistenceCommitteeVotesCommand(
    string EmailChangeBy,
    Guid CompanyId,
    List<CreateEvidenceCoexistenceCommitteeVotesObjectCommand> EvidenceCoexistenceCommitteeVotesList
) : IRequest<ErrorOr<bool>>;

public record CreateEvidenceCoexistenceCommitteeVotesObjectCommand(
    IFormFile EvidenceCoexistenceCommitteeVoteFile,
    string FileName,
    string UrlFile
);

