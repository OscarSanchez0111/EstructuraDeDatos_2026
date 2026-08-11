using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_1.Tests;

public class DataCoreTests
{
    [Fact]
    public void Constructor_ConDatosValidos_GuardaPropiedades()
    {
        var registro = new RegistroDatos(1, 123456789L, 500);

        Assert.Equal(1, registro.Id);
        Assert.Equal(123456789L, registro.HashValidacion);
        Assert.Equal(500, registro.PesoBytes);
    }

    [Fact]
    public void Constructor_ConPesoCero_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => new RegistroDatos(1, 100L, 0));
    }

    [Fact]
    public void Constructor_ConPesoNegativo_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => new RegistroDatos(1, 100L, -10));
    }

    [Fact]
    public void RegistrosConMismosDatos_SonIguales()
    {
        var primero = new RegistroDatos(1, 100L, 500);
        var segundo = new RegistroDatos(1, 100L, 500);

        Assert.True(primero == segundo);
        Assert.Equal(primero, segundo);
    }

    [Fact]
    public void RegistrosConDatosDiferentes_NoSonIguales()
    {
        var primero = new RegistroDatos(1, 100L, 500);
        var segundo = new RegistroDatos(2, 200L, 600);

        Assert.True(primero != segundo);
    }

    [Fact]
    public void ArregloVacio_ProduceCeroMetricas()
    {
        RegistroDatos[] arreglo = [];

        MetricasOrdenacion metricas =
            SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.Empty(arreglo);
        Assert.Equal(0, metricas.TotalComparaciones);
        Assert.Equal(0, metricas.TotalIntercambios);
    }

    [Fact]
    public void ArregloDeUnElemento_NoRealizaOperaciones()
    {
        RegistroDatos[] arreglo =
        [
            new RegistroDatos(1, 100L, 500)
        ];

        MetricasOrdenacion metricas =
            SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.Equal(0, metricas.TotalComparaciones);
        Assert.Equal(0, metricas.TotalIntercambios);
    }

    [Fact]
    public void ArregloOrdenado_NoRealizaIntercambios()
    {
        RegistroDatos[] arreglo =
        [
            CrearRegistro(1),
            CrearRegistro(2),
            CrearRegistro(3)
        ];

        MetricasOrdenacion metricas =
            SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.Equal(0, metricas.TotalIntercambios);
        Assert.Equal([1, 2, 3], arreglo.Select(r => r.Id));
    }

    [Fact]
    public void ArregloDesordenado_SeOrdenaAscendentemente()
    {
        RegistroDatos[] arreglo =
        [
            CrearRegistro(4),
            CrearRegistro(1),
            CrearRegistro(3),
            CrearRegistro(2)
        ];

        SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.Equal([1, 2, 3, 4], arreglo.Select(r => r.Id));
    }

    [Fact]
    public void ArregloInvertido_SeOrdenaCorrectamente()
    {
        RegistroDatos[] arreglo =
        [
            CrearRegistro(5),
            CrearRegistro(4),
            CrearRegistro(3),
            CrearRegistro(2),
            CrearRegistro(1)
        ];

        SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.Equal([1, 2, 3, 4, 5], arreglo.Select(r => r.Id));
    }

    [Fact]
    public void CincoElementos_RealizanDiezComparaciones()
    {
        RegistroDatos[] arreglo =
        [
            CrearRegistro(5),
            CrearRegistro(2),
            CrearRegistro(4),
            CrearRegistro(1),
            CrearRegistro(3)
        ];

        MetricasOrdenacion metricas =
            SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.Equal(10, metricas.TotalComparaciones);
    }

    [Fact]
    public void Intercambios_NuncaSuperanNMenosUno()
    {
        RegistroDatos[] arreglo =
        [
            CrearRegistro(6),
            CrearRegistro(1),
            CrearRegistro(5),
            CrearRegistro(2),
            CrearRegistro(4),
            CrearRegistro(3)
        ];

        MetricasOrdenacion metricas =
            SelectionSort.OrdenarPorSeleccion(arreglo);

        Assert.True(
            metricas.TotalIntercambios <= arreglo.Length - 1);
    }

    [Fact]
    public void ArregloNulo_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => SelectionSort.OrdenarPorSeleccion(null!));
    }

    private static RegistroDatos CrearRegistro(int id)
    {
        return new RegistroDatos(id, id * 1000L, 500);
    }
}