using System;
using System.Collections.Generic;

public class ServidorConexion
{
    private readonly long[] _cache = new long[100];

    public int ID { get; set; }
    public string Nombre { get; set; }
    public PuntoDeRed Ubicacion { get; set; }
    public List<int> CodigosRespuesta { get; set; }

    public ServidorConexion(int id, string nombre, PuntoDeRed ubicacion, List<int> codigos)
    {
        ID = id;
        Nombre = nombre;
        Ubicacion = ubicacion;
        CodigosRespuesta = codigos ?? new List<int>();
    }

    public long DiagnosticarLatencia(int n, out string alerta)
    {
        if (n < 0 || n >= 100)
            throw new ArgumentOutOfRangeException(nameof(n), "El valor de n debe estar entre 0 y 99.");

        if (n <= 1)
        {
            alerta = string.Empty;
            return n;
        }

        if (_cache[n] != 0)
        {
            alerta = string.Empty;
            return _cache[n];
        }

        _cache[n] = DiagnosticarLatencia(n - 1, out _) + DiagnosticarLatencia(n - 2, out _);

        alerta = _cache[n] > 10_000
            ? "ALERTA: Índice de estrés crítico en " + Nombre
            : string.Empty;

        return _cache[n];
    }

    public override string ToString()
    {
        return $"[{ID}] {Nombre} @ {Ubicacion}";
    }
}