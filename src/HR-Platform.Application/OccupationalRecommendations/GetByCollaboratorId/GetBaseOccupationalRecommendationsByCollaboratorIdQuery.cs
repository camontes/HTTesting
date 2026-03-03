using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.GetByCompanyId;

public record GetBaseOccupationalRecommendationsByCollaboratorIdQuery(string? CollaboratorId, string Year) : IRequest<ErrorOr<OccupationalRecommendationsResponse>>;