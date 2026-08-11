using Proyecto_Final_Fase_3;

namespace Proyecto_Final_Fase_3.Tests;

/// <summary>
/// Verifica el funcionamiento de la lista simplemente enlazada genérica.
/// </summary>
public class PruebasTablaDinamicaGenerica
{
    [Fact]
    public void NodoGenerico_AlCrearse_ConservaElDato()
    {
        Nodo<string> nodo = new("DataCore");

        Assert.Equal("DataCore", nodo.Dato);
        Assert.Null(nodo.Siguiente);
    }

    [Fact]
    public void TablaGenerica_AlCrearse_EstaVacia()
    {
        TablaDinamica<int> tabla = new();

        Assert.True(tabla.EstaVacia);
        Assert.Equal(0, tabla.Cantidad);
        Assert.Empty(tabla.ObtenerComoArreglo());
    }

    [Fact]
    public void InsertarInicio_ConEnteros_InvierteOrdenDeInsercion()
    {
        TablaDinamica<int> tabla = new();

        tabla.InsertarInicio(10);
        tabla.InsertarInicio(20);
        tabla.InsertarInicio(30);

        Assert.Equal(
            new[] { 30, 20, 10 },
            tabla.ObtenerComoArreglo());

        Assert.Equal(3, tabla.Cantidad);
    }

    [Fact]
    public void InsertarFinal_ConCadenas_ConservaOrdenDeLlegada()
    {
        TablaDinamica<string> tabla = new();

        tabla.InsertarFinal("Fase 1");
        tabla.InsertarFinal("Fase 2");
        tabla.InsertarFinal("Fase 3");
        tabla.InsertarFinal("Fase 4");

        Assert.Equal(
            new[] { "Fase 1", "Fase 2", "Fase 3", "Fase 4" },
            tabla.ObtenerComoArreglo());
    }

    [Fact]
    public void IntentarBuscar_ElementoExistente_DevuelveResultado()
    {
        TablaDinamica<string> tabla = new();

        tabla.InsertarFinal("Selection Sort");
        tabla.InsertarFinal("QuickSort");
        tabla.InsertarFinal("Búsqueda binaria");

        bool encontrado = tabla.IntentarBuscar(
            elemento => elemento == "QuickSort",
            out string resultado);

        Assert.True(encontrado);
        Assert.Equal("QuickSort", resultado);
    }

    [Fact]
    public void IntentarBuscar_ElementoInexistente_DevuelveFalse()
    {
        TablaDinamica<int> tabla = new();

        tabla.InsertarFinal(10);
        tabla.InsertarFinal(20);

        bool encontrado = tabla.IntentarBuscar(
            elemento => elemento == 99,
            out _);

        Assert.False(encontrado);
    }

    [Fact]
    public void Existe_UtilizaUnCriterioGenerico()
    {
        TablaDinamica<string> tabla = new();

        tabla.InsertarFinal("DataCore");
        tabla.InsertarFinal("Engine");

        Assert.True(
            tabla.Existe(elemento => elemento.StartsWith("Data")));

        Assert.False(
            tabla.Existe(elemento => elemento == "Inexistente"));
    }

    [Fact]
    public void EliminarPrimero_EliminaCabezaIntermedioYCola()
    {
        TablaDinamica<int> tabla = new();

        tabla.InsertarFinal(10);
        tabla.InsertarFinal(20);
        tabla.InsertarFinal(30);
        tabla.InsertarFinal(40);
        tabla.InsertarFinal(50);

        Assert.True(
            tabla.EliminarPrimero(elemento => elemento == 10));

        Assert.True(
            tabla.EliminarPrimero(elemento => elemento == 30));

        Assert.True(
            tabla.EliminarPrimero(elemento => elemento == 50));

        Assert.Equal(
            new[] { 20, 40 },
            tabla.ObtenerComoArreglo());

        Assert.Equal(2, tabla.Cantidad);
    }

    [Fact]
    public void EliminarUltimo_PermiteVolverAInsertarAlFinal()
    {
        TablaDinamica<int> tabla = new();

        tabla.InsertarFinal(10);
        tabla.InsertarFinal(20);
        tabla.InsertarFinal(30);

        tabla.EliminarPrimero(elemento => elemento == 30);
        tabla.InsertarFinal(40);

        Assert.Equal(
            new[] { 10, 20, 40 },
            tabla.ObtenerComoArreglo());
    }

    [Fact]
    public void Limpiar_EliminaTodosLosElementos()
    {
        TablaDinamica<string> tabla = new();

        tabla.InsertarFinal("Uno");
        tabla.InsertarFinal("Dos");
        tabla.InsertarFinal("Tres");

        tabla.Limpiar();

        Assert.True(tabla.EstaVacia);
        Assert.Equal(0, tabla.Cantidad);
        Assert.Empty(tabla.ObtenerComoArreglo());

        tabla.InsertarFinal("Nuevo");

        Assert.Equal(
            new[] { "Nuevo" },
            tabla.ObtenerComoArreglo());
    }
}