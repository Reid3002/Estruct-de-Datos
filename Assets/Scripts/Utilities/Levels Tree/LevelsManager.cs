using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LevelOrder( LevelNode node)
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
        if (availableLevels.Count > 0 && level == (2 ^ (currentLevel - 1) - 1))
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


        
    }
    
}
