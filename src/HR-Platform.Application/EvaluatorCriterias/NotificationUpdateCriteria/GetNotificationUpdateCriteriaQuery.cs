using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.NotificationUpdateCriteria;
public record GetNotificationUpdateCriteriaQuery(string EmailWhoIsLogin) : IRequest<ErrorOr<bool>>;