using ErrorOr;
using HR_Platform.Application.Positions.Common;
using HR_Platform.Application.Positions.GetByCompanyId;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Positions;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetPositionsByCompanyIdHandler(
    IPositionRepository positionRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetPositionsByCompanyIdQuery, ErrorOr<List<PositionsResponse>>>
{
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<List<PositionsResponse>>> Handle(GetPositionsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        List<Position> positions = await _positionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));

        if (positions is null || positions.Count == 0)
        {
            return Error.NotFound("Positions.NotFound", "The position related with the provide Company Id was not found.");
        }

        List<PositionsResponse>? positionsResponse = [];

        if (positions is not null && positions.Count > 0)
        {
            foreach (Position position in positions)
            {
                positionsResponse.Add
                (
                    new PositionsResponse
                    (
                        position.Id.Value,
                        position.CompanyId.Value,

                        position.Name,
                        position.NameEnglish,

                        position.Description,
                        position.DescriptionEnglish,

                        !string.IsNullOrEmpty(position.PositionFile) ? position.PositionFile : string.Empty,
                        !string.IsNullOrEmpty(position.PositionFileName) ? position.PositionFileName:"",

                        position.IsEditable,
                        position.IsDeleteable,

                        position.Collaborators.Count,

                        position.CreationDate.Value,
                        position.EditionDate.Value
                    )
                );
            }
        }

        return positionsResponse;
    }
}