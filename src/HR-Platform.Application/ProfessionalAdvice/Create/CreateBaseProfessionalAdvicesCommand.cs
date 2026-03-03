using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdviceEntities.Create;

public record CreateBaseProfessionalAdvicesCommand(List<BaseProfessionalAdviceEntityCommand> ProfessionalAdviceEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseProfessionalAdviceEntityCommand(
    string Name,
    string NameEnglish,
    string NameAcronyms,
    string NameAcronymsEnglish
);

