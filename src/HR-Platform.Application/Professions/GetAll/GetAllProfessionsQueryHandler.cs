using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Professions.Common;
using HR_Platform.Domain.DefaultProfessions;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllProfessionsQueryHandler(
    IDefaultProfessionRepository defaultProfessionRepository
    ) : IRequestHandler<GetAllProfessionsQuery, ErrorOr<IReadOnlyList<ProfessionsResponse>>>
{
    private readonly IDefaultProfessionRepository _defaultProfessionRepository = defaultProfessionRepository ?? throw new ArgumentNullException(nameof(defaultProfessionRepository));

    public async Task<ErrorOr<IReadOnlyList<ProfessionsResponse>>> Handle(GetAllProfessionsQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultProfession> Professions = await _defaultProfessionRepository.GetAll();

        List<ProfessionsResponse> ProfessionsResponse = [];

        if (Professions is not null && Professions.Count > 0)
        {
            foreach (DefaultProfession assignationType in Professions)
            {
                ProfessionsResponse.Add
                (
                    new ProfessionsResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return ProfessionsResponse;
    }
}