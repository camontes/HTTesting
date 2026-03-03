using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateCoexistenceCommitteeMinutesCommand(
    string EmailChangeBy,
    Guid CompanyId,
    List<CreateCoexistenceCommitteeMinutesObjectCommand> CoexistenceCommitteeMinutesList
) : IRequest<ErrorOr<bool>>;

public record CreateCoexistenceCommitteeMinutesObjectCommand(
    IFormFile CoexistenceCommitteeMinuteFile,
    string FileName,
    string UrlFile
);

