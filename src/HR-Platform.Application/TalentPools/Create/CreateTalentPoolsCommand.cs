using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.Create;

public record CreateTalentPoolsCommand(
    Guid CompanyId,
    string Tittle,
    string Description
) : IRequest<ErrorOr<bool>>;

