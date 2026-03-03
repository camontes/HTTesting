using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Positions.Create;

public record BaseCreatePositionsCommand(
    //string CompanyId,

    string Name,
    string NameEnglish,

    string? Description,
    string? DescriptionEnglish,

    IFormFile? PositionFile
);

