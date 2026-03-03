using HR_Platform.Domain.Banks;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace HR_Platform.Domain.BankAccounts;

public sealed class BankAccount : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private BankAccount()
    {
    }

    public BankAccount(BankAccountId id, BankId bankId, TypeAccountId typeAccountId, string accountNumberString, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        BankId = bankId;
        TypeAccountId = typeAccountId;
        AccountNumber = accountNumberString;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BankAccountId Id { get; set; }

    public BankId BankId { get; set; }
    public Bank Bank { get; set; }

    public TypeAccountId TypeAccountId { get; set; }
    public TypeAccount TypeAccount { get; set; }
    
    public string AccountNumber { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<Collaborator> Collaborators { get; set; }

}

