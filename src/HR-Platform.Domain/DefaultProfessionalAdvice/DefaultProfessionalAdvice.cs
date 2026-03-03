using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultProfessionalAdvices
{
    public sealed class DefaultProfessionalAdvice : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private DefaultProfessionalAdvice()
        {
        }

        public DefaultProfessionalAdvice(DefaultProfessionalAdviceId id, string name, string nameEnglish, string nameAcronyms, string nameAcronymsEnglish)
        {
            Id = id;
            Name = name;
            NameEnglish = nameEnglish;
            NameAcronyms = nameAcronyms;
            NameAcronymsEnglish = nameAcronymsEnglish;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public DefaultProfessionalAdviceId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameAcronyms { get; set; } = string.Empty;
        public string NameEnglish { get; set; } = string.Empty;
        public string NameAcronymsEnglish { get; set; } = string.Empty;

    }
}
