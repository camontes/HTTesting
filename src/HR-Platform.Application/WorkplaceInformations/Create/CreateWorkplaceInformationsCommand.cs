using ErrorOr;
using HR_Platform.Application.WorkplaceInformations.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceInformations.Create;

public record CreateWorkplaceInformationsCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<FileWorkplaceInformationFormatResponse> FormatFiles
) : IRequest<ErrorOr<bool>>;



