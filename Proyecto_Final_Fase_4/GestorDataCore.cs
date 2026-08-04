using Proyecto_Final_Fase_1;
using Proyecto_Final_Fase_3;

namespace Proyecto_Final_Fase_4;

/// <summary>
/// Coordina el almacenamiento, indexación y búsqueda de DataCore.
/// </summary>
public sealed class GestorDataCore
{
    private readonly TablaDinamica tabla;
    private readonly HistorialOperacion historial;
    private RegistroDatos[] indiceOrdenado;
    private bool indiceActualizado;

    /// <summary>
    /// Inicializa un gestor vacío.
    /// </summary>
    public GestorDataCore()
    {
        tabla = new TablaDinamica();
        historial = new HistorialOperacion();
        indiceOrdenado = Array.Empty<RegistroDatos>();
        indiceActualizado = true;
    }

    /// <summary>
    /// Cantidad actual de registros almacenados.
    /// </summary>
    public int Cantidad => tabla.Cantidad;

    /// <summary>
    /// Indica si el índice corresponde al estado actual de la tabla.
    /// </summary>
    public bool IndiceActualizado => indiceActualizado;

    /// <summary>
    /// Cantidad de operaciones registradas en el historial.
    /// </summary>
    public int TotalOperaciones => historial.Cantidad;

    /// <summary>
    /// Inserta un registro si su ID todavía no existe.
    /// </summary>
    /// <param name="registro">Registro que se desea almacenar.</param>
    /// <returns>
    /// True cuando se insertó; false cuando el ID estaba duplicado.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce cuando el ID no es positivo.
    /// </exception>
    public bool Insertar(RegistroDatos registro)
    {
        if (registro.Id <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(registro),
                "El ID debe ser mayor que cero.");
        }

        if (tabla.BuscarPorId(registro.Id).HasValue)
        {
            historial.Registrar(
                $"Inserción rechazada: ID {registro.Id} duplicado.");

            return false;
        }

        tabla.InsertarFinal(registro);
        indiceActualizado = false;

        historial.Registrar(
            $"Registro con ID {registro.Id} insertado.");

        return true;
    }

    /// <summary>
    /// Elimina un registro mediante su ID.
    /// </summary>
    /// <param name="id">ID del registro que se desea eliminar.</param>
    /// <returns>
    /// True cuando se eliminó; false cuando el ID no existía.
    /// </returns>
    public bool Eliminar(int id)
    {
        if (!tabla.BuscarPorId(id).HasValue)
        {
            historial.Registrar(
                $"Eliminación rechazada: ID {id} inexistente.");

            return false;
        }

        tabla.EliminarPorId(id);
        indiceActualizado = false;

        historial.Registrar(
            $"Registro con ID {id} eliminado.");

        return true;
    }

    /// <summary>
    /// Devuelve una copia de los registros en orden de inserción.
    /// </summary>
    /// <returns>Arreglo con los registros actuales.</returns>
    public RegistroDatos[] ObtenerRegistros()
    {
        return tabla.ObtenerComoArreglo();
    }

    /// <summary>
    /// Construye o reconstruye el índice ordenado por ID.
    /// </summary>
    /// <returns>Copia del índice ordenado.</returns>
    public RegistroDatos[] OrdenarEIndexar()
    {
        ReconstruirIndice();

        historial.Registrar(
            $"Índice reconstruido con {indiceOrdenado.Length} registros.");

        return (RegistroDatos[])indiceOrdenado.Clone();
    }

    /// <summary>
    /// Busca secuencialmente un ID en los registros almacenados.
    /// </summary>
    /// <param name="id">ID que se desea localizar.</param>
    /// <returns>Resultado y número de comparaciones realizadas.</returns>
    public ResultadoBusqueda BuscarLineal(int id)
    {
        RegistroDatos[] registros = tabla.ObtenerComoArreglo();
        int comparaciones = 0;

        foreach (RegistroDatos registro in registros)
        {
            comparaciones++;

            if (registro.Id == id)
            {
                historial.Registrar(
                    $"Búsqueda lineal del ID {id}: encontrado.");

                return new ResultadoBusqueda(
                    registro,
                    comparaciones);
            }
        }

        historial.Registrar(
            $"Búsqueda lineal del ID {id}: no encontrado.");

        return new ResultadoBusqueda(
            registro: null,
            comparaciones);
    }

    /// <summary>
    /// Busca un ID mediante el índice ordenado y búsqueda binaria.
    /// </summary>
    /// <param name="id">ID que se desea localizar.</param>
    /// <returns>Resultado y número de comparaciones realizadas.</returns>
    public ResultadoBusqueda BuscarIndexado(int id)
    {
        AsegurarIndiceActualizado();

        ResultadoBusqueda resultado =
            BuscadorIndexado.BuscarRegistroIndexado(
                indiceOrdenado,
                id);

        string estado =
            resultado.Encontrado
                ? "encontrado"
                : "no encontrado";

        historial.Registrar(
            $"Búsqueda indexada del ID {id}: {estado}.");

        return resultado;
    }

    /// <summary>
    /// Devuelve una copia del índice ordenado vigente.
    /// </summary>
    /// <returns>Arreglo ordenado por ID.</returns>
    public RegistroDatos[] ObtenerIndiceOrdenado()
    {
        AsegurarIndiceActualizado();

        return (RegistroDatos[])indiceOrdenado.Clone();
    }

    /// <summary>
    /// Calcula la suma de los pesos declarados por los registros.
    /// </summary>
    /// <returns>Peso total en bytes.</returns>
    public long CalcularPesoTotalBytes()
    {
        long total = 0;

        foreach (RegistroDatos registro in tabla.ObtenerComoArreglo())
        {
            total += registro.PesoBytes;
        }

        return total;
    }

    /// <summary>
    /// Devuelve una copia del historial de operaciones.
    /// </summary>
    /// <returns>Operaciones registradas cronológicamente.</returns>
    public string[] ObtenerHistorial()
    {
        return historial.ObtenerOperaciones();
    }

    /// <summary>
    /// Reconstruye el índice cuando los datos han cambiado.
    /// </summary>
    private void AsegurarIndiceActualizado()
    {
        if (!indiceActualizado)
        {
            ReconstruirIndice();
        }
    }

    /// <summary>
    /// Extrae y ordena todos los registros actuales.
    /// </summary>
    private void ReconstruirIndice()
    {
        indiceOrdenado =
            BuscadorIndexado.ConstruirIndice(tabla);

        indiceActualizado = true;
    }
}