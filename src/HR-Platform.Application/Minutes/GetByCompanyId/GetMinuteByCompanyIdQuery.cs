using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using MediatR;

namespace HR_Platform.Application.Minutes.GetByCompanyId;

public record GetMinuteByCompanyIdQuery(Guid CompanyId, string Year) : IRequest<ErrorOr<MinuteFileAndYearListResponse>>;