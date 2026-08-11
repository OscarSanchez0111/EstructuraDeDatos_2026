# Reporte de rendimiento y memoria — Fase 3

## Objetivo

Comparar el comportamiento de un arreglo de `RegistroDatos` con una lista simplemente enlazada formada por objetos `NodoRegistro` almacenados en el Heap administrado de .NET.

## Configuración

- Registros evaluados: 1,000
- Plataforma: .NET 10
- Modelo reutilizado: `RegistroDatos` de la Fase 1
- Estructura dinámica: `TablaDinamica`
- Método de inserción: `InsertarFinal`
- Medición de tiempo: `Stopwatch`
- Memoria aproximada: `GC.GetAllocatedBytesForCurrentThread`

Los resultados corresponden a una ejecución en una MacBook Air y pueden variar dependiendo del equipo y de los procesos activos.

## Resultados

| Operación | Tiempo | Memoria aproximada |
|---|---:|---:|
| Crear y llenar arreglo | 0.0044 ms | 24,024 bytes |
| Insertar en lista enlazada | 2.2025 ms | 48,032 bytes |
| Convertir lista a arreglo | 0.0090 ms | 24,024 bytes |

## Interpretación

La lista enlazada asignó aproximadamente 2.00 veces la memoria del arreglo. Cada `NodoRegistro` necesita almacenar el dato y una referencia administrada al siguiente nodo, además de la sobrecarga propia de un objeto en .NET.

La inserción al final de la lista fue aproximadamente 500.57 veces más lenta que llenar el arreglo. Esto ocurre porque `InsertarFinal()` recorre la cadena completa para encontrar el último nodo. Repetir este recorrido durante 1,000 inserciones produce un costo acumulado O(n²).

La conversión de la lista a arreglo tiene complejidad O(n), ya que recorre cada nodo una vez y copia su `RegistroDatos` al arreglo resultante.

## Complejidades

| Operación | Complejidad | Explicación |
|---|---:|---|
| `InsertarInicio()` | O(1) | Solo cambia la referencia de la cabeza |
| `InsertarFinal()` | O(n) | Recorre la cadena para localizar el último nodo |
| `BuscarPorId()` | O(n) | Puede necesitar revisar todos los nodos |
| `EliminarPorId()` | O(n) | Busca el nodo y reconecta las referencias |
| `ObtenerComoArreglo()` | O(n) | Copia una vez cada registro |
| QuickSort heredado | O(n log n) promedio | Ordena el arreglo resultante |

## Conclusión

El arreglo ofrece mejor localidad de memoria, menor sobrecarga y acceso directo por índice. La lista enlazada ofrece tamaño dinámico e inserción O(1) al inicio, pero cada nodo consume memoria adicional y el acceso es secuencial.

Ninguna estructura es mejor en todos los escenarios. La elección depende del patrón de acceso, la frecuencia de inserciones y eliminaciones, la necesidad de acceso por índice y la cantidad de memoria disponible.