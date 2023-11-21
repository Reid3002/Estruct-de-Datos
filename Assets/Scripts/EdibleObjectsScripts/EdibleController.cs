using System;
using UnityEngine;

public abstract class EdibleController : MonoBehaviour
{
    public enum EdibleType { AddTail, RemoveTail, SlowPlayer, PlayerReverse, StunEnemies, LightsOff, LightsOn, KillEnemy }
    public Action<EdibleController> OnEatedEvent;

    [Header("Game table")]
    private Vector2 minLocation = Vector2.zero;
    private Vector2 maxLocation = Vector2.zero;

    [Header("Edible Settings")]
    public EdibleType type = EdibleType.AddTail;
    public bool isFriendly = true;
    public float pointQuantity;

    private GameObject manager;

    private void Awake()
    {
        manager = GameObject.Find("GameManager");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnEatedEvent?.Invoke(this);
            OnEated(collision);
        }
    }

    public virtual void OnEated(Collider2D instigator)
    {
        EdibleScoreboard.AddPointToEdible(this.type);
        Destroy(this.gameObject);
    }

    public void EdibleDequeue()
    {
        this.transform.position = SpawnPoint();
    }

    public void UpdateGameTable(Vector2 newMinLocation, Vector2 newMaxLocation)
    {
        this.minLocation = newMinLocation;
        this.maxLocation = newMaxLocation;
    }

    private Vector3 SpawnPoint()
    {
        bool success = false;
        GridObject temp = null;
        while (success == false)
        {
            temp = manager.GetComponent<Grid>().gridObjects[UnityEngine.Random.Range(0, manager.GetComponent<Grid>().gridObjects.Length)];
            if (temp != null && temp.isNavigable)
            {
                success = true;
            }
        }
        return temp.gameObject.transform.position;

    }
}
