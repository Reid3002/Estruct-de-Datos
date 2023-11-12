using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Grid")]
    public Dictionary<Vector2, GridObject> grid = new Dictionary<Vector2, GridObject>();
    public GridObject[] gridObjects;

    void Start()
    {
        InsertObjectsToGrid(gridObjects);


        //print("Testing middle grid object string value: " + GetGridObject(new Vector2(0, 0)).testString);
    }

    public void InsertObjectsToGrid(GridObject[] objects)
    {
        if (objects.Length == 0) return;
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 pos = ForceRoundPosition(objects[i].transform.position);

            if (!grid.ContainsKey(pos))
            {
                grid[pos] = objects[i];
                objects[i].transform.position = pos;
                
            }
        }

        print("Added " + objects.Length + " grid objects to the grid.");
    }


    public Vector3 ForceRoundPosition(Vector3 positionInput, float stepAmountInput = 0.5f)
    {
        float snapX = Mathf.Round(positionInput.x / stepAmountInput) * stepAmountInput;
        float snapY = Mathf.Round(positionInput.y / stepAmountInput) * stepAmountInput;

        return new Vector3(snapX, snapY, 0);
    }

    public GridObject GetGridObject(Vector2 positionReference)
    {
        GridObject gridObjects = null;
        Vector2 searchPosition = ForceRoundPosition(positionReference);

        if (grid.ContainsKey(searchPosition))
        {
            GridObject gridObjectInCell = grid[searchPosition];
            if (gridObjectInCell != null) gridObjects = gridObjectInCell;
        }

        return gridObjects;
    }
}
