using ErrorOr;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Create;

public record CreateSeveranceBenefitsCommand(List<SeveranceBenefitData> SeveranceBenefitsDataList) : IRequest<ErrorOr<bool>>;

public record SeveranceBenefitData(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
);

