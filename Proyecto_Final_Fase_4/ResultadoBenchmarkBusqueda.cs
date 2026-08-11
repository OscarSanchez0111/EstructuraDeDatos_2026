namespace Proyecto_Final_Fase_4;

/// <summary>
/// Almacena los resultados de una comparación entre búsquedas.
/// </summary>
public readonly struct ResultadoBenchmarkBusqueda
{
    public int CantidadRegistros { get; }
    public int IdBuscado { get; }
    public int ComparacionesLineales { get; }
    public int ComparacionesBinarias { get; }
    public double TiempoLinealMs { get; }
    public double TiempoBinarioMs { get; }
    public bool ResultadosEquivalentes { get; }

    /// <summary>
    /// Inicializa el reporte comparativo de búsquedas.
    /// </summary>
    public ResultadoBenchmarkBusqueda(
        int cantidadRegistros,
        int idBuscado,
        int comparacionesLineales,
        int comparacionesBinarias,
        double tiempoLinealMs,
        double tiempoBinarioMs,
        bool resultadosEquivalentes)
    {
        CantidadRegistros = cantidadRegistros;
        IdBuscado = idBuscado;
        ComparacionesLineales = comparacionesLineales;
        ComparacionesBinarias = comparacionesBinarias;
        TiempoLinealMs = tiempoLinealMs;
        TiempoBinarioMs = tiempoBinarioMs;
        ResultadosEquivalentes = resultadosEquivalentes;
    }

    /// <summary>
    /// Devuelve el factor aproximado de reducción de comparaciones.
    /// </summary>
    public double FactorReduccionComparaciones =>
        ComparacionesBinarias > 0
            ? (double)ComparacionesLineales /
              ComparacionesBinarias
            : 0;

    /// <summary>
    /// Devuelve el reporte en formato legible.
    /// </summary>
    /// <returns>Reporte comparativo.</returns>
    public override string ToString()
    {
        return
            $"Registros evaluados       : " +
            $"{CantidadRegistros:N0}\n" +
            $"ID buscado                : {IdBuscado:N0}\n\n" +
            $"--- BÚSQUEDA LINEAL O(n) ---\n" +
            $"Comparaciones             : " +
            $"{ComparacionesLineales:N0}\n" +
            $"Tiempo                    : " +
            $"{TiempoLinealMs:F4} ms\n\n" +
            $"--- BÚSQUEDA BINARIA O(log n) ---\n" +
            $"Comparaciones             : " +
            $"{ComparacionesBinarias:N0}\n" +
            $"Tiempo                    : " +
            $"{TiempoBinarioMs:F4} ms\n\n" +
            $"--- RESULTADO ---\n" +
            $"Resultados equivalentes   : " +
            $"{ResultadosEquivalentes}\n" +
            $"Reducción de comparaciones: " +
            $"{FactorReduccionComparaciones:N2}x";
    }
}