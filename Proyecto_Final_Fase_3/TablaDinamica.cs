using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_3;

/// <summary>
/// Especialización de TablaDinamica para los registros de DataCore.
/// Conserva las operaciones por ID utilizadas por las fases 3 y 4.
/// </summary>
public sealed class TablaDinamica :
    TablaDinamica<RegistroDatos>
{
    /// <summary>
    /// Busca el primer registro que tenga el ID indicado.
    /// La búsqueda lineal tiene complejidad O(n).
    /// </summary>
    /// <param name="id">Identificador que se desea localizar.</param>
    /// <returns>
    /// El registro encontrado o null cuando el ID no existe.
    /// </returns>
    public RegistroDatos? BuscarPorId(int id)
    {
        bool encontrado = IntentarBuscar(
            registro => registro.Id == id,
            out RegistroDatos resultado);

        return encontrado
            ? resultado
            : null;
    }

    /// <summary>
    /// Elimina la primera aparición del ID indicado.
    /// Si el ID no existe, la lista permanece sin cambios.
    /// </summary>
    /// <param name="idObjetivo">
    /// Identificador del registro que se desea eliminar.
    /// </param>
    public void EliminarPorId(int idObjetivo)
    {
        EliminarPrimero(
            registro => registro.Id == idObjetivo);
    }
}