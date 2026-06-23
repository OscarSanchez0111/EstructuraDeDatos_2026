using System;

public readonly struct CoordenadaGPS
{
    public double Latitud { get; }
    public double Longitud { get; }

    public CoordenadaGPS(double lat, double lon)
    {
        if (lat < -90 || lat > 90)
        {
            throw new ArgumentOutOfRangeException(
                nameof(lat),
                "Latitud fuera de rango [-90, 90]");
        }

        if (lon < -180 || lon > 180)
        {
            throw new ArgumentOutOfRangeException(
                nameof(lon),
                "Longitud fuera de rango [-180, 180]");
        }

        Latitud = lat;
        Longitud = lon;
    }

    public void ImprimirUbicacion()
    {
        Console.WriteLine($"Latitud: {Latitud}");
        Console.WriteLine($"Longitud: {Longitud}");
    }
}