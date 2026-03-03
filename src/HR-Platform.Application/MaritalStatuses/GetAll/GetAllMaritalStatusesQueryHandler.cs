using ErrorOr;
using HR_Platform.Application.MaritalStatuses.Common;
using HR_Platform.Domain.MaritalStatuses;
using MediatR;

namespace HR_Platform.Application.MaritalStatuses.GetAll;

internal sealed class GetAllMaritalStatusesQueryHandler(
    IMaritalStatusRepository maritalStatusRepository
    ) : IRequestHandler<GetAllMaritalStatusesQuery, ErrorOr<List<MaritalStatusesResponse>>>
{
    private readonly IMaritalStatusRepository _maritalStatusRepository = maritalStatusRepository ?? throw new ArgumentNullException(nameof(maritalStatusRepository));

    public async Task<ErrorOr<List<MaritalStatusesResponse>>> Handle(GetAllMaritalStatusesQuery getAllMaritalStatusesQuery, CancellationToken cancellationToken)
    {
        if (await _maritalStatusRepository.GetAll() is not List<MaritalStatus> maritalStatuses)
        {
            return Error.NotFound("MaritalStatuses.NotFound", "The marital statuses was not found.");
        }

        List<MaritalStatusesResponse> maritalStatusesResponse = [];

        if (maritalStatuses is not null && maritalStatuses.Count > 0)
        {
            foreach (MaritalStatus maritalStatus in maritalStatuses)
            {
                maritalStatusesResponse.Add
                (
                    new MaritalStatusesResponse
                    (
                        maritalStatus.Id.Value,

                        maritalStatus.Name,
                        maritalStatus.NameEnglish
                    )
                );
            }
        }

        return maritalStatusesResponse;

    }
}