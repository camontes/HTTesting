using ErrorOr;
using HR_Platform.Application.ProfessionalAdvices.Common;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Update;

public record UpdateProfessionalAdviceCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish,
    string NameAcronyms,
    string NameAcronymsEnglish
) : IRequest<ErrorOr<bool>>;
