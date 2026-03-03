using ErrorOr;
using HR_Platform.Domain.Assignations;
using MediatR;

namespace HR_Platform.Application.Assignations.Delete;

public record DeleteAssignationCommand( List<Guid> AssignationList) : IRequest<ErrorOr<bool>>;
