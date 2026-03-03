using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMembers.Common;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.GetAllMembers;

public record GetCoexistenceCommitteeMemberQuery() : IRequest<ErrorOr<List<CoexistenceCommitteeMemberResponse>>>;