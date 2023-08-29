using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailObject : MonoBehaviour
{
    [Header("Direction")]
    public PlayerController.Direction lastSuccessfulDirection = PlayerController.Direction.None;

    [Header("Turn Data")]
    public PlayerController.Direction desiredDirection = PlayerController.Direction.None;
    public Vector3 lastTurnPosition = Vector3.zero;

    [Header("Tail Child")]
    public GameObject tailObject;

    public void MoveTail(float stepAmount, PlayerController.Direction currentDirection = PlayerController.Direction.None)
    {
        if (currentDirection != PlayerController.Direction.None && this.lastSuccessfulDirection == PlayerController.Direction.None)
            this.lastSuccessfulDirection = currentDirection;

        Vector3 newDirection = Vector3.zero;

        switch (this.lastSuccessfulDirection)
        {
            case PlayerController.Direction.Up:
                newDirection = Vector3.up;
                break;
            case PlayerController.Direction.Down:
                newDirection = Vector3.down;
                break;
            case PlayerController.Direction.Left:
                newDirection = Vector3.left;
                break;
            case PlayerController.Direction.Right:
                newDirection = Vector3.right;
                break;
        }

        if (this.tailObject != null)
        {
            this.tailObject.GetComponent<TailObject>().MoveTail(stepAmount, this.lastSuccessfulDirection);
        }

        this.transform.position += newDirection * stepAmount;
        this.transform.position = ForceRoundPosition(this.transform.position, stepAmount);

        if (this.transform.position == this.lastTurnPosition)
        {
            this.lastSuccessfulDirection = this.desiredDirection;
            if (this.tailObject != null)
                this.tailObject.GetComponent<TailObject>().UpdateTail(this.desiredDirection, this.lastTurnPosition);
        }
    }

    public void UpdateTail(PlayerController.Direction newDirection, Vector3 newTurnPosition)
    {
        this.desiredDirection = newDirection;
        this.lastTurnPosition = newTurnPosition;
    }

    public void AddTail(GameObject tailInput)
    {
        if (this.tailObject == null)
        {
            this.tailObject = tailInput;
            this.tailObject.transform.position = this.transform.position + new Vector3(0.5f, 0);
        }
        else
        {
            this.tailObject.GetComponent<TailObject>().AddTail(tailInput);
            //this.tailObject.transform.position += this.transform.position + new Vector3(0.5f, 0);
        }
            
    }

    Vector3 ForceRoundPosition(Vector3 input, float stepAmount)
    {
        float snapX = Mathf.Round(input.x / stepAmount) * stepAmount;
        float snapY = Mathf.Round(input.y / stepAmount) * stepAmount;

        return new Vector3(snapX, snapY);
    }
}
