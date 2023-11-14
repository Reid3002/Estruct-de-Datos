using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public bool active = false;
    private int currentLevel = 1;
    private Scene nextLevel;
    public int score;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            NextLevelCalc();
            SceneManager.LoadScene(nextLevel.name);
            active = false;
        }
    }

    private LevelNode[] LevelOrder( LevelNode node)
    {
        int level = 0;
        int i = 0;
        Queue<LevelNode> availableLevels = new Queue<LevelNode>();
        LevelNode[] elegibleLevels = new LevelNode[100];

        availableLevels.Enqueue(node);

        while (availableLevels.Count > 0 && level != (2^(currentLevel-1)-1))
        {             
            node = availableLevels.Dequeue();

            if (node.level > currentLevel)
            {
                elegibleLevels[i] = node;
                i++;
            }           

            if (node.leftChild != null)
            {
                availableLevels.Enqueue(node.leftChild as LevelNode);
            }

            if (node.rightChild != null)
            {
                availableLevels.Enqueue(node.rightChild as LevelNode);
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
                    elegibleLevels[i] = node;
                    i++;
                }
            }
        }

        return elegibleLevels;
        
    }

    private void NextLevelCalc()
    {
        LevelNode temp;
        LevelNode[] Availablelevels = LevelOrder(gameObject.GetComponent<LevelsABB>().Root());

        for (int i = 0; i < Availablelevels.Length - 1; i++)
        {
            for (int j = 0; j < Availablelevels.Length - i - 1; j++)
            {
                if (Availablelevels[j].index > Availablelevels[j + 1].index)
                {
                    temp = Availablelevels[j];
                    Availablelevels[j] = Availablelevels[j + 1];
                    Availablelevels[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < Availablelevels.Length; i++)
        {
            if (score > Availablelevels[i].index)
            {
                nextLevel = Availablelevels[i].info;
            }
        }
        
    }
    
}
