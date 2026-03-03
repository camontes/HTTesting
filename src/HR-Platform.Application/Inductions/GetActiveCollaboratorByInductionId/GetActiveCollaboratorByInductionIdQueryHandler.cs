using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using HR_Platform.Application.Services;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using MediatR;

namespace HR_Platform.Application.Inductions.GetActiveCollaboratorByInductionId;

internal sealed class GetActiveCollaboratorByInductionIdQueryHandler(
    IInductionRepository inductionRepository,
    ICollaboratorInductionRepository collaboratorInductionRepository,
    ICollaboratorGeneralInductionRepository collaboratorGeneralInductionRepository,
    IStringService stringService

    ) : IRequestHandler<GetActiveCollaboratorByInductionIdQuery, ErrorOr<List<CollaboratorActiveResponse>>>
{
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly ICollaboratorInductionRepository _collaboratorInductionRepository = collaboratorInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorInductionRepository));
    private readonly ICollaboratorGeneralInductionRepository _collaboratorGeneralInductionRepository = collaboratorGeneralInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorGeneralInductionRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorActiveResponse>>> Handle(GetActiveCollaboratorByInductionIdQuery query, CancellationToken cancellationToken)
    {
        if (await _inductionRepository.GetByIdAsync(new InductionId(query.InductionId)) is not Induction oldInduction)
            return Error.NotFound("Induction.NotFound", "The Induction with the provide Id was not found.");


        List<CollaboratorActiveResponse> response = [];
        List<CollaboratorInduction>? collaboratorInductions = await _collaboratorInductionRepository.GetByInductionIdAsync(oldInduction.Id);

        List<CollaboratorGeneralInduction> collaboratorGeneralInductions = await _collaboratorGeneralInductionRepository.GetAllAsync();

        IEnumerable<CollaboratorId> collaboratorGeneralIds = collaboratorGeneralInductions.Select(z => z.CollaboratorId).ToList();


        if (collaboratorInductions is not null && collaboratorInductions.Count > 0)
        {
            foreach (CollaboratorInduction item in collaboratorInductions)
            {
                CollaboratorActiveResponse temp = new
                (
                    item.Collaborator.Id.Value.ToString(),
                    item.Collaborator.Name,
                    item.Collaborator.PersonalEmail.Value,
                    item.Collaborator.BusinessEmail.Value,
                    !string.IsNullOrEmpty(item.Collaborator.Photo) ? item.Collaborator.Photo : string.Empty, //Photo
                    _stringService.GetInitials(item.Collaborator.Name), // ShortNameTh
                    collaboratorGeneralIds.Contains(item.CollaboratorId)
                );
                response.Add(temp);
            }
        }
        return response;
    }
}
