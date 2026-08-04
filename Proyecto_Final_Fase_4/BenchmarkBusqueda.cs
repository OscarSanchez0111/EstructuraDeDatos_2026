using System.Diagnostics;
using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_4;

/// <summary>
/// Compara la búsqueda lineal con la búsqueda binaria indexada.
/// </summary>
public static class BenchmarkBusqueda
{
    /// <summary>
    /// Ejecuta ambas búsquedas sobre el mismo arreglo ordenado.
    /// </summary>
    /// <param name="cantidadRegistros">
    /// Cantidad de registros utilizados en la comparación.
    /// </param>
    /// <returns>Reporte con tiempos y comparaciones.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce cuando la cantidad no es positiva.
    /// </exception>
    public static ResultadoBenchmarkBusqueda Ejecutar(
        int cantidadRegistros = 1_000_000)
    {
        if (cantidadRegistros <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidadRegistros),
                "La cantidad debe ser mayor que cero.");
        }

        RegistroDatos[] indice =
            CrearIndiceOrdenado(cantidadRegistros);

        int idBuscado = cantidadRegistros;

        PrepararCompilacionJustInTime(indice);

        long inicioLineal = Stopwatch.GetTimestamp();

        ResultadoBusqueda resultadoLineal =
            BuscarLineal(
                indice,
                idBuscado);

        double tiempoLinealMs =
            Stopwatch
                .GetElapsedTime(inicioLineal)
                .TotalMilliseconds;

        long inicioBinario = Stopwatch.GetTimestamp();

        ResultadoBusqueda resultadoBinario =
            BuscadorIndexado.BuscarRegistroIndexado(
                indice,
                idBuscado);

        double tiempoBinarioMs =
            Stopwatch
                .GetElapsedTime(inicioBinario)
                .TotalMilliseconds;

        bool equivalentes =
            resultadoLineal.Encontrado ==
                resultadoBinario.Encontrado
            && resultadoLineal
                .Registro
                .GetValueOrDefault()
                .Id ==
               resultadoBinario
                .Registro
                .GetValueOrDefault()
                .Id;

        return new ResultadoBenchmarkBusqueda(
            cantidadRegistros,
            idBuscado,
            resultadoLineal.Comparaciones,
            resultadoBinario.Comparaciones,
            tiempoLinealMs,
            tiempoBinarioMs,
            equivalentes);
    }

    /// <summary>
    /// Genera registros ordenados por ID ascendente.
    /// </summary>
    private static RegistroDatos[] CrearIndiceOrdenado(
        int cantidadRegistros)
    {
        RegistroDatos[] registros =
            new RegistroDatos[cantidadRegistros];

        for (int indice = 0;
             indice < cantidadRegistros;
             indice++)
        {
            int id = indice + 1;

            registros[indice] = new RegistroDatos(
                id,
                hashValidacion: id * 10_000L,
                pesoBytes: 100);
        }

        return registros;
    }

    /// <summary>
    /// Ejecuta operaciones pequeñas antes de medir para reducir el efecto JIT.
    /// </summary>
    private static void PrepararCompilacionJustInTime(
        RegistroDatos[] indice)
    {
        _ = BuscarLineal(
            indice,
            indice[0].Id);

        _ = BuscadorIndexado.BuscarRegistroIndexado(
            indice,
            indice[0].Id);
    }

    /// <summary>
    /// Busca secuencialmente un registro y cuenta comparaciones.
    /// </summary>
    private static ResultadoBusqueda BuscarLineal(
        RegistroDatos[] registros,
        int idBuscado)
    {
        int comparaciones = 0;

        foreach (RegistroDatos registro in registros)
        {
            comparaciones++;

            if (registro.Id == idBuscado)
            {
                return new ResultadoBusqueda(
                    registro,
                    comparaciones);
            }
        }

        return new ResultadoBusqueda(
            registro: null,
            comparaciones);
    }
}