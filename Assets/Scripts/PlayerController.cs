using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    public enum Direction { None, Up, Down, Left, Right }

    [Header("Settings")]
    [Range(0.005f, 0.5f)] public float stepAmount = 0.05f;
    [Range(0.001f, 1)] public float stepTime = 0.5f;
    private float currentStepTime = 0;

    [Header("Direction")]
    public Direction lastSuccessfulDirection = Direction.None;
    public Direction desiredDirection = Direction.None;

    // Start is called before the first frame update
    void Start()
    {
        this.currentStepTime = this.stepTime;
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
            if ((this.desiredDirection == Direction.Up || this.desiredDirection == Direction.Down) && IsRoundNumber(this.transform.position.x))
                this.lastSuccessfulDirection = this.desiredDirection;
            else if ((this.desiredDirection == Direction.Left || this.desiredDirection == Direction.Right) && IsRoundNumber(this.transform.position.y))
                this.lastSuccessfulDirection = this.desiredDirection;

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

            this.transform.position += newDirection * stepAmount;
            this.transform.position = ForceRoundPosition(this.transform.position);
            this.currentStepTime = this.stepTime;
        }
    }

    Vector3 ForceRoundPosition(Vector3 input)
    {
        float snapX = Mathf.Round(input.x / stepAmount) * stepAmount;
        float snapY = Mathf.Round(input.y / stepAmount) * stepAmount;

        return new Vector3(snapX, snapY);
    }

    bool IsRoundNumber(float number, float tolerance = 0.003f)
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
            return Mathf.Abs(fractionalPart - 0.5f) < tolerance;
        }
    }
}
