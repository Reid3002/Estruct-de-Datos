using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerEdibleAddTail : EdibleController
{
    public SnakeController testTail;

    public override void OnEated(Collider2D instigator)
    {
        EdibleScoreboard.AddPointToEdible(this.type);
        SnakeController tailTest = Instantiate(testTail);
        instigator.gameObject.GetComponent<SnakeController>().AddTail(tailTest);
        Destroy(this.gameObject);
    }
}