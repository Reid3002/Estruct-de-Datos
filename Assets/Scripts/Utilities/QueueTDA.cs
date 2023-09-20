using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface QueueTDA
{
    abstract void InitializeQueue();
    abstract void AddToQueue(GameObject objectToAdd);
    abstract void RemoveFromQueue();
    abstract bool EmptyQueue();
    abstract int FirstInQueue();
}
