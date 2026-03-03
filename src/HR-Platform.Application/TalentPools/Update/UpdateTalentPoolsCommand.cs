using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.Update;

public record UpdateTalentPoolsCommand(
    Guid CompanyId,
    string TalentPoolId,
    string Tittle,
    string Description
) : IRequest<ErrorOr<bool>>;

