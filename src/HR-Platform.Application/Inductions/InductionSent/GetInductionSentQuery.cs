using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.InductionSent;

public record GetInductionSentQuery(Guid CompanyId) : IRequest<ErrorOr<List<InductionSentResponse>>>;