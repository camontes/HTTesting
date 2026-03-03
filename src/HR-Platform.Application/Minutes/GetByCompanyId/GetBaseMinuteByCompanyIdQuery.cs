using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using MediatR;

namespace HR_Platform.Application.Minutes.GetByCompanyId;

public record GetBaseMinuteByCompanyIdQuery(string Year) : IRequest<ErrorOr<List<MinuteFileResponse>>>;