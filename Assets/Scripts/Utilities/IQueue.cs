using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueue<T> 
{
    void StartQueue(int size);

    void Enqueue(T item);

    public T Dequeue();

    bool IsQueueEmpty();

}
