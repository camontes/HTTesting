using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.HasThereBeenUpdate;

public record HasThereBeenUpdateQuery() : IRequest<ErrorOr<bool>>;