using ErrorOr;
using HR_Platform.Application.SeveranceBenefits.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.SeveranceBenefits;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.GetByCompanyId;

internal sealed class GetSeveranceBenefitsByCompanyIdHandler : IRequestHandler<GetSeveranceBenefitsByCompanyIdQuery, ErrorOr<SeveranceBenefitWithCountResponse>>
{
    private readonly ISeveranceBenefitRepository _SeveranceBenefitRepository;
    private readonly ICompanyRepository _companyRepository;

    public GetSeveranceBenefitsByCompanyIdHandler
    (
        ISeveranceBenefitRepository SeveranceBenefitRepository,
        ICompanyRepository companyRepository
    )
    {
        _SeveranceBenefitRepository = SeveranceBenefitRepository ?? throw new ArgumentNullException(nameof(SeveranceBenefitRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<ErrorOr<SeveranceBenefitWithCountResponse>> Handle(GetSeveranceBenefitsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if(await _SeveranceBenefitRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<SeveranceBenefit> severanceBenefits)
        {
            return Error.NotFound("SeveranceBenefits.NotFound", "The severanceBenefits related with the provide Company Id was not found.");
        }

        int severaceBenefitsCount = await _SeveranceBenefitRepository.GetNumberOfSeveranceBenefits(
            (new CompanyId(query.CompanyId)));

        List<SeveranceBenefitWithCollaboratorCountResponse>? severanceBenefitsResponse = new();


        if (severanceBenefits is not null && severanceBenefits.Count > 0)
        {
            foreach (SeveranceBenefit severanceBenefit in severanceBenefits)
            {
                severanceBenefitsResponse.Add
                (
                    new SeveranceBenefitWithCollaboratorCountResponse
                    (
                        severanceBenefit.Id.Value,
                        severanceBenefit.CompanyId.Value,

                        severanceBenefit.Name,
                        severanceBenefit.NameEnglish,

                        severanceBenefit.Collaborators.Count,

                        severanceBenefit.IsEditable,
                        severanceBenefit.IsDeleteable,

                        severanceBenefit.CreationDate.Value,
                        severanceBenefit.EditionDate.Value
                    )
                );
            }
        }

        SeveranceBenefitWithCountResponse finalResult = new(
            severanceBenefitsResponse,
            severaceBenefitsCount
        );

        return finalResult;
    }
}