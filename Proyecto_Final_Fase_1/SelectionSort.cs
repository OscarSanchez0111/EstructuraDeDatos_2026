using System.Diagnostics;

namespace Proyecto_Final_Fase_1;

/// <summary>
/// Proporciona el algoritmo Selection Sort instrumentado.
/// </summary>
public static class SelectionSort
{
    /// <summary>
    /// Ordena un arreglo de registros por Id ascendente y devuelve sus métricas.
    /// </summary>
    /// <param name="arreglo">Arreglo que será ordenado en el mismo espacio.</param>
    /// <returns>Reporte con comparaciones, intercambios y tiempo.</returns>
    public static MetricasOrdenacion OrdenarPorSeleccion(
        RegistroDatos[] arreglo)
    {
        ArgumentNullException.ThrowIfNull(arreglo);

        int comparaciones = 0;
        int intercambios = 0;
        long inicio = Stopwatch.GetTimestamp();

        for (int i = 0; i < arreglo.Length - 1; i++)
        {
            int indiceMinimo = i;

            for (int j = i + 1; j < arreglo.Length; j++)
            {
                comparaciones++;

                if (arreglo[j].Id < arreglo[indiceMinimo].Id)
                {
                    indiceMinimo = j;
                }
            }

            if (indiceMinimo != i)
            {
                (arreglo[i], arreglo[indiceMinimo]) =
                    (arreglo[indiceMinimo], arreglo[i]);

                intercambios++;
            }
        }

        double tiempoMs = Stopwatch.GetElapsedTime(inicio).TotalMilliseconds;

        return new MetricasOrdenacion(
            comparaciones,
            intercambios,
            tiempoMs,
            arreglo.Length);
    }
}