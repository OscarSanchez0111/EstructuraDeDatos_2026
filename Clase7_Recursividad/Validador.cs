using System;

public class Validador
{
    public static int SolicitarNumeroPositivo()
    {
        Console.Write("Introduce un número entero positivo: ");

        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int numero) && numero > 0)
        {
            return numero;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: Solo se aceptan enteros positivos.");
        Console.ResetColor();

        return -1;
    }
}