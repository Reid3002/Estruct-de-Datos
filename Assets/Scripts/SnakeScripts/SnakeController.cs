using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnakeController : MonoBehaviour
{
    public enum Direction { None, Up, Down, Right, Left }
    public float tileSize { get; set; }

    [Header("Direction")]
    public Direction lastSuccessfulDirection = Direction.None;
    public Direction desiredDirection = Direction.None;

    [Header("Saved turn")]
    public Direction lastTurnDirection = Direction.None;
    public Vector3 lastTurnPosition = Vector3.zero;

    [Header("Tail")]
    public SnakeController tailObject;

    public virtual void UpdateDirectionData(bool updateTail = true)
    {
        this.lastSuccessfulDirection = this.desiredDirection;
        this.lastTurnPosition = this.transform.position;
        this.desiredDirection = Direction.None;

        if (updateTail && this.tailObject != null)
            tailObject.UpdateTail(this.lastSuccessfulDirection, this.lastTurnPosition);
    }

    // Tail functions
    public virtual void AddTail(SnakeController objectInput)
    {
        if (this.tailObject == null)
        {
            this.tailObject = objectInput;
            this.tailObject.tileSize = this.tileSize;
            this.tailObject.transform.position = this.transform.position - GenerateTailPosition();
            this.tailObject.UpdateTail(this.lastSuccessfulDirection, new Vector3(0, 0, 1));
        }

        else
            this.tailObject.AddTail(objectInput);
    }
    public abstract void UpdateTail(Direction newDirection, Vector3 newTurnPosition);
    public abstract void MoveTail(float stepAmount, Direction currentDirection = Direction.None);

    // yikes 1
    public Vector3 ForceRoundPosition(Vector3 positionInput, float stepAmountInput)
    {
        float snapX = Mathf.Round(positionInput.x / stepAmountInput) * stepAmountInput;
        float snapY = Mathf.Round(positionInput.y / stepAmountInput) * stepAmountInput;

        return new Vector3(snapX, snapY);
    }

    // yikes 2
    public bool IsRoundNumber(float number, float tolerance = 0.003f)
    {
        int roundedInt = Mathf.RoundToInt(number);
        float difference = Mathf.Abs(number - roundedInt);


        if (difference < tolerance)
        {
            return true;
        }
        else
        {
            float fractionalPart = number - Mathf.Floor(number);
            return Mathf.Abs(fractionalPart - this.tileSize) < tolerance;
        }
    }

    // New tails should always be one tile size away from each other.
    public Vector3 GenerateTailPosition()
    {
        Vector3 value = Vector3.zero;

        switch (lastSuccessfulDirection)
        {
            case Direction.None:
                break;
            case Direction.Up:
                value = new Vector3(0, this.tileSize);
                break;
            case Direction.Down:
                value = new Vector3(0, -this.tileSize);
                break;
            case Direction.Right:
                value = new Vector3(this.tileSize, 0);
                break;
            case Direction.Left:
                value = new Vector3(-this.tileSize, 0);
                break;
        }

        return value;
    }
}
