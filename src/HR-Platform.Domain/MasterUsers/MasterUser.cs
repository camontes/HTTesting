using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.MasterUsers;

public sealed class MasterUser : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private MasterUser()
    {
    }

    public MasterUser(MasterUserId id, Email email, string name, string nameEnglish, PhoneNumber phoneNumber, string photo, string photoName, string roleName, string roleNameEnglish, string loginCode,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        Email = email;

        Name = name;
        NameEnglish = nameEnglish;

        PhoneNumber = phoneNumber;
        Photo = photo;
        PhotoName = photoName;

        RoleName = roleName;
        RoleNameEnglish = roleNameEnglish;

        LoginCode = loginCode;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }


#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public MasterUserId Id { get; set; }

    public Email Email { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public PhoneNumber PhoneNumber { get; set; }

    public string Photo { get; set; } = string.Empty;
    public string PhotoName { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;
    public string RoleNameEnglish { get; set; } = string.Empty;

    public string LoginCode { get; set; } = string.Empty;

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

