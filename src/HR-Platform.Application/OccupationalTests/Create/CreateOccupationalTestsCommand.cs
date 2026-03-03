using ErrorOr;
using HR_Platform.Application.OccupationalTests.Common;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateOccupationalTestsCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<FileFormatResponse> FormatFiles
) : IRequest<ErrorOr<bool>>;



