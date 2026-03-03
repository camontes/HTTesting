using ErrorOr;
using HR_Platform.Application.PriorityNovelties.Common;
using MediatR;

namespace HR_Platform.Application.PriorityNovelties.GetAll;

public record GetAllPriorityNoveltiesQuery() : IRequest<ErrorOr<List<PriorityNoveltiesResponse>>>;