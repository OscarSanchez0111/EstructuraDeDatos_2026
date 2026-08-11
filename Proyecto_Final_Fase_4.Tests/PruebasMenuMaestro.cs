namespace Proyecto_Final_Fase_4.Tests;

/// <summary>
/// Verifica la navegación y validación del Menú Maestro.
/// </summary>
public class PruebasMenuMaestro
{
    [Fact]
    public void Ejecutar_SalidaConfirmada_FinalizaCorrectamente()
    {
        string salida = EjecutarMenu(
            "0\ns\n",
            out GestorDataCore gestor);

        Assert.Equal(0, gestor.Cantidad);

        Assert.Contains(
            "DataCore finalizó correctamente.",
            salida);
    }

    [Fact]
    public void Ejecutar_EntradaInvalida_RecuperaControl()
    {
        string salida = EjecutarMenu(
            "abc\n9\n0\ns\n",
            out _);

        Assert.Contains(
            "Entrada inválida.",
            salida);

        Assert.Contains(
            "Seleccione un número entre 0 y 6.",
            salida);

        Assert.Contains(
            "DataCore finalizó correctamente.",
            salida);
    }

    [Fact]
    public void MostrarRegistros_TablaVacia_MuestraMensaje()
    {
        string salida = EjecutarMenu(
            "3\n0\ns\n",
            out GestorDataCore gestor);

        Assert.Equal(0, gestor.Cantidad);

        Assert.Contains(
            "La tabla está vacía.",
            salida);
    }

    [Fact]
    public void Insertar_IdDuplicado_RechazaSegundoRegistro()
    {
        string entradas =
            "1\n10\n10000\n100\n" +
            "1\n10\n99999\n200\n" +
            "0\ns\n";

        string salida = EjecutarMenu(
            entradas,
            out GestorDataCore gestor);

        Assert.Equal(1, gestor.Cantidad);

        Assert.Contains(
            "Registro con ID 10 insertado correctamente.",
            salida);

        Assert.Contains(
            "No se insertó: el ID 10 ya existe.",
            salida);
    }

    [Fact]
    public void Eliminar_UltimoRegistro_DejaTablaVacia()
    {
        string entradas =
            "1\n10\n10000\n100\n" +
            "2\n10\ns\n" +
            "0\ns\n";

        string salida = EjecutarMenu(
            entradas,
            out GestorDataCore gestor);

        Assert.Equal(0, gestor.Cantidad);

        Assert.Contains(
            "Registro con ID 10 eliminado.",
            salida);
    }

    [Fact]
    public void BuscarIndexado_IdExistente_MuestraRegistro()
    {
        string entradas =
            "1\n30\n30000\n300\n" +
            "1\n10\n10000\n100\n" +
            "5\n2\n10\n" +
            "0\ns\n";

        string salida = EjecutarMenu(
            entradas,
            out GestorDataCore gestor);

        Assert.Equal(2, gestor.Cantidad);

        Assert.Contains(
            "Algoritmo: Búsqueda binaria indexada",
            salida);

        Assert.Contains(
            "Registro encontrado:",
            salida);

        Assert.Contains(
            "Id:   10",
            salida);
    }

    [Fact]
    public void BuscarIndexado_IdInexistente_MuestraMensaje()
    {
        string entradas =
            "1\n10\n10000\n100\n" +
            "5\n2\n99\n" +
            "0\ns\n";

        string salida = EjecutarMenu(
            entradas,
            out _);

        Assert.Contains(
            "Registro no encontrado.",
            salida);

        Assert.Contains(
            "Comparaciones realizadas:",
            salida);
    }

    [Fact]
    public void Salir_CancelarConfirmacion_RegresaAlMenu()
    {
        string salida = EjecutarMenu(
            "0\nn\n0\ns\n",
            out _);

        int aparicionesMenu =
            ContarApariciones(
                salida,
                "DATACORE v4.0 - MENÚ MAESTRO");

        Assert.Equal(2, aparicionesMenu);

        Assert.Contains(
            "DataCore finalizó correctamente.",
            salida);
    }

    private static string EjecutarMenu(
        string entradas,
        out GestorDataCore gestor)
    {
        gestor = new GestorDataCore();

        using StringReader lector =
            new(entradas);

        using StringWriter escritor =
            new();

        MenuMaestro menu = new(
            gestor,
            lector,
            escritor);

        menu.Ejecutar();

        return escritor.ToString();
    }

    private static int ContarApariciones(
        string texto,
        string fragmento)
    {
        int cantidad = 0;
        int posicion = 0;

        while (true)
        {
            int encontrada = texto.IndexOf(
                fragmento,
                posicion,
                StringComparison.Ordinal);

            if (encontrada < 0)
            {
                return cantidad;
            }

            cantidad++;
            posicion = encontrada + fragmento.Length;
        }
    }
}