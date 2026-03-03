using ErrorOr;
using HR_Platform.Application.OccupationalTests.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.OccupationalTests.Create;

public record CreateBaseOccupationalTestCommand
(
    Guid CollaboratorId,
    List<IFormFile> Files,
    List<int> FileTypeIds,
    List<string>? Others

) : IRequest<ErrorOr<bool>>;


