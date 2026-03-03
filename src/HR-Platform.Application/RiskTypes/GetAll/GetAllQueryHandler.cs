using ErrorOr;
using HR_Platform.Application.Risks.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.RiskTypeMains;
using MediatR;

namespace HR_Platform.Application.Risks.GetAllByRiskType;

internal sealed class GetAllQueryHandler(
    ICompanyRepository companyRepository,
    IRiskTypeMainRepository riskTypeMainRepository

    ) : IRequestHandler<GetAllQuery, ErrorOr<List<RiskTypeResponse>>>
{
    private readonly IRiskTypeMainRepository _riskTypeMainRepository = riskTypeMainRepository ?? throw new ArgumentNullException(nameof(riskTypeMainRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));


    public async Task<ErrorOr<List<RiskTypeResponse>>> Handle(GetAllQuery query, CancellationToken cancellationToken)
    {
        // Quieres que se vean todos aun estando en false? entonces isVisible debe ser True
        // Si es True traigo todos aunque tengan el campo isVisible en True

        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<RiskTypeMain>? riskTypeMains = await _riskTypeMainRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<RiskTypeResponse> resultOnlyTrue = [];
        List<RiskTypeResponse> resulAll = [];

        if (riskTypeMains is not null && riskTypeMains.Count != 0)
        {
            foreach (RiskTypeMain item in riskTypeMains)
            {
                RiskTypeResponse response = new
                (
                    item.Id.Value.ToString(),
                    item.Name,
                    item.NameEnglish,
                    item.IsVisible
                );

                if( item.IsVisible )
                {
                    resultOnlyTrue.Add( response );
                }

                resulAll.Add(response);
            }
        }

        return query.IsVisible? resulAll: resultOnlyTrue;

    }
}