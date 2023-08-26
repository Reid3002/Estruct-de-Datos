using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if (target.transform.position.x != this.transform.position.x)
            rb.velocity = new Vector2 (target.transform.position.x - transform.position.x, transform.position.y);

       else if (target.transform.position.y != this.transform.position.y)
            rb.velocity = new Vector2(transform.position.x, target.transform.position.y - transform.position.y);
    }
}
