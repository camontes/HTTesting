using FluentValidation;
using SearchFilters.Query;

namespace HR_Platform.Application.BrigadeDocumentations.Create;

public class SearchFilterQueryValidator : AbstractValidator<SearchFilterQuery>
{
    public SearchFilterQueryValidator()
    {
        RuleFor(r => r.Query)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Context)
          .MaximumLength(100)
          .NotEmpty();
    }
}
