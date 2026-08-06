namespace Proyecto_Final_Fase_3;

/// <summary>
/// Representa un nodo genérico de una lista simplemente enlazada.
/// Puede almacenar cualquier tipo de dato no nulo.
/// </summary>
/// <typeparam name="T">Tipo de elemento almacenado.</typeparam>
public sealed class Nodo<T>
    where T : notnull
{
    /// <summary>
    /// Elemento almacenado en el nodo.
    /// </summary>
    public T Dato { get; }

    /// <summary>
    /// Referencia al siguiente nodo de la lista.
    /// Es null cuando este nodo es el último.
    /// </summary>
    public Nodo<T>? Siguiente { get; internal set; }

    /// <summary>
    /// Inicializa un nodo con el elemento indicado.
    /// </summary>
    public Nodo(T dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}