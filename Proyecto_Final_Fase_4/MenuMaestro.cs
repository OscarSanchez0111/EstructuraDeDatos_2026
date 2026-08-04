using Proyecto_Final_Fase_1;

namespace Proyecto_Final_Fase_4;

/// <summary>
/// Proporciona la interfaz interactiva de consola de DataCore v4.0.
/// </summary>
public sealed class MenuMaestro
{
    private readonly GestorDataCore gestor;
    private readonly TextReader entrada;
    private readonly TextWriter salida;

    /// <summary>
    /// Inicializa el menú utilizando la consola del sistema.
    /// </summary>
    public MenuMaestro()
        : this(
            new GestorDataCore(),
            Console.In,
            Console.Out)
    {
    }

    /// <summary>
    /// Inicializa el menú con dependencias configurables.
    /// </summary>
    /// <param name="gestor">Gestor central de DataCore.</param>
    /// <param name="entrada">Origen de las entradas del usuario.</param>
    /// <param name="salida">Destino de los mensajes del sistema.</param>
    public MenuMaestro(
        GestorDataCore gestor,
        TextReader entrada,
        TextWriter salida)
    {
        ArgumentNullException.ThrowIfNull(gestor);
        ArgumentNullException.ThrowIfNull(entrada);
        ArgumentNullException.ThrowIfNull(salida);

        this.gestor = gestor;
        this.entrada = entrada;
        this.salida = salida;
    }

    /// <summary>
    /// Ejecuta el ciclo principal hasta que el usuario confirma la salida.
    /// </summary>
    public void Ejecutar()
    {
        bool continuar;

        do
        {
            MostrarMenu();

            int opcion = LeerEnteroEnRango(
                "Seleccione una opción: ",
                minimo: 0,
                maximo: 6);

            continuar = ProcesarOpcion(opcion);
        }
        while (continuar);

        salida.WriteLine();
        salida.WriteLine("DataCore finalizó correctamente.");
    }

    /// <summary>
    /// Muestra las opciones principales del sistema.
    /// </summary>
    private void MostrarMenu()
    {
        salida.WriteLine();
        salida.WriteLine("===========================================");
        salida.WriteLine("   DATACORE v4.0 - MENÚ MAESTRO");
        salida.WriteLine("===========================================");
        salida.WriteLine($"Registros actuales: {gestor.Cantidad}");
        salida.WriteLine("-------------------------------------------");
        salida.WriteLine("[1] Insertar registro");
        salida.WriteLine("[2] Eliminar registro por ID");
        salida.WriteLine("[3] Mostrar todos los registros");
        salida.WriteLine("[4] Ordenar e indexar registros");
        salida.WriteLine("[5] Buscar registro");
        salida.WriteLine(
            "[6] Estadísticas, historial y benchmark");
        salida.WriteLine("[0] Salir");
        salida.WriteLine("===========================================");
    }

    /// <summary>
    /// Dirige la opción seleccionada hacia su operación.
    /// </summary>
    /// <param name="opcion">Número seleccionado.</param>
    /// <returns>False únicamente cuando se confirma la salida.</returns>
    private bool ProcesarOpcion(int opcion)
    {
        switch (opcion)
        {
            case 1:
                InsertarRegistro();
                return true;

            case 2:
                EliminarRegistro();
                return true;

            case 3:
                MostrarRegistros();
                return true;

            case 4:
                OrdenarEIndexar();
                return true;

            case 5:
                MostrarMenuBusqueda();
                return true;

            case 6:
                MostrarEstadisticas();
                return true;

            case 0:
                return !Confirmar(
                    "¿Confirma que desea salir? (s/n): ");

            default:
                return true;
        }
    }

    /// <summary>
    /// Solicita y almacena un registro nuevo.
    /// </summary>
    private void InsertarRegistro()
    {
        salida.WriteLine();
        salida.WriteLine("--- INSERTAR REGISTRO ---");

        int id = LeerEnteroPositivo("ID: ");
        long hash = LeerLongNoNegativo(
            "Hash de validación: ");
        int peso = LeerEnteroPositivo(
            "Peso en bytes: ");

        try
        {
            RegistroDatos registro = new(
                id,
                hash,
                peso);

            bool insertado = gestor.Insertar(registro);

            if (insertado)
            {
                salida.WriteLine(
                    $"Registro con ID {id} " +
                    "insertado correctamente.");
            }
            else
            {
                salida.WriteLine(
                    $"No se insertó: el ID {id} ya existe.");
            }
        }
        catch (ArgumentException excepcion)
        {
            salida.WriteLine(
                $"No fue posible insertar: " +
                $"{excepcion.Message}");
        }
    }

