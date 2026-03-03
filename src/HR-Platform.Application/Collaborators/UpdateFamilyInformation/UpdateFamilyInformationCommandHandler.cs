using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.ChildrenNamespace;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.FamilyCompositions;
using HR_Platform.Domain.MaritalStatuses;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.UpdateFamilyInformation;

internal sealed class UpdateBasicInformationCommandHandler(
    IChildrenRepository childrenRepository,
    ICollaboratorRepository collaboratorRepository,
    IFamilyCompositionRepository familyCompositionRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateFamilyInformationCommand, ErrorOr<UpdateFamilyInformationResponse>>
{
    private readonly IChildrenRepository _childrenRepository = childrenRepository ?? throw new ArgumentNullException(nameof(childrenRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IFamilyCompositionRepository _familyCompositionRepository = familyCompositionRepository ?? throw new ArgumentNullException(nameof(familyCompositionRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<UpdateFamilyInformationResponse>> Handle(UpdateFamilyInformationCommand command, CancellationToken cancellationToken)
    {
        TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.Id)) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        oldCollaborator.MaritalStatusId = new MaritalStatusId(command.MaritalStatusId);

        oldCollaborator.FamilyMembersNumber = command.FamilyMembersNumber;
        oldCollaborator.ChildrenNumber = command.ChildrenNumber;

        oldCollaborator.EditionDate = editionDate;

        oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
        oldCollaborator.EmailChangedBy = command.EmailChangeBy;

        /* Update Family Compositions */

        List<FamilyComposition> familyCompositions = await _familyCompositionRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        List<FamilyComposition> newFamilyCompositions = [];

        if (command is not null && command.FamilyComposition is not null && command.FamilyComposition.Count > 0)
        {
            foreach (UpdateFamilyCompositionCommand familyCompositionCommand in command.FamilyComposition)
            {
                if (string.IsNullOrEmpty(familyCompositionCommand.Id))
                {
                    FamilyComposition familyComposition = new
                    (
                        new FamilyCompositionId(Guid.NewGuid()),

                        oldCollaborator.Id,

                        familyCompositionCommand.Name,
                        familyCompositionCommand.NameEnglish,

                        true, // IsEditable
                        true, // IsDeleteable

                        editionDate, // CreationDate
                        editionDate // EditionDate
                    );

                    newFamilyCompositions.Add(familyComposition);
                }
                else
                {
                    if (familyCompositions is not null && familyCompositions.Count > 0)
                    {
                        foreach (FamilyComposition familyComposition in familyCompositions)
                        {
                            if (familyComposition.Id.Value.ToString() == familyCompositionCommand.Id)
                            {
                                FamilyComposition newFamilyComposition = new
                                (
                                    familyComposition.Id,

                                    oldCollaborator.Id,

                                    familyCompositionCommand.Name,
                                    familyCompositionCommand.NameEnglish,

                                    true, // IsEditable
                                    true, // IsDeleteable

                                    editionDate, // CreationDate
                                    editionDate // EditionDate
                                );

                                newFamilyCompositions.Add(newFamilyComposition);
                            }

                            _familyCompositionRepository.Delete(familyComposition);
                            //await _unitOfWork.SaveChangesAsync(cancellationToken);
                        }
                    }
                }
            }

            oldCollaborator.FamilyCompositions = []; // newFamilyCompositions;
        }
        else
        {
            if (familyCompositions is not null && familyCompositions.Count > 0)
            {
                foreach (FamilyComposition familyComposition in familyCompositions)
                {
                    _familyCompositionRepository.Delete(familyComposition);
                    //await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

            }
        }

        /* Update Children */

        List<Children> children = await _childrenRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        List<Children> newChildren = [];

        if (command is not null && command.Children is not null && command.Children.Count > 0)
        {
            foreach (UpdateChildrenCommand childrenCommand in command.Children)
            {
                if (string.IsNullOrEmpty(childrenCommand.Id))
                {
                    Children singleChildren = new
                    (
                        new ChildrenId(Guid.NewGuid()),

                        oldCollaborator.Id,

                        childrenCommand.Name,

                        childrenCommand.Age,

                        true, // IsEditable
                        true, // IsDeleteable

                        editionDate, // CreationDate
                        editionDate // EditionDate
                    );

                    newChildren.Add(singleChildren);
                    _collaboratorRepository.Update(oldCollaborator);
                }
                else
                {
                    if (children is not null && children.Count > 0)
                    {
                        foreach (Children singleChildren in children)
                        {
                            if (singleChildren.Id.Value.ToString() == childrenCommand.Id)
                            {
                                Children newSingleChildren = new
                                (
                                    singleChildren.Id,

                                    oldCollaborator.Id,

                                    childrenCommand.Name,

                                    childrenCommand.Age,

                                    true, // IsEditable
                                    true, // IsDeleteable

                                    editionDate, // CreationDate
                                    editionDate // EditionDate
                                );

                                newChildren.Add(newSingleChildren);
                            }

                            _childrenRepository.Delete(singleChildren);
                            _collaboratorRepository.Update(oldCollaborator);

                            //await _unitOfWork.SaveChangesAsync(cancellationToken);
                        }
                    }
                }
            }

            oldCollaborator.Children = []; // newChildren;
        }
        else
        {
            if (children is not null && children.Count > 0)
            {
                foreach (Children singleChildren in children)
                {
                    _childrenRepository.Delete(singleChildren);
                    _collaboratorRepository.Update(oldCollaborator);

                    //await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
        }

        /* Update Collaborator, Children and Family */

        if (newFamilyCompositions is not null && newFamilyCompositions.Count > 0)
        {
            foreach (FamilyComposition familyComposition in newFamilyCompositions)
            {
                _familyCompositionRepository.Add(familyComposition);
                _collaboratorRepository.Update(oldCollaborator);

                //await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        if (newChildren is not null && newChildren.Count > 0)
        {
            foreach (Children childrenSingle in newChildren)
            {
                _childrenRepository.Add(childrenSingle);
                _collaboratorRepository.Update(oldCollaborator);

                //await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }





        await _unitOfWork.SaveChangesAsync(cancellationToken);

        UpdateFamilyInformationResponse response = new(
            oldCollaborator.Id.Value,

            !string.IsNullOrEmpty(oldCollaborator.Document) ? oldCollaborator.Document : string.Empty,

            oldCollaborator.DocumentTypeId.Value,
            oldCollaborator.DocumentType != null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.DocumentType != null ? oldCollaborator.OtherDocumentType : string.Empty,
            oldCollaborator.DocumentType != null ? oldCollaborator.DocumentType.NameEnglish : string.Empty,

            oldCollaborator.BusinessEmail != null && !string.IsNullOrEmpty(oldCollaborator.BusinessEmail.Value) ? oldCollaborator.BusinessEmail.Value : string.Empty,

            !string.IsNullOrEmpty(oldCollaborator.Name) ? oldCollaborator.Name : string.Empty,
            !string.IsNullOrEmpty(oldCollaborator.Name) ? _stringService.GetInitials(oldCollaborator.Name) : string.Empty,

            oldCollaborator.Photo ?? string.Empty,

            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EditionDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EditionDate.Value, "MMM dd yyyy", new CultureInfo("en-US"))
        );

        return response;
    }
}