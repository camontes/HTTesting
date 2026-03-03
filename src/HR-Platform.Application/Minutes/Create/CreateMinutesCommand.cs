using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateMinutesCommand(
    string EmailChangeBy,
    Guid CompanyId,
    List<CreateMinutesObjectCommand> MinutesList
) : IRequest<ErrorOr<bool>>;

public record CreateMinutesObjectCommand(
    IFormFile MinuteFile,
    string FileName,
    string UrlFile
);

