using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABBNode<T> : MonoBehaviour
{
    public int index;
    public T info;
    
    public IABB leftChild;
    public IABB rightChild;
}
