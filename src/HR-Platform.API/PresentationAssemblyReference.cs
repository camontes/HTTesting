using System.Reflection;

namespace HR_Platform.API;

public class PresentationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}