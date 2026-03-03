using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.DeleteTalentPoolQuery;

public record DeleteTalentPoolQuery(Guid Id) : IRequest<ErrorOr<bool>>;