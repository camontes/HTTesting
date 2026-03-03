using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Application.Minutes.Common;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using MediatR;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.GetYearsByCompanyId;

internal sealed class GetYearsByCompanyIdQueryHandler(
    IEvidenceCoexistenceCommitteeVoteRepository evidenceCoexistenceCommitteeVoteRepository,
    ICompanyRepository companyRepository

    ) : IRequestHandler<GetYearsByCompanyIdQuery, ErrorOr<EvidenceCoexistenceCommitteeVoteYearsListResponse>>
{
    private readonly IEvidenceCoexistenceCommitteeVoteRepository _evidenceCoexistenceCommitteeVoteRepository =
        evidenceCoexistenceCommitteeVoteRepository ?? throw new ArgumentNullException(nameof(evidenceCoexistenceCommitteeVoteRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<EvidenceCoexistenceCommitteeVoteYearsListResponse>> Handle(GetYearsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<EvidenceCoexistenceCommitteeVote>? evidenceCoexistenceCommitteeVotesList = 
            await _evidenceCoexistenceCommitteeVoteRepository.GetByCompanyIdAsync(oldCompany.Id);

        List<MinuteFileResponse> filesList = [];

        List<string> distinctYears = [];

        if (evidenceCoexistenceCommitteeVotesList is not null && evidenceCoexistenceCommitteeVotesList.Count > 0)
        {
            distinctYears =
                evidenceCoexistenceCommitteeVotesList
                .Select(m => m.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        EvidenceCoexistenceCommitteeVoteYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}
