using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.Surveys;

namespace HR_Platform.Domain.Areas
{
    public sealed class Area : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private Area()
        {
        }

        public Area(AreaId id, string name, string nameEnglish, bool isFormsVisible)
        {
            Id = id;

            Name = name;
            NameEnglish = nameEnglish;

            IsFormsVisible = isFormsVisible;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public AreaId Id { get; set; }

        public CompanyId CompanyId { get; set; }
        public Company Company { get; set; }

        public string Name { get; set; } = string.Empty;
        public string NameEnglish { get; set; } = string.Empty;

        public bool IsFormsVisible { get; set; }

        public List<Form> Forms { get; set; }
        public List<Role> Roles { get; set; }
        public List<Survey> Surveys { get; set; }

    }
}
