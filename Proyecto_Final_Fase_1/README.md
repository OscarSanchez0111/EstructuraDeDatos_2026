# Proyecto Final - Fase 1: DataCore

## Descripción

Esta fase implementa la base del motor DataCore mediante estructuras de datos inmutables y el algoritmo Selection Sort instrumentado.

El programa genera 40 registros aleatorios, los ordena por su Id y reporta el número de comparaciones, intercambios y tiempo de ejecución.

## Componentes

- `RegistroDatos`: struct inmutable con Id, HashValidacion y PesoBytes.
- `SelectionSort`: algoritmo de ordenación por selección.
- `MetricasOrdenacion`: reporte de comparaciones, intercambios y tiempo.
- `Program`: genera, muestra y ordena 40 registros.
- `Proyecto_Final_Fase_1.Tests`: contiene 13 pruebas unitarias con xUnit.

## Requisitos

- .NET 10.0 o superior.
- Visual Studio Code o Visual Studio.
- Git.

## Ejecutar el programa

Desde la raíz del repositorio:

```bash
dotnet run --project Proyecto_Final_Fase_1/Proyecto_Final_Fase_1.csproj