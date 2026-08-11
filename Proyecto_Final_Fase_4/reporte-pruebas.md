# Reporte de pruebas de integración

## Proyecto Final - Fase 4

Este documento registra las pruebas ejecutadas sobre DataCore v4.0 para validar la integración de almacenamiento dinámico, ordenación, búsqueda indexada, menú interactivo y benchmark.

## Entorno de ejecución

- Lenguaje: C#
- Plataforma: .NET 10
- Framework de pruebas: xUnit
- Sistema de ejecución local: macOS
- Proyecto probado: `Proyecto_Final_Fase_4.Tests`

## Comando utilizado

```bash
dotnet test Proyecto_Final_Fase_4.Tests/Proyecto_Final_Fase_4.Tests.csproj
```

## Resultado general

```text
Total: 37
Correctas: 37
Errores: 0
Omitidas: 0
```

## Casos de búsqueda indexada

| Caso | Resultado esperado | Resultado obtenido | Estado |
|---|---|---|---|
| Construir índice con datos desordenados | IDs ordenados ascendentemente | IDs ordenados | Correcto |
| Construir índice con tabla vacía | Arreglo vacío | Arreglo vacío | Correcto |
| Construir índice con tabla nula | `ArgumentNullException` | Excepción controlada | Correcto |
| Buscar en arreglo vacío | No encontrado y 0 comparaciones | No encontrado y 0 comparaciones | Correcto |
| Buscar en arreglo con un elemento | Encontrado en 1 comparación | Encontrado en 1 comparación | Correcto |
| Buscar ID inexistente en un elemento | No encontrado en 1 comparación | No encontrado en 1 comparación | Correcto |
| Buscar primer elemento | Registro encontrado | Registro encontrado | Correcto |
| Buscar elemento central | Registro encontrado | Registro encontrado | Correcto |
| Buscar último elemento | Registro encontrado | Registro encontrado | Correcto |
| Buscar ID inexistente | No encontrado | No encontrado | Correcto |
| Buscar en arreglo nulo | `ArgumentNullException` | Excepción controlada | Correcto |
| Construir índice | No modificar la lista original | Lista original conservada | Correcto |

## Casos del gestor central

| Caso | Resultado esperado | Resultado obtenido | Estado |
|---|---|---|---|
| Insertar registro nuevo | Cantidad aumenta | Cantidad aumentó | Correcto |
| Insertar ID duplicado | Segundo registro rechazado | Registro rechazado | Correcto |
| Insertar ID no positivo | Excepción controlada | Excepción controlada | Correcto |
| Eliminar ID existente | Cantidad disminuye | Cantidad disminuyó | Correcto |
| Eliminar ID inexistente | Sin modificación | Sin modificación | Correcto |
| Buscar con índice desactualizado | Reconstrucción automática | Índice reconstruido | Correcto |
| Eliminar después de indexar | Índice se invalida | Índice invalidado | Correcto |
| Buscar linealmente | Comparaciones correctas | Comparaciones correctas | Correcto |
| Ordenar e indexar | IDs ascendentes | IDs ascendentes | Correcto |
| Calcular peso total | Suma correcta | Suma correcta | Correcto |
| Registrar historial | Operaciones conservadas | Operaciones conservadas | Correcto |
| Flujo completo | Sin pérdida ni corrupción | Flujo correcto | Correcto |

## Casos del Menú Maestro

| Caso | Resultado esperado | Resultado obtenido | Estado |
|---|---|---|---|
| Salida confirmada | Programa finaliza | Finalización correcta | Correcto |
| Entrada no numérica | Mensaje y recuperación | Mensaje y recuperación | Correcto |
| Opción fuera de rango | Solicitar otra opción | Nueva entrada solicitada | Correcto |
| Mostrar tabla vacía | Mensaje informativo | Mensaje mostrado | Correcto |
| Insertar ID duplicado | Rechazar duplicado | Duplicado rechazado | Correcto |
| Eliminar último registro | Tabla queda vacía | Tabla vacía | Correcto |
| Buscar ID existente | Mostrar registro | Registro mostrado | Correcto |
| Buscar ID inexistente | Mensaje informativo | Mensaje mostrado | Correcto |
| Cancelar salida | Regresar al menú | Menú mostrado nuevamente | Correcto |

## Casos del benchmark

| Caso | Resultado esperado | Resultado obtenido | Estado |
|---|---|---|---|
| Cantidad igual a cero | Excepción | Excepción controlada | Correcto |
| Cantidad negativa | Excepción | Excepción controlada | Correcto |
| Comparación con 1,024 registros | Resultados equivalentes | Resultados equivalentes | Correcto |
| Comparación con 1,000,000 | Lineal: 1,000,000 comparaciones | 1,000,000 comparaciones | Correcto |
| Búsqueda binaria con 1,000,000 | Máximo aproximado de 20 | 20 comparaciones | Correcto |
| Generación del reporte | Texto completo | Reporte completo | Correcto |

## Benchmark documentado

Ejecución realizada con 1,000,000 de registros:

```text
Búsqueda lineal:
Comparaciones: 1,000,000
Tiempo: 3.9127 ms

Búsqueda binaria:
Comparaciones: 20
Tiempo: 0.0012 ms

Resultados equivalentes: True
Reducción de comparaciones: 50,000.00x
```

Los tiempos pueden cambiar entre ejecuciones debido al sistema operativo, procesador, carga del equipo y optimizaciones del entorno de ejecución.

El número de comparaciones permanece como evidencia reproducible de la diferencia entre O(n) y O(log n).

## Prueba manual del Menú Maestro

Se ejecutaron más de 10 operaciones:

1. Entrada no numérica.
2. Opción fuera de rango.
3. Inserción del ID 10.
4. Inserción del ID 30.
5. Inserción del ID 20.
6. Intento de insertar nuevamente el ID 20.
7. Visualización de registros.
8. Ordenación e indexación.
9. Búsqueda lineal del ID 30.
10. Búsqueda binaria del ID 30.
11. Búsqueda binaria del ID 99.
12. Eliminación del ID 30.
13. Visualización posterior a la eliminación.
14. Consulta de estadísticas.
15. Consulta del historial.
16. Ejecución del benchmark.
17. Salida confirmada.

Todas las operaciones finalizaron sin excepciones no controladas.

## Conclusión

Las pruebas demuestran que los componentes de las cuatro fases interoperan correctamente.

La lista enlazada conserva los registros, QuickSort genera el índice ordenado y la búsqueda binaria reduce significativamente las comparaciones frente a la búsqueda lineal.

El Menú Maestro mantiene el estado de la aplicación, controla entradas inválidas y permite acceder a todas las operaciones principales sin modificar el código fuente.