Reflexión – Clase 10

En esta práctica aprendí la diferencia entre un struct y una class en C#. Entendí que los structs son tipos por valor y que al copiarlos se crea una nueva instancia independiente en memoria, mientras que las clases funcionan mediante referencias.

También comprendí la importancia de validar información antes de utilizarla, ya que un sistema profesional no debe aceptar datos incorrectos. Mediante el uso de `ArgumentOutOfRangeException` pude controlar coordenadas GPS inválidas y evitar errores en la ejecución.

Lo que más me llamó la atención fue observar cómo dos variables que contienen un struct pueden tener valores distintos aun cuando una fue creada a partir de la otra, demostrando claramente la copia por valor y el comportamiento del Stack.

Considero que este conocimiento será útil para seleccionar correctamente entre structs y clases dependiendo de los requerimientos de rendimiento y diseño de futuras aplicaciones.
