using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    private bool canMove = true;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager.GetComponent<EnemyQueue>().Enqueue(this.gameObject);
        
    }

    private void Update()
    {
        
        
    }

    private void Movement(bool active)
    {

    }

}
