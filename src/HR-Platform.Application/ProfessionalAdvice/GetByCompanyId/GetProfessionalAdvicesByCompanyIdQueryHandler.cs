using ErrorOr;
using HR_Platform.Application.ProfessionalAdvices.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ProfessionalAdvices;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.GetByCompanyId;

internal sealed class GetProfessionalAdvicesByCompanyIdHandler(
    IProfessionalAdviceRepository ProfessionalAdviceRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetProfessionalAdvicesByCompanyIdQuery, ErrorOr<ProfessionalAdvicesAndCountByCompanyResponse>>
{
    private readonly IProfessionalAdviceRepository _ProfessionalAdviceRepository = ProfessionalAdviceRepository ?? throw new ArgumentNullException(nameof(ProfessionalAdviceRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<ProfessionalAdvicesAndCountByCompanyResponse>> Handle(GetProfessionalAdvicesByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }


        if (await _ProfessionalAdviceRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<ProfessionalAdvice> professionalAdvices)
        {
            return Error.NotFound("ProfessionalAdvices.NotFound", "The professionalAdvices related with the provide Company Id was not found.");
        }

        int professionalAdvicesCount = await _ProfessionalAdviceRepository.GetNumberOfProfessionalAdvices(
            (new CompanyId(query.CompanyId)));

        List<ProfessionalAdviceWIthCollaboratorCountResponse> professionalAdvicesResponse = [];


        if (professionalAdvices is not null && professionalAdvices.Count > 0)
        {
            foreach (ProfessionalAdvice professionalAdvice in professionalAdvices)
            {
                professionalAdvicesResponse.Add
                (
                    new ProfessionalAdviceWIthCollaboratorCountResponse
                    (
                        professionalAdvice.Id.Value,
                        professionalAdvice.CompanyId.Value,

                        professionalAdvice.Name,
                        professionalAdvice.NameEnglish,

                        professionalAdvice.NameAcronyms,
                        professionalAdvice.NameAcronymsEnglish,

                        professionalAdvice.Collaborators.Count,

                        professionalAdvice.IsEditable,
                        professionalAdvice.IsDeleteable,

                        professionalAdvice.CreationDate.Value,
                        professionalAdvice.EditionDate.Value
                    )
                );
            }
        }

        ProfessionalAdvicesAndCountByCompanyResponse finalResult = new(
            professionalAdvicesResponse,
            professionalAdvicesCount
        );

        return finalResult;

    }
}