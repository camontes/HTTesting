using ErrorOr;
using HR_Platform.Application.Regulations.Common;
using HR_Platform.Application.Regulations.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Regulations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Regulations.GetByCollaboratorId;

internal sealed class GetRegulationByCompanyIdQueryHandler(
    IRegulationRepository regulationRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetRegulationByCompanyIdQuery, ErrorOr<RegulationFileAndYearListResponse>>
{
    private readonly IRegulationRepository _regulationRepository = regulationRepository ?? throw new ArgumentNullException(nameof(regulationRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<RegulationFileAndYearListResponse>> Handle(GetRegulationByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<Regulation>? regulationList = await _regulationRepository.GetByCompanyIdAsync(oldCompany.Id, query.Year);
        List<RegulationFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (regulationList is not null && regulationList.Count > 0)
        {
            foreach (Regulation item in regulationList)
            {
                RegulationFileResponse temp = new
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

        if (regulationList is not null)
        {
            distinctYears = regulationList
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        RegulationFileAndYearListResponse response = new
        (
            [.. filesList.OrderByDescending(x => x.CreationDate)],
            distinctYears
        );

        return response;

    }
}