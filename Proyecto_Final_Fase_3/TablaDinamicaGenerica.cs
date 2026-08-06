namespace Proyecto_Final_Fase_3;

/// <summary>
/// Implementa una lista simplemente enlazada genérica.
/// Mantiene referencias a la cabeza y a la cola para permitir
/// inserciones al inicio y al final en tiempo O(1).
/// </summary>
/// <typeparam name="T">Tipo de elemento almacenado.</typeparam>
public class TablaDinamica<T>
    where T : notnull
{
    private Nodo<T>? cabeza;
    private Nodo<T>? cola;
    private int contadorElementos;

    /// <summary>
    /// Cantidad de elementos almacenados. Consulta en tiempo O(1).
    /// </summary>
    public int Cantidad => contadorElementos;

    /// <summary>
    /// Indica si la lista se encuentra vacía.
    /// </summary>
    public bool EstaVacia => cabeza is null;

    /// <summary>
    /// Crea una lista genérica vacía.
    /// </summary>
    public TablaDinamica()
    {
        cabeza = null;
        cola = null;
        contadorElementos = 0;
    }

    /// <summary>
    /// Inserta un elemento al inicio en tiempo O(1).
    /// </summary>
    public void InsertarInicio(T nuevoElemento)
    {
        Nodo<T> nuevoNodo = new(nuevoElemento)
        {
            Siguiente = cabeza
        };

        cabeza = nuevoNodo;

        if (cola is null)
        {
            cola = nuevoNodo;
        }

        contadorElementos++;
    }

    /// <summary>
    /// Inserta un elemento al final en tiempo O(1).
    /// </summary>
    public void InsertarFinal(T nuevoElemento)
    {
        Nodo<T> nuevoNodo = new(nuevoElemento);

        if (cabeza is null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
            contadorElementos++;
            return;
        }

        cola!.Siguiente = nuevoNodo;
        cola = nuevoNodo;
        contadorElementos++;
    }

    /// <summary>
    /// Busca el primer elemento que cumpla con el criterio indicado.
    /// La búsqueda tiene complejidad O(n).
    /// </summary>
    public bool IntentarBuscar(
        Predicate<T> criterio,
        out T resultado)
    {
        ArgumentNullException.ThrowIfNull(criterio);

        Nodo<T>? actual = cabeza;

        while (actual is not null)
        {
            if (criterio(actual.Dato))
            {
                resultado = actual.Dato;
                return true;
            }

            actual = actual.Siguiente;
        }

        resultado = default!;
        return false;
    }

    /// <summary>
    /// Determina si existe un elemento que cumpla con el criterio.
    /// </summary>
    public bool Existe(Predicate<T> criterio)
    {
        return IntentarBuscar(criterio, out _);
    }

    /// <summary>
    /// Elimina la primera aparición que cumpla con el criterio.
    /// Devuelve true cuando se realizó la eliminación.
    /// </summary>
    public bool EliminarPrimero(Predicate<T> criterio)
    {
        ArgumentNullException.ThrowIfNull(criterio);

        if (cabeza is null)
        {
            return false;
        }

        if (criterio(cabeza.Dato))
        {
            cabeza = cabeza.Siguiente;
            contadorElementos--;

            if (cabeza is null)
            {
                cola = null;
            }

            return true;
        }

        Nodo<T> anterior = cabeza;
        Nodo<T>? actual = cabeza.Siguiente;

        while (actual is not null)
        {
            if (criterio(actual.Dato))
            {
                anterior.Siguiente = actual.Siguiente;

                if (ReferenceEquals(actual, cola))
                {
                    cola = anterior;
                }

                contadorElementos--;
                return true;
            }

            anterior = actual;
            actual = actual.Siguiente;
        }

        return false;
    }

    /// <summary>
    /// Copia los elementos a un arreglo conservando su orden.
    /// </summary>
    public T[] ObtenerComoArreglo()
    {
        T[] resultado = new T[contadorElementos];

        Nodo<T>? actual = cabeza;
        int indice = 0;

        while (actual is not null)
        {
            resultado[indice] = actual.Dato;
            indice++;
            actual = actual.Siguiente;
        }

        return resultado;
    }

    /// <summary>
    /// Elimina todos los elementos de la lista.
    /// </summary>
    public void Limpiar()
    {
        cabeza = null;
        cola = null;
        contadorElementos = 0;
    }
}