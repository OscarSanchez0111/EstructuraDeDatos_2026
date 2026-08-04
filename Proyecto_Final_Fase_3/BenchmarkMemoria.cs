using System.Diagnostics;
using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_3;

/// <summary>
/// Resultados de la comparación entre arreglo y lista enlazada.
/// </summary>
public readonly struct ResultadoBenchmarkMemoria
{
    public int CantidadRegistros { get; }
    public double TiempoArregloMs { get; }
    public long BytesArreglo { get; }
    public double TiempoListaMs { get; }
    public long BytesLista { get; }
    public double TiempoConversionMs { get; }
    public long BytesConversion { get; }

    public ResultadoBenchmarkMemoria(
        int cantidadRegistros,
        double tiempoArregloMs,
        long bytesArreglo,
        double tiempoListaMs,
        long bytesLista,
        double tiempoConversionMs,
        long bytesConversion)
    {
        CantidadRegistros = cantidadRegistros;
        TiempoArregloMs = tiempoArregloMs;
        BytesArreglo = bytesArreglo;
        TiempoListaMs = tiempoListaMs;
        BytesLista = bytesLista;
        TiempoConversionMs = tiempoConversionMs;
        BytesConversion = bytesConversion;
    }
}

/// <summary>
/// Compara tiempo y asignación aproximada de memoria.
/// </summary>
public static class BenchmarkMemoria
{
    public static ResultadoBenchmarkMemoria Ejecutar(
        int cantidadRegistros)
    {
        if (cantidadRegistros <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidadRegistros),
                "La cantidad debe ser mayor que cero.");
        }

        RegistroDatos[] datosFuente =
            GenerarRegistros(cantidadRegistros);

        ForzarRecoleccion();

        long memoriaInicialArreglo =
            GC.GetAllocatedBytesForCurrentThread();

        long inicioArreglo = Stopwatch.GetTimestamp();

        RegistroDatos[] arreglo =
            new RegistroDatos[cantidadRegistros];

        for (int i = 0; i < datosFuente.Length; i++)
        {
            arreglo[i] = datosFuente[i];
        }

        double tiempoArreglo =
            Stopwatch.GetElapsedTime(inicioArreglo)
                .TotalMilliseconds;

        long bytesArreglo =
            GC.GetAllocatedBytesForCurrentThread()
            - memoriaInicialArreglo;

        GC.KeepAlive(arreglo);

        ForzarRecoleccion();

        long memoriaInicialLista =
            GC.GetAllocatedBytesForCurrentThread();

        long inicioLista = Stopwatch.GetTimestamp();

        TablaDinamica lista = new();

        foreach (RegistroDatos registro in datosFuente)
        {
            lista.InsertarFinal(registro);
        }

        double tiempoLista =
            Stopwatch.GetElapsedTime(inicioLista)
                .TotalMilliseconds;

        long bytesLista =
            GC.GetAllocatedBytesForCurrentThread()
            - memoriaInicialLista;

        long memoriaInicialConversion =
            GC.GetAllocatedBytesForCurrentThread();

        long inicioConversion = Stopwatch.GetTimestamp();

        RegistroDatos[] arregloConvertido =
            lista.ObtenerComoArreglo();

        double tiempoConversion =
            Stopwatch.GetElapsedTime(inicioConversion)
                .TotalMilliseconds;

        long bytesConversion =
            GC.GetAllocatedBytesForCurrentThread()
            - memoriaInicialConversion;

        GC.KeepAlive(lista);
        GC.KeepAlive(arregloConvertido);

        return new ResultadoBenchmarkMemoria(
            cantidadRegistros,
            tiempoArreglo,
            bytesArreglo,
            tiempoLista,
            bytesLista,
            tiempoConversion,
            bytesConversion);
    }

    private static RegistroDatos[] GenerarRegistros(
        int cantidad)
    {
        RegistroDatos[] registros =
            new RegistroDatos[cantidad];

        for (int i = 0; i < registros.Length; i++)
        {
            int id = i + 1;

            registros[i] = new RegistroDatos(
                id,
                hashValidacion: id * 10_000L,
                pesoBytes: 100);
        }

        return registros;
    }

    private static void ForzarRecoleccion()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}