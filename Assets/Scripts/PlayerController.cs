using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< Updated upstream
    public Snake theSnake;

    [Header ("Movement")]
    [Range(0.01f, 1)] public float ogStepTime = 1;
    [Range(0.01f, 1)] public float stepSize = 0.5f;
    private float stepTime = 1;

    [Header("Tail")]
    public float tailOffset = 1;
    public GameObject tailTest;
    public List<GameObject> tailObjects = new List<GameObject>();


    void Start()
    {
        this.theSnake = new Snake(this.transform.position, Snake.Direction.None);
    }

=======
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
>>>>>>> Stashed changes
    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

<<<<<<< Updated upstream
        if (hAxis != 0 || vAxis != 0)
        {
            if (hAxis < 0 && this.theSnake.lastDirection != Snake.Direction.Right)          
                this.theSnake.direction = Snake.Direction.Left;
            else if (hAxis > 0 && this.theSnake.lastDirection != Snake.Direction.Left)      
                this.theSnake.direction = Snake.Direction.Right;

            if (vAxis < 0 && this.theSnake.lastDirection != Snake.Direction.Up)             
                this.theSnake.direction = Snake.Direction.Down;
            else if (vAxis > 0 && this.theSnake.lastDirection != Snake.Direction.Down)      
                this.theSnake.direction = Snake.Direction.Up;
        }

        if (stepTime <= 0)
        {
            SnakeStep();
            stepTime = ogStepTime;
        }
        else this.stepTime -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            GameObject theTail =  Instantiate(tailTest, this.transform.position, new Quaternion());
            //theTail.transform.parent = this.transform;
            this.tailObjects.Add(theTail);
        }
    }

    void SnakeStep()
    {
        // Save current position
        this.theSnake.posLastDirection = this.transform.position;

        Vector3 value = Vector3.zero;

        // Make the movement
        if (IsRoundNumber(this.transform.position.x) && (this.theSnake.direction == Snake.Direction.Up || this.theSnake.direction == Snake.Direction.Down))
            switch (this.theSnake.direction)
            {
                case Snake.Direction.Up:
                    value = new Vector3(0, stepSize);
                    break;
                case Snake.Direction.Down:
                    value = new Vector3(0, -stepSize);
                    break;
            }

        if (IsRoundNumber(this.transform.position.y) && (this.theSnake.direction == Snake.Direction.Left || this.theSnake.direction == Snake.Direction.Right))
            switch (this.theSnake.direction)
            {
                case Snake.Direction.Left:
                    value = new Vector3(-stepSize, 0);
                    break;
                case Snake.Direction.Right:
                    value = new Vector3(stepSize, 0);
                    break;
            }

        switch (this.theSnake.direction)
        {
            case Snake.Direction.None:

                break;
            default:
                break;
        }


        //print(this.theSnake.lastDirection);

        //Vector3 value = this.theSnake.SnakeDirection(stepSize);
        this.transform.position += value;

        // Force round values
        this.transform.position = ForceRoundPosition(this.transform.position);

        if (tailObjects.Count > 0)
        {
            for (int i = 0; i < tailObjects.Count; i++)
            {
                tailObjects[i].transform.position = this.theSnake.posLastDirection - new Vector3(tailOffset, tailOffset) * i;
            }
        }
    }

    Vector2 ForceRoundPosition(Vector3 input)
    {
        float snapX = Mathf.Round(input.x / stepSize) * stepSize;
        float snapY = Mathf.Round(input.y / stepSize) * stepSize;
=======
        if (hAxis != 0)
        {
            if (hAxis > 0) this.desiredDirection= Direction.Right;
            else this.desiredDirection= Direction.Left;
        }

        if (vAxis != 0) 
        {
            if (vAxis > 0) this.desiredDirection = Direction.Up;
            else this.desiredDirection= Direction.Down;
        }

        if (this.currentStepTime > 0)
            this.currentStepTime -= Time.deltaTime;
        else
        {
            if ((this.desiredDirection == Direction.Up || this.desiredDirection == Direction.Down) && IsRoundNumber(this.transform.position.x))
                this.lastSuccessfulDirection = this.desiredDirection;
            else if ((this.desiredDirection == Direction.Left|| this.desiredDirection == Direction.Right) && IsRoundNumber(this.transform.position.y))
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
>>>>>>> Stashed changes

        return new Vector3(snapX, snapY);
    }

    bool IsRoundNumber(float number, float tolerance = 0.003f)
    {
        int roundedInt = Mathf.RoundToInt(number);
        float difference = Mathf.Abs(number - roundedInt);

<<<<<<< Updated upstream
        return difference < tolerance;
    }

=======
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
>>>>>>> Stashed changes
}
