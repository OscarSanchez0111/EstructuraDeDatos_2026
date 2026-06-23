using System;

Console.WriteLine(" TELEMETRÍA GPS ");

try
{
    Console.Write("Latitud: ");
    double lat = double.Parse(Console.ReadLine()!);

    Console.Write("Longitud: ");
    double lon = double.Parse(Console.ReadLine()!);

    CoordenadaGPS coord = new CoordenadaGPS(lat, lon);

    Console.WriteLine();
    coord.ImprimirUbicacion();

    Console.WriteLine("\nDEMOSTRACIÓN DE COPIA POR VALOR ");

    CoordenadaGPS c1 = new CoordenadaGPS(19.4326, -99.1332);
    CoordenadaGPS c2 = c1;

    c2 = new CoordenadaGPS(52.5200, 13.4050);

    Console.WriteLine("\n--- c1 ---");
    c1.ImprimirUbicacion();

    Console.WriteLine("\n--- c2 ---");
    c2.ImprimirUbicacion();
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
catch (FormatException)
{
    Console.WriteLine("Error: Debes ingresar números válidos.");
}