using ErrorOr;
using HR_Platform.Application.PriorityNovelties.Common;
using HR_Platform.Domain.PriorityNovelties;
using MediatR;

namespace HR_Platform.Application.PriorityNovelties.GetAll;

internal sealed class GetAllPriorityNoveltiesQueryHandler : IRequestHandler<GetAllPriorityNoveltiesQuery, ErrorOr<List<PriorityNoveltiesResponse>>>
{
    private readonly IPriorityNoveltyRepository _priorityNoveltyRepository;

    public GetAllPriorityNoveltiesQueryHandler
    (
        IPriorityNoveltyRepository priorityNoveltyRepository
    )
    {
        _priorityNoveltyRepository = priorityNoveltyRepository ?? throw new ArgumentNullException(nameof(priorityNoveltyRepository));
    }

    public async Task<ErrorOr<List<PriorityNoveltiesResponse>>> Handle(GetAllPriorityNoveltiesQuery getAllPriorityNoveltiesQuery, CancellationToken cancellationToken)
    {
        if (await _priorityNoveltyRepository.GetAll() is not List<PriorityNovelty> priorityNovelties)
        {
            return Error.NotFound("PriorityNovelties.NotFound", "The priority novelties compositions was not found.");
        }

        List<PriorityNoveltiesResponse> priorityNoveltiesResponse = new();

        if (priorityNovelties is not null && priorityNovelties.Count > 0)
        {
            foreach (PriorityNovelty priorityNovelty in priorityNovelties)
            {
                priorityNoveltiesResponse.Add
                (
                    new PriorityNoveltiesResponse
                    (
                        priorityNovelty.Id.Value,

                        priorityNovelty.Name,
                        priorityNovelty.NameEnglish
                    )
                );
            }
        }

        return priorityNoveltiesResponse;

    }
}