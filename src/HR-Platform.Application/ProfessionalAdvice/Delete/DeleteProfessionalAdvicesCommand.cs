using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Delete;

public record DeleteProfessionalAdvicesCommand
(
    List<Guid> ProfessionalAdvicesList,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

