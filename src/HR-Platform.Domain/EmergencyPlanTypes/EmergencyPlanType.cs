using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.RiskTypeMains;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.EmergencyPlanTypes
{
    public sealed class EmergencyPlanType : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private EmergencyPlanType()
        {
        }
        
        public EmergencyPlanType(EmergencyPlanTypeId id, string name, string nameEnglish, CompanyId companyId, bool isVisible, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
        {
            Id = id;
            Name = name;
            NameEnglish = nameEnglish;
            CompanyId = companyId;
            IsVisible = isVisible;

            IsEditable = isEditable;
            IsDeleteable = isDeleteable;

            CreationDate = creationDate;
            EditionDate = editionDate;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public EmergencyPlanTypeId Id { get; set; }

        public CompanyId CompanyId { get; set; }
        public Company Company { get; set; }

        public string EmergencyPlanMainName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NameEnglish { get; set; } = string.Empty;
        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        public TimeDate CreationDate { get; set; }
        public TimeDate EditionDate { get; set; }

        public List<EmergencyPlan> EmergencyPlans { get; set; }
        public List<RiskTypeMain> RiskTypeMains { get; set; }

    }
}
