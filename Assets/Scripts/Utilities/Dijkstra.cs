using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    public static int[] distance;
    public Transform[] nodos;

    private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    public void DijkstraProcess(GraphMA grafo, int source)
    {
        int[,] graph = GraphMA.MAdy;
        int verticesCount = GraphMA.totalNodes;
        source = grafo.Vertex2Index(source);

        distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];

        Transform[] nodos1 = new Transform[verticesCount];  
        Transform[] nodos2 = new Transform[verticesCount];

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;

            nodos1[i] = nodos2[i] = null; 
        }

        distance[source] = 0;
        nodos1[source] = nodos2[source] = grafo.GetTransformById(grafo.IDs[source]);

        for (int count = 0; count < verticesCount - 1; ++count)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                {
                    distance[v] = distance[u] + graph[u, v];
                    nodos1[v] = grafo.GetTransformById(grafo.IDs[u]);
                    nodos2[v] = grafo.GetTransformById(grafo.IDs[v]);
                }
            }
        }

        nodos = new Transform[verticesCount];
        Transform nodOrig = grafo.GetTransformById(grafo.IDs[source]);
        for (int i = 0; i < verticesCount; i++)
        {
            if (nodos1[i] != null)
            {
                List<Transform> l1 = new List<Transform>();
                l1.Add(nodos1[i]);
                l1.Add(nodos2[i]);
                while (l1[0] != nodOrig)
                {
                    for (int j = 0; j < verticesCount; j++)
                    {
                        if (j != source && l1[0] == nodos2[j])
                        {
                            l1.Insert(0, nodos1[j]);
                            break;
                        }
                    }
                }
                for (int j = 0; j < l1.Count; j++)
                {
                    if (j == 0)
                    {
                        nodos[i] = l1[j];
                    }                    
                }
            }
        }
    }
}
