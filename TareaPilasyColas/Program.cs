using System;
using System.Text.Json;
//Eduardo Gabriel Canul May
//Eduardo Huerta Bailon


class Inicio
{
    static void Main()
    {
        ListaPersonalizada inventario = new ListaPersonalizada();
        Queue<string> pedidos = new Queue<string>();
        Stack<Producto> mercanciaRecibida = new Stack<Producto>();

        (inventario, pedidos, mercanciaRecibida) = Persistencia.Cargar();


        int opcion;

        do
        {
            Console.WriteLine("\n           MENÚ PRINCIPAL     ");
            Console.WriteLine("====== Gestión de Inventario ======");
            Console.WriteLine("1. Agregar un nuevo producto al inventario");
            Console.WriteLine("2. Ver todos los productos del inventario");
            Console.WriteLine("3. Buscar un producto por ID");
            Console.WriteLine("4. Actualizar stock de un producto");
            Console.WriteLine("5. Eliminar un producto");
            Console.WriteLine("6. Ordenar productos por nombre");
            Console.WriteLine("\n====== Gestión de Pedidos (Queue) ======");
            Console.WriteLine("7. Registrar un nuevo pedido");
            Console.WriteLine("8. Procesar el próximo pedido");
            Console.WriteLine("\n====== Gestión de Mercancía (Stack) ======");
            Console.WriteLine("9. Recibir mercancía");
            Console.WriteLine("10. Reabastecer inventario desde el stack");
            Console.WriteLine("\n=== Persistencia (JSON) ===");
            Console.WriteLine("11. Guardar en archivo JSON.");
            Console.WriteLine("12. Cargar desde archivo JSON.");
            Console.WriteLine("\n0. Salir");
            Console.Write("Elige una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = -1;
            }

            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("=== Agregar Nuevo Producto ===");
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine()!;
                    Console.Write("Precio: ");
                    decimal precio = decimal.Parse(Console.ReadLine()!);
                    Console.Write("Stock: ");
                    int stock = int.Parse(Console.ReadLine()!);
                    inventario.Add(new Producto(id, nombre, precio, stock));
                    Console.WriteLine("Producto agregado al inventario.");
                    break;

                case 2:
                    Console.WriteLine("=== Productos en el Inventario ===");
                    inventario.Imprimir();
                    break;

                case 3:
                    Console.WriteLine("=== Buscar Producto ===");
                    Console.Write("Ingrese el ID a buscar: ");
                    int idBuscar = int.Parse(Console.ReadLine()!);
                    var encontrado = inventario.Find(idBuscar);
                    if (encontrado != null)
                        Console.WriteLine($"Encontrado: {encontrado}");
                    else
                        Console.WriteLine("Producto no encontrado.");
                    break;

                case 4:
                    Console.WriteLine("=== Actualizar Stock ===");
                    Console.Write("Ingrese el ID del producto a actualizar: ");
                    int idStock = int.Parse(Console.ReadLine()!);
                    var productoStock = inventario.Find(idStock);
                    if (productoStock != null)
                    {
                        Console.Write("Nuevo stock: ");
                        int nuevoStock = int.Parse(Console.ReadLine()!);
                        productoStock.Stock = nuevoStock;
                        Console.WriteLine("Stock actualizado.");
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado.");
                    }
                    break;

                case 5: // Aca en el case Num 5 para poder eliminar un producto
                    Console.WriteLine("=== Eliminar Producto ===");
                    Console.Write("Ingrese el ID a eliminar: ");
                    int idEliminar = int.Parse(Console.ReadLine()!);
                    if (inventario.Remove(idEliminar))
                        Console.WriteLine("Producto eliminado.");
                    else
                        Console.WriteLine("Producto no encontrado.");
                    break;

                case 6:
                    Console.WriteLine("=== Ordenar Productos ===");
                    inventario.SortByName();
                    Console.WriteLine("Inventario ordenado por nombre.");
                    inventario.Imprimir();
                    break;

                case 7:
                    Console.WriteLine("=== Registrar Nuevo Pedido ===");
                    Console.Write("Ingrese el nombre del producto para el pedido: ");
                    string productoPedido = Console.ReadLine()!;
                    pedidos.Enqueue(productoPedido);
                    Console.WriteLine($"Pedido para '{productoPedido}' registrado. Hay {pedidos.Count} pedidos en espera.");
                    if (pedidos.Count > 0)
                    {
                        Console.WriteLine($"Próximo pedido a procesar: {pedidos.Peek()}");
                    }
                    break;

                case 8: // Procesar el próximo pedido (este es el queue)
                    Console.WriteLine("=== Procesar Pedido ===");
                    if (pedidos.Count > 0)
                    {
                        string pedidoProcesado = pedidos.queue();
                        Console.WriteLine($"Se ha procesado y despachado el pedido de: '{pedidoProcesado}'.");
                        Console.WriteLine($"Quedan {pedidos.Count} pedidos pendientes.");
                    }
                    else
                    {
                        Console.WriteLine("No hay pedidos pendientes para procesar.");
                    }
                    break;

                case 9: // Recibir mercancía (Stack)
                    Console.WriteLine("=== Recibir Mercancía (Apilar) ===");
                    Console.Write("ID del producto recibido: ");
                    int idMercancia = int.Parse(Console.ReadLine()!);
                    Console.Write("Nombre del producto: ");
                    string nombreMercancia = Console.ReadLine()!;
                    Console.Write("Cantidad recibida (stock): ");
                    int stockMercancia = int.Parse(Console.ReadLine()!);

                    Producto nuevoLote = new Producto(idMercancia, nombreMercancia, 0, stockMercancia);
                    mercanciaRecibida.Push(nuevoLote);

                    Console.WriteLine($"Se ha recibido y apilado un lote de '{nombreMercancia}' con {stockMercancia} unidades.");
                    Console.WriteLine($"Hay {mercanciaRecibida.Count} lotes en la pila de recepción.");
                    if (mercanciaRecibida.Count > 0)
                    {
                        Console.WriteLine($"El último lote en la cima de la pila es: {mercanciaRecibida.Peek().Nombre}");
                    }
                    break;

                case 10: // Reabastecer inventario (Stack)
                    Console.WriteLine("=== Reabastecer Inventario ===");
                    if (mercanciaRecibida.Count > 0)
                    {
                        Producto loteParaReabastecer = mercanciaRecibida.Pop();
                        Producto productoEnInventario = inventario.Find(loteParaReabastecer.Id);

                        if (productoEnInventario != null)
                        {
                            productoEnInventario.Stock += loteParaReabastecer.Stock;
                            Console.WriteLine($"Inventario reabastecido: Se añadieron {loteParaReabastecer.Stock} unidades al producto '{productoEnInventario.Nombre}'.");
                            Console.WriteLine($"Nuevo stock de '{productoEnInventario.Nombre}': {productoEnInventario.Stock}.");
                        }
                        else
                        {
                            Console.WriteLine($"El producto con ID {loteParaReabastecer.Id} no existe en el inventario. Agréguelo primero.");
                            mercanciaRecibida.Push(loteParaReabastecer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay mercancía en la pila de recepción para reabastecer.");
                    }
                    break;
                case 11:
                    Persistencia.Guardar(inventario, pedidos, mercanciaRecibida);
                    break;

                case 12:
                    Console.WriteLine("Los datos se cargan automáticamente al iniciar el programa. Reinicie la aplicación para cargar los últimos datos guardados.");
                    break;

                case 0: // Salir
                    Console.WriteLine("Saliendo del programa...");
                    break;

                    Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                    break;
            }
        } while (opcion != 0);
    }
}