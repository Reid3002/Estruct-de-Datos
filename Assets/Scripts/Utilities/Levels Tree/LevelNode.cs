using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNode : ABBNode<Scene>
{
    new public int index;
    public Scene scene;
    public int level;    


    new public IABB leftChild;
    new public IABB rightChild;
}
