using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultRepeatEveryEvents
{
    public sealed class DefaultRepeatEveryEvent : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private DefaultRepeatEveryEvent()
        {
        }

        public DefaultRepeatEveryEvent(DefaultRepeatEveryEventId id, string name, string nameEnglish)
        {
            Id = id;
            Name = name;
            NameEnglish = nameEnglish;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public DefaultRepeatEveryEventId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameEnglish { get; set; } = string.Empty;
        public List<CollaboratorEducation> CollaboratorEducations { get; set; }

    }
}
