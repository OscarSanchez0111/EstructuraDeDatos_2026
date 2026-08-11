using Proyecto_Final_Fase_1;
using Proyecto_Final_Fase_3;

namespace Proyecto_Final_Fase_4.Tests;

/// <summary>
/// Verifica la construcción del índice y la búsqueda binaria.
/// </summary>
public class PruebasBusquedaIndexada
{
    [Fact]
    public void ConstruirIndice_OrdenaRegistrosPorId()
    {
        TablaDinamica tabla = new();

        tabla.InsertarFinal(CrearRegistro(30));
        tabla.InsertarFinal(CrearRegistro(10));
        tabla.InsertarFinal(CrearRegistro(20));

        RegistroDatos[] indice =
            BuscadorIndexado.ConstruirIndice(tabla);

        int[] ids = indice
            .Select(registro => registro.Id)
            .ToArray();

        Assert.Equal(new[] { 10, 20, 30 }, ids);
    }

    [Fact]
    public void ConstruirIndice_TablaVacia_DevuelveArregloVacio()
    {
        TablaDinamica tabla = new();

        RegistroDatos[] indice =
            BuscadorIndexado.ConstruirIndice(tabla);

        Assert.Empty(indice);
    }

    [Fact]
    public void ConstruirIndice_TablaNula_LanzaExcepcion()
    {
        Assert.Throws<ArgumentNullException>(
            () => BuscadorIndexado.ConstruirIndice(null!));
    }

    [Fact]
    public void Buscar_ArregloVacio_NoEncuentraRegistro()
    {
        ResultadoBusqueda resultado =
            BuscadorIndexado.BuscarRegistroIndexado(
                Array.Empty<RegistroDatos>(),
                idBuscado: 10);

        Assert.False(resultado.Encontrado);
        Assert.False(resultado.Registro.HasValue);
        Assert.Equal(0, resultado.Comparaciones);
    }

    [Fact]
    public void Buscar_UnElementoExistente_LoEncuentra()
    {
        RegistroDatos[] indice =
        {
            CrearRegistro(42)
        };

        ResultadoBusqueda resultado =
            BuscadorIndexado.BuscarRegistroIndexado(
                indice,
                idBuscado: 42);

        Assert.True(resultado.Encontrado);
        Assert.Equal(
            42,
            resultado.Registro.GetValueOrDefault().Id);
        Assert.Equal(1, resultado.Comparaciones);
    }

    [Fact]
    public void Buscar_UnElementoInexistente_NoLoEncuentra()
    {
        RegistroDatos[] indice =
        {
            CrearRegistro(42)
        };

        ResultadoBusqueda resultado =
            BuscadorIndexado.BuscarRegistroIndexado(
                indice,
                idBuscado: 99);

        Assert.False(resultado.Encontrado);
        Assert.Equal(1, resultado.Comparaciones);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(40)]
    [InlineData(70)]
    public void Buscar_PrimeroCentroYUltimo_LosEncuentra(
        int idBuscado)
    {
        RegistroDatos[] indice =
        {
            CrearRegistro(10),
            CrearRegistro(20),
            CrearRegistro(30),
            CrearRegistro(40),
            CrearRegistro(50),
            CrearRegistro(60),
            CrearRegistro(70)
        };

        ResultadoBusqueda resultado =
            BuscadorIndexado.BuscarRegistroIndexado(
                indice,
                idBuscado);

        Assert.True(resultado.Encontrado);
        Assert.Equal(
            idBuscado,
            resultado.Registro.GetValueOrDefault().Id);
        Assert.InRange(resultado.Comparaciones, 1, 3);
    }

    [Fact]
    public void Buscar_IdInexistente_NoSuperaComplejidadEsperada()
    {
        RegistroDatos[] indice =
        {
            CrearRegistro(10),
            CrearRegistro(20),
            CrearRegistro(30),
            CrearRegistro(40),
            CrearRegistro(50),
            CrearRegistro(60),
            CrearRegistro(70)
        };

        ResultadoBusqueda resultado =
            BuscadorIndexado.BuscarRegistroIndexado(
                indice,
                idBuscado: 55);

        Assert.False(resultado.Encontrado);
        Assert.InRange(resultado.Comparaciones, 1, 3);
    }

    [Fact]
    public void Buscar_ArregloNulo_LanzaExcepcion()
    {
        Assert.Throws<ArgumentNullException>(
            () => BuscadorIndexado.BuscarRegistroIndexado(
                null!,
                idBuscado: 10));
    }

    [Fact]
    public void ConstruirIndice_NoModificaOrdenDeLaListaOriginal()
    {
        TablaDinamica tabla = new();

        tabla.InsertarFinal(CrearRegistro(30));
        tabla.InsertarFinal(CrearRegistro(10));
        tabla.InsertarFinal(CrearRegistro(20));

        _ = BuscadorIndexado.ConstruirIndice(tabla);

        int[] idsOriginales = tabla
            .ObtenerComoArreglo()
            .Select(registro => registro.Id)
            .ToArray();

        Assert.Equal(new[] { 30, 10, 20 }, idsOriginales);
    }

    private static RegistroDatos CrearRegistro(int id)
    {
        return new RegistroDatos(
            id,
            hashValidacion: id * 10_000L,
            pesoBytes: 100);
    }
}