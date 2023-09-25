using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerEdibleAddTail : EdibleController
{
    public SnakeController testTail;

    public override void OnEated(Collider2D instigator)
    {
        SnakeController tailTest = Instantiate(testTail);
        instigator.gameObject.GetComponent<SnakeController>().AddTail(tailTest);
        Destroy(this.gameObject);
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdibleTail : EdibleController
{
    [SerializeField] GameObject gameManager;

    public SnakeController testTail;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pointQuantity += 5;
            OnEated(collision);
        }
    }

    public override void OnEated(Collider2D instigator)
    {
        SnakeController tailTest = Instantiate(testTail);
        instigator.gameObject.GetComponent<SnakeController>().AddTail(tailTest);
        gameManager.GetComponent<TailStack>().Stack(tailTest);
        Destroy(this.gameObject);
        //  this.transform.position = new Vector3(Random.Range(this.minLocation.x, this.maxLocation.x), Random.Range(this.minLocation.y, this.maxLocation.y), 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = new Vector3(Random.Range(this.minLocation.x, this.maxLocation.x), Random.Range(this.minLocation.y, this.maxLocation.y), 0);
        //tailsList.Add(tailTest);
        //tailTest.tileSize = this.newTileSize;
        //AddTail();
    }
}
*/