    /// <summary>
    /// Solicita un ID y elimina el registro correspondiente.
    /// </summary>
    private void EliminarRegistro()
    {
        salida.WriteLine();
        salida.WriteLine("--- ELIMINAR REGISTRO ---");

        if (gestor.Cantidad == 0)
        {
            salida.WriteLine("La tabla está vacía.");
            return;
        }

        int id = LeerEnteroPositivo(
            "ID que desea eliminar: ");

        if (!Confirmar(
            $"¿Confirma eliminar el ID {id}? (s/n): "))
        {
            salida.WriteLine("Eliminación cancelada.");
            return;
        }

        bool eliminado = gestor.Eliminar(id);

        salida.WriteLine(
            eliminado
                ? $"Registro con ID {id} eliminado."
                : $"No existe un registro con ID {id}.");
    }

    /// <summary>
    /// Presenta todos los registros en orden de inserción.
    /// </summary>
    private void MostrarRegistros()
    {
        salida.WriteLine();
        salida.WriteLine("--- REGISTROS EN MEMORIA ---");

        RegistroDatos[] registros =
            gestor.ObtenerRegistros();

        if (registros.Length == 0)
        {
            salida.WriteLine("La tabla está vacía.");
            return;
        }

        foreach (RegistroDatos registro in registros)
        {
            salida.WriteLine(registro);
        }

        salida.WriteLine(
            $"Total mostrado: {registros.Length}");
    }

    /// <summary>
    /// Construye el índice y muestra los registros ordenados.
    /// </summary>
    private void OrdenarEIndexar()
    {
        salida.WriteLine();
        salida.WriteLine("--- ÍNDICE ORDENADO POR ID ---");

        RegistroDatos[] indice =
            gestor.OrdenarEIndexar();

        if (indice.Length == 0)
        {
            salida.WriteLine(
                "La tabla está vacía; " +
                "se generó un índice vacío.");

            return;
        }

        foreach (RegistroDatos registro in indice)
        {
            salida.WriteLine(registro);
        }

        salida.WriteLine(
            $"Índice creado con {indice.Length} registros.");
    }

    /// <summary>
    /// Permite elegir entre búsqueda lineal e indexada.
    /// </summary>
    private void MostrarMenuBusqueda()
    {
        salida.WriteLine();
        salida.WriteLine("--- MÓDULO DE BÚSQUEDA ---");
        salida.WriteLine("[1] Búsqueda lineal O(n)");
        salida.WriteLine(
            "[2] Búsqueda binaria indexada O(log n)");
        salida.WriteLine("[0] Regresar");

        int opcion = LeerEnteroEnRango(
            "Seleccione el tipo de búsqueda: ",
            minimo: 0,
            maximo: 2);

        if (opcion == 0)
        {
            salida.WriteLine(
                "Regresando al menú principal.");

            return;
        }

        int id = LeerEnteroPositivo(
            "ID que desea buscar: ");

        ResultadoBusqueda resultado =
            opcion == 1
                ? gestor.BuscarLineal(id)
                : gestor.BuscarIndexado(id);

        string algoritmo =
            opcion == 1
                ? "Búsqueda lineal"
                : "Búsqueda binaria indexada";

        MostrarResultadoBusqueda(
            algoritmo,
            resultado);
    }

    /// <summary>
    /// Muestra el resultado y las comparaciones de una búsqueda.
    /// </summary>
    /// <param name="algoritmo">
    /// Nombre del algoritmo utilizado.
    /// </param>
    /// <param name="resultado">Resultado obtenido.</param>
    private void MostrarResultadoBusqueda(
        string algoritmo,
        ResultadoBusqueda resultado)
    {
        salida.WriteLine();
        salida.WriteLine($"Algoritmo: {algoritmo}");

        if (resultado.Registro is RegistroDatos registro)
        {
            salida.WriteLine("Registro encontrado:");
            salida.WriteLine(registro);
        }
        else
        {
            salida.WriteLine("Registro no encontrado.");
        }

        salida.WriteLine(
            $"Comparaciones realizadas: " +
            $"{resultado.Comparaciones}");
    }

    /// <summary>
    /// Presenta estadísticas, historial y el benchmark opcional.
    /// </summary>
    private void MostrarEstadisticas()
    {
        salida.WriteLine();
        salida.WriteLine("--- ESTADÍSTICAS DEL SISTEMA ---");
        salida.WriteLine(
            $"Registros en memoria : {gestor.Cantidad}");
        salida.WriteLine(
            $"Peso total declarado : " +
            $"{gestor.CalcularPesoTotalBytes():N0} bytes");
        salida.WriteLine(
            $"Índice actualizado   : " +
            $"{gestor.IndiceActualizado}");
        salida.WriteLine(
            $"Operaciones registradas: " +
            $"{gestor.TotalOperaciones}");

        string[] historial =
            gestor.ObtenerHistorial();

        if (historial.Length == 0)
        {
            salida.WriteLine("El historial está vacío.");
        }
        else
        {
            salida.WriteLine();
            salida.WriteLine("--- HISTORIAL ---");

            foreach (string operacion in historial)
            {
                salida.WriteLine(operacion);
            }
        }

        salida.WriteLine();

        bool ejecutarBenchmark = Confirmar(
            "¿Ejecutar benchmark con " +
            "1,000,000 de registros? (s/n): ");

        if (!ejecutarBenchmark)
        {
            salida.WriteLine("Benchmark omitido.");
            return;
        }

        MostrarBenchmark();
    }

