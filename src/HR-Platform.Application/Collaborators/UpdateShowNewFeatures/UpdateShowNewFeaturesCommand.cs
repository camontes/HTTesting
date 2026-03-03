using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateShowNewFeatures;

public record UpdateShowNewFeaturesCommand
(
    Guid Id,

    bool ShowNewFeatures
) : IRequest<ErrorOr<CollaboratorsResponse>>;
