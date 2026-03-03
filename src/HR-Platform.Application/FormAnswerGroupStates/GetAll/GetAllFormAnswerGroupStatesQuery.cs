using ErrorOr;
using HR_Platform.Application.FormAnswerGroupStates.Common;
using MediatR;

namespace HR_Platform.Application.FormAnswerGroupStates.GetAll;

public record GetAllFormAnswerGroupStatesQuery() : IRequest<ErrorOr<List<FormAnswerGroupStatesResponse>>>;