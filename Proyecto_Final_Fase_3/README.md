# Proyecto Final — Fase 3

## Lista simplemente enlazada en memoria Heap

Esta fase amplía DataCore mediante una lista simplemente enlazada capaz de insertar, buscar y eliminar registros dinámicamente.

La lista se integra con QuickSort de la Fase 2 y reutiliza el modelo `RegistroDatos` de la Fase 1 sin modificar los proyectos anteriores.

## Arquitectura

```text
Fase 3 → Fase 2 → Fase 1

TablaDinamica
    ↓
NodoRegistro
    ↓
RegistroDatos
    ↓
ObtenerComoArreglo()
    ↓
MotorQuickSort