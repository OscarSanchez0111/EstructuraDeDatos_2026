# Clase 10 – Tipos de Datos Personalizados con Structs

Objetivo

Comprender el uso de tipos de datos personalizados mediante structs en C# moderno, analizando su comportamiento en memoria y la diferencia entre tipos por valor y tipos por referencia.

Descripción del proyecto

Se desarrolló un sistema básico de telemetría GPS utilizando un struct inmutable llamado `CoordenadaGPS`. Este struct almacena una latitud y una longitud, valida que los valores se encuentren dentro de los rangos geográficos permitidos y permite mostrar la ubicación en consola.

Además, se implementó un experimento para demostrar que los structs se copian por valor, generando instancias independientes en memoria.

Conceptos aplicados

* Structs (`readonly struct`)
* Propiedades de solo lectura
* Constructores personalizados
* Validación de datos
* Manejo de excepciones
* Stack vs Heap
* Copia por valor
* Git Flow

Resultado obtenido

El programa solicita coordenadas al usuario, valida los datos ingresados y muestra la ubicación. También demuestra que al copiar un struct se crea una nueva instancia independiente, confirmando el comportamiento de los tipos por valor en C#.
