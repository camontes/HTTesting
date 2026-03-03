using ErrorOr;
using HR_Platform.Application.EducationalLevels.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EducationalLevels;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.GetByCompanyId;

internal sealed class GetEducationalLevelsByCompanyIdHandler(
    IEducationalLevelRepository EducationalLevelRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetEducationalLevelsByCompanyIdQuery, ErrorOr<EducationalLevelsAndCountByCompanyResponse>>
{
    private readonly IEducationalLevelRepository _EducationalLevelRepository = EducationalLevelRepository ?? throw new ArgumentNullException(nameof(EducationalLevelRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public   async Task<ErrorOr<EducationalLevelsAndCountByCompanyResponse>> Handle(GetEducationalLevelsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }


        if (await _EducationalLevelRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<EducationalLevel> educationalLevels)
        {
            return Error.NotFound("EducationalLevels.NotFound", "The educationalLevels related with the provide Company Id was not found.");
        }

        int educationalLevelsCount = await _EducationalLevelRepository.GetNumberOfEducationalLevels(
            (new CompanyId(query.CompanyId)));

        List<EducationalLevelWIthCollaboratorCountResponse> educationalLevelsResponse = [];


        if (educationalLevels is not null && educationalLevels.Count > 0)
        {
            foreach (EducationalLevel educationalLevel in educationalLevels)
            {
                educationalLevelsResponse.Add
                (
                    new EducationalLevelWIthCollaboratorCountResponse
                    (
                        educationalLevel.Id.Value,
                        educationalLevel.CompanyId.Value,

                        educationalLevel.Name,
                        educationalLevel.NameEnglish,

                        educationalLevel.Collaborators.Count,

                        educationalLevel.IsEditable,
                        educationalLevel.IsDeleteable,

                        educationalLevel.CreationDate.Value,
                        educationalLevel.EditionDate.Value
                    )
                );
            }
        }

        EducationalLevelsAndCountByCompanyResponse finalResult = new(
            educationalLevelsResponse,
            educationalLevelsCount
        );

        return finalResult;

    }
}