public class Nodo
{
    public int ID { get; set; }
    public string Dato { get; set; } = string.Empty;

    public Nodo? HijoIzquierdo { get; set; }
    public Nodo? HijoDerecho { get; set; }

    public Nodo(int id, string dato)
    {
        ID = id;
        Dato = dato;
    }
}

public class Program
{
    public static Nodo InsertarNodo(Nodo? raiz, Nodo nuevoNodo)
    {
        if (raiz == null)
            return nuevoNodo;

        if (nuevoNodo.ID < raiz.ID)
        {
            raiz.HijoIzquierdo = InsertarNodo(raiz.HijoIzquierdo, nuevoNodo);
        }
        else if (nuevoNodo.ID > raiz.ID)
        {
            raiz.HijoDerecho = InsertarNodo(raiz.HijoDerecho, nuevoNodo);
        }

        return raiz;
    }

    public static string? BuscarNodo(Nodo? raiz, int idTarget)
    {
        if (raiz == null)
            return null;

        if (idTarget == raiz.ID)
            return raiz.Dato;

        if (idTarget < raiz.ID)
            return BuscarNodo(raiz.HijoIzquierdo, idTarget);
        else
            return BuscarNodo(raiz.HijoDerecho, idTarget);
    }

    public static void MostrarEnOrden(Nodo? raiz)
    {
        if (raiz == null)
            return;

        MostrarEnOrden(raiz.HijoIzquierdo);
        Console.WriteLine($"ID: {raiz.ID} - Dato: {raiz.Dato}");
        MostrarEnOrden(raiz.HijoDerecho);
    }

    public static void Main()
    {
        Console.WriteLine("Árbol Binario de Búsqueda \n");

        Nodo? raiz = null;

        raiz = InsertarNodo(raiz, new Nodo(10, "Raíz"));
        raiz = InsertarNodo(raiz, new Nodo(5, "Nodo izquierdo"));
        raiz = InsertarNodo(raiz, new Nodo(15, "Nodo derecho"));
        raiz = InsertarNodo(raiz, new Nodo(3, "Hoja izquierda"));
        raiz = InsertarNodo(raiz, new Nodo(7, "Hoja derecha del 5"));
        raiz = InsertarNodo(raiz, new Nodo(12, "Hoja izquierda del 15"));
        raiz = InsertarNodo(raiz, new Nodo(20, "Hoja derecha del 15"));

        Console.WriteLine("Recorrido en orden del árbol:");
        MostrarEnOrden(raiz);

        Console.WriteLine("\nBúsqueda de nodos:");

        string? resultado1 = BuscarNodo(raiz, 7);
        Console.WriteLine(resultado1 != null
            ? $"ID 7 encontrado: {resultado1}"
            : "ID 7 no encontrado");

        string? resultado2 = BuscarNodo(raiz, 99);
        Console.WriteLine(resultado2 != null
            ? $"ID 99 encontrado: {resultado2}"
            : "ID 99 no encontrado");

        Console.WriteLine("\nBig O:");
        Console.WriteLine("En un árbol balanceado, la búsqueda promedio es O(log n).");
        Console.WriteLine("Si el árbol se desbalancea, puede convertirse en O(n).");
    }
}