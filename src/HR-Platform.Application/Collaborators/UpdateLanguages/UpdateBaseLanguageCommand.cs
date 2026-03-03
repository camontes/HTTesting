using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateLanguages;

public record UpdateBaseLanguageCommand
(
   Guid CollaboratorId,
   List<UpdateLanguageRequest> LanguageList
) : IRequest<ErrorOr<bool>>;

public record UpdateLanguageRequest
(
    string Id,
    string CollaboratorId,
    string? LanguageNameId,
    string? LanguageLevelId,
    string? OtherLanguageName
);




