using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int[] calificaciones = new int[100];
            Random rng = new Random();

            // Generar 100 calificaciones aleatorias entre 0 y 100
            for (int i = 0; i < calificaciones.Length; i++)
            {
                calificaciones[i] = rng.Next(0, 101);
            }

            Console.WriteLine(" Estado inicial: calificaciones desordenadas ");
            ImprimirArreglo(calificaciones);

            // Ordenar el arreglo
            OrdenarPorBurbuja(calificaciones);

            Console.WriteLine("\n Estado final: calificaciones ordenadas (menor a mayor) ");
            ImprimirArreglo(calificaciones);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"[ERROR] Índice fuera de rango detectado: {ex.Message}");
            Console.WriteLine("Revisa los límites de tus ciclos for anidados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR inesperado]: {ex.Message}");
        }
    }

    static void ImprimirArreglo(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }

    static void OrdenarPorBurbuja(int[] arr)
    {
        int n = arr.Length;
        int contadorIntercambios = 0;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Intercambio utilizando tuplas (C# moderno)
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);

                    contadorIntercambios++;
                }
            }
        }

        Console.WriteLine($"\nTotal de intercambios realizados: {contadorIntercambios}");
    }
}