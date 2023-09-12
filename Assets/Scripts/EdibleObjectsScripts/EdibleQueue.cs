using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EdibleController;

public class EdibleQueue : MonoBehaviour
{
    public int nextEdible;
    public int queueIndex = 4;
    public Queue<int> queueEdiblesIDs = new Queue<int>();

    [Header("Queue items & positions")]
    public List<EdibleController> allEdibles;
    public List<EdibleController> currentEdiblesInQueue;
    public List<GameObject> positionsReferences = new List<GameObject>();

    [Header("Settings & References")]
    private GameManager gameManager;
    public int maxNextQueueItems = 5;
    public int edibleTypesAmount;

    private void Awake()
    {
        this.edibleTypesAmount = System.Enum.GetNames(typeof(EdibleType)).Length;
    }

    private void Start()
    {
        this.gameManager = GetComponent<GameManager>();
    }

    public void InitializeQueue()
    {
        for (int i = 0; i < maxNextQueueItems; i++)
        {
            this.nextEdible = Random.Range(0, this.edibleTypesAmount);
            AddRandomEdible();
        }

        RefreshItemPositionsFromQueue();
    }

    public void NextEdible()
    {
        if (this.queueEdiblesIDs.Count == 0)
            InitializeQueue();

        // Remove edible
        this.queueIndex = this.queueEdiblesIDs.Dequeue();

        this.currentEdiblesInQueue[0].Dequeue();
        this.currentEdiblesInQueue.RemoveAt(0);

        // Add new edible
        this.nextEdible = Random.Range(0, this.edibleTypesAmount);
        AddRandomEdible();
        RefreshItemPositionsFromQueue();
    }

    void AddRandomEdible()
    {
        string name = "n/a";
        EdibleController edible = null;
        EdibleType eType = (EdibleType)this.nextEdible;

        switch (eType)
        {
            case EdibleType.AddTail:
                name = "AddTail";
                edible = Instantiate(allEdibles[0], new Vector2(100,100), new Quaternion());
                break;
            case EdibleType.RemoveTail:
                name = "RemoveTail";
                edible = Instantiate(allEdibles[1], new Vector2(100, 100), new Quaternion());
                break;
            case EdibleType.SlowPlayer:
                name = "SlowPlayer";
                edible = Instantiate(allEdibles[4], new Vector2(100, 100), new Quaternion());
                break;
            case EdibleType.PlayerReverse:
                name = "PlayerReverse";
                edible = Instantiate(allEdibles[2], new Vector2(100, 100), new Quaternion());
                break;
            case EdibleType.StunEnemies:
                name = "StunEnemies";
                edible = Instantiate(allEdibles[3], new Vector2(100, 100), new Quaternion());
                break;
        }

        // If object is valid, then listen for event and add to queue
        if (edible != null)
        {
            edible.OnEatedEvent += this.gameManager.OnEdibleEated;
            this.currentEdiblesInQueue.Add(edible);
            this.queueEdiblesIDs.Enqueue(this.nextEdible);

            print("Adding edible '" + name + "'.");
        }
        else
            print("Edible is null, not doing anything...");

    }

    void RefreshItemPositionsFromQueue()
    {
        for (int i = 0; i < currentEdiblesInQueue.Count; i++)
        {
            currentEdiblesInQueue[i].transform.localScale = new Vector2(1, 1);
            currentEdiblesInQueue[i].transform.position = positionsReferences[i].transform.position;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            NextEdible();

        if (Input.GetKeyDown(KeyCode.V))
            InitializeQueue();

        if (Input.GetKeyDown(KeyCode.N))
            NextEdible();
    }
}
