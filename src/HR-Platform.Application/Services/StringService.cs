using HR_Platform.Application.ServicesInterfaces;

namespace HR_Platform.Application.Services;

public class StringService : IStringService
{
    public string NameFormat(string name)
    {
        string nameFormat = string.Empty;

        if (string.IsNullOrEmpty(name))
            return string.Empty;

        string[] nameSplit = name.Split();

        if (nameSplit != null && nameSplit.Length > 0)
        {
            foreach (string nameSingle in nameSplit)
            {
                nameFormat += StringFormat(nameSingle) + " ";
            }
        }

        return nameFormat.TrimEnd();
    }

    public string StringFormat(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        return string.Concat(str[0].ToString().ToUpper(), str.ToLower().AsSpan(1));
    }

    public string GetInitials(string name)
    {
        if (string.IsNullOrEmpty(name))
            return string.Empty;

        string[] nameSplit = name.Split(" ");

        if (nameSplit.Length == 1)
            return name[0].ToString().ToUpper();

        return nameSplit[0][0].ToString().ToUpper() + nameSplit[^1][0].ToString();
    }
}
