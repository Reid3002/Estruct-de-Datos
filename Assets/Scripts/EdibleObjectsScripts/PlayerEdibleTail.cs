using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdibleTail : EdibleController
{
    public SnakeController testTail;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SnakeController tailTest = Instantiate(testTail);
            collision.gameObject.GetComponent<SnakeController>().AddTail(tailTest);
            this.transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
        //tailsList.Add(tailTest);
        //tailTest.tileSize = this.newTileSize;
        //AddTail();
    }
}
