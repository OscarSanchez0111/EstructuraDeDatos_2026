namespace Proyecto_Final_Fase_2;

/// <summary>
/// Almacena las métricas obtenidas durante la ejecución de QuickSort.
/// </summary>
public readonly struct MetricasQuickSort
{
    public long TotalComparaciones { get; }
    public long TotalIntercambios { get; }
    public long TotalLlamadasRecursivas { get; }
    public int ProfundidadMaxima { get; }
    public double TiempoMs { get; }
    public int TamanoEntrada { get; }

    public MetricasQuickSort(
        long totalComparaciones,
        long totalIntercambios,
        long totalLlamadasRecursivas,
        int profundidadMaxima,
        double tiempoMs,
        int tamanoEntrada)
    {
        TotalComparaciones = totalComparaciones;
        TotalIntercambios = totalIntercambios;
        TotalLlamadasRecursivas = totalLlamadasRecursivas;
        ProfundidadMaxima = profundidadMaxima;
        TiempoMs = tiempoMs;
        TamanoEntrada = tamanoEntrada;
    }

    public override string ToString()
    {
        return
            $"Tamaño de entrada   : {TamanoEntrada}\n" +
            $"Comparaciones       : {TotalComparaciones}\n" +
            $"Intercambios        : {TotalIntercambios}\n" +
            $"Llamadas recursivas : {TotalLlamadasRecursivas}\n" +
            $"Profundidad máxima  : {ProfundidadMaxima}\n" +
            $"Tiempo              : {TiempoMs:F4} ms";
    }
}