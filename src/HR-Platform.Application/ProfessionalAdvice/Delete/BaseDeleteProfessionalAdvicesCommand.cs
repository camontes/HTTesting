using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Delete;

public record BaseDeleteProfessionalAdvicesCommand(List<Guid> ProfessionalAdvicesList) : IRequest<ErrorOr<bool>>;

