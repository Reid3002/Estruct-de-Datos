using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{
    [Header("Settings")]
    [Range(0.05f, 5)] public float newTileSize = 0.5f;
    [Range(0.005f, 0.5f)] public float stepAmount = 0.05f;
    [Range(0.0001f, 1)] public float stepTime = 0.5f;
    [Range(1, 10)] public int steps = 1;

    private float currentStepTime = 0;

    [SerializeField] GameObject gameManager;
    private GameManager Manager;
    [SerializeField] GraphMA graph;
    [SerializeField] GraphSetUp graphManager;
    private Dijkstra dijkstra;
    private bool start = false;

    public Transform[] path = new Transform[163];
    public Vector3 nextNode;    
    public int currentNodeId;
    int index = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        Manager = gameManager.GetComponent<GameManager>();
        dijkstra = gameObject.GetComponent<Dijkstra>();
        gameManager.GetComponent<EnemyQueue>().Enqueue(this.gameObject);
        this.currentStepTime = this.stepTime;        
        
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (graphManager.done && start == false)
        {
            dijkstra.DijkstraProcess(graph, currentNodeId);
            path = GetNodesPositions(StringToNumbers(NarrowResults(dijkstra.nodos, Manager.playerPosition.id)));
            nextNode = path[0].position;
            start = true;
        }
        else if (nextNode == transform.position)
        {
            dijkstra.DijkstraProcess(graph, currentNodeId);
            path = GetNodesPositions(StringToNumbers(NarrowResults(dijkstra.nodos, Manager.playerPosition.id)));
            nextNode = path[0].position;
        }

        if (nextNode != transform.position)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = (nextNode - transform.position).normalized;

            if (this.currentStepTime > 0)
                this.currentStepTime -= Time.deltaTime;
            else
            {
                // Make X steps
                for (int i = 0; i < steps; i++)
                {
                    // Enforce cardinal direction movement
                    Vector3 newPosition = this.transform.position + directionToPlayer * this.stepAmount;

                    if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                    {
                        // Horizontal movement is dominant
                        newPosition.y = this.transform.position.y;
                    }
                    else
                    {
                        // Vertical movement is dominant
                        newPosition.x = this.transform.position.x;
                    }

                    this.transform.position = newPosition;

                    this.transform.position = ForceRoundPosition(this.transform.position, stepAmount);
                    this.currentStepTime = this.stepTime;
                }
            }
        }        
    }

    private string NarrowResults(string[] Ids, int target)
    {
        return Ids[target];
    }

    private int[] StringToNumbers(string Ids)
    {
        string[] numbersInString = Ids.Split(' ');

        int[] numbers = new int[numbersInString.Length];

        for (int i = 0; i < numbersInString.Length; i++)
        {
            // Trim any leading or trailing whitespaces
            string trimmedString = numbersInString[i].Trim();

            // Parse the substring as an integer
            if (int.TryParse(trimmedString, out int parsedNumber))
            {               
                numbers[i] = parsedNumber;               
            }
            else
            {                
                Debug.LogWarning("Failed to parse: " + trimmedString);
            }
        }
        return numbers;
    }

    private Transform[] GetNodesPositions(int[] nodes)
    {
        Transform[] result;
        Queue<Transform> temp = new Queue<Transform>();

        for (int i = 0; i < nodes.Length; i++)
        {
            temp.Enqueue(graph.GetTransformById(nodes[i]));
        }
        result = temp.ToArray();
        return result;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().alive = false;
        }
        
    }
}
