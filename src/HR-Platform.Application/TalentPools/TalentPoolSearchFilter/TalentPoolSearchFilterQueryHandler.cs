using ErrorOr;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.TalentPools.Common;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.TalentPools;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.TalentPools.TalentPoolSearchFilter;

internal sealed class TalentPoolSearchFilterQueryHandler(
    ITalentPoolRepository talentPoolRepository,
    ICalculateTimeDifference calculateTimeDifference,
    ITimeFormatService timeFormatService
    ) : IRequestHandler<TalentPoolSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(TalentPoolSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _talentPoolRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize, IsTalentPoolArchived = query.IsTalentPoolArchived });

        List<TalentPoolWIthCollaboratorCountResponse> items = [];

        if (results.TotalCount > 0)
        {
            bool isUpdated = false;
            DateTime IsUpdateDatetime = DateTime.MinValue;

            foreach (TalentPool talentPool in results.Items)
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
                items.Add(temp);
            }
        }
        return new SearchFilterResponse
        (
            results.TotalCount,
            items,
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}