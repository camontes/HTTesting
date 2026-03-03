using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Application.CoexistenceCommitteeMinutes.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetByCollaboratorId;

internal sealed class GetCoexistenceCommitteeMinuteByCompanyIdQueryHandler(
    ICoexistenceCommitteeMinuteRepository minuteRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetCoexistenceCommitteeMinuteByCompanyIdQuery, ErrorOr<CoexistenceCommitteeMinuteFileAndYearListResponse>>
{
    private readonly ICoexistenceCommitteeMinuteRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<CoexistenceCommitteeMinuteFileAndYearListResponse>> Handle(GetCoexistenceCommitteeMinuteByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<CoexistenceCommitteeMinute>? minuteListFull = await _minuteRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<CoexistenceCommitteeMinute>? minuteList = minuteListFull?.Where(h => h.CreationDate.Value.Year.ToString() == query.Year).ToList();
        List<CoexistenceCommitteeMinuteFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (minuteList is not null && minuteList.Count > 0)
        {
            foreach (CoexistenceCommitteeMinute item in minuteList)
            {
                CoexistenceCommitteeMinuteFileResponse temp = new
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

        if (minuteListFull is not null)
        {
            distinctYears = minuteListFull
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        CoexistenceCommitteeMinuteFileAndYearListResponse response = new
        (
            [.. filesList.OrderByDescending(x=> x.CreationDate)],
            distinctYears
        );

        return response;

    }
}