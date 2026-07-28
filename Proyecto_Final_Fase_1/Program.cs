namespace Proyecto_Final_Fase_1;

internal static class Program
{
    private const int CantidadRegistros = 40;

    private static void Main()
    {
        var generador = new Random(42);
        var idsUtilizados = new HashSet<int>();
        var registros = new RegistroDatos[CantidadRegistros];

        try
        {
            for (int i = 0; i < registros.Length; i++)
            {
                int id;

                do
                {
                    id = generador.Next(1, 1001);
                }
                while (!idsUtilizados.Add(id));

                registros[i] = new RegistroDatos(
                    id,
                    generador.NextInt64(),
                    generador.Next(10, 5001));
            }
        }
        catch (ArgumentException excepcion)
        {
            Console.WriteLine(
                $"Error al crear un registro: {excepcion.Message}");
            return;
        }

        ImprimirRegistros("ESTADO INICIAL", registros);

        MetricasOrdenacion metricas =
            SelectionSort.OrdenarPorSeleccion(registros);

        ImprimirRegistros("ESTADO FINAL ORDENADO", registros);

        Console.WriteLine("\n=== MÉTRICAS DE ORDENACIÓN ===");
        Console.WriteLine(metricas);

        Console.WriteLine(
            $"\nOrdenación correcta: {EstaOrdenado(registros)}");
    }

    private static void ImprimirRegistros(
        string titulo,
        RegistroDatos[] registros)
    {
        Console.WriteLine($"\n=== {titulo} ===");

        foreach (RegistroDatos registro in registros)
        {
            Console.WriteLine(registro);
        }
    }

    private static bool EstaOrdenado(RegistroDatos[] registros)
    {
        for (int i = 1; i < registros.Length; i++)
        {
            if (registros[i - 1].Id > registros[i].Id)
            {
                return false;
            }
        }

        return true;
    }
}