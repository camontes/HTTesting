using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateOrganizationChartsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    bool IsByFile,
    bool IsByUrl,
    string? FileName,
    string FileURL
) : IRequest<ErrorOr<bool>>;


