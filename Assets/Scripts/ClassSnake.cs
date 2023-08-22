using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Snake
{
    public enum Direction { Up, Down, Left, Right, None };

    public Vector3 lastPosition;
    public Vector3 posLastDirection;
    public Direction direction;
    public Direction lastDirection;

    public Snake(Vector3 newLastPosition, Direction newDirection)
    {
        this.lastPosition = newLastPosition;
        this.posLastDirection = newLastPosition;
        this.direction = newDirection;
        this.lastDirection = newDirection;  
    }

    public Vector3 SnakeDirection(float inputStepSize)
    {
        Vector3 value;

        this.lastDirection = this.direction;
        switch (this.direction)
        {
            case Direction.Up:
                value = new Vector3(0, inputStepSize);
                break;
            case Direction.Down:
                value = new Vector3(0, -inputStepSize);
                break;
            case Direction.Right:
                value = new Vector3(inputStepSize, 0);
                break;
            case Direction.Left:
                value = new Vector3(-inputStepSize, 0);
                break;
            default:
                value = Vector3.zero;
                break;
        }

        return value;
    }

    public bool CanChangeAxis()
    {
        bool value = false;

        if ((this.direction == Direction.Left || this.direction == Direction.Right)
            && (this.lastDirection == Direction.Up || this.lastDirection == Direction.Down))
            value = true;

        return value;
    }
}