    /// <summary>
    /// Ejecuta y muestra la comparación de búsquedas.
    /// </summary>
    private void MostrarBenchmark()
    {
        salida.WriteLine();
        salida.WriteLine(
            "--- BENCHMARK: O(n) VS. O(log n) ---");
        salida.WriteLine(
            "Preparando 1,000,000 de registros...");

        ResultadoBenchmarkBusqueda resultado =
            BenchmarkBusqueda.Ejecutar(
                cantidadRegistros: 1_000_000);

        salida.WriteLine();
        salida.WriteLine(resultado);
        salida.WriteLine();
        salida.WriteLine(
            "Nota: los tiempos pueden variar " +
            "entre ejecuciones.");
    }

    /// <summary>
    /// Lee un número entero positivo y controla formatos inválidos.
    /// </summary>
    /// <param name="mensaje">
    /// Texto mostrado antes de leer.
    /// </param>
    /// <returns>Número entero mayor que cero.</returns>
    private int LeerEnteroPositivo(string mensaje)
    {
        while (true)
        {
            salida.Write(mensaje);

            string valor =
                entrada.ReadLine() ?? string.Empty;

            try
            {
                int numero = int.Parse(valor);

                if (numero <= 0)
                {
                    salida.WriteLine(
                        "El valor debe ser mayor que cero.");

                    continue;
                }

                return numero;
            }
            catch (FormatException)
            {
                salida.WriteLine(
                    "Entrada inválida. " +
                    "Escriba un número entero.");
            }
            catch (OverflowException)
            {
                salida.WriteLine(
                    "El número está fuera " +
                    "del rango permitido.");
            }
        }
    }

    /// <summary>
    /// Lee un entero dentro de un rango permitido.
    /// </summary>
    /// <param name="mensaje">
    /// Texto mostrado antes de leer.
    /// </param>
    /// <param name="minimo">Valor mínimo aceptado.</param>
    /// <param name="maximo">Valor máximo aceptado.</param>
    /// <returns>Número validado.</returns>
    private int LeerEnteroEnRango(
        string mensaje,
        int minimo,
        int maximo)
    {
        while (true)
        {
            salida.Write(mensaje);

            string valor =
                entrada.ReadLine() ?? string.Empty;

            try
            {
                int numero = int.Parse(valor);

                if (numero < minimo || numero > maximo)
                {
                    salida.WriteLine(
                        $"Seleccione un número entre " +
                        $"{minimo} y {maximo}.");

                    continue;
                }

                return numero;
            }
            catch (FormatException)
            {
                salida.WriteLine(
                    "Entrada inválida. " +
                    "Escriba una opción numérica.");
            }
            catch (OverflowException)
            {
                salida.WriteLine(
                    "El número está fuera " +
                    "del rango permitido.");
            }
        }
    }

    /// <summary>
    /// Lee un número long igual o mayor que cero.
    /// </summary>
    /// <param name="mensaje">
    /// Texto mostrado antes de leer.
    /// </param>
    /// <returns>Número long validado.</returns>
    private long LeerLongNoNegativo(string mensaje)
    {
        while (true)
        {
            salida.Write(mensaje);

            string valor =
                entrada.ReadLine() ?? string.Empty;

            try
            {
                long numero = long.Parse(valor);

                if (numero < 0)
                {
                    salida.WriteLine(
                        "El valor no puede ser negativo.");

                    continue;
                }

                return numero;
            }
            catch (FormatException)
            {
                salida.WriteLine(
                    "Entrada inválida. " +
                    "Escriba un número entero.");
            }
            catch (OverflowException)
            {
                salida.WriteLine(
                    "El número está fuera " +
                    "del rango permitido.");
            }
        }
    }

    /// <summary>
    /// Solicita una confirmación mediante las respuestas s o n.
    /// </summary>
    /// <param name="mensaje">
    /// Pregunta que se mostrará al usuario.
    /// </param>
    /// <returns>
    /// True cuando se responde afirmativamente.
    /// </returns>
    private bool Confirmar(string mensaje)
    {
        while (true)
        {
            salida.Write(mensaje);

            string respuesta =
                (entrada.ReadLine() ?? string.Empty)
                .Trim()
                .ToLowerInvariant();

            if (respuesta == "s")
            {
                return true;
            }

            if (respuesta == "n")
            {
                return false;
            }

            salida.WriteLine(
                "Respuesta inválida. Escriba s o n.");
        }
    }
}