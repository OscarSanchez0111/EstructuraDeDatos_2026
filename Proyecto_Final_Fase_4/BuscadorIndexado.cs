using Proyecto_Final_Fase_1;
using Proyecto_Final_Fase_2;
using Proyecto_Final_Fase_3;

namespace Proyecto_Final_Fase_4;

/// <summary>
/// Construye índices auxiliares y realiza búsquedas binarias por ID.
/// </summary>
public static class BuscadorIndexado
{
    /// <summary>
    /// Extrae los registros de una tabla dinámica y los ordena por ID.
    /// </summary>
    /// <param name="tabla">Lista enlazada que contiene los registros.</param>
    /// <returns>Arreglo auxiliar ordenado por ID ascendente.</returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce cuando la tabla recibida es null.
    /// </exception>
    public static RegistroDatos[] ConstruirIndice(
        TablaDinamica tabla)
    {
        ArgumentNullException.ThrowIfNull(tabla);

        RegistroDatos[] indice = tabla.ObtenerComoArreglo();

        MotorQuickSort.Ordenar(indice);

        return indice;
    }

    /// <summary>
    /// Busca un registro por ID mediante búsqueda binaria.
    /// </summary>
    /// <param name="indiceOrdenado">
    /// Arreglo previamente ordenado por ID ascendente.
    /// </param>
    /// <param name="idBuscado">ID que se desea localizar.</param>
    /// <returns>
    /// Resultado con el registro localizado y el número de comparaciones.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce cuando el arreglo recibido es null.
    /// </exception>
    public static ResultadoBusqueda BuscarRegistroIndexado(
        RegistroDatos[] indiceOrdenado,
        int idBuscado)
    {
        ArgumentNullException.ThrowIfNull(indiceOrdenado);

        int izquierda = 0;
        int derecha = indiceOrdenado.Length - 1;
        int comparaciones = 0;

        while (izquierda <= derecha)
        {
            int medio =
                izquierda + ((derecha - izquierda) / 2);

            comparaciones++;

            int idActual = indiceOrdenado[medio].Id;

            if (idActual == idBuscado)
            {
                return new ResultadoBusqueda(
                    indiceOrdenado[medio],
                    comparaciones);
            }

            if (idActual < idBuscado)
            {
                izquierda = medio + 1;
            }
            else
            {
                derecha = medio - 1;
            }
        }

        return new ResultadoBusqueda(
            registro: null,
            comparaciones);
    }
}