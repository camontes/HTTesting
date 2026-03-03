using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.WorkplaceEvidences.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceEvidences;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceEvidences.GetByCollaboratorId;

internal sealed class GetWorkplaceEvidencesByCollaboratorIdHandler(
    IWorkplaceEvidenceRepository WorkplaceEvidenceRepository,
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetWorkplaceEvidencesByCollaboratorIdQuery, ErrorOr<WorkplaceEvidencesResponse>>
{
    private readonly IWorkplaceEvidenceRepository _workplaceEvidenceRepository = WorkplaceEvidenceRepository ?? throw new ArgumentNullException(nameof(WorkplaceEvidenceRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<WorkplaceEvidencesResponse>> Handle(GetWorkplaceEvidencesByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        Collaborator? oldCollaborator = null;

        if (!string.IsNullOrEmpty(query.CollaboratorId))
        {
            oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.TryParse(query.CollaboratorId, out Guid result) ? result : Guid.NewGuid()));
        }
        else
        {
            oldCollaborator = await _collaboratorRepository.GetByEmailAsync(query.EmailChangeBy);
        }

        if (oldCollaborator == null)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        List<WorkplaceEvidence>? workplaceEvidenceList = await _workplaceEvidenceRepository.GetByCollaboratorIdAsync(oldCollaborator.Id, query.Year);
        List<WorkplaceEvidenceFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (workplaceEvidenceList is not null && workplaceEvidenceList.Count > 0)
        {
            foreach (WorkplaceEvidence item in workplaceEvidenceList)
            {
                WorkplaceEvidenceFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.FileName, // FileName
                   item.UrlFile, // FileURL
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   item.CreationDate.Value, // CreationDate
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                   item.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                   item.NameWhoChangedByTH, // FullNameTh
                   _stringService.GetInitials(item.NameWhoChangedByTH) // ShortNameTh
                );

                filesList.Add(temp);
            }
        }

        if (workplaceEvidenceList is not null)
        {
            distinctYears = workplaceEvidenceList
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        WorkplaceEvidencesResponse workplaceEvidencesResponse = new
        (
            oldCollaborator.Id.Value,
            [.. filesList.OrderByDescending(x => x.CreationDate)],
            distinctYears
        );

        return workplaceEvidencesResponse;

    }
}