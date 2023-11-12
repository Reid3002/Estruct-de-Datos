using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMA : MonoBehaviour, IGraph
{
    static int n = 100;
    public int[,] MAdy;
    public int[] IDs;
    public Transform[] positions;
    public int totalNodes;

    public void InitializeGraph()
    {
        MAdy = new int[n, n];
        IDs = new int[n];
        positions = new Transform[n];
        totalNodes = 0;
    }

    public void AddVertex(int id, Transform transform)
    {
        IDs[totalNodes] = id;
        positions[totalNodes] = transform;
        for (int i = 0; i <= totalNodes; i++)
        {
            MAdy[totalNodes, i] = 0;
            MAdy[i, totalNodes] = 0;
        }
        totalNodes++;
    }

    public void RemoveVertex(int id)
    {
        int ind = Vertex2Index(id);

        for (int k = 0; k < totalNodes; k++)
        {
            MAdy[k, ind] = MAdy[k, totalNodes - 1];
        }

        for (int k = 0; k < totalNodes; k++)
        {
            MAdy[ind, k] = MAdy[totalNodes - 1, k];
        }

        IDs[ind] = IDs[totalNodes - 1];
        totalNodes--;
    }

    private int Vertex2Index(int id)
    {
        int i = totalNodes - 1;
        while (i >= 0 && IDs[i] != id)
        {
            i--;
        }

        return i;
    }

    //public ConjuntoTDA Vertices()
    //{
    //    ConjuntoTDA Vert = new ConjuntoLD();
    //    Vert.InicializarConjunto();
    //    for (int i = 0; i < totalNodes; i++)
    //    {
    //        Vert.Agregar(IDs[i]);
    //    }
    //    return Vert;
    //}

    public void AddEdge(int id1, int id2, int weight)
    {
        int o = Vertex2Index(id1);
        int d = Vertex2Index(id2);
        MAdy[o, d] = weight;
    }

    public void RemoveEdge(int id1, int id2)
    {
        int o = Vertex2Index(id1);
        int d = Vertex2Index(id2);
        MAdy[o, d] = 0;
    }

    public bool ExistsEdge(int id1, int id2)
    {
        int o = Vertex2Index(id1);
        int d = Vertex2Index(id2);
        return MAdy[o, d] != 0;
    }

    public int WeightEdge(int id1, int id2)
    {
        int o = Vertex2Index(id1);
        int d = Vertex2Index(id2);
        return MAdy[o, d];
    }
}

