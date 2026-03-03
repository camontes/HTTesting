using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.InductionCompleted;

public record GetInductionCompletedQuery(Guid CompanyId) : IRequest<ErrorOr<List<InductionCompletedResponse>>>;