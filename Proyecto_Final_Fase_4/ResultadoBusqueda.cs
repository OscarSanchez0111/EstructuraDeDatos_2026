using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_4;

/// <summary>
/// Representa el resultado de una búsqueda binaria indexada.
/// </summary>
public readonly struct ResultadoBusqueda
{
    /// <summary>
    /// Registro localizado o null cuando el ID no existe.
    /// </summary>
    public RegistroDatos? Registro { get; }

    /// <summary>
    /// Número de comparaciones realizadas durante la búsqueda.
    /// </summary>
    public int Comparaciones { get; }

    /// <summary>
    /// Indica si se encontró un registro.
    /// </summary>
    public bool Encontrado => Registro.HasValue;

    /// <summary>
    /// Inicializa el resultado de una búsqueda.
    /// </summary>
    /// <param name="registro">Registro localizado o null.</param>
    /// <param name="comparaciones">Comparaciones realizadas.</param>
    public ResultadoBusqueda(
        RegistroDatos? registro,
        int comparaciones)
    {
        Registro = registro;
        Comparaciones = comparaciones;
    }

    /// <summary>
/// Devuelve una representación legible del resultado.
/// </summary>
/// <returns>Descripción de la búsqueda.</returns>
public override string ToString()
{
    if (Registro is not RegistroDatos registroEncontrado)
    {
        return
            $"Registro no encontrado. " +
            $"Comparaciones: {Comparaciones}";
    }

    return
        $"{registroEncontrado}\n" +
        $"Comparaciones: {Comparaciones}";
}
}