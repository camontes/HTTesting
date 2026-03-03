using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.FormAnswerGroupFiles;
using HR_Platform.Domain.FormAnswerGroupStates;
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.FormAnswerGroups;

public sealed class FormAnswerGroup : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private FormAnswerGroup()
    {
    }

    public FormAnswerGroup(FormAnswerGroupId id, FormId formId, CollaboratorId collaboratorId, FormAnswerGroupStateId formAnswerGroupStateId,
        string referenceNumber, string descriptionState, string file, string fileName, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        FormId = formId;

        CollaboratorId = collaboratorId;

        FormAnswerGroupStateId = formAnswerGroupStateId;

        ReferenceNumber = referenceNumber;

        DescriptionState = descriptionState;

        File = file;
        FileName = fileName;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FormAnswerGroupId Id { get; set; }

    public FormId FormId { get; set; }
    public Form Form { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public FormAnswerGroupStateId FormAnswerGroupStateId { get; set; }
    public FormAnswerGroupState FormAnswerGroupState { get; set; }

    public string ReferenceNumber { get; set; } = string.Empty;

    public string? DescriptionState { get; set; } = string.Empty;
        
    public string? File { get; set; } = string.Empty;
    public string? FileName { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<FormAnswer> FormAnswers { get; set; }
    public List<FormAnswerGroupFile> FormAnswerGroupFiles { get; set; }
}

