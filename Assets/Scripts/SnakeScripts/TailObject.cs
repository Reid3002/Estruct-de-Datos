using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailObject : SnakeController
{
    public override void UpdateTail(Direction newDirection, Vector3 newTurnPosition)
    {
        this.desiredDirection = newDirection;
        this.lastTurnPosition = newTurnPosition;
    }

    public override void MoveTail(float stepAmount, Direction currentDirection = Direction.None)
    {
        if (currentDirection != PlayerController.Direction.None && this.lastSuccessfulDirection == Direction.None)
            this.lastSuccessfulDirection = currentDirection;

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

        // Move the other tail before moving this one
        if (this.tailObject != null && this.lastSuccessfulDirection != Direction.None)
            this.tailObject.MoveTail(stepAmount, this.lastSuccessfulDirection);

        // Move this tail
        this.transform.position += newDirection * stepAmount;
        this.transform.position = ForceRoundPosition(this.transform.position, stepAmount);

        // If the position of turning is reached, then turn
        if (this.transform.position == this.lastTurnPosition)
        {
            this.lastSuccessfulDirection = this.desiredDirection;

            // Then change the other tail turning data
            if (this.tailObject != null)
                this.tailObject.UpdateTail(this.desiredDirection, this.lastTurnPosition);
        }
    }
}
