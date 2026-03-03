using HR_Platform.Application.ServicesInterfaces;
using System.Text;

namespace HR_Platform.Application.Services;

public class ReferenceGenerator : IReferenceGenerator
{
    private static readonly Random random = new Random();

    public string GenerateReference(string Start)
    {
        // "B" indica que es un beneficio, siempre fijo y en mayúscula
        var reference = new StringBuilder(Start);

        // Generar 4 letras aleatorias en mayúsculas
        for (int i = 0; i < 4; i++)
        {
            char randomLetter = (char)random.Next('A', 'Z' + 1); // Letras mayúsculas
            reference.Append(randomLetter);
        }

        // Generar 6 números aleatorios
        for (int i = 0; i < 6; i++)
        {
            int randomNumber = random.Next(0, 10); // Números del 0 al 9
            reference.Append(randomNumber);
        }

        return reference.ToString();
    }
}