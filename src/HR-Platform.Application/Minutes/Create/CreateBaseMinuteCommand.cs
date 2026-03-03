using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Minutes.Create;

public record CreateBaseMinuteCommand
(
    List<IFormFile> MinuteFiles
) : IRequest<ErrorOr<bool>>;


