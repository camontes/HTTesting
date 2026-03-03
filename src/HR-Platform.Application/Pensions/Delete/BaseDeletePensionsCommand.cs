using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Pensions.Delete;

public record BaseDeletePensionsCommand(List<Guid> PensionsList) : IRequest<ErrorOr<bool>>;

