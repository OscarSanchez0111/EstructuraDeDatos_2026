using System;
using System.Collections.Generic;
using System.Linq;

try
{
    var servidores = new List<ServidorConexion>
    {
        new ServidorConexion(1, "Servidor-CDMX", new PuntoDeRed(19.43, -99.13), new List<int> { 200, 200, 500 }),
        new ServidorConexion(2, "Servidor-NYC", new PuntoDeRed(40.71, -74.01), new List<int> { 200, 404 }),
        new ServidorConexion(3, "Servidor-Sydney", new PuntoDeRed(-33.87, 151.21), new List<int> { 500, 500 }),
        new ServidorConexion(4, "Servidor-Londres", new PuntoDeRed(51.51, -0.13), new List<int> { 200, 200, 200 })
    };

    Console.WriteLine("SISTEMA DE MONITOREO DE CONEXIONES DE RED \n");

    Console.WriteLine("SERVIDORES REGISTRADOS");
    foreach (var servidor in servidores)
        Console.WriteLine(servidor);

    var servidoresCriticos = servidores
        .Where(s => s.Ubicacion.Latitud > 0 && s.CodigosRespuesta.Contains(500))
        .ToList();

    Console.WriteLine("\n SERVIDORES CRÍTICOS");
    foreach (var servidor in servidoresCriticos)
        Console.WriteLine(servidor);

    Console.WriteLine("\n REPORTE DE ERRORES 500 ");
    var reporteErrores = servidores.Select(s => new
    {
        s.Nombre,
        TotalErrores = s.CodigosRespuesta.Count(c => c == 500)
    });

    foreach (var reporte in reporteErrores)
        Console.WriteLine($"{reporte.Nombre}: {reporte.TotalErrores} error(es) 500");

    Console.WriteLine("\n ORDENADOS POR ERRORES CRÍTICOS ");
    var ordenados = servidores
        .OrderByDescending(s => s.CodigosRespuesta.Count(c => c == 500));

    foreach (var servidor in ordenados)
        Console.WriteLine($"{servidor.Nombre} - Errores 500: {servidor.CodigosRespuesta.Count(c => c == 500)}");

    Console.WriteLine("\n DIAGNÓSTICO DE LATENCIA ");
    var servidorDiagnostico = servidores[0];
    long resultado = servidorDiagnostico.DiagnosticarLatencia(25, out string alerta);

    Console.WriteLine($"Servidor analizado: {servidorDiagnostico.Nombre}");
    Console.WriteLine($"Resultado Fibonacci: {resultado}");

    if (!string.IsNullOrEmpty(alerta))
        Console.WriteLine(alerta);

    Console.WriteLine("\n VALIDACIONES ANY / ALL ");
    bool existeErrorCritico = servidores.Any(s => s.CodigosRespuesta.Contains(500));
    bool todosCorrectosLondres = servidores[3].CodigosRespuesta.All(c => c == 200);

    Console.WriteLine($"¿Existe algún servidor con error 500?: {existeErrorCritico}");
    Console.WriteLine($"¿Todos los códigos de Londres son 200?: {todosCorrectosLondres}");
}
catch (FormatException fe)
{
    Console.WriteLine($"[ERROR DE FORMATO] {fe.Message}");
}
catch (ArgumentOutOfRangeException aore)
{
    Console.WriteLine($"[ERROR DE RANGO] {aore.Message}");
}
catch (OverflowException oe)
{
    Console.WriteLine($"[DESBORDAMIENTO] {oe.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR GENERAL] {ex.Message}");
}