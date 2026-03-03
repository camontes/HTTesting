using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.ActiveBreaks
{
    public sealed class ActiveBreak : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private ActiveBreak()
        {
        }

        public ActiveBreak(ActiveBreakId id, CompanyId companyId, string name, string description, string image, string imageName, string file, string fileName, 
            string emailWhoChangedByHR, string nameWhoChangedByHR,
            bool isVisible, bool isPinned, bool isEditable, bool isDeleteable,
            TimeDate creationDateImage, TimeDate creationDateFile, TimeDate creationDate, TimeDate editionDate)
        {
            Id = id;

            CompanyId = companyId;

            Name = name;

            Description = description;

            Image = image;
            ImageName = imageName;

            File = file;
            FileName = fileName;

            EmailWhoChangedByHR = emailWhoChangedByHR;
            NameWhoChangedByHR = nameWhoChangedByHR;

            IsVisible = isVisible;
            IsPinned = isPinned;

            IsEditable = isEditable;
            IsDeleteable = isDeleteable;

            CreationDateImage = creationDateImage;
            CreationDateFile = creationDateFile;

            CreationDate = creationDate;
            EditionDate = editionDate;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public ActiveBreakId Id { get; set; }

        public CompanyId CompanyId { get; set; }
        public Company Company { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;
        public string? ImageName { get; set; } = string.Empty;

        public string? File { get; set; } = string.Empty;
        public string? FileName { get; set; } = string.Empty;

        public string EmailWhoChangedByHR { get; set; } = string.Empty;
        public string NameWhoChangedByHR { get; set; } = string.Empty;

        public bool IsVisible { get; set; }
        public bool IsPinned { get; set; }

        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        public TimeDate? CreationDateImage { get; set; }
        public TimeDate? CreationDateFile { get; set; }

        public TimeDate CreationDate { get; set; }
        public TimeDate EditionDate { get; set; }
    }
}
