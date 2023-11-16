using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdibleKillEnemy : EdibleController
{

    public override void OnEated(Collider2D instigator)
    {
        EdibleScoreboard.AddPointToEdible(this.type);
        Destroy(this.gameObject);
    }
}
