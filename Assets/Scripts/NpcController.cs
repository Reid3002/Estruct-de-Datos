using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public int index;
    public GameObject nextInLine;
    public bool inQeue = false;
    public enum Direction { None, Up, Down, Left, Right }

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
        if (inQeue)
        {
            if (index > 2)
            {
                this.desiredDirection = nextInLine.GetComponent<NpcController>().lastSuccessfulDirection;
            }
            else
            {
                this.desiredDirection = GetPlayerDirection();
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

    private Direction GetPlayerDirection()
    {
        switch (nextInLine.GetComponent<PlayerController>().lastSuccessfulDirection)
        {
            case PlayerController.Direction.Left:
                return Direction.Left;

            case PlayerController.Direction.Right:
                return Direction.Right;

            case PlayerController.Direction.Up:
                return Direction.Up;

            case PlayerController.Direction.Down:
                return Direction.Down;

            default: return Direction.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Game Manager").GetComponent<QueueManager>().AddToQueue(this.gameObject);
    }
}

