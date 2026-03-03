using ErrorOr;
using HR_Platform.Application.CopasstMembers.Common;
using MediatR;

namespace HR_Platform.Application.CopasstMembers.GetAllMembers;

public record GetCopasstMemberQuery() : IRequest<ErrorOr<List<CopasstMemberResponse>>>;