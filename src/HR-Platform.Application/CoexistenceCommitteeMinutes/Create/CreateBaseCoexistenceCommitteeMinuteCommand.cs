using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.Create;

public record CreateBaseCoexistenceCommitteeMinuteCommand
(
    List<IFormFile> CoexistenceCommitteeMinuteFiles
) : IRequest<ErrorOr<bool>>;


