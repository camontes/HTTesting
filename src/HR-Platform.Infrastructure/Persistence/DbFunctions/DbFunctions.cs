using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.DbFunctions;

public static class DbFunctions
{
    [DbFunction("remove_accents", "public")] 
    public static string RemoveAccents(string input) => throw new NotImplementedException();
}