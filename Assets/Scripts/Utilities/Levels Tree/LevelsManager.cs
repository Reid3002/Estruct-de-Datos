using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelsManager : MonoBehaviour
{
    private LevelsABB tree;
    public bool active = false;
    public int currentLevel = 1;
    public LevelNode[] GameLevels;
    private string nextLevel = "null";
    public float score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        tree = new LevelsABB();
        tree.InitializeTree();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameLevels != null)
        {
            for (int i = 0; i < GameLevels.Length; i++)
            {
                tree.AddNode(GameLevels[i].id,GameLevels[i].sceneName, GameLevels[i].index, GameLevels[i].level);
                LevelNode root = tree.Root();
                Debug.Log("Level loaded");
            }
        }
        else { Debug.Log("No levels to load into the tree"); }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            NextLevelCalc();
            if (nextLevel != "null")
            {
                SceneManager.LoadScene(nextLevel);
                currentLevel++;
                active = false;
            }
            else
            {
                GameObject.Find("Player").GetComponent<PlayerController>().alive = false;
            }
        }
    }

    private LevelNode[] LevelOrder( LevelNode node)
    {
        Debug.Log(node);
        int level = 0;
        int i = 0;
        Queue<LevelNode> availableLevels = new Queue<LevelNode>();
        Queue<LevelNode> temp = new Queue<LevelNode>();
        LevelNode[] elegibleLevels;

        if (node != null)
        {
            availableLevels.Enqueue(node);
        }       

        while (availableLevels.Count > 0 /*&& level != (2^(currentLevel-1)-1)*/)
        {             
            node = availableLevels.Dequeue();

            if (node.level == currentLevel+1)
            {
                temp.Enqueue(node);
                i++;
            }           

            if (node.leftChild.Root() != null)
            {
                availableLevels.Enqueue(node.leftChild.Root());
            }

            if (node.rightChild.Root() != null)
            {
                availableLevels.Enqueue(node.rightChild.Root());
            }

            level++;
        }
        if (availableLevels.Count > 0 && level == (Mathf.Pow(2, currentLevel - 1) - 1))
        {
            while (availableLevels.Count > 0)
            {
                node = availableLevels.Dequeue();

                if (node.level > currentLevel)
                {
                    temp.Enqueue(node);
                    i++;
                }
            }
        }

        return elegibleLevels=temp.ToArray();
        
    }

    private void NextLevelCalc()
    {
        //LevelNode temp;
        Debug.Log("Geting Availablelevels");        
        LevelNode[] Availablelevels = LevelOrder(tree.Root());
        Debug.Log("Availablelevels adquiered");
        int targetScore=0;               

        for (int i = 0; i < Availablelevels.Length; i++)
        {
            if (score > Availablelevels[i].index && targetScore < Availablelevels[i].index)
            {
                targetScore = Availablelevels[i].index;
            }
            else if(score == Availablelevels[i].index && targetScore < Availablelevels[i].index)
            {
                targetScore = Availablelevels[i].index;
            }
        }

        foreach(LevelNode node in Availablelevels)
        {
            if (node.index == targetScore)
            {
                nextLevel = node.sceneName;
                break;
            }
        }
        
    }
    
}
