using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("EF1 - Clase 15: Algoritmos de Búsqueda");
        Console.WriteLine("--------------------------------------");

        int[] matriculas = new int[10000];

        for (int i = 0; i < matriculas.Length; i++)
        {
            matriculas[i] = i + 1;
        }

        Console.WriteLine($"Arreglo generado con {matriculas.Length} matrículas.");
        Console.WriteLine($"Primera matrícula: {matriculas[0]}");
        Console.WriteLine($"Última matrícula: {matriculas[matriculas.Length - 1]}");

        Console.WriteLine();
        Console.Write("Ingresa la matrícula que deseas buscar: ");
        int objetivo = int.Parse(Console.ReadLine()!);

        int iteracionesLineal;
        int indiceLineal = BusquedaLineal(matriculas, objetivo, out iteracionesLineal);

        int iteracionesBinaria;
        int indiceBinaria = BusquedaBinaria(matriculas, objetivo, out iteracionesBinaria);

        Console.WriteLine();
        Console.WriteLine("Reporte de resultados");
        Console.WriteLine("---------------------");

        Console.WriteLine();
        Console.WriteLine("Resultado de la búsqueda lineal");

        if (indiceLineal != -1)
        {
            Console.WriteLine($"Matrícula encontrada en el índice: {indiceLineal}");
        }
        else
        {
            Console.WriteLine("Matrícula no encontrada.");
        }

        Console.WriteLine($"Iteraciones realizadas: {iteracionesLineal}");

        Console.WriteLine();
        Console.WriteLine("Resultado de la búsqueda binaria");

        if (indiceBinaria != -1)
        {
            Console.WriteLine($"Matrícula encontrada en el índice: {indiceBinaria}");
        }
        else
        {
            Console.WriteLine("Matrícula no encontrada.");
        }

        Console.WriteLine($"Iteraciones realizadas: {iteracionesBinaria}");

        Console.WriteLine();

        if (iteracionesLineal > iteracionesBinaria)
        {
            Console.WriteLine("La búsqueda binaria fue más eficiente porque realizó menos iteraciones.");
        }
        else
        {
            Console.WriteLine("Ambos algoritmos realizaron la misma cantidad de iteraciones.");
        }
    }

    static int BusquedaLineal(int[] arr, int objetivo, out int iteraciones)
    {
        iteraciones = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            iteraciones++;

            if (arr[i] == objetivo)
            {
                return i;
            }
        }

        return -1;
    }

    static int BusquedaBinaria(int[] arr, int objetivo, out int iteraciones)
    {
        iteraciones = 0;

        int izquierda = 0;
        int derecha = arr.Length - 1;

        while (izquierda <= derecha)
        {
            iteraciones++;

            int centro = izquierda + (derecha - izquierda) / 2;

            if (arr[centro] == objetivo)
            {
                return centro;
            }

            if (arr[centro] < objetivo)
            {
                izquierda = centro + 1;
            }
            else
            {
                derecha = centro - 1;
            }
        }

        return -1;
    }
}