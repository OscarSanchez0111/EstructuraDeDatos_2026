namespace Proyecto_Final_Fase_4.Tests;

/// <summary>
/// Verifica la comparación entre búsqueda lineal y binaria.
/// </summary>
public class PruebasBenchmarkBusqueda
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ejecutar_CantidadNoPositiva_LanzaExcepcion(
        int cantidad)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => BenchmarkBusqueda.Ejecutar(cantidad));
    }

    [Fact]
    public void Ejecutar_MilVeinticuatroRegistros_ResultadosEquivalentes()
    {
        ResultadoBenchmarkBusqueda resultado =
            BenchmarkBusqueda.Ejecutar(
                cantidadRegistros: 1_024);

        Assert.True(resultado.ResultadosEquivalentes);
        Assert.Equal(1_024, resultado.CantidadRegistros);
        Assert.Equal(1_024, resultado.IdBuscado);
        Assert.Equal(1_024, resultado.ComparacionesLineales);
        Assert.InRange(
            resultado.ComparacionesBinarias,
            1,
            11);
    }

    [Fact]
    public void Ejecutar_UnMillon_BinariaUsaVeinteComparaciones()
    {
        ResultadoBenchmarkBusqueda resultado =
            BenchmarkBusqueda.Ejecutar(
                cantidadRegistros: 1_000_000);

        Assert.True(resultado.ResultadosEquivalentes);

        Assert.Equal(
            1_000_000,
            resultado.ComparacionesLineales);

        Assert.Equal(
            20,
            resultado.ComparacionesBinarias);

        Assert.Equal(
            50_000,
            resultado.FactorReduccionComparaciones);
    }

    [Fact]
    public void ToString_IncluyeResultadosPrincipales()
    {
        ResultadoBenchmarkBusqueda resultado =
            BenchmarkBusqueda.Ejecutar(
                cantidadRegistros: 100);

        string reporte = resultado.ToString();

        Assert.Contains(
            "BÚSQUEDA LINEAL",
            reporte);

        Assert.Contains(
            "BÚSQUEDA BINARIA",
            reporte);

        Assert.Contains(
            "Resultados equivalentes",
            reporte);
    }
}