using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.UpdateIsVisibleForm;

public record UpdateIsVisibleFormCommand(Guid Id) : IRequest<ErrorOr<bool>>;
