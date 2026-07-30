# Proyecto Final - Fase 2

## Motor de ordenación avanzada con QuickSort

Esta fase amplía el proyecto DataCore desarrollado en la Fase 1. Se implementa QuickSort recursivo y se compara su rendimiento contra Selection Sort utilizando exactamente el mismo modelo `RegistroDatos`.

## Relación con la Fase 1

La Fase 2 referencia directamente el proyecto `Proyecto_Final_Fase_1` para reutilizar sin modificaciones:

- `RegistroDatos`
- `SelectionSort`
- `MetricasOrdenacion`

Esto conserva la compatibilidad entre ambas fases y garantiza que los dos algoritmos trabajen con el mismo modelo de datos.

## Implementación

QuickSort incluye:

- Ordenamiento ascendente por `Id`.
- Recursividad con caso base.
- Partición mediante esquema Lomuto.
- Selección de pivote por mediana de tres.
- Intercambio mediante asignación de tuplas.
- Conteo de comparaciones.
- Conteo de intercambios.
- Conteo de llamadas recursivas.
- Medición de profundidad máxima.
- Medición de tiempo con `Stopwatch`.

## Benchmark

El programa genera 10,000 registros reproducibles mediante una semilla fija. Después clona el arreglo para que Selection Sort y QuickSort reciban exactamente la misma entrada.

Resultados obtenidos durante la ejecución de referencia:

| Algoritmo | Comparaciones | Intercambios | Tiempo |
|---|---:|---:|---:|
| Selection Sort | 49,995,000 | 9,994 | 189.6810 ms |
| QuickSort | 152,046 | 75,432 | 1.9818 ms |

QuickSort fue aproximadamente 95.71 veces más rápido en esta ejecución.

Los tiempos pueden variar dependiendo del equipo.

## Ejecutar el programa

Desde la raíz del repositorio:

```bash
dotnet run --project Proyecto_Final_Fase_2/Proyecto_Final_Fase_2.csproj