using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPoolEntities.Update;

public record UpdateBaseTalentPoolsCommand(
    string TalentPoolId,
    string Tittle,
    string? Description
) :IRequest<ErrorOr<bool>>;

