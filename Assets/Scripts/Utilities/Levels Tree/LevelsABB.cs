using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsABB : MonoBehaviour, IABB
{
    LevelNode root;

    public LevelNode Root()
    {
        return root;
    }
    public bool IsTreeEmpty()
    {
        return (root == null);
    }
    public void InitializeTree()
    {
        root = null;
    }
    public IABB LeftChild()
    {
        return root.leftChild;
    }
    public IABB RightChild()
    {
        return root.rightChild;
    }

    public void AddNode(Scene level, int index)
    {
        if (root == null)
        {
            root = new LevelNode();
            root.index = index;
            root.info = level;
            root.leftChild = new LevelsABB();
            root.leftChild.InitializeTree();
            root.rightChild = new LevelsABB();
            root.rightChild.InitializeTree();
        }
        else if (root.index > index)
        {
            root.leftChild.AddNode(level,index);
        }
        else if (root.index < index)
        {
            root.rightChild.AddNode(level, index);
        }
    }

    public void RemoveNode(int index)
    {
        if (root!= null)
        {
           if (root.index == index && root.rightChild.IsTreeEmpty() && root.leftChild.IsTreeEmpty())
           {
                root = null;
           }
           else if (root.index == index && !root.leftChild.IsTreeEmpty())
            {
                root.index = this.Greater(root.leftChild);
                root.leftChild.RemoveNode(root.index);
            }
           else if (root.index == index && root.leftChild.IsTreeEmpty())
            {
                root.index = this.Lesser(root.leftChild);
                root.leftChild.RemoveNode(root.index);
            }
           else if (root.index < index)
            {
                root.rightChild.RemoveNode(index);
            }
            else
            {
                root.leftChild.RemoveNode(index);
            }
        }
    }

    public int Greater(IABB node)
    {
        if (node.RightChild().IsTreeEmpty())
        {
            return node.Root().index;
        }
        else
        {
            return Greater(node.RightChild());
        }
    }

    public int Lesser(IABB node)
    {
        if (node.LeftChild().IsTreeEmpty())
        {
            return node.Root().index;
        }
        else
        {
            return Lesser(node.LeftChild());
        }
    }
}
