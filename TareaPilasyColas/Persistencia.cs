using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
//Eduardo Gabriel Canul May
//Eduardo Huerta Bailon

public static class Persistencia
{
    private const string NOMBRE_ARCHIVO = "inventario.json";

    private class DatosInventario
    {
        public List<Producto> Inventario { get; set; }
        public List<string> Pedidos { get; set; }
        public List<Producto> MercanciaRecibida { get; set; }
    }

    public static void Guardar(ListaPersonalizada inventario, Queue<string> pedidos, Stack<Producto> mercanciaRecibida)
    {
        try
        {
            var datos = new DatosInventario
            {
                Inventario = ConvertirLista(inventario),
                Pedidos = ConvertirCola(pedidos),
                MercanciaRecibida = ConvertirPila(mercanciaRecibida)
            };
            
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(datos, opciones);
            File.WriteAllText(NOMBRE_ARCHIVO, json);
            Console.WriteLine($"Datos guardados en '{NOMBRE_ARCHIVO}' exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar los datos: {ex.Message}");
        }
    }

    public static (ListaPersonalizada, Queue<string>, Stack<Producto>) Cargar()
    {
        if (!File.Exists(NOMBRE_ARCHIVO))
        {
            Console.WriteLine("Archivo de datos no encontrado. Se iniciará con datos vacíos.");
            return (new ListaPersonalizada(), new Queue<string>(), new Stack<Producto>());
        }

        try
        {
            string json = File.ReadAllText(NOMBRE_ARCHIVO);
            var datos = JsonSerializer.Deserialize<DatosInventario>(json);
            Console.WriteLine($"Datos cargados desde '{NOMBRE_ARCHIVO}' exitosamente.");

            var inventario = new ListaPersonalizada();
            foreach (var p in datos.Inventario)
            {
                inventario.Add(p);
            }

            var pedidos = new Queue<string>();
            foreach (var p in datos.Pedidos)
            {
                pedidos.Enqueue(p);
            }

            var mercanciaRecibida = new Stack<Producto>();
            foreach (var p in datos.MercanciaRecibida)
            {
                mercanciaRecibida.Push(p);
            }

            return (inventario, pedidos, mercanciaRecibida);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar los datos: {ex.Message}");
            return (new ListaPersonalizada(), new Queue<string>(), new Stack<Producto>());
        }
    }
    private static List<Producto> ConvertirLista(ListaPersonalizada lista)
    {
        var productos = new List<Producto>();
        for (int i = 0; i < lista.Count; i++)
        {
            productos.Add(lista.GetElemento(i));
        }
        return productos;
    }

    private static List<string> ConvertirCola(Queue<string> cola)
    {
        var pedidosTemp = new List<string>();
        for (int i = 0; i < cola.Count; i++)
        {
        }
        var tempQueue = new Queue<string>();
        while (cola.Count > 0)
        {
            var item = cola.queue();
            pedidosTemp.Add(item);
            tempQueue.Enqueue(item);
        }
        while (tempQueue.Count > 0)
        {
            cola.Enqueue(tempQueue.queue());
        }
        return pedidosTemp;
    }

    private static List<Producto> ConvertirPila(Stack<Producto> pila)
    {
        var lotesTemp = new List<Producto>();
        var tempStack = new Stack<Producto>();
        while (pila.Count > 0)
        {
            var item = pila.Pop();
            lotesTemp.Add(item);
            tempStack.Push(item);
        }
        while (tempStack.Count > 0)
        {
            pila.Push(tempStack.Pop());
        }
        lotesTemp.Reverse();
        return lotesTemp;
    }
}