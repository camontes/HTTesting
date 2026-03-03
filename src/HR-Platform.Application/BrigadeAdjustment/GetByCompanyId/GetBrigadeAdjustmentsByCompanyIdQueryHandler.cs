using ErrorOr;
using HR_Platform.Application.BrigadeAdjustments.Common;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.GetByCompanyId;

internal sealed class GetBrigadeAdjustmentsByCompanyIdHandler(
    IBrigadeAdjustmentRepository brigadeAdjustmentRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetBrigadeAdjustmentsByCompanyIdQuery, ErrorOr<BrigadeAdjustmentsAndCountResponse>>
{
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<BrigadeAdjustmentsAndCountResponse>> Handle(GetBrigadeAdjustmentsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (await _brigadeAdjustmentRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId)) is not List<BrigadeAdjustment> brigadeAdjustments)
            return Error.NotFound("BrigadeAdjustments.NotFound", "The brigadeAdjustments related with the provide Company Id was not found.");

        List<BrigadeAdjustmentsResponse> brigadeAdjustmentsResponse = [];


        if (brigadeAdjustments is not null && brigadeAdjustments.Count > 0)
        {
            foreach (BrigadeAdjustment brigadeAdjustment in brigadeAdjustments)
            {
                brigadeAdjustmentsResponse.Add
                (
                    new BrigadeAdjustmentsResponse
                    (
                        brigadeAdjustment.Id.Value,

                        brigadeAdjustment.Name,
                        brigadeAdjustment.NameEnglish,
                        brigadeAdjustment.IconId,

                        brigadeAdjustment.IsEditable,
                        brigadeAdjustment.IsDeleteable
                    )
                );
            }
        }

        BrigadeAdjustmentsAndCountResponse finalResult = new(
            brigadeAdjustmentsResponse,
            brigadeAdjustments?.Count == 6
        );

        return finalResult;

    }
}