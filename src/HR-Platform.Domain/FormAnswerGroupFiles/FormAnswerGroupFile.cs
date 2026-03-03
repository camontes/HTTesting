using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.FormAnswerGroupFiles;

public sealed class FormAnswerGroupFile : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private FormAnswerGroupFile()
    {
    }

    public FormAnswerGroupFile(FormAnswerGroupFileId id, FormAnswerGroupId formAnswerGroupId, string file, string fileName,
        bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        FormAnswerGroupId = formAnswerGroupId;

        File = file;
        FileName = fileName;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FormAnswerGroupFileId Id { get; set; }

    public FormAnswerGroupId FormAnswerGroupId { get; set; }
    public FormAnswerGroup FormAnswerGroup { get; set; }

    public string? File { get; set; } = string.Empty;
    public string? FileName { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

