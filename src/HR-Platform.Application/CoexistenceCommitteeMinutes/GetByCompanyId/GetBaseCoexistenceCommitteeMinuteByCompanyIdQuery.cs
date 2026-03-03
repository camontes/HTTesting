using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetByCompanyId;

public record GetBaseCoexistenceCommitteeMinuteByCompanyIdQuery(string Year) : IRequest<ErrorOr<List<CoexistenceCommitteeMinuteFileResponse>>>;