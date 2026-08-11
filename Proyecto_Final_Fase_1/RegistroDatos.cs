namespace Proyecto_Final_Fase_1;

/// <summary>
/// Representa un registro inmutable procesado por el motor DataCore.
/// </summary>
public readonly struct RegistroDatos : IEquatable<RegistroDatos>
{
    public int Id { get; }
    public long HashValidacion { get; }
    public int PesoBytes { get; }

    /// <summary>
    /// Inicializa un registro con sus datos de identificación y almacenamiento.
    /// </summary>
    /// <param name="id">Identificador utilizado para ordenar el registro.</param>
    /// <param name="hashValidacion">Código de validación del registro.</param>
    /// <param name="pesoBytes">Tamaño físico del registro en bytes.</param>
    /// <exception cref="ArgumentException">
    /// Se produce cuando el peso es menor o igual que cero.
    /// </exception>
    public RegistroDatos(int id, long hashValidacion, int pesoBytes)
    {
        if (pesoBytes <= 0)
        {
            throw new ArgumentException(
                "PesoBytes debe ser mayor a 0.",
                nameof(pesoBytes));
        }

        Id = id;
        HashValidacion = hashValidacion;
        PesoBytes = pesoBytes;
    }

    public bool Equals(RegistroDatos otro)
    {
        return Id == otro.Id
            && HashValidacion == otro.HashValidacion
            && PesoBytes == otro.PesoBytes;
    }

    public override bool Equals(object? objeto)
    {
        return objeto is RegistroDatos otro && Equals(otro);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, HashValidacion, PesoBytes);
    }

    public static bool operator ==(
        RegistroDatos izquierdo,
        RegistroDatos derecho)
    {
        return izquierdo.Equals(derecho);
    }

    public static bool operator !=(
        RegistroDatos izquierdo,
        RegistroDatos derecho)
    {
        return !izquierdo.Equals(derecho);
    }

    public override string ToString()
    {
        return $"Id: {Id,4} | Hash: {HashValidacion,20} | " +
               $"Peso: {PesoBytes,4} bytes";
    }
}