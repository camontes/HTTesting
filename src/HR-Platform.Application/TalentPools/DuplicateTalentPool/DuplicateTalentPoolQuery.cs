using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.DuplicateTalentPool;

public record DuplicateTalentPoolQuery(Guid Id) : IRequest<ErrorOr<bool>>;