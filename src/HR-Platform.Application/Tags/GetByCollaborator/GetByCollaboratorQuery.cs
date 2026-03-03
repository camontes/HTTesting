using ErrorOr;
using HR_Platform.Application.Tags.Common;
using MediatR;

namespace HR_Platform.Application.Tags.GetByCollaborator;

public record GetByCollaboratorQuery(Guid CollaboratorId) : IRequest<ErrorOr<List<TagNamesResponse>>>;