using ErrorOr;
using HR_Platform.Application.BrigadeDocumentations.Common;
using HR_Platform.Application.BrigadeDocumentations.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BrigadeDocumentations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BrigadeDocumentations.GetByCollaboratorId;

internal sealed class GetBrigadeDocumentationByCompanyIdQueryHandler(
    IBrigadeDocumentationRepository brigadeDocumentationRepository,
    ICompanyRepository companyRepository,
    ITimeFormatService timeFormatService,
    IStringService stringService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetBrigadeDocumentationByCompanyIdQuery, ErrorOr<BrigadeDocumentationFileAndYearListResponse>>
{
    private readonly IBrigadeDocumentationRepository _brigadeDocumentationRepository = brigadeDocumentationRepository ?? throw new ArgumentNullException(nameof(brigadeDocumentationRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<BrigadeDocumentationFileAndYearListResponse>> Handle(GetBrigadeDocumentationByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<BrigadeDocumentation>? brigadeDocumentationList = await _brigadeDocumentationRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<BrigadeDocumentationFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (brigadeDocumentationList is not null && brigadeDocumentationList.Count > 0)
        {
            foreach (BrigadeDocumentation item in brigadeDocumentationList)
            {
                BrigadeDocumentationFileResponse temp = new
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

        if (brigadeDocumentationList is not null)
        {
            distinctYears = brigadeDocumentationList
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        BrigadeDocumentationFileAndYearListResponse response = new
        (
            [.. filesList.OrderByDescending(x => x.CreationDate)],
            distinctYears
        );

        return response;

    }
}