using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public int id;
    public bool isNavigable;


    private LayerMask layerMask = 6;
    private static float rayLength = 0.8f;
    public GridObject[] adjancentGrids = new GridObject[3];

    private void Awake()
    {
        RaycastHit2D up = Physics2D.Raycast(this.transform.position, transform.up * rayLength,layerMask);
        RaycastHit2D right = Physics2D.Raycast(this.transform.position, transform.right * rayLength, layerMask);
        RaycastHit2D left = Physics2D.Raycast(this.transform.position, -transform.right * rayLength, layerMask);
        RaycastHit2D down = Physics2D.Raycast(this.transform.position, -transform.up * rayLength, layerMask);

        if (up != false)
        {
            adjancentGrids[0] = up.rigidbody.gameObject.GetComponent<GridObject>();
        }
        if (right != false)
        {
            adjancentGrids[1] = right.rigidbody.gameObject.GetComponent<GridObject>();
        }
        if (left != false)
        {
            adjancentGrids[2] = left.rigidbody.gameObject.GetComponent<GridObject>();
        }
        if (down != false)
        {
            adjancentGrids[3] = down.rigidbody.gameObject.GetComponent<GridObject>();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.playerPosition = this;
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().currentNodeId = id;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isNavigable = false;
        }

        if (collision.CompareTag("Player"))
        {
            GameManager.playerPosition = this;
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().currentNodeId = id;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + transform.up * rayLength);
        Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + transform.right * rayLength);
    }


}
