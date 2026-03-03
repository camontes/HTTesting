using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateInductionsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    string Name,
    string Description,
    List<CreateInductionsObjectCommand> InductionsList
) : IRequest<ErrorOr<bool>>;

public record CreateInductionsObjectCommand(
    IFormFile InductionFile,
    string FileName,
    string UrlFile
);

