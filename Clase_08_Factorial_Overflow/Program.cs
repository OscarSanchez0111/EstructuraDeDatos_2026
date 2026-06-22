using System;
using System.Numerics;

class Program
{
    static int FactorialInt(int n)
    {
        if (n == 0 || n == 1)
            return 1;

        return n * FactorialInt(n - 1);
    }

    static int FactorialIterativo(int n)
    {
        int resultado = 1;

        for (int i = 2; i <= n; i++)
            resultado *= i;

        return resultado;
    }

    static BigInteger FactorialProfesional(BigInteger n)
    {
        if (n == 0 || n == 1)
            return BigInteger.One;

        return n * FactorialProfesional(n - 1);
    }

    static void Main()
    {
        Console.WriteLine("Clase 8 - Factorial y Overflow\n");

        Console.WriteLine("Comparación entre factorial recursivo e iterativo con int:\n");

        for (int i = 1; i <= 20; i++)
        {
            Console.WriteLine($"n={i:D2} | Recursivo: {FactorialInt(i),15} | Iterativo: {FactorialIterativo(i),15}");
        }

        // Punto de quiebre:
// El overflow comienza en n = 13.
// El valor real de 13! es 6,227,020,800 pero excede
// el límite máximo de int (2,147,483,647).
//
// Aunque el primer resultado incorrecto aparece en n = 13,
// el primer valor negativo se observa en n = 17 debido
// al comportamiento de wraparound de los enteros.

        Console.WriteLine("\nFactorial profesional usando BigInteger:\n");

        BigInteger resultado = FactorialProfesional(100);
        Console.WriteLine($"100! = {resultado}");
    }
}