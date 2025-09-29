using System;
//Eduardo Gabriel Canul May
//Eduardo Huerta Bailon

public class Queue<T>
{
    private T[] _elementos;
    private int _conteo;
    private int _cabeza;
    private int _Queue; 

    public Queue(int capacidad = 10)
    {
        _elementos = new T[capacidad];
        _conteo = 0;
        _cabeza = 0;
        _Queue = 0;
    }

    public int Count => _conteo;
    public void Enqueue(T elemento)
    {
        if (_conteo == _elementos.Length)
        {
            throw new InvalidOperationException("El queue está lleno.");
        }
        _elementos[_Queue] = elemento;
        _Queue = (_Queue + 1) % _elementos.Length;
        _conteo++;
    }
    public T queue()
    {
        if (_conteo == 0)
        {
            throw new InvalidOperationException("El queue está vacía.");
        }
        T elemento = _elementos[_cabeza];
        _elementos[_cabeza] = default(T); 
        _cabeza = (_cabeza + 1) % _elementos.Length;
        _conteo--;
        return elemento;
    }
    public T Peek()
    {
        if (_conteo == 0)
        {
            throw new InvalidOperationException("El queue está vacío.");
        }
        return _elementos[_cabeza];
    }
}