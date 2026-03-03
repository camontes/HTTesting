using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.TalentPools.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TalentPools;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.TalentPools.GetByCompanyId;

internal sealed class GetTalentPoolsByCompanyIdHandler(
    ITalentPoolRepository TalentPoolRepository,
    ICompanyRepository companyRepository,
    ICalculateTimeDifference calculateTimeDifference,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetTalentPoolsByCompanyIdQuery, ErrorOr<TalentPoolsAndCountByCompanyResponse>>
{
    private readonly ITalentPoolRepository _TalentPoolRepository = TalentPoolRepository ?? throw new ArgumentNullException(nameof(TalentPoolRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<TalentPoolsAndCountByCompanyResponse>> Handle(GetTalentPoolsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (await _TalentPoolRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<TalentPool> talentPools)
            return Error.NotFound("TalentPools.NotFound", "The Talent Pools related with the provide Company Id was not found.");

        int talentPoolsCount = await _TalentPoolRepository.GetNumberOfTalentPools(
            (new CompanyId(query.CompanyId)));

        List<TalentPoolWIthCollaboratorCountResponse> talentPoolsResponseActive = [];
        List<TalentPoolWIthCollaboratorCountResponse> talentPoolsResponseArchived = [];
        bool isUpdated = false; 
        DateTime IsUpdateDatetime = DateTime.MinValue;
        if (talentPools is not null && talentPools.Count > 0)
        {
            foreach (TalentPool talentPool in talentPools)
            {
                isUpdated = talentPool.EditionDate.Value > talentPool.CreationDate.Value;
                IsUpdateDatetime = isUpdated ? talentPool.EditionDate.Value : talentPool.CreationDate.Value;

                var temp = new TalentPoolWIthCollaboratorCountResponse
                (
                    talentPool.Id.Value,
                    talentPool.Tittle,
                    talentPool.Description,
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction(isUpdated ? "Actualizado" : "Creado", isUpdated ? "Updated" : "Created", IsUpdateDatetime).Split('.')[0]), // TimePosted
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction(isUpdated ? "Actualizado" : "Creado", isUpdated ? "Updated" : "Created", IsUpdateDatetime).Split('.')[1]), // TimePostedEnglish
                    _timeFormatService.GetDateTimeFormatMonthToltip(IsUpdateDatetime, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateTimeFormatMonthToltip(IsUpdateDatetime, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")),
                    talentPool.IsArchived,
                    talentPool.CollaboratorTalentPools.Count,
                    talentPool.CreationDate.Value,
                    talentPool.EditionDate.Value
            );
                if (!temp.IsArchived)
                {
                    talentPoolsResponseActive.Add(temp);
                }
                else
                {
                    talentPoolsResponseArchived.Add(temp);
                }
            }

        }

        TalentPoolsAndCountByCompanyResponse finalResult = new(
            [.. talentPoolsResponseActive.OrderByDescending(x => x.CreationDate)],
            [.. talentPoolsResponseArchived.OrderByDescending(x => x.CreationDate)],
            talentPoolsResponseArchived.Count,
            talentPoolsResponseActive.Count
        );

        return finalResult;

    }
}