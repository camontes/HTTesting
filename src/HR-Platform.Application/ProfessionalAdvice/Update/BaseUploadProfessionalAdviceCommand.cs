using ErrorOr;
using HR_Platform.Application.ProfessionalAdvices.Common;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Update;

public record BaseUpdateProfessionalAdviceCommand
(
    Guid Id,
    string Name,
    string NameAcronyms

) : IRequest<ErrorOr<bool>>;
