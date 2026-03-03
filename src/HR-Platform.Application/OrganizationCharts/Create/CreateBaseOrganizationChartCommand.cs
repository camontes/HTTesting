using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.OrganizationCharts.Create;

public record CreateBaseOrganizationChartCommand
(
    bool IsByFile,
    bool IsByUrl,
    string? FileName,
    string? FileURL,
    IFormFile? OrganizationChartFiles
) : IRequest<ErrorOr<bool>>;


