using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public int id;
    public bool isNavigable = true;

    public GameManager gManagerScript;
    private LayerMask layerMask = 1 << 6;
    private static float rayLength = 0.6f;
    public GridObject[] adjancentGrids;

    private void Start()
    {
        gManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void DetectNeighbors()
    {
        Queue<GridObject> temp = new Queue<GridObject> ();

        RaycastHit2D up = Physics2D.Raycast(gameObject.transform.position + transform.up * 0.3f, transform.up,  rayLength, layerMask);
        RaycastHit2D right = Physics2D.Raycast(gameObject.transform.position + transform.right * 0.3f, transform.right, rayLength, layerMask);
        RaycastHit2D left = Physics2D.Raycast(gameObject.transform.position -transform.right * 0.3f, -transform.right, rayLength, layerMask);
        RaycastHit2D down = Physics2D.Raycast(gameObject.transform.position  -transform.up * 0.3f, -transform.up, rayLength, layerMask);

        if (up.collider != null)
        {
            temp.Enqueue(up.collider.gameObject.GetComponent<GridObject>());
        }
        if (right.collider != null)
        {
            temp.Enqueue(right.collider.gameObject.GetComponent<GridObject>());
        }
        if (left.collider != null)
        {
            temp.Enqueue(left.collider.gameObject.GetComponent<GridObject>());
        }
        if (down.collider != null)
        {
            temp.Enqueue(down.collider.gameObject.GetComponent<GridObject>());
        }

        adjancentGrids = temp.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isNavigable = false;
        }
        if (collision.CompareTag("Player"))
        {
            gManagerScript.playerPosition = this;
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().currentNodeId = id;
        }        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer ==7)
        {
            isNavigable = false;
        }

        if (collision.CompareTag("Player"))
        {
            gManagerScript.playerPosition = this;
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().currentNodeId = id;
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;        
        Debug.DrawRay(gameObject.transform.position + transform.up * 0.3f, transform.up * rayLength, Color.red);
        Debug.DrawRay(gameObject.transform.position + transform.right * 0.3f, transform.right * rayLength, Color.yellow);
        Debug.DrawRay(gameObject.transform.position -transform.right * 0.3f, -transform.right * rayLength, Color.white);
        Debug.DrawRay(gameObject.transform.position -transform.up * 0.3f, -transform.up * rayLength, Color.blue);
    }


}
