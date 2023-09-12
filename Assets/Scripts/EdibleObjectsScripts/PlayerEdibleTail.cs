using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdibleTail : EdibleController
{
    //public float pointQuantity;
    //public Points points;

    public SnakeController testTail;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnEatedEvent?.Invoke(this);
            OnEated(collision);
        }
    }

    public override void OnEated(Collider2D instigator)
    {
        SnakeController tailTest = Instantiate(testTail);
        instigator.gameObject.GetComponent<SnakeController>().AddTail(tailTest);
        Destroy(this.gameObject);
    }
}
