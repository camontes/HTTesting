using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Update;

public record UpdateInductionsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    Guid InductionId,
    string Name,
    string Description,
    bool HasChangedFiles,
    List<Guid>? FileNamesDeleted,
    List<UpdateInductionsObjectCommand> InductionsList
) : IRequest<ErrorOr<bool>>;

public record UpdateInductionsObjectCommand(
    IFormFile InductionFile,
    string FileName,
    string UrlFile
);

