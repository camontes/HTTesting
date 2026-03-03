using ErrorOr;
using HR_Platform.Application.Assignations.Common;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

public record UpdateAssignationCommand
(
    Guid Id,

    string CompanyId,

    string Name,

    string NameEnglish
) : IRequest<ErrorOr<AssignationsResponse>>;
