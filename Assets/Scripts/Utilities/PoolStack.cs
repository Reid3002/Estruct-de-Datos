using UnityEngine;

public class PoolStack : MonoBehaviour, IStack<GameObject>
{
    private GameObject[] enemyPool;
    [SerializeField] int Size = 50;
    public int index;
    [SerializeField] float spawnTimeInSeconds = 10;
    private float timerCount = 0;
    [SerializeField] EnemyQueue enemyQueueScript;

    private GameManager gameManager;
    // Start is called before the first frame update
    private void Awake()
    {
        StartStack(Size);
    }

    private void Start()
    {
        this.gameManager = GetComponent<GameManager>();
        this.enemyQueueScript = GetComponent<EnemyQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsStackEmpty())
        {
            ReleaseTimer();
        }
    }

    public void StartStack(int size)
    {
        enemyPool = new GameObject[size];
        index = 0;
    }

    public void Stack(GameObject item)
    {
        if (item != null)
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
            GameObject temp = enemyPool[index - 1];

            enemyPool[index - 1] = null;

            index--;

            temp.transform.position = RespawnPoint();

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

    private void ReleaseTimer()
    {
        if (timerCount < spawnTimeInSeconds)
        {
            timerCount += Time.deltaTime;

        }
        else if (timerCount >= spawnTimeInSeconds)
        {
            this.enemyQueueScript.Enqueue(Unstack());// Lo saca del la pila del pool y lo mete en la cola de enemigos.
            timerCount = 0;
        }
    }

    private Vector3 RespawnPoint()
    {
        bool success = false;
        GridObject temp = null;
        while (success == false)
        {
            temp = gameManager.GetComponent<Grid>().gridObjects[Random.Range(0, gameManager.GetComponent<Grid>().gridObjects.Length)];
            if (temp != null && temp.isNavigable)
            {
                success = true;
            }
        }
        return temp.gameObject.transform.position;
       
    }
}