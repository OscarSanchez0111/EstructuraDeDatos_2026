using System;
using System.Collections.Generic;
using System.Linq;

public class Producto
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int Cantidad { get; set; }

    public Producto(int id, string nombre, double precio, int cantidad)
    {
        ID = id;
        Nombre = nombre;
        Precio = precio;
        Cantidad = cantidad;
    }

    public override string ToString()
    {
        return $"[{ID}] {Nombre} - ${Precio:F2} | Stock: {Cantidad}";
    }
}

class Program
{
    static void Main()
    {
        List<Producto> inventario = new List<Producto>
        {
            new Producto(1, "Laptop Lenovo", 15999.00, 10),
            new Producto(2, "Mouse Inalámbrico", 349.00, 25),
            new Producto(3, "Teclado Mecánico", 899.00, 0),
            new Producto(4, "Monitor 24\"", 4500.00, 5),
            new Producto(5, "Audífonos Sony", 1200.00, 0)
        };

        inventario.Add(new Producto(6, "Webcam HD", 750.00, 12));

        var otroProducto = new Producto(7, "Hub USB-C", 450.00, 8);
        inventario.Add(otroProducto);

        Console.WriteLine($"Total en inventario: {inventario.Count} productos\n");

        var porPrecio = inventario
            .OrderByDescending(p => p.Precio)
            .ToList();

        Console.WriteLine(" Productos por Precio:");
        foreach (var producto in porPrecio)
        {
            Console.WriteLine(producto);
        }

        var agotados = inventario
            .Where(p => p.Cantidad == 0)
            .ToList();

        Console.WriteLine("\n Productos Agotados:");
        if (agotados.Count == 0)
        {
            Console.WriteLine("Sin productos agotados.");
        }
        else
        {
            agotados.ForEach(p => Console.WriteLine(p));
        }

        Dictionary<int, Producto> catalogo = inventario
            .ToDictionary(p => p.ID, p => p);

        Console.WriteLine("\n Búsqueda por ID:");
        BuscarPorID(catalogo);
    }

    static void BuscarPorID(Dictionary<int, Producto> catalogo)
    {
        Console.Write("Ingresa el ID del producto a buscar: ");

        if (int.TryParse(Console.ReadLine(), out int idBuscado))
        {
            if (catalogo.TryGetValue(idBuscado, out Producto? encontrado))
            {
                Console.WriteLine($"Producto encontrado: {encontrado}");
            }
            else
            {
                Console.WriteLine("No existe un producto con ese ID.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido. Debes escribir un número.");
        }
    }
}