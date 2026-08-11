using System.Diagnostics;
using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_2;

/// <summary>
/// Implementa QuickSort recursivo e instrumentado por Id ascendente.
/// </summary>
public static class MotorQuickSort
{
    private static long comparaciones;
    private static long intercambios;
    private static long llamadasRecursivas;
    private static int profundidadMaxima;

    /// <summary>
    /// Ordena el arreglo y devuelve las métricas de ejecución.
    /// </summary>
    public static MetricasQuickSort Ordenar(RegistroDatos[] arreglo)
    {
        ArgumentNullException.ThrowIfNull(arreglo);

        comparaciones = 0;
        intercambios = 0;
        llamadasRecursivas = 0;
        profundidadMaxima = 0;

        long inicio = Stopwatch.GetTimestamp();

        QuickSort(
            arreglo,
            0,
            arreglo.Length - 1,
            profundidadActual: 1);

        double tiempoMs =
            Stopwatch.GetElapsedTime(inicio).TotalMilliseconds;

        return new MetricasQuickSort(
            comparaciones,
            intercambios,
            llamadasRecursivas,
            profundidadMaxima,
            tiempoMs,
            arreglo.Length);
    }

    /// <summary>
    /// Divide recursivamente el arreglo alrededor de cada pivote.
    /// </summary>
    private static void QuickSort(
        RegistroDatos[] arreglo,
        int bajo,
        int alto,
        int profundidadActual)
    {
        llamadasRecursivas++;

        if (profundidadActual > profundidadMaxima)
        {
            profundidadMaxima = profundidadActual;
        }

        if (bajo >= alto)
        {
            return;
        }

        int indicePivote = Particionar(arreglo, bajo, alto);

        QuickSort(
            arreglo,
            bajo,
            indicePivote - 1,
            profundidadActual + 1);

        QuickSort(
            arreglo,
            indicePivote + 1,
            alto,
            profundidadActual + 1);
    }

    /// <summary>
    /// Particiona mediante Lomuto y coloca el pivote en su posición final.
    /// </summary>
    private static int Particionar(
        RegistroDatos[] arreglo,
        int bajo,
        int alto)
    {
        int indicePivote = SeleccionarPivoteMedianaDeTres(
            arreglo,
            bajo,
            alto);

        Intercambiar(arreglo, indicePivote, alto);

        RegistroDatos pivote = arreglo[alto];
        int indiceMenor = bajo - 1;

        for (int actual = bajo; actual < alto; actual++)
        {
            comparaciones++;

            if (arreglo[actual].Id <= pivote.Id)
            {
                indiceMenor++;
                Intercambiar(arreglo, indiceMenor, actual);
            }
        }

        Intercambiar(arreglo, indiceMenor + 1, alto);

        return indiceMenor + 1;
    }

    /// <summary>
    /// Selecciona la mediana entre el primer, central y último elemento.
    /// </summary>
    private static int SeleccionarPivoteMedianaDeTres(
        RegistroDatos[] arreglo,
        int bajo,
        int alto)
    {
        int primero = bajo;
        int central = bajo + ((alto - bajo) / 2);
        int ultimo = alto;

        comparaciones++;

        if (arreglo[primero].Id > arreglo[central].Id)
        {
            (primero, central) = (central, primero);
        }

        comparaciones++;

        if (arreglo[central].Id > arreglo[ultimo].Id)
        {
            (central, ultimo) = (ultimo, central);
        }

        comparaciones++;

        if (arreglo[primero].Id > arreglo[central].Id)
        {
            (primero, central) = (central, primero);
        }

        return central;
    }

    /// <summary>
    /// Intercambia dos posiciones y evita contar movimientos consigo mismas.
    /// </summary>
    private static void Intercambiar(
        RegistroDatos[] arreglo,
        int indiceA,
        int indiceB)
    {
        if (indiceA == indiceB)
        {
            return;
        }

        (arreglo[indiceA], arreglo[indiceB]) =
            (arreglo[indiceB], arreglo[indiceA]);

        intercambios++;
    }
}