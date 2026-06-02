using System;

class Program
{
    static void Main()
    {
        int lados = SeleccionarPoligono();

        double[] datos = PedirDatos();

        double area = CalcularArea(lados, datos[0], datos[1]);

        Console.WriteLine("\nEl área del polígono es: " + area);
    }

    static int SeleccionarPoligono()
    {
        Console.WriteLine("Seleccione un polígono:");
        Console.WriteLine("1. Pentágono");
        Console.WriteLine("2. Hexágono");
        Console.WriteLine("3. Heptágono");

        int opcion = Convert.ToInt32(Console.ReadLine());

        switch (opcion)
        {
            case 1:
                return 5;

            case 2:
                return 6;

            case 3:
                return 7;

            default:
                Console.WriteLine("Opción inválida.");
                return 0;
        }
    }

    static double[] PedirDatos()
    {
        Console.Write("\nIngrese la medida del lado: ");
        double lado = Convert.ToDouble(Console.ReadLine());

        Console.Write("Ingrese la apotema: ");
        double apotema = Convert.ToDouble(Console.ReadLine());

        return new double[] { lado, apotema };
    }

    static double CalcularArea(int lados, double lado, double apotema)
    {
        double perimetro = lados * lado;

        double area = (perimetro * apotema) / 2;

        return area;
    }
}