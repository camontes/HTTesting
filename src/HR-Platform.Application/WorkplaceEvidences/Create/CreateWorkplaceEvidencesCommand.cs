using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.Create;

public record CreateWorkplaceEvidencesCommand(
    string EmailChangeBy,
    string? CollaboratorId,
    List<FileWorkplaceEvidenceFormatResponse> FormatFiles
) : IRequest<ErrorOr<bool>>;



