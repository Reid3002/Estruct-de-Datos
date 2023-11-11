using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraph
{
    void InitializeGraph();
    void AddVertex(int id, Transform transform);
    void RemoveVertex(int id);
    //ConjuntoTDA Vertices();
    void AddEdge(int id1, int id2, int peso);
    void RemoveEdge(int id1, int id2);
    bool ExistsEdge(int id1, int id2);
    int WeightEdge(int id1, int id2);
}
