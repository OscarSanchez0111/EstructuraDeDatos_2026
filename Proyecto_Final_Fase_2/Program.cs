using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_2;

internal static class Program
{
    private const int CantidadRegistros = 10_000;
    private const int SemillaAleatoria = 42;

    private static void Main()
    {
        Console.WriteLine("=== PROYECTO FINAL - FASE 2 ===");
        Console.WriteLine("Selection Sort vs. QuickSort");
        Console.WriteLine();

        RegistroDatos[] original =
            GenerarRegistros(CantidadRegistros, SemillaAleatoria);

        // Ambos algoritmos reciben exactamente los mismos registros.
        RegistroDatos[] copiaSeleccion =
            (RegistroDatos[])original.Clone();

        RegistroDatos[] copiaQuickSort =
            (RegistroDatos[])original.Clone();

        Console.WriteLine(
            $"Registros generados: {original.Length:N0}");
        Console.WriteLine();

        MetricasOrdenacion metricasSeleccion =
            SelectionSort.OrdenarPorSeleccion(copiaSeleccion);

        MetricasQuickSort metricasQuickSort =
            MotorQuickSort.Ordenar(copiaQuickSort);

        bool seleccionCorrecta = EstaOrdenado(copiaSeleccion);
        bool quickSortCorrecto = EstaOrdenado(copiaQuickSort);
        bool resultadosEquivalentes =
            TienenMismoOrden(copiaSeleccion, copiaQuickSort);

        ImprimirReporte(
            metricasSeleccion,
            metricasQuickSort,
            seleccionCorrecta,
            quickSortCorrecto,
            resultadosEquivalentes);
    }

    private static RegistroDatos[] GenerarRegistros(
        int cantidad,
        int semilla)
    {
        Random generador = new(semilla);
        RegistroDatos[] registros = new RegistroDatos[cantidad];
        HashSet<int> idsUtilizados = [];

        for (int i = 0; i < registros.Length; i++)
        {
            int id;

            do
            {
                id = generador.Next(1, 100_001);
            }
            while (!idsUtilizados.Add(id));

            registros[i] = new RegistroDatos(
                id,
                generador.NextInt64(1, long.MaxValue),
                generador.Next(1, 5_001));
        }

        return registros;
    }

    private static bool EstaOrdenado(RegistroDatos[] registros)
    {
        for (int i = 0; i < registros.Length - 1; i++)
        {
            if (registros[i].Id > registros[i + 1].Id)
            {
                return false;
            }
        }

        return true;
    }

    private static bool TienenMismoOrden(
        RegistroDatos[] seleccion,
        RegistroDatos[] quickSort)
    {
        if (seleccion.Length != quickSort.Length)
        {
            return false;
        }

        for (int i = 0; i < seleccion.Length; i++)
        {
            if (seleccion[i] != quickSort[i])
            {
                return false;
            }
        }

        return true;
    }

    private static void ImprimirReporte(
        MetricasOrdenacion seleccion,
        MetricasQuickSort quickSort,
        bool seleccionCorrecta,
        bool quickSortCorrecto,
        bool resultadosEquivalentes)
    {
        Console.WriteLine(
            "=== REPORTE COMPARATIVO DE ORDENAMIENTO ===");
        Console.WriteLine();

        Console.WriteLine("--- SELECTION SORT (FASE 1) ---");
        Console.WriteLine(seleccion);
        Console.WriteLine(
            $"Ordenación correcta : {seleccionCorrecta}");
        Console.WriteLine();

        Console.WriteLine("--- QUICKSORT (FASE 2) ---");
        Console.WriteLine(quickSort);
        Console.WriteLine(
            $"Ordenación correcta : {quickSortCorrecto}");
        Console.WriteLine();

        double razonVelocidad = quickSort.TiempoMs > 0
            ? seleccion.TiempoMs / quickSort.TiempoMs
            : 0;

        Console.WriteLine("--- RESULTADO GENERAL ---");
        Console.WriteLine(
            $"Resultados equivalentes : {resultadosEquivalentes}");
        Console.WriteLine(
            $"Razón de velocidad       : {razonVelocidad:F2}x");
    }
}