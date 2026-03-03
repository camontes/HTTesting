using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Inductions.GetByCollaboratorId;

internal sealed class GetInductionByCollaboratorIdQueryHandler(
    IInductionRepository inductionRepository,
    ICollaboratorGeneralInductionRepository collaboratorGeneralInductionRepository,
    ICollaboratorInductionRepository collaboratorInductionRepository,
    ICollaboratorRepository collaboratorRepository,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetInductionByCollaboratorIdQuery, ErrorOr<List<InductionForCollaboratorResponse>>>
{
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly ICollaboratorGeneralInductionRepository _collaboratorGeneralInductionRepository = collaboratorGeneralInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorGeneralInductionRepository));
    private readonly ICollaboratorInductionRepository _collaboratorInductionRepository = collaboratorInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorInductionRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<InductionForCollaboratorResponse>>> Handle(GetInductionByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");

        //Validar las inducciones a las que tiene acceso :  Restricciones :  Por fecha, por permiso de ojo

        List<Induction>? inductionListFull = await _inductionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));
        List<InductionForCollaboratorResponse> response = [];

        if (inductionListFull is not null && inductionListFull.Count > 0)
        {
            //Filter to exclude hidden inductions
            List<Induction> firstFilterByHiddenInduction = inductionListFull.Where(x => x.IsVisible).ToList();

            if (firstFilterByHiddenInduction.Count > 0)
            {

                //Collaborators who are allowed to see through the ** Green Eye ** 
                List<CollaboratorInduction> collaboratorInductions = await _collaboratorInductionRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

                //Collaborator who have completed their inductions
                List<CollaboratorGeneralInduction> collaboratorGeneralInduction = await _collaboratorGeneralInductionRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

                foreach (Induction induction in firstFilterByHiddenInduction)
                {
                    List<InductionFileResponse> filesList = [];

                    //Checks whether the Collaborator has already completed a specific induction
                    CollaboratorGeneralInduction? IsCollaboratorFinishedInduction = collaboratorGeneralInduction.SingleOrDefault(x => x.CollaboratorId.Value == oldCollaborator.Id.Value && x.InductionId.Value == induction.Id.Value);

                    //Has Access For Seeing a specific induction  **
                    CollaboratorInduction? HasAccessForSeeing = collaboratorInductions.SingleOrDefault(x => x.CollaboratorId.Value == oldCollaborator.Id.Value && x.InductionId.Value == induction.Id.Value);

                    //Filter the inductions that the Collaborator can see when the entry date is greater than the creation of the induction ***
                    List<Induction> filterByCreationDate = inductionListFull.Where(x => x.CreationDate.Value < oldCollaborator.CreationDate.Value && x.AllowForAllCollaborators).ToList();

                    // If the collaborator has the green eye option, the date validation does not enter.
                    List<Induction> filterByGreenEye = inductionListFull.Where(x => !x.AllowForAllCollaborators && HasAccessForSeeing is not null).ToList();

                    List<Induction> secondFilterByCreationDate = [.. filterByCreationDate, .. filterByGreenEye];

                    //the only way it can appear is if the induction has been completed and the induction has been eliminated.
                    bool showWhenHasDeletedButHasInductionCompleted = IsCollaboratorFinishedInduction is not null && induction.IsInductionDeleted;


                    if (secondFilterByCreationDate.Count > 0)
                    {
                        if (!induction.IsInductionDeleted)
                        {
                            foreach (InductionFile file in induction.InductionFiles)
                            {
                                InductionFileResponse fileTemp = new
                                (
                                    file.Id.Value,
                                    file.FileName,
                                    file.UrlFile,
                                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", induction.CreationDate.Value).Split('.')[0]), // TimePosted
                                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", induction.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                                    _timeFormatService.GetDateFormatMonthLarge(induction.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // CreationDate,
                                    _timeFormatService.GetDateFormatMonthLarge(induction.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // CreationDateEnglish,
                                    _timeFormatService.GetDateTimeFormatMonthToltip(induction.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) // CreationDateTooltip,
                                );
                                filesList.Add(fileTemp);
                            }
                        }

                        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(induction.EmailWhoDeletedByTH);

                        bool showWithValidation = true;

                        if (IsCollaboratorFinishedInduction is null && induction.IsInductionDeleted)
                        {
                            showWithValidation = false;
                        }
                        
                        if (showWithValidation)
                        {
                            InductionForCollaboratorResponse temp = new //Missing more data 
                            (
                               induction.Id.Value.ToString(),
                               induction.Name,
                               !induction.IsInductionDeleted ? induction.Description : string.Empty,
                               !induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(induction.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // UpdatedFormat,
                               !induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(induction.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")) : string.Empty, // UpdatedFormatEnglish,
                               !induction.IsInductionDeleted ? _timeFormatService.GetDateTimeFormatMonthToltip(induction.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, // CreationDateTooltip,
                               !induction.IsInductionDeleted ? induction.NameWhoChangedByTH : string.Empty, // FullNameTh
                               induction.IsVisible,
                               induction.AllowForAllCollaborators,
                               induction.CreationDate.Value,
                               IsCollaboratorFinishedInduction is not null ? _timeFormatService.GetDateFormatMonthLarge(IsCollaboratorFinishedInduction.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, //InductionAnswerDate
                               IsCollaboratorFinishedInduction is not null ? _timeFormatService.GetDateFormatMonthLarge(IsCollaboratorFinishedInduction.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")) : string.Empty, //InductionAnswerDateEnglish
                               IsCollaboratorFinishedInduction is not null ? _timeFormatService.GetDateTimeFormatMonthToltip(IsCollaboratorFinishedInduction.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, // InductionAnswerDateToltip,
                               !induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(induction.IsVisibleChangeDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // InductionSendDate,
                               !induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(induction.IsVisibleChangeDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")) : string.Empty, // InductionSendDateEnglish,
                               !induction.IsInductionDeleted ? _timeFormatService.GetDateTimeFormatMonthToltip(induction.IsVisibleChangeDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, // InductionSendDateToltip,
                               induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(induction.DeleteDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // InductionDeleteDate,
                               induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(induction.DeleteDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")) : string.Empty, // InductionDeleteDateEnglish,
                               induction.IsInductionDeleted ? _timeFormatService.GetDateTimeFormatMonthToltip(induction.DeleteDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, // InductionDeleteDateToltip,
                               induction.IsInductionDeleted && CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty, // NameWhoDeletedByTh
                               IsCollaboratorFinishedInduction is not null, // If it exists, it is because induction has finished
                               filesList
                            );
                            if (temp.AllowForAllCollaborators)
                            {
                                response.Add(temp);
                            }
                            else
                            {
                                if (HasAccessForSeeing is not null)
                                {
                                    response.Add(temp);
                                }

                            }
                        }
                    }
                }
            }

        }
        return response.OrderByDescending(x => x.CreacionDate).ToList();
    }
}