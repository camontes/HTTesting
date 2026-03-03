using FluentValidation;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateEvidenceCoexistenceCommitteeVoteCommandValidator : AbstractValidator<CreateEvidenceCoexistenceCommitteeVotesCommand>
{
    public CreateEvidenceCoexistenceCommitteeVoteCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.EvidenceCoexistenceCommitteeVotesList)
            .NotEmpty();
    }
}
