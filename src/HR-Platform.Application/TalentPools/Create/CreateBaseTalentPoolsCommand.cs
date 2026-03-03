using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPoolEntities.Create;

public record CreateBaseTalentPoolsCommand(
    string Tittle,
    string Description
) :IRequest<ErrorOr<bool>>;

