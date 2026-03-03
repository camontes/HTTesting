using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Update;

public record UpdateNewCommunicationsCommand(
    Guid NewCommunicationId,
    string EmailChangeBy,
    Guid CompanyId,
    string Name,
    string Description,
    bool IsChangedFile,
    string FileName,
    string FileURL,
    bool IsChangedImage,
    string ImageName,
    string ImageURL,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;



