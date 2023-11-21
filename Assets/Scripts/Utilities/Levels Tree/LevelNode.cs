using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelNode : ScriptableObject
{
    public int id;
    public string sceneName;
    public int index;
    public int level;


    public IABB leftChild;
    public IABB rightChild;

    public LevelNode(int cid, string clevelName, int cindex, int clevel)
    {
        id = cid;
        sceneName = clevelName;
        index = cindex;
        level = clevel;       

    }

}
