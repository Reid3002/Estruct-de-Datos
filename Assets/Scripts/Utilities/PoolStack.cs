using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolStack : MonoBehaviour, IStack<GameObject>
{
    private GameObject[] enemyPool;
    [SerializeField] int Size = 50;
    private int index;
    [SerializeField] float spawnTimeInSeconds = 10;
    private float timerCount = 0;
    [SerializeField] GameObject spawner;
    [SerializeField] EnemyQueue enemyQueueScript;
    // Start is called before the first frame update
    private void Awake()
    {
        StartStack(Size);
    }

    private void Start()
    {
        this.enemyQueueScript = GetComponent<EnemyQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsStackEmpty())
        {
            bool timerExpired = ReleaseTimer();

            if (timerExpired) 
            {
                this.enemyQueueScript.Enqueue(Unstack());// Lo saca del la pila del pool y lo mete en la cola de enemigos.
                timerCount = 0;
            }            
        }
        else 
        { 
            if(timerCount != 0) 
            {
                timerCount = 0;
            }
        }

    }

    public void StartStack(int size) 
    {
        enemyPool = new GameObject[size];
        index = 0;
    }

    public void Stack(GameObject item) 
    { 
        if(item != null)
        {
            enemyPool[index] = item;
            index++;
        }
        else
        {
            Debug.Log("Tried to add a null item to the Pool");
        }

    }

    public GameObject Unstack()
    {
        if (!IsStackEmpty())
        {
            GameObject temp = enemyPool[index];

            enemyPool[index] = null;

            index--;

            temp.transform.position = spawner.transform.position;

            return temp;
        }

        Debug.Log("Tried to remove an item from an empty pool");
        return null;

    }

    public bool IsStackEmpty()
    {
        if (enemyPool[0] == null)
        {
            return true;
        }
        else { return false; }

    }

    private bool ReleaseTimer()
    {
        if (timerCount < spawnTimeInSeconds)
        {
            timerCount += Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }
}
