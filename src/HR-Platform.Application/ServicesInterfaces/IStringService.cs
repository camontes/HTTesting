namespace HR_Platform.Application.ServicesInterfaces;

public interface IStringService
{
    string NameFormat(string name);
    string StringFormat(string str);
    string GetInitials(string name);
}
