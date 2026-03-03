using ErrorOr;
using HR_Platform.Application.DefaultFamilyCompositions.Common;
using HR_Platform.Domain.DefaultFamilyCompositions;
using MediatR;

namespace HR_Platform.Application.DefaultFamilyCompositions.GetAll;

internal sealed class GetAllDefaultFamilyCompositionsQueryHandler : IRequestHandler<GetAllDefaultFamilyCompositionsQuery, ErrorOr<List<DefaultFamilyCompositionsResponse>>>
{
    private readonly IDefaultFamilyCompositionRepository _defaultFamilyCompositionRepository;

    public GetAllDefaultFamilyCompositionsQueryHandler
    (
        IDefaultFamilyCompositionRepository defaultFamilyCompositionRepository
    )
    {
        _defaultFamilyCompositionRepository = defaultFamilyCompositionRepository ?? throw new ArgumentNullException(nameof(defaultFamilyCompositionRepository));
    }

    public async Task<ErrorOr<List<DefaultFamilyCompositionsResponse>>> Handle(GetAllDefaultFamilyCompositionsQuery getAllDefaultFamilyCompositionsQuery, CancellationToken cancellationToken)
    {
        if (await _defaultFamilyCompositionRepository.GetAll() is not List<DefaultFamilyComposition> defaultFamilyCompositions)
        {
            return Error.NotFound("DefaultFamilyCompositions.NotFound", "The default family compositions was not found.");
        }

        List<DefaultFamilyCompositionsResponse> defaultFamilyCompositionsResponse = new();

        if (defaultFamilyCompositions is not null && defaultFamilyCompositions.Count > 0)
        {
            foreach (DefaultFamilyComposition defaultFamilyComposition in defaultFamilyCompositions)
            {
                defaultFamilyCompositionsResponse.Add
                (
                    new DefaultFamilyCompositionsResponse
                    (
                        defaultFamilyComposition.Id.Value,

                        defaultFamilyComposition.Name,
                        defaultFamilyComposition.NameEnglish
                    )
                );
            }
        }

        return defaultFamilyCompositionsResponse;

    }
}