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

    public Transform[] path = new Transform[163];
    public Vector3 nextNode;    
    public int currentNodeId;

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
        if (graphManager.done && nextNode == null)
        {
            dijkstra.DijkstraProcess(graph, currentNodeId);
            path = TranslateIds(NarrowResults(dijkstra.nodos, Manager.playerPosition.id.ToString()));
            nextNode = path[0].position;
        }
        else if (nextNode == transform.position)
        {
            dijkstra.DijkstraProcess(graph, currentNodeId);
            path = TranslateIds(NarrowResults(dijkstra.nodos, Manager.playerPosition.id.ToString()));
            nextNode = path[0].position;
        }

        if (nextNode != null)
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

    private string[] NarrowResults(string[] Ids, string target)
    {
        string[] result;
        Queue<string> temp = new Queue<string>();

        foreach (string id in Ids)
        {
            if (id != target)
            {
                temp.Enqueue(id);
            }
            else if (id == target)
            {
                temp.Enqueue(id);
                break;
            }

        }
        result = temp.ToArray();
        return result;
    }

    private Transform[] TranslateIds(string[] Ids)
    {
        Transform[] result;
        Queue<Transform> temp = new Queue<Transform>();

        foreach (string id in Ids)
        {
            int intValue = int.Parse(id);
            temp.Enqueue(graph.GetTransformById(intValue));
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
