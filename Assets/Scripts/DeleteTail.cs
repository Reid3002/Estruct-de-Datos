using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTail : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          Destroy(gameManager.GetComponent<TailStack>().Unstack());
        }
        
    }

}
