using ErrorOr;
using MediatR;
using HR_Platform.Application.AssignationTypes.Common;

namespace HR_Platform.Application.AssignationTypes.GetAll;

public record GetAllAssignationTypesQuery() : IRequest<ErrorOr<IReadOnlyList<AssignationTypesResponse>>>;