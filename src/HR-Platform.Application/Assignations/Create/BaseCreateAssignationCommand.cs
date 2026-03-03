using ErrorOr;
using HR_Platform.Domain.Assignations;
using MediatR;

namespace HR_Platform.Application.Assignations.Create;

public record BaseCreateAssignationCommand(
    List<BaseCreateAssignationElementCommand> AssignationList
) : IRequest<ErrorOr<List<Assignation>>>;
