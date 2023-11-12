using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSetUp : MonoBehaviour
{
    public GridObject[] nodes;
    private GraphMA matrix;
    // Start is called before the first frame update
    private void Awake()
    {
        int id = 0;
        matrix = gameObject.GetComponent<GraphMA>();

        matrix.InitializeGraph();

        foreach(var node in nodes)
        {
            matrix.AddVertex(id, node.transform);
            node.id = id;
            id++;
        }


// CONECTING THE NODES--------------------------------------------------------------------

        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i].adjancentGrids.Length; j++)
            {
                if (nodes[i].adjancentGrids[j] != null && nodes[i].adjancentGrids[j].isNavigable)
                {
                    matrix.AddEdge(i, nodes[i].adjancentGrids[j].id, 1);
                }
                else if (nodes[i].adjancentGrids[j] != null && nodes[i].adjancentGrids[j].isNavigable == false)
                {
                    matrix.AddEdge(i, nodes[i].adjancentGrids[j].id, 100);
                }
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
