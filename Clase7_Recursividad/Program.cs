using System;

Console.WriteLine("Simulador del Call Stack");
Console.WriteLine();

Console.WriteLine("Demostración del comportamiento LIFO del Call Stack");
Console.WriteLine();

Console.WriteLine("Ejercicio A: Cuenta Regresiva");
SimuladorStack.ImprimirCuentaRegresiva(3);

Console.WriteLine();
Console.WriteLine("Ejercicio B: Sumatoria Recursiva");

int numero = Validador.SolicitarNumeroPositivo();

if (numero != -1)
{
    int resultado = SimuladorStack.SumarHasta(numero);
    Console.WriteLine($"La suma de 1 hasta {numero} es: {resultado}");
}