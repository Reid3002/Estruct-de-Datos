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

    [Header("Tail")]
    public GameObject tail;
    public List<GameObject> tailObjects = new List<GameObject>();
    public Vector3 offset = Vector3.zero;

    public Direction lastTurnDirection = Direction.None;
    public Vector3 lastTurnPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.currentStepTime = this.stepTime;
        /*
        for (int i = 0; i < tailList.Count; i++)
        {
            tailList[i].transform.position = this.transform.position - new Vector3(0.5f, 0);

        }*/

        if (this.tail != null)
            this.tail.transform.position = this.transform.position + new Vector3(0.5f, 0);
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
            this.tail.GetComponent<TailObject>().MoveTail(this.stepAmount, this.lastSuccessfulDirection);

            if ((this.desiredDirection == Direction.Up || this.desiredDirection == Direction.Down) && (this.lastSuccessfulDirection != Direction.Up && this.lastSuccessfulDirection != Direction.Down) && IsRoundNumber(this.transform.position.x))
                UpdateDirectionData();

            else if ((this.desiredDirection == Direction.Left || this.desiredDirection == Direction.Right) && (this.lastSuccessfulDirection != Direction.Left && this.lastSuccessfulDirection != Direction.Right) && IsRoundNumber(this.transform.position.y))
                UpdateDirectionData();

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

            this.transform.position += newDirection * this.stepAmount;
            this.transform.position = ForceRoundPosition(this.transform.position);
            this.currentStepTime = this.stepTime;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.tail.GetComponent<TailObject>().AddTail(tailObjects[0]);
            this.tailObjects.RemoveAt(0);
        }
    }

    private void UpdateDirectionData()
    {

        this.lastSuccessfulDirection = this.desiredDirection;
        this.desiredDirection = Direction.None;
        this.lastTurnPosition = this.transform.position;
        tail.GetComponent<TailObject>().UpdateTail(this.lastSuccessfulDirection, this.lastTurnPosition);
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
