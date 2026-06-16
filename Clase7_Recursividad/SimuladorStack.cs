using System;

public class SimuladorStack
{
    public static void ImprimirCuentaRegresiva(int numero)
    {    
        // Cuando el número es menor que 1, la recursividad se detiene
        if (numero < 1)
        {
            Console.WriteLine();
            Console.WriteLine("Caso base alcanzado. No se apilan más llamadas.");

            Console.WriteLine(); 
            Console.WriteLine("¡Despegue! 🚀");
            Console.WriteLine(); 
            return;
        }
     
        // Aquí se crea un nuevo marco de activación en el Call Stack.
        Console.WriteLine($"[APILANDO] Llamada Recursiva = {numero}");
        
        // La función se llama a sí misma con un problema más pequeño
        ImprimirCuentaRegresiva(numero - 1);

        // Este mensaje aparece cuando el marco se libera del Stack
        Console.WriteLine($"[LIBERANDO] Llamada Recursiva = {numero}");
    }
    public static int SumarHasta(int n)
    {
        // La suma desde 1 hasta 1 es 1
        if (n == 1)
        {
            return 1;
        }
        // n se suma con el resultado de SumarHasta(n - 1).
        return n + SumarHasta(n - 1);
    }
}