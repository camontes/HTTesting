using ErrorOr;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Common;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.GetByCollaboratorId;

internal sealed class GetEvidenceCoexistenceCommitteeVoteByCompanyIdQueryHandler(
    IEvidenceCoexistenceCommitteeVoteRepository evidenceCoexistenceCommitteeVoteRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetEvidenceCoexistenceCommitteeVoteByCompanyIdQuery, ErrorOr<EvidenceCoexistenceCommitteeVoteFileAndYearListResponse>>
{
    private readonly IEvidenceCoexistenceCommitteeVoteRepository _evidenceCoexistenceCommitteeVoteRepository = evidenceCoexistenceCommitteeVoteRepository ?? throw new ArgumentNullException(nameof(evidenceCoexistenceCommitteeVoteRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<EvidenceCoexistenceCommitteeVoteFileAndYearListResponse>> Handle(GetEvidenceCoexistenceCommitteeVoteByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<EvidenceCoexistenceCommitteeVote>? evidenceCoexistenceCommitteeVoteListFull = await _evidenceCoexistenceCommitteeVoteRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<EvidenceCoexistenceCommitteeVote>? evidenceCoexistenceCommitteeVoteList = evidenceCoexistenceCommitteeVoteListFull?.Where(h => h.CreationDate.Value.Year.ToString() == query.Year).ToList();
        List<EvidenceCoexistenceCommitteeVoteFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (evidenceCoexistenceCommitteeVoteList is not null && evidenceCoexistenceCommitteeVoteList.Count > 0)
        {
            foreach (EvidenceCoexistenceCommitteeVote item in evidenceCoexistenceCommitteeVoteList)
            {
                EvidenceCoexistenceCommitteeVoteFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.FileName, // FileName
                   item.UrlFile, // FileURL
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded",item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   item.CreationDate.Value, // CreationDate
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                   item.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                   item.NameWhoChangedByTH, // FullNameTh
                   _stringService.GetInitials(item.NameWhoChangedByTH) // ShortNameTh
                );

                filesList.Add(temp);
            }

        }

        if (evidenceCoexistenceCommitteeVoteListFull is not null)
        {
            distinctYears = evidenceCoexistenceCommitteeVoteListFull
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        EvidenceCoexistenceCommitteeVoteFileAndYearListResponse response = new
        (
            [.. filesList.OrderByDescending(x=> x.CreationDate)],
            distinctYears
        );

        return response;

    }
}