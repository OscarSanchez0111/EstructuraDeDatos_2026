using Proyecto_Final_Fase_1;
using Proyecto_Final_Fase_2;
using Proyecto_Final_Fase_3;

namespace Proyecto_Final_Fase_3.Tests;

public class PruebasTablaDinamica
{
    [Fact]
    public void NodoRegistro_AlCrearse_AlmacenaDatoYSiguienteNulo()
    {
        RegistroDatos registro = CrearRegistro(10);

        NodoRegistro nodo = new(registro);

        Assert.Equal(registro, nodo.Dato);
        Assert.Null(nodo.Siguiente);
    }

    [Fact]
    public void TablaNueva_EstaVaciaYConCantidadCero()
    {
        TablaDinamica tabla = new();

        Assert.True(tabla.EstaVacia);
        Assert.Equal(0, tabla.Cantidad);
        Assert.Empty(tabla.ObtenerComoArreglo());
    }

    [Fact]
    public void InsertarInicio_AgregaRegistroComoCabeza()
    {
        TablaDinamica tabla = new();

        tabla.InsertarInicio(CrearRegistro(10));
        tabla.InsertarInicio(CrearRegistro(20));

        RegistroDatos[] resultado =
            tabla.ObtenerComoArreglo();

        Assert.Equal([20, 10], ObtenerIds(resultado));
        Assert.Equal(2, tabla.Cantidad);
        Assert.False(tabla.EstaVacia);
    }

    [Fact]
    public void InsertarFinal_PreservaOrdenDeLlegada()
    {
        TablaDinamica tabla = new();

        tabla.InsertarFinal(CrearRegistro(30));
        tabla.InsertarFinal(CrearRegistro(10));
        tabla.InsertarFinal(CrearRegistro(20));

        Assert.Equal(
            [30, 10, 20],
            ObtenerIds(tabla.ObtenerComoArreglo()));

        Assert.Equal(3, tabla.Cantidad);
    }

    [Fact]
    public void BuscarPorId_IdExistente_DevuelveRegistro()
    {
        TablaDinamica tabla = CrearTabla(10, 20, 30);

        RegistroDatos? resultado =
            tabla.BuscarPorId(20);

        Assert.True(resultado.HasValue);
        Assert.Equal(20, resultado.Value.Id);
    }

    [Fact]
    public void BuscarPorId_IdInexistente_DevuelveNulo()
    {
        TablaDinamica tabla = CrearTabla(10, 20, 30);

        RegistroDatos? resultado =
            tabla.BuscarPorId(99);

        Assert.Null(resultado);
    }

    [Fact]
    public void EliminarPorId_ListaVacia_NoLanzaExcepcion()
    {
        TablaDinamica tabla = new();

        tabla.EliminarPorId(10);

        Assert.True(tabla.EstaVacia);
        Assert.Equal(0, tabla.Cantidad);
    }

    [Fact]
    public void EliminarPorId_EliminaCabeza()
    {
        TablaDinamica tabla = CrearTabla(10, 20, 30);

        tabla.EliminarPorId(10);

        Assert.Equal(
            [20, 30],
            ObtenerIds(tabla.ObtenerComoArreglo()));

        Assert.Equal(2, tabla.Cantidad);
    }

    [Fact]
    public void EliminarPorId_EliminaNodoIntermedio()
    {
        TablaDinamica tabla = CrearTabla(10, 20, 30);

        tabla.EliminarPorId(20);

        Assert.Equal(
            [10, 30],
            ObtenerIds(tabla.ObtenerComoArreglo()));

        Assert.Equal(2, tabla.Cantidad);
    }

    [Fact]
    public void EliminarPorId_EliminaUltimoNodo()
    {
        TablaDinamica tabla = CrearTabla(10, 20, 30);

        tabla.EliminarPorId(30);

        Assert.Equal(
            [10, 20],
            ObtenerIds(tabla.ObtenerComoArreglo()));

        Assert.Equal(2, tabla.Cantidad);
    }

    [Fact]
    public void EliminarPorId_EliminaUnicoNodo()
    {
        TablaDinamica tabla = CrearTabla(10);

        tabla.EliminarPorId(10);

        Assert.True(tabla.EstaVacia);
        Assert.Equal(0, tabla.Cantidad);
        Assert.Empty(tabla.ObtenerComoArreglo());
    }

    [Fact]
    public void EliminarPorId_IdInexistente_NoModificaLista()
    {
        TablaDinamica tabla = CrearTabla(10, 20, 30);

        tabla.EliminarPorId(99);

        Assert.Equal(
            [10, 20, 30],
            ObtenerIds(tabla.ObtenerComoArreglo()));

        Assert.Equal(3, tabla.Cantidad);
    }

    [Fact]
    public void EliminarPorId_IdsRepetidos_EliminaSoloPrimero()
    {
        TablaDinamica tabla = CrearTabla(
            10,
            20,
            20,
            30);

        tabla.EliminarPorId(20);

        Assert.Equal(
            [10, 20, 30],
            ObtenerIds(tabla.ObtenerComoArreglo()));

        Assert.Equal(3, tabla.Cantidad);
    }

    [Fact]
    public void ObtenerComoArreglo_ConservaTodosLosRegistros()
    {
        TablaDinamica tabla =
            CrearTabla(5, 15, 25, 35);

        RegistroDatos[] resultado =
            tabla.ObtenerComoArreglo();

        Assert.Equal(4, resultado.Length);

        Assert.Equal(
            [5, 15, 25, 35],
            ObtenerIds(resultado));
    }

    [Fact]
    public void ListaConvertida_AceptaQuickSortDeFase2()
    {
        TablaDinamica tabla =
            CrearTabla(42, 7, 19, 3, 55, 28, 11);

        RegistroDatos[] arreglo =
            tabla.ObtenerComoArreglo();

        MetricasQuickSort metricas =
            MotorQuickSort.Ordenar(arreglo);

        Assert.Equal(
            [3, 7, 11, 19, 28, 42, 55],
            ObtenerIds(arreglo));

        Assert.Equal(7, metricas.TamanoEntrada);
        Assert.True(metricas.TotalComparaciones > 0);
    }

    [Fact]
    public void Benchmark_CantidadInvalida_LanzaExcepcion()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => BenchmarkMemoria.Ejecutar(0));
    }

    [Fact]
    public void Benchmark_MilRegistros_ProduceMetricasValidas()
    {
        ResultadoBenchmarkMemoria resultado =
            BenchmarkMemoria.Ejecutar(1_000);

        Assert.Equal(
            1_000,
            resultado.CantidadRegistros);

        Assert.True(resultado.BytesArreglo > 0);
        Assert.True(resultado.BytesLista > 0);
        Assert.True(resultado.BytesConversion > 0);

        Assert.True(
            resultado.BytesLista
            > resultado.BytesArreglo);

        Assert.True(
            resultado.TiempoArregloMs >= 0);

        Assert.True(
            resultado.TiempoListaMs >= 0);

        Assert.True(
            resultado.TiempoConversionMs >= 0);
    }

    private static TablaDinamica CrearTabla(
        params int[] ids)
    {
        TablaDinamica tabla = new();

        foreach (int id in ids)
        {
            tabla.InsertarFinal(
                CrearRegistro(id));
        }

        return tabla;
    }

    private static RegistroDatos CrearRegistro(
        int id)
    {
        return new RegistroDatos(
            id,
            hashValidacion: id * 1_000L,
            pesoBytes: 100);
    }

    private static int[] ObtenerIds(
        RegistroDatos[] registros)
    {
        return registros
            .Select(registro => registro.Id)
            .ToArray();
    }
}