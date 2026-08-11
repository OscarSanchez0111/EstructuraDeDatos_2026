using Proyecto_Final_Fase_1;
using Proyecto_Final_Fase_2;

namespace Proyecto_Final_Fase_2.Tests;

public class PruebasQuickSort
{
    [Fact]
    public void Ordenar_ArregloNulo_LanzaExcepcion()
    {
        Assert.Throws<ArgumentNullException>(
            () => MotorQuickSort.Ordenar(null!));
    }

    [Fact]
    public void Ordenar_ArregloVacio_NoLanzaExcepcion()
    {
        RegistroDatos[] registros = [];

        MetricasQuickSort metricas =
            MotorQuickSort.Ordenar(registros);

        Assert.Empty(registros);
        Assert.Equal(0, metricas.TamanoEntrada);
    }

    [Fact]
    public void Ordenar_UnElemento_NoLoModifica()
    {
        RegistroDatos registro = CrearRegistro(15);
        RegistroDatos[] registros = [registro];

        MetricasQuickSort metricas =
            MotorQuickSort.Ordenar(registros);

        Assert.Equal(registro, registros[0]);
        Assert.Equal(1, metricas.TamanoEntrada);
        Assert.Equal(0, metricas.TotalComparaciones);
    }

    [Fact]
    public void Ordenar_DosElementosOrdenados_MantieneOrden()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(10),
            CrearRegistro(20)
        ];

        MotorQuickSort.Ordenar(registros);

        Assert.Equal(10, registros[0].Id);
        Assert.Equal(20, registros[1].Id);
    }

    [Fact]
    public void Ordenar_DosElementosInvertidos_LosOrdena()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(20),
            CrearRegistro(10)
        ];

        MotorQuickSort.Ordenar(registros);

        Assert.Equal(10, registros[0].Id);
        Assert.Equal(20, registros[1].Id);
    }

    [Fact]
    public void Ordenar_ArregloInverso_OrdenaAscendente()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(50),
            CrearRegistro(40),
            CrearRegistro(30),
            CrearRegistro(20),
            CrearRegistro(10)
        ];

        MotorQuickSort.Ordenar(registros);

        Assert.True(EstaOrdenado(registros));
        Assert.Equal(
            [10, 20, 30, 40, 50],
            registros.Select(registro => registro.Id).ToArray());
    }

    [Fact]
    public void Ordenar_ArregloYaOrdenado_ConservaOrden()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(10),
            CrearRegistro(20),
            CrearRegistro(30),
            CrearRegistro(40),
            CrearRegistro(50)
        ];

        MotorQuickSort.Ordenar(registros);

        Assert.True(EstaOrdenado(registros));
    }

    [Fact]
    public void Ordenar_IdsRepetidos_FinalizaCorrectamente()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(5, 101),
            CrearRegistro(3, 102),
            CrearRegistro(5, 103),
            CrearRegistro(3, 104),
            CrearRegistro(5, 105),
            CrearRegistro(3, 106)
        ];

        MotorQuickSort.Ordenar(registros);

        Assert.True(EstaOrdenado(registros));
        Assert.Equal(
            [3, 3, 3, 5, 5, 5],
            registros.Select(registro => registro.Id).ToArray());
    }

    [Fact]
    public void Ordenar_ArregloAleatorio_ProduceOrdenCorrecto()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(42),
            CrearRegistro(7),
            CrearRegistro(19),
            CrearRegistro(3),
            CrearRegistro(55),
            CrearRegistro(28),
            CrearRegistro(11)
        ];

        MetricasQuickSort metricas =
            MotorQuickSort.Ordenar(registros);

        Assert.True(EstaOrdenado(registros));
        Assert.Equal(7, metricas.TamanoEntrada);
        Assert.True(metricas.TotalComparaciones > 0);
        Assert.True(metricas.TotalLlamadasRecursivas > 0);
        Assert.True(metricas.ProfundidadMaxima > 0);
    }

    [Fact]
    public void Ordenar_ConservaTodosLosRegistrosOriginales()
    {
        RegistroDatos[] registros =
        [
            CrearRegistro(30, 300),
            CrearRegistro(10, 100),
            CrearRegistro(20, 200)
        ];

        RegistroDatos[] originales =
            (RegistroDatos[])registros.Clone();

        MotorQuickSort.Ordenar(registros);

        foreach (RegistroDatos original in originales)
        {
            Assert.Contains(original, registros);
        }
    }

    private static RegistroDatos CrearRegistro(
        int id,
        long? hash = null)
    {
        return new RegistroDatos(
            id,
            hash ?? id * 1_000L,
            pesoBytes: 100);
    }

    private static bool EstaOrdenado(
        RegistroDatos[] registros)
    {
        for (int i = 0; i < registros.Length - 1; i++)
        {
            if (registros[i].Id > registros[i + 1].Id)
            {
                return false;
            }
        }

        return true;
    }
}