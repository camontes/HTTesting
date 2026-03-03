using ErrorOr;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.Delete;

public record DeleteWorkplaceEvidencesCommand
(
    Guid WorkplaceEvidenceId
) : IRequest<ErrorOr<bool>>;

