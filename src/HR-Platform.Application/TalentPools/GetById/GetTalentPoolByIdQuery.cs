using ErrorOr;
using HR_Platform.Application.TalentPools.Common;
using MediatR;

namespace HR_Platform.Application.TalentPools.GetById;

public record GetTalentPoolByIdQuery(Guid Id) : IRequest<ErrorOr<TalentPoolByIdResponse>>;