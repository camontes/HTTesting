using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.EmergencyPlans
{
    public sealed class EmergencyPlan : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private EmergencyPlan()
        {
        }

        public EmergencyPlan(EmergencyPlanId id, EmergencyPlanTypeId emergencyPlanTypeId, string name, string description, string imageURL, string imageName, TimeDate imageCreationTime, string videoURL, string videoName, bool isVisible, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
        {
            Id = id;
            EmergencyPlanTypeId = emergencyPlanTypeId;
            Name = name;
            Description = description;
            ImageURL = imageURL;
            ImageName = imageName;
            ImageCreationTime = imageCreationTime;
            VideoURL = videoURL;
            VideoName = videoName;

            IsVisible = isVisible;


            IsEditable = isEditable;
            IsDeleteable = isDeleteable;

            CreationDate = creationDate;
            EditionDate = editionDate;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public EmergencyPlanId Id { get; set; }

        public EmergencyPlanType EmergencyPlanType { get; set; }
        public EmergencyPlanTypeId EmergencyPlanTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageURL { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public TimeDate ImageCreationTime { get; set; }

        public string VideoURL { get; set; } = string.Empty;
        public string VideoName { get; set; } = string.Empty;

        public bool IsVisible { get; set; } = false;

        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        public TimeDate CreationDate { get; set; }
        public TimeDate EditionDate { get; set; }


    }
}
