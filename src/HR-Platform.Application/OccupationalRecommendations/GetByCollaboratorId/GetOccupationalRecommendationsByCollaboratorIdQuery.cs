using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.GetByCompanyId;

public record GetOccupationalRecommendationsByCollaboratorIdQuery(string? CollaboratorId, string Year, string EmailChangeBy) : IRequest<ErrorOr<OccupationalRecommendationsResponse>>;