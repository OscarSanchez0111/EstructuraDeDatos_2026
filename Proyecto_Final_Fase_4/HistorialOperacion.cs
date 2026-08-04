namespace Proyecto_Final_Fase_4;

/// <summary>
/// Conserva un historial cronológico de las operaciones del sistema.
/// </summary>
public sealed class HistorialOperacion
{
    private readonly List<string> operaciones = new();

    /// <summary>
    /// Cantidad de operaciones registradas.
    /// </summary>
    public int Cantidad => operaciones.Count;

    /// <summary>
    /// Registra una operación con la fecha y hora actuales.
    /// </summary>
    /// <param name="descripcion">Descripción de la operación.</param>
    /// <exception cref="ArgumentException">
    /// Se produce cuando la descripción está vacía.
    /// </exception>
    public void Registrar(string descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            throw new ArgumentException(
                "La descripción no puede estar vacía.",
                nameof(descripcion));
        }

        string entrada =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {descripcion}";

        operaciones.Add(entrada);
    }

    /// <summary>
    /// Devuelve una copia de las operaciones registradas.
    /// </summary>
    /// <returns>Arreglo con el historial completo.</returns>
    public string[] ObtenerOperaciones()
    {
        return operaciones.ToArray();
    }
}