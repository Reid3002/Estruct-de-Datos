using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EdibleController : MonoBehaviour
{
    [Header("Game table")]
    public Vector2 minLocation = Vector2.zero;
    public Vector2 maxLocation = Vector2.zero;

    [Header("Edible Settings")]
    public bool isFriendly = true;

    // Events
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public abstract void OnEated(Collider2D instigator = null);
}
