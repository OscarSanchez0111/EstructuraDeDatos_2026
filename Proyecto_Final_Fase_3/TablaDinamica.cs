using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_3;

/// <summary>
/// Implementa una lista simplemente enlazada de registros.
/// </summary>
public sealed class TablaDinamica
{
    private NodoRegistro? cabeza;
    private int contadorRegistros;

    /// <summary>
    /// Cantidad actual de registros. Consulta en tiempo O(1).
    /// </summary>
    public int Cantidad => contadorRegistros;

    /// <summary>
    /// Indica si la lista no contiene nodos.
    /// </summary>
    public bool EstaVacia => cabeza is null;

    public TablaDinamica()
    {
        cabeza = null;
        contadorRegistros = 0;
    }

    /// <summary>
    /// Inserta un registro al comienzo de la lista en tiempo O(1).
    /// </summary>
    public void InsertarInicio(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new(nuevoRegistro)
        {
            Siguiente = cabeza
        };

        cabeza = nuevoNodo;
        contadorRegistros++;
    }

    /// <summary>
    /// Inserta un registro al final, conservando el orden de llegada.
    /// </summary>
    public void InsertarFinal(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new(nuevoRegistro);

        if (cabeza is null)
        {
            cabeza = nuevoNodo;
            contadorRegistros++;
            return;
        }

        NodoRegistro actual = cabeza;

        while (actual.Siguiente is not null)
        {
            actual = actual.Siguiente;
        }

        actual.Siguiente = nuevoNodo;
        contadorRegistros++;
    }

    /// <summary>
    /// Busca el primer registro que tenga el Id indicado.
    /// </summary>
    public RegistroDatos? BuscarPorId(int id)
    {
        NodoRegistro? actual = cabeza;

        while (actual is not null)
        {
            if (actual.Dato.Id == id)
            {
                return actual.Dato;
            }

            actual = actual.Siguiente;
        }

        return null;
    }

    /// <summary>
    /// Elimina la primera aparición del Id indicado.
    /// </summary>
    public void EliminarPorId(int idObjetivo)
    {
        if (cabeza is null)
        {
            return;
        }

        if (cabeza.Dato.Id == idObjetivo)
        {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return;
        }

        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual is not null)
        {
            if (actual.Dato.Id == idObjetivo)
            {
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return;
            }

            anterior = actual;
            actual = actual.Siguiente;
        }
    }

    /// <summary>
    /// Copia todos los registros a un arreglo en el mismo orden.
    /// </summary>
    public RegistroDatos[] ObtenerComoArreglo()
    {
        RegistroDatos[] resultado =
            new RegistroDatos[contadorRegistros];

        NodoRegistro? actual = cabeza;
        int indice = 0;

        while (actual is not null)
        {
            resultado[indice] = actual.Dato;
            actual = actual.Siguiente;
            indice++;
        }

        return resultado;
    }
}