using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultCollaboratorContracts
{
    public sealed class DefaultCollaboratorContract : AggregateRoot
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private DefaultCollaboratorContract()
        {
        }

        public DefaultCollaboratorContract(DefaultCollaboratorContractId id, string arl, string bonus, string contractType, decimal salary)
        {
            Id = id;
            Arl = arl;
            Bonus = bonus;
            ContractType = contractType;
            Salary = salary;
        }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public DefaultCollaboratorContractId Id { get; set; }
        public string Arl { get; set; } = string.Empty;
        public string Bonus { get; set; } = string.Empty;
        public string ContractType { get; set; } = string.Empty;
        public decimal Salary { get; set; }

    }
}
