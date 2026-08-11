using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_3;

/// <summary>
/// Representa un nodo individual de la lista simplemente enlazada.
/// </summary>
public sealed class NodoRegistro
{
    /// <summary>
    /// Registro almacenado dentro del nodo.
    /// </summary>
    public RegistroDatos Dato { get; }

    /// <summary>
    /// Referencia al siguiente nodo o null si este es el último.
    /// </summary>
    public NodoRegistro? Siguiente { get; internal set; }

    /// <summary>
    /// Crea un nodo con el registro recibido.
    /// </summary>
    public NodoRegistro(RegistroDatos dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}