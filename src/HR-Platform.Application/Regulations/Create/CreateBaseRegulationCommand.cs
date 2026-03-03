using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Regulations.Create;

public record CreateBaseRegulationCommand
(
    List<IFormFile> RegulationFiles
) : IRequest<ErrorOr<bool>>;


