using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateRegulationsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    List<CreateRegulationsObjectCommand> RegulationsList
) : IRequest<ErrorOr<bool>>;

public record CreateRegulationsObjectCommand(
    IFormFile RegulationFile,
    string FileName,
    string UrlFile
);

