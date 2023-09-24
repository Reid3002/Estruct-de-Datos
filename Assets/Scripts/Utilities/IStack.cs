using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStack<T>
{
    void StartStack(int size);

    void Stack(T item);

    public T Unstack();

    bool IsStackEmpty();

}
