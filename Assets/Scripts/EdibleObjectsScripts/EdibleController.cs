using System;
using UnityEngine;

public abstract class EdibleController : MonoBehaviour
{
    public enum EdibleType { AddTail, RemoveTail, SlowPlayer, PlayerReverse, StunEnemies }
    public Action<EdibleController> OnEatedEvent;

    [Header("Game table")]
    public Vector2 minLocation = Vector2.zero;
    public Vector2 maxLocation = Vector2.zero;

    [Header("Edible Settings")]
    public EdibleType type = EdibleType.AddTail;
    public bool isFriendly = true;
    public float pointQuantity;

    // Events
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public abstract void OnEated(Collider2D instigator = null);

    public void EdibleDequeue()
    {
        this.transform.position = new Vector3(UnityEngine.Random.Range(this.minLocation.x, this.maxLocation.x), UnityEngine.Random.Range(this.minLocation.y, this.maxLocation.y), 0);
    }
}
