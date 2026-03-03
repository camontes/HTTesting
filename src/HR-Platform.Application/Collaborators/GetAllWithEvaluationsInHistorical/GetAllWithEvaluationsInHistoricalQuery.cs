using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetAllWithEvaluationsInHistorical;

public record GetAllWithEvaluationsInHistoricalQuery(Guid companyId) : IRequest<ErrorOr<List<CollaboratorWithEvaluationsResponse>>>;
