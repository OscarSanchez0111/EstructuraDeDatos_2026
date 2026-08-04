# Proyecto Final - Fase 4

## DataCore v4.0: búsqueda indexada e integración definitiva

Esta fase integra las estructuras y algoritmos desarrollados durante las fases anteriores en una aplicación interactiva de consola.

DataCore v4.0 permite almacenar registros en una lista simplemente enlazada, ordenarlos mediante QuickSort, construir un índice auxiliar y realizar búsquedas lineales o binarias.

## Funcionalidades

- Inserción de registros.
- Validación de IDs duplicados.
- Eliminación de registros por ID.
- Visualización de registros en memoria.
- Conversión de la lista enlazada a un arreglo.
- Ordenación mediante QuickSort.
- Construcción automática de un índice ordenado.
- Búsqueda lineal O(n).
- Búsqueda binaria indexada O(log n).
- Conteo de comparaciones.
- Historial de operaciones.
- Estadísticas de la sesión.
- Benchmark con 1,000,000 de registros.
- Validación de entradas.
- Confirmación de operaciones destructivas y salida.

## Integración de las fases

La Fase 4 conserva y reutiliza los proyectos anteriores mediante referencias entre proyectos:

```text
Proyecto_Final_Fase_4
        ↓
Proyecto_Final_Fase_3
        ↓
Proyecto_Final_Fase_2
        ↓
Proyecto_Final_Fase_1
```

### Fase 1

Proporciona:

- `RegistroDatos`.
- `SelectionSort`.
- Métricas de ordenación.

### Fase 2

Proporciona:

- QuickSort recursivo.
- Particionado Lomuto.
- Selección de pivote mediante mediana de tres.
- Métricas de comparaciones, intercambios y recursividad.

### Fase 3

Proporciona:

- `NodoRegistro`.
- `TablaDinamica`.
- Lista simplemente enlazada.
- Inserción, búsqueda, eliminación y conversión a arreglo.

### Fase 4

Agrega:

- `BuscadorIndexado`.
- `ResultadoBusqueda`.
- `GestorDataCore`.
- `HistorialOperacion`.
- `MenuMaestro`.
- `BenchmarkBusqueda`.
- `ResultadoBenchmarkBusqueda`.

## Arquitectura

El sistema está dividido por responsabilidades:

| Componente | Responsabilidad |
|---|---|
| `RegistroDatos` | Representar un registro inmutable |
| `TablaDinamica` | Almacenar registros en una lista enlazada |
| `MotorQuickSort` | Ordenar arreglos por ID |
| `BuscadorIndexado` | Construir el índice y buscar mediante búsqueda binaria |
| `GestorDataCore` | Coordinar inserciones, eliminaciones, índice y búsquedas |
| `HistorialOperacion` | Registrar las operaciones de la sesión |
| `MenuMaestro` | Controlar la interacción con el usuario |
| `BenchmarkBusqueda` | Comparar búsqueda lineal y binaria |

## Menú Maestro

```text
===========================================
   DATACORE v4.0 - MENÚ MAESTRO
===========================================
[1] Insertar registro
[2] Eliminar registro por ID
[3] Mostrar todos los registros
[4] Ordenar e indexar registros
[5] Buscar registro
[6] Estadísticas, historial y benchmark
[0] Salir
===========================================
```

El menú utiliza un ciclo `do-while` y conserva el estado de los registros durante toda la sesión.

También controla:

- Letras ingresadas en campos numéricos.
- Opciones fuera de rango.
- Números demasiado grandes.
- IDs duplicados.
- IDs inexistentes.
- Tablas vacías.
- Confirmaciones inválidas.
- Cancelación de eliminaciones.
- Confirmación antes de salir.

## Construcción del índice

La lista enlazada no permite acceso aleatorio O(1), por lo que la búsqueda binaria no puede ejecutarse directamente sobre sus nodos.

El sistema realiza estos pasos:

1. Recorre `TablaDinamica`.
2. Copia sus registros a un arreglo.
3. Ordena el arreglo por ID mediante QuickSort.
4. Conserva el arreglo como índice auxiliar.
5. Ejecuta la búsqueda binaria sobre el índice.

Cuando se inserta o elimina un registro, el índice se marca como desactualizado. La siguiente búsqueda indexada lo reconstruye automáticamente.

## Análisis de complejidad

| Operación | Complejidad temporal | Espacio adicional |
|---|---:|---:|
| Insertar al inicio de lista | O(1) | O(1) |
| Insertar al final de lista | O(n) | O(1) |
| Eliminar por ID | O(n) | O(1) |
| Buscar linealmente | O(n) | O(1) |
| Convertir lista a arreglo | O(n) | O(n) |
| QuickSort promedio | O(n log n) | O(log n) |
| QuickSort peor caso | O(n²) | O(n) |
| Construir índice | O(n log n) | O(n) |
| Buscar en índice | O(log n) | O(1) |

El costo de construcción del índice se amortiza cuando se realizan múltiples búsquedas sobre los mismos datos.

## Benchmark

El benchmark busca el último ID de un arreglo ordenado con 1,000,000 de registros.

Resultado de referencia:

| Algoritmo | Comparaciones | Complejidad |
|---|---:|---:|
| Búsqueda lineal | 1,000,000 | O(n) |
| Búsqueda binaria | 20 | O(log n) |

La búsqueda binaria reduce el número de comparaciones aproximadamente 50,000 veces.

Los tiempos dependen del equipo y pueden variar entre ejecuciones, por lo que el número de comparaciones es la evidencia principal de complejidad.

## Requisitos

- .NET 10 SDK.
- Terminal o consola.
- Git para el control de versiones.

## Compilación

Desde la raíz del repositorio:

```bash
dotnet build Proyecto_Final_Fase_4/Proyecto_Final_Fase_4.csproj
```

## Ejecución

```bash
dotnet run --project Proyecto_Final_Fase_4/Proyecto_Final_Fase_4.csproj
```

## Pruebas

```bash
dotnet test Proyecto_Final_Fase_4.Tests/Proyecto_Final_Fase_4.Tests.csproj
```

Resultado actual:

```text
Total: 37
Correctas: 37
Errores: 0
Omitidas: 0
```

Las pruebas cubren:

- Construcción del índice.
- Tabla vacía.
- Arreglo nulo.
- Un solo elemento.
- Primer, central y último elemento.
- ID inexistente.
- Conteo de comparaciones.
- Inserciones.
- IDs duplicados.
- Eliminaciones.
- Actualización automática del índice.
- Historial.
- Flujo completo.
- Validación del menú.
- Salida segura.
- Benchmark de 1,000,000 de registros.

## Flujo de Git

El desarrollo se realizó en:

```text
feature/proyecto-fase4-integracion
```

Todos los cambios se organizaron mediante commits semánticos y serán integrados mediante Pull Request. No se realizaron commits directos sobre `main`.

## Documentación

La entrega incluye:

- Código comentado en español.
- Comentarios XML en métodos públicos.
- README de ejecución.
- Reporte de pruebas.
- Sustento teórico.
- Evidencias de consola.
- Evidencias de GitHub Actions.
- Pull Request documentado.

## Uso de inteligencia artificial

Se utilizó inteligencia artificial como apoyo para comprender los requisitos, estructurar clases, revisar casos borde, generar pruebas y organizar la documentación.

Todo el código fue compilado, ejecutado y validado mediante pruebas automatizadas antes de incorporarse al repositorio.

## Autor

Oscar Sánchez  
Proyecto Final de Estructura de Datos  
DataCore v4.0