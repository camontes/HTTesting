using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Create;

public record CreateProfessionalAdvicesCommand(List<ProfessionalAdviceData> ProfessionalAdvicesDataList) : IRequest<ErrorOr<bool>>;

public record ProfessionalAdviceData(
    string CompanyId,
    string Name,
    string NameEnglish,
    string NameAcronyms,
    string NameAcronymsEnglish,
    bool IsEditable,
    bool IsDeleteable
);

