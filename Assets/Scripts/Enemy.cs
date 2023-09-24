using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{
    [Header("Settings")]
    [Range(0.05f, 5)] public float newTileSize = 0.5f;
    [Range(0.005f, 0.5f)] public float stepAmount = 0.05f;
    [Range(0.0001f, 1)] public float stepTime = 0.5f;
    [Range(1, 10)] public int steps = 1;

    private float currentStepTime = 0;

    [SerializeField] GameObject gameManager;
    private bool canMove = true;

    private Transform playerTransform; // Reference to the player's transform

    // Start is called before the first frame update
    private void Start()
    {
        gameManager.GetComponent<EnemyQueue>().Enqueue(this.gameObject);
        this.currentStepTime = this.stepTime;

        // Find and store a reference to the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            if (this.currentStepTime > 0)
                this.currentStepTime -= Time.deltaTime;
            else
            {
                // Make X steps
                for (int i = 0; i < steps; i++)
                {
                    // Enforce cardinal direction movement
                    Vector3 newPosition = this.transform.position + directionToPlayer * this.stepAmount;

                    if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                    {
                        // Horizontal movement is dominant
                        newPosition.y = this.transform.position.y;
                    }
                    else
                    {
                        // Vertical movement is dominant
                        newPosition.x = this.transform.position.x;
                    }

                    this.transform.position = newPosition;

                    this.transform.position = ForceRoundPosition(this.transform.position, stepAmount);
                    this.currentStepTime = this.stepTime;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().alive = false;
        }
    }
}
