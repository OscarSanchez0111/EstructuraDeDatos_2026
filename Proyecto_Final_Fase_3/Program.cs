using Proyecto_Final_Fase_1;
using Proyecto_Final_Fase_2;

namespace Proyecto_Final_Fase_3;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("=== PROYECTO FINAL - FASE 3 ===");
        Console.WriteLine("Lista simplemente enlazada en memoria Heap");
        Console.WriteLine();

        TablaDinamica dataCore = new();

        // Paso 1: insertar 15 registros al final.
        for (int id = 1; id <= 15; id++)
        {
            RegistroDatos registro = new(
                id,
                hashValidacion: id * 100_000L + 7,
                pesoBytes: id * 100);

            dataCore.InsertarFinal(registro);

            Console.WriteLine(
                $"[INSERT] Registro {id} añadido a la cadena.");
        }

        Console.WriteLine();
        Console.WriteLine($"Cantidad inicial: {dataCore.Cantidad}");
        Console.WriteLine();

        // Paso 2: eliminar dos registros.
        Console.WriteLine(
            "--- Eliminando registros con Id 5 y Id 11 ---");

        dataCore.EliminarPorId(5);
        dataCore.EliminarPorId(11);

        Console.WriteLine(
            "Cadena reestructurada correctamente.");
        Console.WriteLine(
            $"Cantidad después de eliminar: {dataCore.Cantidad}");
        Console.WriteLine();

        // Paso 3: comprobar la búsqueda.
        RegistroDatos? encontrado =
            dataCore.BuscarPorId(8);

        Console.WriteLine(
            encontrado.HasValue
                ? $"Búsqueda Id 8: encontrado"
                : $"Búsqueda Id 8: no encontrado");

        Console.WriteLine();

        // Paso 4: convertir a arreglo.
        RegistroDatos[] arreglo =
            dataCore.ObtenerComoArreglo();

        Console.WriteLine(
            $"Registros en arreglo: {arreglo.Length} " +
            "(esperado: 13)");

        // Paso 5: ordenar mediante QuickSort de la Fase 2.
        MetricasQuickSort metricas =
            MotorQuickSort.Ordenar(arreglo);

        Console.WriteLine();
        Console.WriteLine(
            "--- ARREGLO ORDENADO POR ID (QUICKSORT) ---");

        foreach (RegistroDatos registro in arreglo)
        {
            Console.WriteLine(registro);
        }

        bool ordenCorrecto = EstaOrdenado(arreglo);
        bool eliminadosAusentes =
            arreglo.All(registro =>
                registro.Id != 5 && registro.Id != 11);

        Console.WriteLine();
        Console.WriteLine("--- VERIFICACIÓN ---");
        Console.WriteLine(
            $"Cantidad correcta       : {arreglo.Length == 13}");
        Console.WriteLine(
            $"Ordenación correcta     : {ordenCorrecto}");
        Console.WriteLine(
            $"Id 5 y 11 eliminados    : {eliminadosAusentes}");
        Console.WriteLine(
            $"Sin referencias nulas   : " +
            $"{arreglo.All(registro => registro.PesoBytes > 0)}");

        Console.WriteLine();
        Console.WriteLine("--- MÉTRICAS DE QUICKSORT ---");
        Console.WriteLine(metricas);
    }

    private static bool EstaOrdenado(
        RegistroDatos[] registros)
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
}