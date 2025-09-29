using System;
//Eduardo Gabriel Canul May
//Eduardo Huerta Bailon

public class ListaPersonalizada
{
    public Producto GetElemento(int index)
{
    if (index >= 0 && index < _conteo)
    {
        return _elementos[index];
    }
    throw new IndexOutOfRangeException("Índice fuera de los límites de la lista.");
}
    private Producto[] _elementos;
    private int _conteo;

    public ListaPersonalizada

(int capacidadInicial = 1000000)
    {
        _elementos = new Producto[capacidadInicial];
        _conteo = 0;
    }

    public int Count => _conteo;
    public void Add(Producto p)
    {
        if (_conteo == _elementos.Length)
        {
            Console.WriteLine("La lista está llena.");
            return;
        }
        _elementos[_conteo] = p;
        _conteo++;
    }
    public void Imprimir()
    {
        if (_conteo == 0)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        for (int i = 0; i < _conteo; i++)
        {
            Console.WriteLine(_elementos[i]);
        }
    }
    public Producto Find(int id)
    {
        for (int i = 0; i < _conteo; i++)
        {
            if (_elementos[i].Id == id)
                return _elementos[i];
        }
        return null;
    }
    public bool Remove(int id)
    {
        for (int i = 0; i < _conteo; i++)
        {
            if (_elementos[i].Id == id)
            {
                for (int j = i; j < _conteo - 1; j++)
                {
                    _elementos[j] = _elementos[j + 1];
                }
                _conteo--;
                return true;
            }
        }
        return false;
    }
    public void SortByName()
    {
        for (int i = 0; i < _conteo - 1; i++)
        {
            for (int j = i + 1; j < _conteo; j++)
            {
                if (string.Compare(_elementos[i].Nombre, _elementos[j].Nombre) > 0)
                {
                    var temp = _elementos[i];
                    _elementos[i] = _elementos[j];
                    _elementos[j] = temp;
                }
            }
        }
    }
}


