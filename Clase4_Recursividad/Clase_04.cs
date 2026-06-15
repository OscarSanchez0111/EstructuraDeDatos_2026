using System;
class Program
{
    static void Main()
    {
        Console.Write("Ingresa un número: ");

        string entrada = Console.ReadLine();
        int numero = int.Parse(entrada);

        long resultado = CalcularFactorial(numero);
    

        Console.WriteLine("El factorial es: " + resultado);
    }

    static long CalcularFactorial(int n)
    {
        // Validación de entrada
        if (n < 0)
            throw new ArgumentException(
            "No existe factorial de negativos.");

        // Caso Base
        if (n == 0 || n == 1)
            return 1;

        // Caso Recursivo
        return n * CalcularFactorial(n - 1);
    }
}