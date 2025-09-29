// Stack.cs
using System;

public class Stack<T>
{
    private T[] _elementos;
    private int _conteo;
    private const int CAPACIDAD_INICIAL = 10;

    public Stack(int capacidad = CAPACIDAD_INICIAL)
    {
        _elementos = new T[capacidad];
        _conteo = 0; 
    }

    public int Count => _conteo;
    public void Push(T elemento)
    {
        if (_conteo == _elementos.Length)
        {
            
            throw new InvalidOperationException("El stack está llena.");
        }
        
        _elementos[_conteo] = elemento;
        _conteo++;
    }
    public T Pop()
    {
        if (_conteo == 0)
        {
            throw new InvalidOperationException("El stack está vacía.");
        }
        _conteo--;
        T elemento = _elementos[_conteo];
        _elementos[_conteo] = default(T); 
        return elemento;
    }
    public T Peek()
    {
        if (_conteo == 0)
        {
            throw new InvalidOperationException("El stack está vacío.");
        }
        return _elementos[_conteo - 1];
    }
}