using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public Queue<GameObject> snake = new Queue<GameObject>();

    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        snake.Enqueue(this.gameObject);
    }

  

    // Update is called once per frame
    void Update()
    {
         
    }
}
