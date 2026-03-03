using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.TalentPools.Common;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.TalentPools;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.TalentPools.GetById;

internal sealed class GetTalentPoolByIdQueryHandler(
    ITalentPoolRepository talentPoolRepository,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference,
    ICollaboratorTalentPoolRepository collaboratorTalentPoolRepository
    ) : IRequestHandler<GetTalentPoolByIdQuery, ErrorOr<TalentPoolByIdResponse>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ICollaboratorTalentPoolRepository _collaboratorTalentPoolRepository = collaboratorTalentPoolRepository ?? throw new ArgumentNullException(nameof(collaboratorTalentPoolRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<TalentPoolByIdResponse>> Handle(GetTalentPoolByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _talentPoolRepository.GetByIdAsync(new TalentPoolId(query.Id)) is not TalentPool oldTalentPool)
            return Error.NotFound("TalentPool.NotFound", "The Talent Pool with the provide Id was not found.");

        List<CollaboratorInfo> collaboratorInfoList = [];
        List<CollaboratorTalentPool> AllTalentPoolById = await _collaboratorTalentPoolRepository.GetByTalentPoolIdAsync(oldTalentPool.Id);

        if (AllTalentPoolById.Count > 0)
        {
            foreach (CollaboratorTalentPool item in AllTalentPoolById)
            {
                CollaboratorInfo temp = new
                (
                    item.Collaborator.Id.Value.ToString(),
                    item.Collaborator.Document,
                    item.Collaborator.DocumentType is not null ? item.Collaborator.DocumentType.Name : string.Empty,
                    item.Collaborator.DocumentType is not null ? item.Collaborator.OtherDocumentType : string.Empty,
                    item.Collaborator.Name,
                    _timeFormatService.GetDateFormatMonthLarge(item.Collaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                    _timeFormatService.GetDateFormatMonthLarge(item.Collaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // EntranceDateEnglish,
                    item.Collaborator.Assignation.Name

                );
                collaboratorInfoList.Add(temp);
            }
        }


        TalentPoolByIdResponse response = new
        (
            oldTalentPool.Id.Value.ToString(),
            oldTalentPool.Tittle,
            oldTalentPool.Description,
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldTalentPool.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldTalentPool.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            collaboratorInfoList
        );
        return response;
    }
}