using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] int moveSpeed = 5;
    private Queue<GameObject> greenDots = new Queue<GameObject>();

    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = (transform.right * Mathf.Abs(Input.GetAxis("Horizontal")) * moveSpeed);
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            rb.velocity = (transform.up * Mathf.Abs(Input.GetAxis("Vertical")) * moveSpeed);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3 (0, 180, 0);
        }

        if (Input.GetAxis ("Vertical") > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (Input.GetAxis ("Vertical") < 0)
        {
            transform.eulerAngles = new Vector3(180, 0, 0);
        }
 
    }
}
