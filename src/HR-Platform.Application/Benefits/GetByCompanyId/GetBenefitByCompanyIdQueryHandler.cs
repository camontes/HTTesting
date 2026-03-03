using ErrorOr;
using HR_Platform.Application.Benefits.Common;
using HR_Platform.Application.Benefits.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Benefits.GetByCollaboratorId;

internal sealed class GetBenefitByCompanyIdQueryHandler(
    IBenefitRepository benefitRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetBenefitByCompanyIdQuery, ErrorOr<List<BenefitFileResponse>>>
{
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<BenefitFileResponse>>> Handle(GetBenefitByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<Benefit>? benefitListFull = await _benefitRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<BenefitFileResponse> BenefitListVisibles = [];
        List<BenefitFileResponse> BenefitListAll = [];

        if (benefitListFull is not null && benefitListFull.Count > 0)
        {
            foreach (Benefit item in benefitListFull)
            {
                BenefitFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // TimePostedTolTip
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")), // TimePostedTolTipEnglish
                   item.Name, 
                   item.Description,
                   item.IsVisible,
                   item.IsPinned,
                   _timeFormatService.GetDateFormatMonthLarge(item.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // CreatedFormat,
                   _timeFormatService.GetDateFormatMonthLarge(item.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // CreatedFormatEnglish,
                   _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // UpdatedFormat,
                   _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // UpdatedFormatEnglish,
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), //UpdateToltip
                   item.FileName,
                   item.FileURL,
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDateFile.Value).Split('.')[0]), // TimePostedFile
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDateFile.Value).Split('.')[1]), // TimePostedFileEnglish
                   item.ImageName,
                   item.ImageURL,
                   item.NameWhoChangedByTH, // FullNameTh
                   item.CreationDate.Value, // Creation date,
                   item.IsAddedButton,
                   item.ButtonName,
                   item.IsSurveyInclude,
                   item.IsAvailableForAll,
                   item.IsAnotherContraint,
                   item.AnotherContraint,
                   item.MinimumMonthsConstraint
                );

                if (item.IsVisible)
                {
                    BenefitListVisibles.Add(temp);
                }
                BenefitListAll.Add(temp);
            }
        }
        return query.IsVisible ? BenefitListVisibles.OrderByDescending(x => x.CreationDate).ToList(): [.. BenefitListAll.OrderByDescending(x => x.CreationDate)];
    }
}