using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetByCompanyId;

public record GetCoexistenceCommitteeMinuteByCompanyIdQuery(Guid CompanyId, string Year) : IRequest<ErrorOr<CoexistenceCommitteeMinuteFileAndYearListResponse>>;