using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHashingV2 : MonoBehaviour
{
    [Header("Grid")]
    public Dictionary<Vector2, GameObject> grid = new Dictionary<Vector2,GameObject>();

    [Header("Testing fields")]
    public bool showDetectionCubes = true;
    public List<GameObject> gridBlocks = new List<GameObject>();
    public Vector2 currentDetectionCenter;
    public List<GameObject> visualFeedbackObjets = new List<GameObject>();

    private void Start()
    {
        InsertObjectsToGrid(gridBlocks);
    }

    // Add single
    public void InsertObjectToGrid(GameObject obj)
    {
        Vector2 floorPosition = new Vector2(Mathf.FloorToInt(obj.transform.position.x), Mathf.FloorToInt(obj.transform.position.y));
        obj.transform.position = floorPosition;

        if (!grid.ContainsKey(floorPosition))
        {
            grid[floorPosition] = obj;
            print("Adding: " + obj.name + " at " + floorPosition);
        }
    }

    // Add List
    public void InsertObjectsToGrid(List<GameObject> objs)
    {
        if (objs.Count > 0)
            for (int i = 0; i < objs.Count; i++)
            {
                Vector2 currentPosPosition = objs[i].transform.position;
                Vector2 floorPosition = new Vector2(Mathf.FloorToInt(currentPosPosition.x), Mathf.FloorToInt(currentPosPosition.y));

                if (!grid.ContainsKey(floorPosition))
                {
                    grid[floorPosition] = objs[i];
                    print("Adding: " + objs[i].name + " at " + floorPosition);
                }
            }
    }

    //Remove From Grid
    public void RemoveObjectFromGrid(Vector2 objPosition)
    {
        if (grid.ContainsKey(objPosition))
        {
            grid.Remove(objPosition);
            Debug.Log("Blocked removed. Update Manager and spawn powerup if needed. Blocks Left: " + grid.Count);
            //Send objPosition to GameManager for Intanciate a powerup if needed on that position
        }
    }

    //Quantity of objects in grid
    public int GetObjectsCount()
    {
        return grid.Count;
    }

    // Objects detection public call
    public List<GameObject> GetNearbyObjects(Vector2 positionReference, Vector2 direction)
    {
        List<GameObject> nearbyObjects = new List<GameObject>();
        List<Vector2> searchPositions = WorldToGridPositions(positionReference, direction);

        foreach (Vector2 position in searchPositions)
        {
            if (grid.ContainsKey(position))
            {
                GameObject objectInCell = grid[position];

                if (objectInCell != null && objectInCell.activeSelf)
                    nearbyObjects.Add(objectInCell);
            }
        }

        return nearbyObjects;
    }

    // Objects detection - 3x3 Grid detection "system"
    private List<Vector2> WorldToGridPositions(Vector3 worldPosition, Vector2 direction)
    {
        List<Vector2> values = new List<Vector2>();
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = Mathf.FloorToInt(worldPosition.y);

        if (direction.x > 0)
            x = Mathf.CeilToInt(x) + 1;

        if (direction.y > 0)
            y = Mathf.CeilToInt(y) + 1;

        values.Add(new Vector2(x, y));              // Middle (priority)
        values.Add(new Vector2(x - 1, y + 1));      // Left Top
        values.Add(new Vector2(x, y + 1));          // Mid Top
        values.Add(new Vector2(x + 1, y + 1));      // Right Top
        values.Add(new Vector2(x - 1, y));          // Left Mid
        values.Add(new Vector2(x + 1, y));          // Right Mid
        values.Add(new Vector2(x - 1, y - 1));      // Left Bottom
        values.Add(new Vector2(x, y - 1));          // Middle Bottom
        values.Add(new Vector2(x + 1, y - 1));      // Right Bottom

        // Visual representation -- can be removed after
        currentDetectionCenter = values[0];
        if (showDetectionCubes)
        {
            visualFeedbackObjets[0].transform.position = values[1];
            visualFeedbackObjets[1].transform.position = values[2];
            visualFeedbackObjets[2].transform.position = values[3];
            visualFeedbackObjets[3].transform.position = values[4];
            visualFeedbackObjets[8].transform.position = values[0];
            visualFeedbackObjets[4].transform.position = values[5];
            visualFeedbackObjets[5].transform.position = values[6];
            visualFeedbackObjets[6].transform.position = values[7];
            visualFeedbackObjets[7].transform.position = values[8];
        }
        else
            for (int i = 0; i < visualFeedbackObjets.Count; i++)
                visualFeedbackObjets[i].transform.position = new Vector2(-1, -1);
        // end

        return values;
    }
}
