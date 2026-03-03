using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using HR_Platform.Application.Minutes.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Minutes;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Minutes.GetByCollaboratorId;

internal sealed class GetMinuteByCompanyIdQueryHandler(
    IMinuteRepository minuteRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetMinuteByCompanyIdQuery, ErrorOr<MinuteFileAndYearListResponse>>
{
    private readonly IMinuteRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<MinuteFileAndYearListResponse>> Handle(GetMinuteByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<Minute>? minuteListFull = await _minuteRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<Minute>? minuteList = minuteListFull?.Where(h => h.CreationDate.Value.Year.ToString() == query.Year).ToList();
        List<MinuteFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (minuteList is not null && minuteList.Count > 0)
        {
            foreach (Minute item in minuteList)
            {
                MinuteFileResponse temp = new
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

        MinuteFileAndYearListResponse response = new
        (
            [.. filesList.OrderByDescending(x=> x.CreationDate)],
            distinctYears
        );

        return response;

    }
}