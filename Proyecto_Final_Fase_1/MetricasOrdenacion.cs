namespace Proyecto_Final_Fase_1;

/// <summary>
/// Almacena las métricas obtenidas durante un proceso de ordenación.
/// </summary>
public readonly struct MetricasOrdenacion
{
    public int TotalComparaciones { get; }
    public int TotalIntercambios { get; }
    public double TiempoMs { get; }
    public int TamanoEntrada { get; }

    /// <summary>
    /// Inicializa el reporte de métricas del algoritmo.
    /// </summary>
    public MetricasOrdenacion(
        int totalComparaciones,
        int totalIntercambios,
        double tiempoMs,
        int tamanoEntrada)
    {
        TotalComparaciones = totalComparaciones;
        TotalIntercambios = totalIntercambios;
        TiempoMs = tiempoMs;
        TamanoEntrada = tamanoEntrada;
    }

    public override string ToString()
    {
        return
            $"Tamaño de entrada : {TamanoEntrada}\n" +
            $"Comparaciones     : {TotalComparaciones}\n" +
            $"Intercambios      : {TotalIntercambios}\n" +
            $"Tiempo            : {TiempoMs:F4} ms";
    }
}