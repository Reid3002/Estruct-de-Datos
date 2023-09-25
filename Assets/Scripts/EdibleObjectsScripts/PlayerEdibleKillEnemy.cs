using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdibleKillEnemy : EdibleController
{

    public override void OnEated(Collider2D instigator)
    {
        Destroy(this.gameObject);
    }

    /*
    public virtual OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            gameManager.GetComponent<PoolStack>().Stack(gameManager.GetComponent<EnemyQueue>().Dequeue());
            Destroy(this.gameObject);
        }
    }*/
}
