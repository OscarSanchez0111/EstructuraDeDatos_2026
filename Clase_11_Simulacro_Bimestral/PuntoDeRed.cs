using System;

public struct PuntoDeRed
{
    public double Latitud { get; }
    public double Longitud { get; }

    public PuntoDeRed(double latitud, double longitud)
    {
        if (latitud < -90.0 || latitud > 90.0)
            throw new ArgumentOutOfRangeException(nameof(latitud), "La latitud debe estar entre -90 y 90 grados.");

        if (longitud < -180.0 || longitud > 180.0)
            throw new ArgumentOutOfRangeException(nameof(longitud), "La longitud debe estar entre -180 y 180 grados.");

        Latitud = latitud;
        Longitud = longitud;
    }

    public override string ToString()
    {
        return $"({Latitud}°, {Longitud}°)";
    }
}