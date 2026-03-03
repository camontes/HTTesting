using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateLanguages;

public record UpdateLanguageCommand
(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<UpdateLanguageRequest> LanguageList
) : IRequest<ErrorOr<bool>>;





