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
        Destroy(this.gameObject);
    }

    public void EdibleDequeue()
    {
        this.transform.position = new Vector3(UnityEngine.Random.Range(this.minLocation.x, this.maxLocation.x), UnityEngine.Random.Range(this.minLocation.y, this.maxLocation.y), 0);
    }

    public void UpdateGameTable(Vector2 newMinLocation, Vector2 newMaxLocation)
    {
        this.minLocation = newMinLocation;
        this.maxLocation = newMaxLocation;
    }
}
