using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Pensions.Delete;

public record DeletePensionsCommand
(
    List<Guid> PensionsList,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

