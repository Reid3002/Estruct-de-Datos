using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EdibleController : MonoBehaviour
{
    [Header("Edible Settings")]
    public bool isFriendly = true;

    public abstract void OnTriggerEnter2D(Collider2D collision);
}
