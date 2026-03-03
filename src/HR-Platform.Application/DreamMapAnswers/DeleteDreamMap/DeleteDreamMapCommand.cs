using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.DeleteDreamMap;

public record DeleteDreamMapCommand(Guid CollaboratorDreamMapAnswerId) : IRequest<ErrorOr<bool>>;