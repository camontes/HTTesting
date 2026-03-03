using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Pensions.Create;

public record CreatePensionsCommand(List<PensionData> PensionsDataList) : IRequest<ErrorOr<bool>>;

public record PensionData(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
);

