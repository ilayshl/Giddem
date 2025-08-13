using System;
using UnityEngine;

/// <summary>
/// Responsible for each attack collider's trigger activation.
/// Place this on any object with an attack collider
/// </summary>
public class HitCollider : MonoBehaviour
{
    public Action<Collider> OnTriggerCollision;

    void OnTriggerEnter(Collider other)
    {
        OnTriggerCollision?.Invoke(other);
    }
}
