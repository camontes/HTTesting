using ErrorOr;
using HR_Platform.Application.Forms.Common;
using MediatR;

namespace HR_Platform.Application.Forms.GetByAreaId;

public record GetFormByAreaIdQuery(Guid AreaId, bool IsOrderingByName) : IRequest<ErrorOr<List<FormsResponse>>>;