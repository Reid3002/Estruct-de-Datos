using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MapLimits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 10)
        {
            gameObject.GetComponent<PlayerController>().alive = false;
        }

        if (transform.position.x <= -9.4f)
        {
            gameObject.GetComponent<PlayerController>().alive = false;
        }

        if (transform.position.y >= 5)
        {
            gameObject.GetComponent<PlayerController>().alive = false;
        }

        if (transform.position.y <= -6)
        {
            gameObject.GetComponent<PlayerController>().alive = false;
        }
    }
}
