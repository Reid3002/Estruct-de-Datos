using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SnakeController
{
    [Header("Objects")]
    public SnakeController testTail;

    [Header("Settings")]
    [Range(0.005f, 0.5f)] public float stepAmount = 0.05f;
    [Range(0.00001f, 1)] public float stepTime = 0.5f;
    [Range(0.05f, 5)] public float newTileSize = 0.5f;

    private float currentStepTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.currentStepTime = this.stepTime;
        this.tileSize = newTileSize;
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        if (hAxis != 0)
        {
            if (hAxis > 0) this.desiredDirection = Direction.Right;
            else this.desiredDirection = Direction.Left;
        }

        if (vAxis != 0)
        {
            if (vAxis > 0) this.desiredDirection = Direction.Up;
            else this.desiredDirection = Direction.Down;
        }

        if (this.currentStepTime > 0)
            this.currentStepTime -= Time.deltaTime;
        else
        {
            // Call update tail
            if (this.tailObject != null)
                this.tailObject.MoveTail(this.stepAmount, this.lastSuccessfulDirection);

            // Check for direction change ( X - Y ) - snake like movement
            if ((this.desiredDirection == Direction.Up || this.desiredDirection == Direction.Down) && (this.lastSuccessfulDirection != Direction.Up && this.lastSuccessfulDirection != Direction.Down) && IsRoundNumber(this.transform.position.x))
                UpdateDirectionData();

            else if ((this.desiredDirection == Direction.Left || this.desiredDirection == Direction.Right) && (this.lastSuccessfulDirection != Direction.Left && this.lastSuccessfulDirection != Direction.Right) && IsRoundNumber(this.transform.position.y))
                UpdateDirectionData();

            // Generate vector and give it the direction
            Vector3 newDirection = Vector3.zero;

            switch (this.lastSuccessfulDirection)
            {
                case Direction.Up:
                    newDirection = Vector3.up;
                    break;
                case Direction.Down:
                    newDirection = Vector3.down;
                    break;
                case Direction.Left:
                    newDirection = Vector3.left;
                    break;
                case Direction.Right:
                    newDirection = Vector3.right;
                    break;
            }

            // Change object position & time
            this.transform.position += newDirection * this.stepAmount;
            this.transform.position = ForceRoundPosition(this.transform.position, stepAmount);
            this.currentStepTime = this.stepTime;
        }

        // Test - Add tail
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SnakeController tailTest = testTail;
            tailTest.tileSize = this.newTileSize;
            AddTail(Instantiate(tailTest));
        }
    }

    public override void UpdateTail(Direction newDirection, Vector3 newTurnPosition)
    {
        this.tailObject.desiredDirection = newDirection;
        this.tailObject.lastTurnPosition = newTurnPosition;
    }

    public override void MoveTail(float stepAmount, Direction currentDirection = Direction.None)
    {
        print("HEAD: MoveTail n/a.");
    }
}
