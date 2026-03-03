using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultSoftSkills
{
    public sealed class DefaultSoftSkill : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private DefaultSoftSkill()
        {
        }

        public DefaultSoftSkill(DefaultSoftSkillId id, string name, string nameEnglish)
        {
            Id = id;
            Name = name;
            NameEnglish = nameEnglish;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public DefaultSoftSkillId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameEnglish { get; set; } = string.Empty;
        public List<CollaboratorSoftSkill> CollaboratorSoftSkills { get; set; }


    }
}
