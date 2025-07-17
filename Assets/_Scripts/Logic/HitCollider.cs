using System;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public Action<Collider> OnTriggerCollision;

    void OnTriggerEnter(Collider other)
    {
        OnTriggerCollision?.Invoke(other);
    }
}
