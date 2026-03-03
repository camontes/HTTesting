using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Inductions.Delete;

public record DeleteInductionCommand(Guid CompanyId, Guid InductionId, string EmailChangeBy) : IRequest<ErrorOr<bool>>;