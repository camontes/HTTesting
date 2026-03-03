using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Surveys.UpdateIsVisibleSurvey;

public record UpdateIsVisibleSurveyCommand(Guid Id) : IRequest<ErrorOr<bool>>;
