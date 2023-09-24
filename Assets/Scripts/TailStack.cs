using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailStack : MonoBehaviour, IStack<SnakeController>
{
    private SnakeController[] tailStack;
    [SerializeField] int Size = 50;
    public int index;

    private void Awake()
    {
        StartStack(Size);
    }

    public void StartStack(int size)
    {
        tailStack = new SnakeController[size];
        index = 0;
    }

    public void Stack(SnakeController item)
    {
        if (item != null)
        {
            tailStack[index] = item;
            index++;
        }
        else
        {
            Debug.Log("Tried to add a null item to the Pool");
        }

    }

    public SnakeController Unstack()
    {
        if (!IsStackEmpty())
        {
            SnakeController temp = tailStack[index];

            tailStack[index] = null;

            if (index > 0)
            {
                index--;
            }

            return temp;
        }

        Debug.Log("Tried to remove an item from an empty pool");
        return null;

    }

    public bool IsStackEmpty()
    {
        if (tailStack[0] == null)
        {
            return true;
        }
        else { return false; }

    }
    
}


