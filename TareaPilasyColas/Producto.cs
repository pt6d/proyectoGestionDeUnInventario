using System;
//Eduardo Gabriel Canul May
//Eduardo Huerta Bailon

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public Producto(int id, string nombre, decimal precio, int stock)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Nombre: {Nombre}, Precio: {Precio}, Stock: {Stock}";
    }
}