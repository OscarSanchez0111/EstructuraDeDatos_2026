using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_4.Tests;

/// <summary>
/// Verifica la integración entre almacenamiento, índice y búsquedas.
/// </summary>
public class PruebasGestorDataCore
{
    [Fact]
    public void Insertar_RegistroNuevo_AumentaCantidad()
    {
        GestorDataCore gestor = new();

        bool insertado = gestor.Insertar(CrearRegistro(10));

        Assert.True(insertado);
        Assert.Equal(1, gestor.Cantidad);
        Assert.False(gestor.IndiceActualizado);
    }

    [Fact]
    public void Insertar_IdDuplicado_RechazaSegundoRegistro()
    {
        GestorDataCore gestor = new();

        bool primero = gestor.Insertar(CrearRegistro(10));
        bool duplicado = gestor.Insertar(CrearRegistro(10));

        Assert.True(primero);
        Assert.False(duplicado);
        Assert.Equal(1, gestor.Cantidad);
    }

    [Fact]
    public void Insertar_IdNoPositivo_LanzaExcepcion()
    {
        GestorDataCore gestor = new();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => gestor.Insertar(CrearRegistro(0)));
    }

    [Fact]
    public void Eliminar_IdExistente_DisminuyeCantidad()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(20));

        bool eliminado = gestor.Eliminar(10);

        Assert.True(eliminado);
        Assert.Equal(1, gestor.Cantidad);
        Assert.False(gestor.IndiceActualizado);
    }

    [Fact]
    public void Eliminar_IdInexistente_NoModificaCantidad()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(10));

        bool eliminado = gestor.Eliminar(99);

        Assert.False(eliminado);
        Assert.Equal(1, gestor.Cantidad);
    }

    [Fact]
    public void BuscarIndexado_ReconstruyeIndiceAutomaticamente()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(30));
        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(20));

        Assert.False(gestor.IndiceActualizado);

        ResultadoBusqueda resultado =
            gestor.BuscarIndexado(20);

        Assert.True(resultado.Encontrado);
        Assert.True(gestor.IndiceActualizado);
        Assert.Equal(
            20,
            resultado.Registro.GetValueOrDefault().Id);
    }

    [Fact]
    public void Eliminar_ActualizaIndiceEnSiguienteBusqueda()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(20));
        gestor.Insertar(CrearRegistro(30));

        _ = gestor.BuscarIndexado(20);

        bool eliminado = gestor.Eliminar(20);
        ResultadoBusqueda resultado =
            gestor.BuscarIndexado(20);

        Assert.True(eliminado);
        Assert.False(resultado.Encontrado);
        Assert.True(gestor.IndiceActualizado);
    }

    [Fact]
    public void BuscarLineal_CuentaComparacionesCorrectamente()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(20));
        gestor.Insertar(CrearRegistro(30));

        ResultadoBusqueda encontrado =
            gestor.BuscarLineal(30);

        ResultadoBusqueda inexistente =
            gestor.BuscarLineal(99);

        Assert.True(encontrado.Encontrado);
        Assert.Equal(3, encontrado.Comparaciones);
        Assert.False(inexistente.Encontrado);
        Assert.Equal(3, inexistente.Comparaciones);
    }

    [Fact]
    public void OrdenarEIndexar_DevuelveIdsAscendentes()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(50));
        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(30));

        RegistroDatos[] indice =
            gestor.OrdenarEIndexar();

        int[] ids = indice
            .Select(registro => registro.Id)
            .ToArray();

        Assert.Equal(new[] { 10, 30, 50 }, ids);
        Assert.True(gestor.IndiceActualizado);
    }

    [Fact]
    public void CalcularPesoTotalBytes_SumaTodosLosRegistros()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(10, 100));
        gestor.Insertar(CrearRegistro(20, 250));
        gestor.Insertar(CrearRegistro(30, 500));

        long pesoTotal =
            gestor.CalcularPesoTotalBytes();

        Assert.Equal(850, pesoTotal);
    }

    [Fact]
    public void Historial_RegistraOperacionesExitosasYRechazadas()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(10));
        gestor.Eliminar(99);
       _ = gestor.BuscarLineal(99);

        string[] historial = gestor.ObtenerHistorial();

        Assert.Equal(4, historial.Length);

        Assert.Contains(
            historial,
            entrada => entrada.Contains(
                "duplicado",
                StringComparison.OrdinalIgnoreCase));

        Assert.Contains(
            historial,
            entrada => entrada.Contains(
                "no encontrado",
                StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void FlujoCompleto_InsertaEliminaIndexaYBusca()
    {
        GestorDataCore gestor = new();

        gestor.Insertar(CrearRegistro(30));
        gestor.Insertar(CrearRegistro(10));
        gestor.Insertar(CrearRegistro(40));
        gestor.Insertar(CrearRegistro(20));

        Assert.True(gestor.Eliminar(30));

        RegistroDatos[] indice =
            gestor.OrdenarEIndexar();

        ResultadoBusqueda existente =
            gestor.BuscarIndexado(40);

        ResultadoBusqueda eliminado =
            gestor.BuscarIndexado(30);

        Assert.Equal(3, gestor.Cantidad);
        Assert.Equal(
            new[] { 10, 20, 40 },
            indice.Select(registro => registro.Id).ToArray());
        Assert.True(existente.Encontrado);
        Assert.False(eliminado.Encontrado);
    }

    private static RegistroDatos CrearRegistro(
        int id,
        int pesoBytes = 100)
    {
        return new RegistroDatos(
            id,
            hashValidacion: id * 10_000L,
            pesoBytes);
    }
}