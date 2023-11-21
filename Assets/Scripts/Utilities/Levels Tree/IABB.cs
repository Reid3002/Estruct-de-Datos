using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public interface IABB
{
    LevelNode Root();

    IABB LeftChild();
    IABB RightChild();

    bool IsTreeEmpty();

    void InitializeTree();

    void AddNode(int id, string levelName, int index, int level);

    void RemoveNode(int index);
}
