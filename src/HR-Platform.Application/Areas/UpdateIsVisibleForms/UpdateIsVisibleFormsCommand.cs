using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Areas.UpdateIsVisibleForms;

public record UpdateIsVisibleFormsCommand(Guid Id) : IRequest<ErrorOr<bool>>;