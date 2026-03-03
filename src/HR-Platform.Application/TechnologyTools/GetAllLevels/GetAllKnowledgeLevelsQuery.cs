using ErrorOr;
using HR_Platform.Application.TechnologyTools.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllKnowledgeLevelsQuery() : IRequest<ErrorOr<IReadOnlyList<TechnologyToolsResponse>>>;