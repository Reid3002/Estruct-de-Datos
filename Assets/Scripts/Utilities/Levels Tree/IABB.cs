using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IABB
{
    LevelNode Root();

    IABB LeftChild();
    IABB RightChild();

    bool IsTreeEmpty();

    void InitializeTree();

    void AddNode(Scene node, int index);

    void RemoveNode(int index);
}
