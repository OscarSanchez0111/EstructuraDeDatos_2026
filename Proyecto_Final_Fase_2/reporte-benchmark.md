# Reporte comparativo de ordenamiento

## Configuración del experimento

- Registros procesados: 10,000
- Semilla aleatoria: 42
- Campo de ordenamiento: `Id`
- Algoritmos comparados: Selection Sort y QuickSort
- Estrategia de QuickSort: partición Lomuto con pivote seleccionado mediante mediana de tres
- Plataforma: .NET 10

Para garantizar una comparación justa, ambos algoritmos recibieron una copia del mismo arreglo original.

## Resultados

| Métrica | Selection Sort | QuickSort |
|---|---:|---:|
| Tamaño de entrada | 10,000 | 10,000 |
| Comparaciones | 49,995,000 | 152,046 |
| Intercambios | 9,994 | 75,432 |
| Llamadas recursivas | No aplica | 11,411 |
| Profundidad máxima | No aplica | 23 |
| Tiempo | 189.6810 ms | 1.9818 ms |
| Ordenación correcta | Sí | Sí |

## Interpretación

QuickSort fue aproximadamente 95.71 veces más rápido que Selection Sort durante esta ejecución.

Selection Sort realizó 49,995,000 comparaciones debido a su complejidad temporal O(n²). QuickSort realizó 152,046 comparaciones gracias a su comportamiento promedio O(n log n).

QuickSort efectuó más intercambios que Selection Sort, pero redujo considerablemente el número total de comparaciones y el tiempo de ejecución.

La profundidad máxima de 23 llamadas demuestra que la selección del pivote mediante mediana de tres produjo particiones razonablemente equilibradas para esta entrada.

## Verificación

Los dos resultados fueron validados automáticamente:

- Selection Sort ordenó correctamente.
- QuickSort ordenó correctamente.
- Ambos algoritmos produjeron exactamente el mismo orden final.

Los tiempos pueden variar dependiendo del equipo y de los procesos activos, pero la diferencia de complejidad y rendimiento se mantiene.