using System;

class Alumno
{
    public string Nombre { get; set; }
}

class Program
{
    static void Intercambiar(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static int CalcularYValidar(
        int dividendo,
        int divisor,
        out int residuo)
    {
        residuo = dividendo % divisor;
        return dividendo / divisor;
    }

    static void Main()
    {
        Console.WriteLine ();
        Console.WriteLine("\nModulo 1: El Modificador ref");

        int x = 10;
        int y = 25;

        Console.WriteLine($"\nAntes: x={x}, y={y}");

        Intercambiar(ref x, ref y);

        Console.WriteLine($"Después: x={x}, y={y}");

        Console.WriteLine();

        Console.WriteLine("Modulo 2: El Modificador out");

        int cociente =
            CalcularYValidar(17, 5, out int residuo);

        Console.WriteLine($"\nCociente: {cociente}");
        Console.WriteLine($"Residuo: {residuo}");

        Console.WriteLine();

        Console.WriteLine("Modulo 3: Referencias");

        Alumno alumno1 = new Alumno
        {
            Nombre = "Oscar"
        };

        Alumno alumno2 = alumno1;

        alumno2.Nombre = "Clon";

        Console.WriteLine($"\nAlumno 1: {alumno1.Nombre}");
        Console.WriteLine($"Alumno 2: {alumno2.Nombre}");
    }
}