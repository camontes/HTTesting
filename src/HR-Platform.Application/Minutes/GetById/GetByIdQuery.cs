using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using MediatR;

namespace HR_Platform.Application.Minutes.GetById;

public record GetByIdQuery(Guid MinuteId) : IRequest<ErrorOr<MinuteFileResponse>>;