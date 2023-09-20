using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdibleEmpty : EdibleController
{
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
        Destroy(this.gameObject);
    }
}
